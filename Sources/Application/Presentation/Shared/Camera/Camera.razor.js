let stream = null;

function wait(ms) {
    return new Promise(r => setTimeout(r, ms));
}

export async function startCamera() {
    const video = document.getElementById("videoElement");
    if (!video) throw new Error("videoElement not found");

    try {
        if (stream) {
            stream.getTracks().forEach(t => t.stop());
            stream = null;
        }

        video.muted = true;
        video.setAttribute("muted", "");
        video.setAttribute("autoplay", "");
        video.setAttribute("playsinline", "");

        // Erst ohne facingMode testen
        stream = await navigator.mediaDevices.getUserMedia({
            video: true,
            audio: false
        });

        video.srcObject = stream;

        await new Promise((resolve, reject) => {
            const timeout = setTimeout(() => reject(new Error("loadedmetadata timeout")), 5000);

            video.onloadedmetadata = () => {
                clearTimeout(timeout);
                resolve();
            };
        });

        await video.play();

        // iPhone/Safari braucht oft noch etwas Zeit bis echte Frames da sind
        for (let i = 0; i < 20; i++) {
            if (video.videoWidth > 0 && video.videoHeight > 0 && video.readyState >= 2) {
                await wait(100);
                return {
                    ok: true,
                    width: video.videoWidth,
                    height: video.videoHeight,
                    readyState: video.readyState
                };
            }

            await wait(100);
        }

        throw new Error(`Video not ready. readyState=${video.readyState}, width=${video.videoWidth}, height=${video.videoHeight}`);
    } catch (err) {
        console.error("startCamera failed", err);
        throw err;
    }
}

export async function takePicture() {
    const video = document.getElementById("videoElement");
    const canvas = document.getElementById("canvasElement");

    if (!video) throw new Error("videoElement not found");
    if (!canvas) throw new Error("canvasElement not found");

    if (!video.srcObject) {
        throw new Error("No active video stream");
    }

    // Mehrere Frames warten, sonst schwarzes Bild auf iOS moeglich
    for (let i = 0; i < 20; i++) {
        if (video.videoWidth > 0 && video.videoHeight > 0 && video.readyState >= 2) {
            break;
        }
        await wait(100);
    }

    if (!video.videoWidth || !video.videoHeight) {
        throw new Error(`Invalid video size. width=${video.videoWidth}, height=${video.videoHeight}, readyState=${video.readyState}`);
    }

    const MAX_WIDTH = 512;
    const scale = Math.min(1, MAX_WIDTH / video.videoWidth);

    canvas.width = Math.round(video.videoWidth * scale);
    canvas.height = Math.round(video.videoHeight * scale);

    const ctx = canvas.getContext("2d");
    if (!ctx) throw new Error("2d context not available");

    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    const dataUrl = canvas.toDataURL("image/jpeg", 0.8);

    if (!dataUrl || dataUrl.length < 100) {
        throw new Error("Image capture failed");
    }

    return {
        dataUrl,
        width: canvas.width,
        height: canvas.height,
        videoWidth: video.videoWidth,
        videoHeight: video.videoHeight,
        readyState: video.readyState
    };
}