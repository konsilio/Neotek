$(document).ready(function () {
    $('#Chkboxcorrecto').click(function () {
        if (!$(this).is(':checked')) {
            $("#btnLiquidar").prop("disabled", true);
        }
        else {
            $("#btnLiquidar").prop("disabled", false);
        }
    });

$(function () {

    $('select').on('change', function () {
        $('#tblfilterable tbody tr').each(function () {
            var value = $('#cbxentidad').find('option:selected').text();//get value to filter of selected
            //alert(value);
            var hiderow = $(this).find("td").eq(1).html();//get value of column entidad
            /*---------------------------------------*/
            var valuec = $('#cbxconcepto').find('option:selected').text();//get value to filter of selected
            var hiderowc = $(this).find("td").eq(3).html();//get value of column concepto

            if (value != 'Seleccione' || valuec != 'Seleccione') {

                if (valuec != 'Seleccione') {
                    if (valuec != hiderowc) {
                        var rowindex = $(this).index();
                        $('#tblfilterable tbody tr:eq(' + rowindex + ')').hide();
                    }
                    if (valuec == hiderowc) {
                        var rowindex = $(this).index();
                        $('#tblfilterable tbody tr:eq(' + rowindex + ')').show();
                    }
                   
                }
                if (value != 'Seleccione') {
                    if (value != hiderow) {
                        var rowindex = $(this).index();
                        $('#tblfilterable tbody tr:eq(' + rowindex + ')').hide();
                    }
                    if (value == hiderow) {
                        var rowindex = $(this).index();
                        $('#tblfilterable tbody tr:eq(' + rowindex + ')').show();
                    }
                }
            }
            else { $('#tblfilterable tbody tr').show(); }
        });
    });
    /*******/
    $("#IdA").keyup(function () {
        var from = $("#IdDe").val();
        var To = $(this).val();
        var index = 0;
        if (from != "" && To != "") {
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
        }
        else {
            $('#tblfilterable tbody tr').show();
        }
    });
});
});
