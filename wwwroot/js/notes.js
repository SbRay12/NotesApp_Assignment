
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".note-editable").forEach(el => {
        el.addEventListener("blur", async (e) => {
            const id = e.target.dataset.id;
            const field = e.target.dataset.field;
            const value = e.target.innerText;

            try {
                await fetch(`/Notes/AjaxUpdate/${id}`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ field, value })
                });
            } catch (err) {
                console.error("Failed to save note", err);
            }
        });

        el.addEventListener("keydown", (e) => {
            if (e.key === "Enter") {
                e.preventDefault();
                (e.target).blur();
            }
        });
    });
});
