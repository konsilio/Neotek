$(document).ready(function () {
    $(function () {

        $('select').on('change', function () {

            if ($('#ddlTipopersona').find('option:selected').text() == 'Moral') {
                $('#div_hideRP').show();
                $('#div_hidePM').hide();
                $('#hide_razonS').show();
            }

            if ($('#ddlTipopersona').find('option:selected').text() == 'Física') {
                $('#div_hideRP').hide();
                $('#hide_razonS').hide();
                $('#div_hidePM').show();
            }

            if ($('#ddlTipopersona').find('option:selected').text() == 'Seleccione') {
                $('#div_hidePM').show();
                $('#div_hideRP').show();
                $('#hide_razonS').show();
            }

        });

    });
    $('#Dir').hide();
    $("#payMethod").change(function () {

        if ($(this).prop("checked") == true) {
            $('#Dir').show();
            $('#DirManual').hide();
        } else {
            $('#Dir').hide();
            $('#DirManual').show();
        }
    });
    $("#btnGuardaEdicion").click(function () {

        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Clientes", Action = "GuardaEdicionCliente" }) %>');
        form.attr('method', 'post');
    });
    $("#btnRegresarIndexNC").click(function () {

        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Clientes", Action = "Index" }) %>');
        form.attr('method', 'post');
    });
    $("#btnBuscar").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Clientes", Action = "Buscar" }) %>');
        form.attr('method', 'post');
    });
    $("#btncrearCl").click(function () {
               debugger
        var form = $('#FrmClientes');
        //form.attr('action', '<%= Url.RouteUrl(new { Controller = "Clientes", Action = "GuardarCliente" }) %>');
        //form.attr('method', 'post');
        form.submit();
       // $("form").submit();
    });

});