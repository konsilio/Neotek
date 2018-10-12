$(document).ready(function () {
    $("#btnFinalizar").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Requisicion", Action = "Autorizacion" }) %>');
        form.attr('method', 'post');
    });
});