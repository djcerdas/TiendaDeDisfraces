// Contador visual del carrito en la barra de menú
let count = 0;

function addToCart() {
    const badge = document.querySelector(".cart-count");

    if (!badge) {
        return;
    }

    count++;
    badge.textContent = count;
}