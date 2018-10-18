$(document).ready(function () {
    $("#btnRegresarIndexPVO").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "PrecioVentaOtro", Action = "Index" }) %>');
        form.attr('method', 'post');
    });

    $("#txtUtilidad").keyup(function () {
        var precio = $("#PrecioPMX").val();

        var utilidad = $(this).val();

        var operacion = parseFloat(precio) + parseFloat(utilidad);

        $("#IdPrecioSalidaKg").val(operacion)

        //$("#IDPrecioSalidaKg").val(operacion);
        // alert($('#IdPrecioSalidaKg').val());
    });//key up.
    $("#txtUtilidad").keyup(function () {

        var precio = $("#PrecioPMX").val();
        var utilidad = $(this).val();
        var operacion = parseFloat(precio) + parseFloat(utilidad);
        $("#IdPrecioSalidaKg").val(operacion)

        /*****factorLitrosaKilos*****/
        var url = "/PrecioVenta/GetConfiguracionEmpresa";
        var id = $('#ddlEmpresas').find('option:selected').val();
        $.getJSON(url, { idEmpresa: id }, function (data) {
            var factor = data;
            $("#IdPrecioSalidaLt").val(factor)
        });
        /**********/

    });//key up.

    $("#IdA").keyup(function () {
        var from = $("#IdDe").val();
        var To = $(this).val();
        var index = 0;
        var value = $('#IdTipoFecha').find('option:selected').val();//get value to filter of selected
        if (value == 1) {
            index = 4;
        }
        if (value == 2) {
            index = 7;
        }
        if (value == 3) {
            index = 6;
        }

        var dateParts = from.split("/");
        var datefrom = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
        var dateParts2 = To.split("/");
        var dateto = new Date(dateParts2[2], dateParts2[1] - 1, dateParts2[0]);

        $('#tblfilterable tbody tr').each(function () {
            var filterdate = $(this).find("td").eq(index).html();//get value of column Fecha x
            var dateParts3 = filterdate.split("/");
            var datefilter = new Date(dateParts3[2].split(" ", 1), dateParts3[1] - 1, dateParts3[0]);

            if (datefilter >= datefrom && datefilter <= dateto) {
                var rowindex = $(this).index();
                $('#tblfilterable tbody tr:eq(' + rowindex + ')').show();
            }
            else {
                var rowindex = $(this).index();
                $('#tblfilterable tbody tr:eq(' + rowindex + ')').hide();
            }
        });
    });
    $('select').on('change', function () {

        var from = $("#IdDe").val();
        var To = $("#IdA").val();
        var value = $('#IdTipoFecha').find('option:selected').val();//get value to filter of selected
        if (from != "" && To != "" && value != 0) {

            var index = 0;

            if (value == 1) {
                index = 4;
            }
            if (value == 2) {
                index = 7;
            }
            if (value == 3) {
                index = 5;
            }

            var dateParts = from.split("/");
            var datefrom = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
            var dateParts2 = To.split("/");
            var dateto = new Date(dateParts2[2], dateParts2[1] - 1, dateParts2[0]);

            $('#tblfilterable tbody tr').each(function () {
                var filterdate = $(this).find("td").eq(index).html();//get value of column Fecha Programada  x
                var dateParts3 = filterdate.split("/");
                var datefilter = new Date(dateParts3[2].split(" ", 1), dateParts3[1] - 1, dateParts3[0]);
                if (datefilter >= datefrom && datefilter <= dateto) {
                    var rowindex = $(this).index();
                    $('#tblfilterable tbody tr:eq(' + rowindex + ')').show();
                }
                else {
                    var rowindex = $(this).index();
                    $('#tblfilterable tbody tr:eq(' + rowindex + ')').hide();
                }
            });
        }

        if (value == 0) {
            $('#tblfilterable tbody tr').show();
        }
    });
});
