let stream = null;

// ------------------------
// Utils
// ------------------------
function wait(ms) {
    return new Promise(r => setTimeout(r, ms));
}

// 🔥 Synchronisiert echten Video-Frame (entscheidend für iOS)
// drawImage muss exakt mit Video-Frame synchron sein :contentReference[oaicite:0]{index=0}
async function ensureFrame(video) {
    if ('requestVideoFrameCallback' in HTMLVideoElement.prototype) {
        return new Promise(resolve => {
            video.requestVideoFrameCallback(() => resolve());
        });
    }

    // fallback für ältere iOS-Versionen
    await wait(100);
}

// ------------------------
// Start Camera
// ------------------------
export async function startCamera() {
    const video = document.getElementById("videoElement");
    if (!video) throw new Error("videoElement not found");

    try {
        // alten Stream stoppen
        if (stream) {
            stream.getTracks().forEach(t => t.stop());
            stream = null;
        }

        // 🔥 iOS Pflicht
        video.muted = true;
        video.setAttribute("muted", "");
        video.setAttribute("playsinline", "");
        video.setAttribute("autoplay", "");

        // bewusst ohne facingMode (iOS Bugquelle)
        stream = await navigator.mediaDevices.getUserMedia({
            video: true,
            audio: false
        });

        video.srcObject = stream;

        // warten bis Metadaten geladen
        await new Promise((resolve, reject) => {
            const timeout = setTimeout(() => reject(new Error("metadata timeout")), 5000);

            video.onloadedmetadata = () => {
                clearTimeout(timeout);
                resolve();
            };
        });

        await video.play();

        // warten bis echtes Bild kommt
        for (let i = 0; i < 20; i++) {
            if (video.videoWidth > 0 && video.videoHeight > 0 && video.readyState >= 2) {
                await ensureFrame(video);
                return true;
            }
            await wait(100);
        }

        throw new Error("Video not ready");
    } catch (err) {
        console.error("startCamera failed", err);
        throw err;
    }
}

// ------------------------
// Take Picture
// ------------------------
export async function takePicture() {
    const video = document.getElementById("videoElement");
    const canvas = document.getElementById("canvasElement");

    if (!video) throw new Error("videoElement not found");
    if (!canvas) throw new Error("canvasElement not found");

    if (!video.srcObject) {
        throw new Error("No active video stream");
    }

    // 🔥 WICHTIG: echten Frame holen
    await ensureFrame(video);

    if (!video.videoWidth || !video.videoHeight) {
        throw new Error(`Invalid video size ${video.videoWidth}x${video.videoHeight}`);
    }

    const MAX_WIDTH = 512;
    const scale = Math.min(1, MAX_WIDTH / video.videoWidth);

    canvas.width = Math.round(video.videoWidth * scale);
    canvas.height = Math.round(video.videoHeight * scale);

    const ctx = canvas.getContext("2d");
    if (!ctx) throw new Error("2d context not available");

    // 🔥 Safari Bug: erster draw oft leer
    // bekannte Workaround: doppelt zeichnen :contentReference[oaicite:1]{index=1}
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);
    await wait(30);
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    const dataUrl = canvas.toDataURL("image/jpeg", 0.8);

    if (!dataUrl || dataUrl.length < 100) {
        throw new Error("Image capture failed (Safari bug)");
    }

    return dataUrl;
}

// ------------------------
// Stop Camera (optional)
// ------------------------
export function stopCamera() {
    if (stream) {
        stream.getTracks().forEach(t => t.stop());
        stream = null;
    }
}