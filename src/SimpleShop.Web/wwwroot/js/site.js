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