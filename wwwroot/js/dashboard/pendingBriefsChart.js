document.addEventListener('DOMContentLoaded', function () {
    // Extraer datos del script JSON
    var chartDataScript = document.getElementById('chart-data');
    var chartData = JSON.parse(chartDataScript.textContent);

    var ctx = document.getElementById('pendingBriefsChart').getContext('2d');
    var pendingBriefsChart = new Chart(ctx, {
        type: 'bar', // Puedes usar 'bar', 'line', 'pie', etc.
        data: {
            labels: ['Pendientes'], // Etiquetas para las barras
            datasets: [{
                label: 'Briefs Pendientes',
                data: [chartData.pendingBriefsCount], // Datos del gráfico
                backgroundColor: 'rgba(255, 193, 7, 0.2)', // Color de fondo
                borderColor: 'rgba(255, 193, 7, 1)', // Color del borde
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    // Extraer datos del script JSON
    var chartDataScript = document.getElementById('chart-data');
    var chartData = JSON.parse(chartDataScript.textContent);

    var ctxApproved = document.getElementById('approvedBriefsChart').getContext('2d');
    var approvedBriefsChart = new Chart(ctxApproved, {
        type: 'bar', // Puedes usar 'bar', 'line', 'pie', etc.
        data: {
            labels: ['Aprobadas'], // Etiquetas para las barras
            datasets: [{
                label: 'Briefs Aprobados',
                data: [chartData.approvedBriefsCount], // Usa el dato del JSON
                backgroundColor: 'rgba(75, 192, 192, 0.2)', // Color de fondo
                borderColor: 'rgba(75, 192, 192, 1)', // Color del borde
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    // Extraer datos del script JSON
    var chartDataScript = document.getElementById('chart-data');
    var chartData = JSON.parse(chartDataScript.textContent);

    var ctx = document.getElementById('rejectedBriefsChart').getContext('2d');
    var rejectedBriefsChart = new Chart(ctx, {
        type: 'bar', // Puedes usar 'bar', 'line', 'pie', etc.
        data: {
            labels: ['Rechazadas'], // Etiquetas para las barras
            datasets: [{
                label: 'Briefs Rechazados',
                data: [chartData.rejectedBriefsCount], // Usa el dato del JSON
                backgroundColor: 'rgba(255, 99, 132, 0.2)', // Color de fondo rojo
                borderColor: 'rgba(255, 99, 132, 1)', // Color del borde
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    // Extraer datos del script JSON
    var chartDataScript = document.getElementById('chart-data');
    var chartData = JSON.parse(chartDataScript.textContent);

    var ctx = document.getElementById('cancelledBriefsChart').getContext('2d');
    var cancelledBriefsChart = new Chart(ctx, {
        type: 'bar', // Puedes usar 'bar', 'line', 'pie', etc.
        data: {
            labels: ['Cancelados'], // Etiquetas para las barras
            datasets: [{
                label: 'Briefs Cancelados',
                data: [chartData.cancelledBriefsCount], // Usa el dato del JSON
                backgroundColor: 'rgba(255, 206, 86, 0.2)', // Color de fondo amarillo
                borderColor: 'rgba(255, 206, 86, 1)', // Color del borde
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    // Extraer datos del script JSON
    var chartDataScript = document.getElementById('chart-data');
    var chartData = JSON.parse(chartDataScript.textContent);

    var ctx = document.getElementById('cotizacionesBriefsChart').getContext('2d');

    // Crear un array de objetos para almacenar la información de cada gráfico
    var chartDataArray = [
        {
            label: 'Total de Briefs',
            data: [chartData.totalCotizaciones], // Usa el dato del JSON
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgba(54, 162, 235, 1)'
        },
        {
            label: 'Briefs Pendientes',
            data: [chartData.totalBriefsPendientes], // Usa el dato del JSON
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderColor: 'rgba(255, 99, 132, 1)'
        },
        {
            label: 'Briefs Aprobados',
            data: [chartData.totalBriefsAprobados], // Usa el dato del JSON
            backgroundColor: 'rgba(153, 204, 102, 0.2)', // Verde claro
            borderColor: 'rgba(153, 204, 102, 1)'
        },
        {
            label: 'Briefs Cancelados',
            data: [chartData.newCancelledBriefsCount], // Usa el dato del JSON
            backgroundColor: 'rgba(255, 159, 64, 0.2)', // Naranja claro
            borderColor: 'rgba(255, 159, 64, 1)' // Naranja oscuro
        }
    ];
    // Crear el gráfico de tipo 'doughnut'
    var chart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: chartDataArray.map(item => item.label),
            datasets: [{
                data: chartDataArray.map(item => item.data[0]),
                backgroundColor: chartDataArray.map(item => item.backgroundColor),
                borderColor: chartDataArray.map(item => item.borderColor),
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true // Mostrar la leyenda con las etiquetas
                },
                tooltip: {
                    enabled: true // Mostrar valores en el tooltip
                }
            }
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    // Extraer datos del script JSON
    var chartDataScript = document.getElementById('chart-data');
    var chartData = JSON.parse(chartDataScript.textContent);

    // Datos para la gráfica de líneas
    var ctxLine = document.getElementById('lineChart').getContext('2d');

    // Datos para la gráfica de líneas
    var labels = ['Pendientes', 'Aprobados', 'Rechazados', 'Cancelados'];
    var data = {
        labels: labels,
        datasets: [{
            label: 'Briefs por Estado',
            data: [
                chartData.totalBriefsPendientes,
                chartData.totalBriefsAprobados,
                chartData.rejectedBriefsCount,
                chartData.newCancelledBriefsCount
            ], // Usa los datos del JSON
            backgroundColor: 'rgba(75, 192, 192, 0.2)', // Color de fondo
            borderColor: 'rgba(75, 192, 192, 1)', // Color del borde
            borderWidth: 1
        }]
    };
    // Crear la gráfica de líneas
    var lineChart = new Chart(ctxLine, {
        type: 'line',
        data: data,
        options: {
            responsive: true,
            scales: {
                x: {
                    beginAtZero: true
                },
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                legend: {
                    display: true // Mostrar la leyenda con las etiquetas
                },
                tooltip: {
                    enabled: true // Mostrar valores en el tooltip
                }
            }
        }
    });
});