$(function () {
    $('select').on('change', function () {

        if ($('#ddlPais').find('option:selected').text() != 'México') {
            $('#ddlEdo').parent().hide();
            $('#txtEdoP').show();
        }

        if ($('#ddlPais').find('option:selected').text() == 'México' || $('#ddlPais').find('option:selected').text() == 'Seleccione') {
            $('#ddlEdo').parent().show();
            $('#txtEdoP').hide();
        }

    });

});
$("#RegresaIndex").click(function () {
    var form = $(this).parent("form");
    form.attr('action', '<%= Url.RouteUrl(new { Controller = "Empresas", Action = "Index" }) %>');
    form.attr('method', 'post');
});
