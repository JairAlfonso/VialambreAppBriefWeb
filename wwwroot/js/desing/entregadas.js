document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.btn-outline-success').forEach(function (element) {
        element.addEventListener('click', function (event) {
            var closestButton = event.target.closest('.btn-outline-success');
            if (closestButton) {
                var confirmation = confirm('¿Estás seguro de que quieres aceptar este brief?');
                if (!confirmation) {
                    event.preventDefault();
                }
            } else {
                event.preventDefault();
                alert('Botón incorrecto. No se puede proceder a aceptar.');
            }
        });
    });
});