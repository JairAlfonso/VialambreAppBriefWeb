document.addEventListener('DOMContentLoaded', function () {
    // Asignar el evento de clic a todos los elementos con la clase 'btn-outline-danger'
    document.querySelectorAll('.btn-outline-danger').forEach(function (element) {
        element.addEventListener('click', function (event) {
            // Buscar el botón más cercano con el ID 'btnCancelar' desde el elemento cliqueado
            var closestButton = event.target.closest('#btnCancelar');

            // Verificar si se encontró el botón correcto
            if (closestButton && closestButton.id === 'btnCancelar') {
                // Mostrar confirmación
                var confirmation = confirm('¿Estás seguro de que quieres cancelar este brief?');
                if (!confirmation) {
                    event.preventDefault(); // Cancelar la acción si no se confirma
                }
            } else {
                // Si no es el botón correcto, evitar cualquier acción
                event.preventDefault();
                alert('Botón incorrecto. No se puede proceder con la cancelación.');
            }
        });
    });
});