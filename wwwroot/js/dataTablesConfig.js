$(function () {
    new DataTable('#briefscostos', {
        "lengthChange": false,
        "lengthMenu": [[6, 12, 18, -1], [6, 12, 18, "All"]],
        "autoWidth": false,
        "ordering": false,
        "info": false,
        "dom": '<"top"i>rt<"bottom"flp><"clear">'
    });
});