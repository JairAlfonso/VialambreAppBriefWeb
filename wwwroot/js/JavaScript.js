// Obtener el número de notificaciones pendientes (reemplace con su método real)
const numeroPendientes = obtenerNumeroPendientes();

// Crear el HTML de la notificación con el conteo dinámico
const notificationHTML = `
<span class="badge badge-pill bg-danger" style="position: absolute; top: -5px; right: -5px;">${numeroPendientes}</span>
`;

// Identificar el elemento objetivo (enlace "Pendientes")
const pendingLink = document.querySelector('.dropdown-item[href="/Briefs/Client/Pendientes"]');

// Insertar la notificación como hijo del enlace (no dentro del `<li>`)
pendingLink.appendChild(document.createElement('span')).innerHTML = notificationHTML;

// Función para obtener el número de notificaciones pendientes (reemplace con su implementación)
function obtenerNumeroPendientes() {
    // Implemente su lógica para recuperar el número real de notificaciones pendientes
    // Esto podría implicar realizar una llamada a la API o acceder a una fuente de datos
    return 10; // Valor de marcador de ejemplo
}


