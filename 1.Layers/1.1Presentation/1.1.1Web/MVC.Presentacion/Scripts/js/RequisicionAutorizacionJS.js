$(document).ready(function () {
    $("#btnFinalizar").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Requisicion", Action = "Autorizacion" }) %>');
        form.attr('method', 'post');
    });
    $("#btnCancelar").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '@Url.Action("CrearCancelar","Requisicion")');
        form.attr('method', 'post');
    });
});