document.addEventListener('DOMContentLoaded', function () {
    // Asignar el evento de clic a todos los elementos con la clase 'confirm-recotizar'
    document.querySelectorAll('.confirm-recotizar').forEach(function (element) {
        element.addEventListener('click', function (event) {
            // Buscar el botón más cercano con el ID 'btnRecotizar' desde el elemento cliqueado
            var closestButton = event.target.closest('#btnRecotizar');

            // Verificar si se encontró el botón correcto
            if (closestButton && closestButton.id === 'btnRecotizar') {
                // Mostrar confirmación
                var confirmation = confirm('¿Estás seguro de que quieres recotizar este brief?');
                if (!confirmation) {
                    event.preventDefault(); // Cancelar la acción si no se confirma
                }
            } else {
                // Si no es el botón correcto, evitar cualquier acción
                event.preventDefault();
                alert('Botón incorrecto. No se puede proceder con la recotización.');
            }
        });
    });
});