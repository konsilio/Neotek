$(document).ready(function () {
    debugger
    //var valueSel = TipoUnidad.GetValue();
    // function(s, e) { alert('!!!')}

    $('select').on('change', function () {

        var value = $('#TipoUnidad').find('option:selected').val();//get value to filter of selected

        if (value == 1)//Pipa
        {
            $('.selPipa').show();
            $('.selCamioneta').hide();
        }
        if (value == 2)//Camioneta
        {
            $('.selPipa').hide();
            $('.selCamioneta').show();
        }
    });
    //$('#btnBuscarC').click(function () {
    //    var url = "/Pedidos/BuscarClientesPedido";
    //    var tel1 = $('#txtTel1').val();
    //    var tel2 = $('#txtTel2').val();
    //    var rfc = $('#txtRfc').val();

    //    $.getJSON(url, { Tel1: tel1, Tel2: tel2, Rfc: rfc }, function (data) {
    //        debugger
    //        var x = data;
    //        var $select = $('#cbxCte');
    //        if (data != "[]") {
    //            $select.find('option').remove().end().append('<option selected="selected" value="0">Seleccione</option>').val('0')
    //            $.each(JSON.parse(data), function (key, val) {
    //                $select.append($('<option />', { value: (val['IdCliente']), text: val['Cliente'] }));
    //            });
    //            $select.selectpicker("refresh");
    //            var $selectDom = $('#cbxDomicilio');

    //        }
    //        else {
    //            $select.find('option').remove().end().append('<option selected="selected" value="0">Seleccione</option>').val('0')
    //            $select.selectpicker("refresh")
    //            $('#btnCrearCte').prop('disabled', false);

    //        }
    //    });

    //    var urlDom = "/Pedidos/BuscarClientesPedidoDireccion";
    //    $.getJSON(urlDom, { Tel1: tel1, Tel2: tel2, Rfc: rfc }, function (data) {
    //        debugger
    //        var x = data;
    //        var $select = $('#cbxDomicilio');
    //        if (data != "[]") {
    //            $select.find('option').remove().end().append('<option selected="selected" value="0">Seleccione</option>').val('0')
    //            $.each(JSON.parse(data), function (key, val) {
    //                $.each(JSON.parse(data), function (key, val) {
    //                    $select.append($('<option />', { value: (val['IdCliente']), text: val['Calle'] }));
    //                });
    //                $select.selectpicker("refresh");
    //            });
    //        }
    //        else {
    //            $select.find('option').remove().end().append('<option selected="selected" value="0">Seleccione</option>').val('0')
    //            $select.selectpicker("refresh")
    //        }
    //    });
    //});
});
function OnSelectedChange(s, e) {

    var value = s.GetValue();
    if (value == 1)//Pipa
    {
        $('.selPipa').show();
        $('.selCamioneta').hide();
    }
    if (value == 2)//Camioneta
    {
        $('.selPipa').hide();
        $('.selCamioneta').show();
    }
}
//$("#altaPedido").click(function () {
//    var form = $(this).parent("form");
//    form.attr('action', '<%= Url.RouteUrl(new { Controller = "Pedidos", Action = "CrearPedido" }) %>');
//    form.attr('method', 'post');
//});