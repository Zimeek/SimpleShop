// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', () => {
    handleNavbarToggle();
});

function handleNavbarToggle() {
    const navbarBurger = document.querySelector('.navbar-burger');

    if (navbarBurger != null) {
        navbarBurger.addEventListener('click', () => {
            const navbarMenu = document.querySelector('.navbar-menu');
            navbarBurger.classList.toggle('is-active');
            navbarMenu.classList.toggle('is-active');
        });
    }
}

function getSelectedSize() {
    const select = document.getElementById('size-select');
    return select.options[select.selectedIndex].value;
}