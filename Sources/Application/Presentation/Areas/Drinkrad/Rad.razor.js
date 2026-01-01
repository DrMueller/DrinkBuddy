
export function restartSpin() {
    const el = document.querySelector('.wheel');
    if (!el) return;

    el.classList.remove("spin");
    void el.offsetWidth; // reflow
    el.classList.add("spin");
}