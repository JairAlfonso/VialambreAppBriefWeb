document.addEventListener('DOMContentLoaded', function () {
    // Asignar el evento de clic a todos los elementos con la clase 'btn-outline-info'
    document.querySelectorAll('.btn-outline-warning').forEach(function (element) {
        element.addEventListener('click', function (event) {
            // Buscar el botón más cercano con la clase 'btn-outline-info' desde el elemento cliqueado
            var closestButton = event.target.closest('.btn-outline-warning');

            // Verificar si se encontró el botón correcto
            if (closestButton) {
                // Mostrar confirmación
                var confirmation = confirm('¿Estás seguro de que quieres reenviar este brief?');
                if (!confirmation) {
                    event.preventDefault(); // Cancelar la acción si no se confirma
                }
            } else {
                // Si no es el botón correcto, evitar cualquier acción
                event.preventDefault();
                alert('Botón incorrecto. No se puede proceder con el reenvío.');
            }
        });
    });
});