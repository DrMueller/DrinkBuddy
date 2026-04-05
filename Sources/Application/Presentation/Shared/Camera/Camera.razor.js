let stream = null;

export async function startCamera() {
    if (stream) {
        stream.getTracks().forEach(t => t.stop());
        stream = null;
    }

    const video = document.getElementById("videoElement");
    if (!video) return;

    video.setAttribute("playsinline", "true");
    video.setAttribute("autoplay", "true");
    video.muted = true;

    stream = await navigator.mediaDevices.getUserMedia({
        video: { facingMode: "environment" }
    });

    video.srcObject = stream;

    await new Promise(resolve => {
        if (video.readyState >= 1) {
            resolve();
            return;
        }

        video.onloadedmetadata = () => resolve();
    });

    await video.play();
}

export async function takePicture() {
    const video = document.getElementById("videoElement");
    const canvas = document.getElementById("canvasElement");

    if (!video || !canvas) return '';

    if (video.readyState < 2 || !video.videoWidth || !video.videoHeight) {
        await new Promise(resolve => {
            video.onloadedmetadata = () => resolve();
        });
    }

    const MAX_WIDTH = 512;
    const scale = Math.min(1, MAX_WIDTH / video.videoWidth);

    canvas.width = Math.round(video.videoWidth * scale);
    canvas.height = Math.round(video.videoHeight * scale);

    const ctx = canvas.getContext("2d");
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    return canvas.toDataURL("image/jpeg", 0.7);
}