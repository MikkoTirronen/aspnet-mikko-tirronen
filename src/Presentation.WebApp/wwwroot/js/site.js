// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const burgerButton = document.getElementById("burgerButton");
const mobileMenu = document.getElementById("mobileMenu");

if (burgerButton && mobileMenu) {
    burgerButton.addEventListener("click", () => {
        const isOpen = mobileMenu.classList.toggle("is-open");
        burgerButton.setAttribute("aria-expanded", isOpen);
    });
}