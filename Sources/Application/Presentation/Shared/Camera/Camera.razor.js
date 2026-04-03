let stream = null;

export async function startCamera() {
    if (stream) {
        stream.getTracks().forEach(t => t.stop());
        stream = null;
    }

    const video = document.getElementById("videoElement");

    stream = await navigator.mediaDevices.getUserMedia({ video: true });

    video.srcObject = stream;
    await video.play();
}

export async function takePicture() {
    const video = document.getElementById("videoElement");
    const canvas = document.getElementById("canvasElement");

    if (!video || !canvas) return '';

    // warten bis ready
    if (video.readyState < 2) {
        await new Promise(r => video.onloadeddata = r);
    }

    const MAX_WIDTH = 512;
    const scale = Math.min(1, MAX_WIDTH / video.videoWidth);

    canvas.width = video.videoWidth * scale;
    canvas.height = video.videoHeight * scale;

    const ctx = canvas.getContext("2d");
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    return canvas.toDataURL("image/jpeg", 0.7);
}