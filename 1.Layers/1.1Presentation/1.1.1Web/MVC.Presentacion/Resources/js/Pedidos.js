$(document).ready(function () {

    //var valueSel = TipoUnidad.GetValue();
    // function(s, e) { alert('!!!')}
    //var $select = $('#cbxCte');

    //if ($('#cbxCte').has('option').length > 0) {
    //    $('#btnCrearCte').prop('disabled', true);
    //} else { $('#btnCrearCte').prop('disabled', false); alert("empty")}
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
    $('#btnBuscarC').click(function () {
        var url = "/Pedidos/BuscarClientesPedido";
        var tel1 = $('#txtTel1').val();
        var tel2 = $('#txtTel2').val();
        var rfc = $('#txtRfc').val();

        $.getJSON(url, { Tel1: tel1, Tel2: tel2, Rfc: rfc }, function (data) {
           
            var x = data;
            var $select = $('#cbxCte');
            if (data != "[]") {
                $('#btnCrearCte').prop('disabled', true);
            }
            else {               
                $('#ModalConfirmacion').modal('show');

            }
        });

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
function getvalues(s, e) {
    if (s.GetValue() != null) {//si viene null       
       
        $('#btnCrearCte').prop('disabled', true)//desactivo           
    }   
}
var Aceptar = function () {
    $('#btnCrearCte').prop('disabled', true) 
};
var Cancelar = function () {
    $('#btnCrearCte').prop('disabled', false)
};
