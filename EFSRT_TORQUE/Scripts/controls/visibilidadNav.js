document.addEventListener("DOMContentLoaded", function () {
    // Obtener el título actual de la página
    var tituloPagina = document.title;
    var div = document.getElementById('navegacion');

    // Mostrar el título en la consola (opcional)
    console.log("El título de la página es:", tituloPagina);

    // Verificar si el elemento div existe en el DOM
    if (div) {
        if (tituloPagina === "Iniciar Sesión - Express") {
            div.style.display = 'none';
        } else if (tituloPagina === "Registro - Express") {
            div.style.display = 'none';
        }
        else{
            div.style.display = 'flex';
        }
    } else {
        console.error("Elemento con id 'navegacion' no encontrado.");
    }
});
