﻿$(document).ready(function () {

    //var valueSel = TipoUnidad.GetValue();
    // function(s, e) { alert('!!!')}
    //var $select = $('#cbxCte');

    //if ($('#cbxCte').has('option').length > 0) {
    //    $('#btnCrearCte').prop('disabled', true);
    //} else { $('#btnCrearCte').prop('disabled', false); alert("empty")}
    //$('select').on('change', function () {

    //    var value = $('#TipoUnidad').find('option:selected').val();//get value to filter of selected

    //    if (value == 1)//Pipa
    //    {
    //        $('.selPipa').show();
    //        $('.selCamioneta').hide();
    //    }
    //    if (value == 2)//Camioneta
    //    {
    //        $('.selPipa').hide();
    //        $('.selCamioneta').show();
    //    }
    //});
   
    //  btnCrearCte
    //$("#btnCrearCte").click(function () {
    //    var form = $(this).parent("form");
    //    form.attr('action', '<%= Url.RouteUrl(new { Controller = "Pedidos", Action = "AltaCliente" }) %>');
    //    form.attr('method', 'get');
    //});
    $('#btnBuscarC').click(function () {
        IdCliente.ClearItems();
        Orden.ClearItems();
        debugger
        var url = "/Pedidos/BuscarClientesPedido";
        var tel1 = $('#txtTel1').val();
        var rfc = $('#txtRfc').val();

        $.getJSON(url, { Tel1: tel1, Rfc: rfc }, function (data) {
            var x = data;

            if (data != "[]") {
              //  $('#btnCrearCte').prop('disabled', true);
                $.each(JSON.parse(data), function (key, val) {
                    //IdCliente.AddItem(val.Nombre, val.IdCliente)
                    IdCliente.AddItem([val.Nombre, val.Apellido1, val.Rfc], val.IdCliente)
                });
                $(window).on("load", showNotification('alert-success', 'Seleccione el cliente', 'top', 'center', '', ''));
            }
            else {
                $('#ModalConfirmacion').modal('show');
               // $('#btnCrearCte').prop('disabled', false);
            }
        });

        var urlDom = "/Pedidos/BuscarClientesPedidoDireccion";
        $.getJSON(urlDom, { Tel1: tel1, Rfc: rfc }, function (data) {
            var x = data;
            if (data != "[]") {
                $.each(JSON.parse(data), function (key, val) {
                    Orden.AddItem([val.Calle, val.NumExt, val.Colonia], val.Orden)
                });
            }
            //else {
            //    Orden.find('option').remove().end().append('<option selected="selected" value="0">Seleccione</option>').val('0')
            //}
        });
    });
});
function OnSelectedChange(s, e) {
    debugger
    var value = s.GetValue();
    if (value == 1)//Pipa
    {
        $('.selPipa').show();
        $('.selCamioneta').hide();
        //clear all form Camionetas        
        IdCamioneta.SetSelectedIndex(0);
        $("#Cantidad45").val(0);
        $("#Cantidad30").val(0);
        $("#Cantidad20").val(0);
        //Cantidad45.value = 0;
        //Cantidad30.value = 0;
        //Cantidad20.value = 0;
    }
    if (value == 2)//Camioneta
    {
        $('.selPipa').hide();
        $('.selCamioneta').show();
        //clear all form Pipas        
        IdPipa.SetSelectedIndex(0);
        // Cantidad.value = 0;
        $("#Cantidad").val(0);
    }
}
//function getvalues(s, e) {
//    if (s.GetValue() != null) {//si viene null       

//        $('#btnCrearCte').prop('disabled', true)//desactivo           
//    }   
//}
var Aceptar = function () {
  //  $('#btnCrearCte').prop('disabled', true)
};
//var Cancelar = function () {
//    $('#btnCrearCte').prop('disabled', false)
//};
