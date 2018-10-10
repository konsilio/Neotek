
    $(function () {

        $('select').on('change', function () {

            if ($('#ddlPais').find('option:selected').text() != 'México')
            {
                $('#ddlEdo').parent().hide();
                $('#txtEdoP').show();
            }

            if ($('#ddlPais').find('option:selected').text() == 'México' || $('#ddlPais').find('option:selected').text() == 'Seleccione') {
                $('#ddlEdo').parent().show();
                $('#txtEdoP').hide();
            }

        });

    });

$(document).ready(function () {
    $("#btnGuardaEdicion").click(function () {

        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Usuarios", Action = "GuardaEdicionUsuario" }) %>');
        form.attr('method', 'post');
    });
    $("#btnRegresarIndex").click(function () {

        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Usuarios", Action = "Index" }) %>');
        form.attr('method', 'post');
    });

});
