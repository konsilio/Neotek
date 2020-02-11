
$(document).ready(function () {
    $("#btnCrear").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "OrdenCompra", Action = "CrearOrdenCompra" }) %>');
        form.attr('method', 'post');
    });
});

MVCxClientGlobalEvents.AddControlsInitializedEventHandler(AttachEditorValueChangedEvent);

function AttachEditorValueChangedEvent() {
    var demoOptionNames = ["EditMode", "StartEditAction", "HighlightDeletedRows"];
    $.each(demoOptionNames, function (i, name) {
        var editor = ASPxClientControl.GetControlCollection().GetByName(name);      
    });
}
function OnBatchEditEndEditing(s, e) {
    var precioindex = s.GetColumnByField("Precio").index;
    var cantidadindex = s.GetColumnByField("Cantidad").index;
    var descuentoindex = s.GetColumnByField("Descuento").index;
    var ivaindex = s.GetColumnByField("IVA").index;
    var iepsindex = s.GetColumnByField("IEPS").index;

    var precio = e.rowValues[precioindex].value;
    var cantidad = e.rowValues[cantidadindex].value;
    var descuento = ((precio * cantidad) * (e.rowValues[descuentoindex].value / 100));
    var subtotal = (precio * cantidad) - (descuento);
    var iva = ((subtotal) * (e.rowValues[ivaindex].value / 100));
    var ieps = ((subtotal) * (e.rowValues[iepsindex].value / 100));
    var total = subtotal + iva + ieps;
    s.batchEditApi.SetCellValue(e.visibleIndex, "Importe", total, null, true);

}
function OnBeginGridCallback(s, e) {
    e.customArgs = MVCxClientUtils.GetSerializedEditorValuesInContainer("options");
}