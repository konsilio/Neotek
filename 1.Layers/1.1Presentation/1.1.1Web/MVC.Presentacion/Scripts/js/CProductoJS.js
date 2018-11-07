$(document).ready(function () {
    $(".filterable tr:has(td)").each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
         .hide().text(t).appendTo(this);
    });
    $("#txtNumeroFiltro").keyup(function () {
        var s = $(this).val().toLowerCase().split(" ");
        $(".filterable tr:hidden").show();
        $.each(s, function () {
            $(".filterable tr:visible .indexColumn:not(:contains('"
               + this + "'))").parent().hide();
        });//each
    });
    $("#empresas").on('change', function () {
        var url = "/Producto/GetBuscarCuentasCtbls";
        var id = $('#empresas').find('option:selected').val();

        $.getJSON(url, { idEmpresa: id }, function (data) {
            var $select = $('#cuentacontable');
            if (data == "[]") {
                $select.find('option').remove().end().append('<option selected="selected" value="0">Sin Productos</option>').val('0')
                $select.selectpicker("refresh")
            }
            else {
                $select.find('option').remove().end().append('<option selected="selected" value="0">Seleccione un producto</option>').val('0')
                $.each(JSON.parse(data), function (key, val) {
                    $select.append($('<option />', { value: (val['IdEmpresa']), text: val['Descripcion'] }));
                });
                $select.selectpicker("refresh");
            }
        });
    });
});//document.ready
$(function () {
    $('select').on('change', function () {
        if ($('#tipoproducto').find('option:selected').val() == 2) {
            $('#divProd').show();
            $('#divServ').hide();
            $('#cbServicioTransporte').prop('checked', false);
        }
        if ($('#tipoproducto').find('option:selected').val() == 3) {
            $('#divProd').hide();
            $('#divServ').show();
            $('#cbEsGas').prop('checked', false);
            $('#cbActivoVenta').prop('checked', false);
        }
        if ($('#tipoproducto').find('option:selected').val() == 0) {
            $('#divProd').hide();
            $('#divServ').hide();
            $('#cbEsGas').prop('checked', false);
            $('#cbActivoVenta').prop('checked', false);
            $('#cbServicioTransporte').prop('checked', false);
        }
        cbServicioTransporte
    });

    $('#ddlEmpresasFilter').on('change', function () {

        var id = $(this).val();

        var url = '@Url.Action("Producto", "Producto", new { idempresa = "__param__" })';
        window.location.href = url.replace('__param__', encodeURIComponent(id));
    });
});