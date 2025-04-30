document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.btn-outline-warning').forEach(function (element) {
        element.addEventListener('click', function (event) {
            var closestButton = event.target.closest('.btn-outline-warning');
            if (closestButton) {
                var confirmation = confirm('¿Estás seguro de que quieres reasignar este brief?');
                if (!confirmation) {
                    event.preventDefault();
                }
            } else {
                event.preventDefault();
                alert('Botón incorrecto. No se puede proceder con la reasignación.');
            }
        });
    });
});