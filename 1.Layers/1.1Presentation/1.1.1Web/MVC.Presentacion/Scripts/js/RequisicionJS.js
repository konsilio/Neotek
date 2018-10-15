$(document).ready(function () {
    $("#selectTipo").on('change', function () {
        var url = "/Requisicion/GetProductos";
        var id = $('#selectTipo').find('option:selected').val();
        document.getElementById("unidadPord").innerHTML = "";
        $.getJSON(url, { idTipo: id }, function (data) {
            var $select = $('#productos');
            if (data == "[]") {
                $select.find('option').remove().end().append('<option selected="selected" value="0">No hay productos</option>').val('0')
                $select.selectpicker("refresh")
            }
            else {
                $select.find('option').remove().end().append('<option selected="selected" value="0">Seleccione un producto</option>').val('0')
                $.each(JSON.parse(data), function (key, val) {
                    $select.append($('<option />', { value: (val['IdProducto']), text: val['Descripcion'] }));
                });
                $select.selectpicker("refresh");
            }
        });
        if (id == '3') {
            $("#Cantidad").prop('disabled', true);
        }
        else {
            $("#Cantidad").prop('disabled', false);
        }
    });
    $("#productos").on('change', function () {
        var url = "/Requisicion/GetUnidadMedida";
        var id = $('#productos').find('option:selected').val();
        if (id == '0') {
            document.getElementById("unidadPord").innerHTML = "";
        }
        else {
            $.getJSON(url, { idPord: id }, function (data) {
                var $unidad = $('#unidadPord');
                if (data == "[]") {
                    document.getElementById("unidadPord").innerHTML = "";
                }
                else {
                    document.getElementById("unidadPord").innerHTML = data;
                }
            });
        }
    });
});