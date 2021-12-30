
const regClass = document.querySelector('.col-md-offset-2');

window.addEventListener('DOMContentLoaded', (event) => {
    if (regClass)
        regClass.innerHTML = `<img alt="logo image" class="imgLogo" src="/images/logo/garden.svg" /> `;
});
