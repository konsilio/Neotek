var CalcularImporte = function () {

    var index;
    var idtxtPrec;
    var idtxtDesc;
    var idddlIVA;
    var idddlIEPS;
    var precio = 0;
    var descuento = 0;
    var cantidad = 0;
    var iva;
    var ieps;
    var total;
    var subtotal;
    var id = $(".calculaPrecio").prevObject[0].activeElement.id;
    if (id == "")
        id = $(".calculaPrecio").prevObject[0].activeElement.dataset.id;

    if (id.includes("Precio")) {
        index = id.replace("OrdenCompraProductos_", "").replace("__Precio", "");
    }
    if (id.includes("Descuento")) {
        index = id.replace("OrdenCompraProductos_", "").replace("__Descuento", "");
    }
    if (id.includes("IVA")) {
        index = id.replace("OrdenCompraProductos_", "").replace("__IVA", "");
    }
    if (id.includes("IEPS")) {
        index = id.replace("OrdenCompraProductos_", "").replace("__IEPS", "");
    }

    idtxtPrec = "#OrdenCompraProductos_" + index + "__Precio";
    idtxtDesc = "#OrdenCompraProductos_" + index + "__Descuento";
    idddlIVA = "#OrdenCompraProductos_" + index + "__IVA";
    idddlIEPS = "#OrdenCompraProductos_" + index + "__IEPS";
    idCantidad = "OrdenCompraProductos_" + index + "__CantidadAComprar";
    idTotlaImporte = "OrdenCompraProductos_" + index + "__Importe";

    precio = parseFloat($(idtxtPrec).val());
    cantidad = parseFloat($("label[for = '" + idCantidad + "']").text());
    descuento = ((precio * cantidad) * ($(idtxtDesc).val() / 100));
    subtotal = (precio * cantidad) - (descuento);
    iva = ((subtotal) * ($(idddlIVA + " option:selected").val().replace("IVA", "") / 100));
    ieps = ((subtotal) * ($(idddlIEPS + " option:selected").val().replace("IEPS", "") / 100));
    total = subtotal + iva + ieps;
    $("label[for = '" + idTotlaImporte + "']").text(total.toFixed(2));
}

$(document).ready(function () {
    $("#btnCrear").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "OrdenCompra", Action = "CrearOrdenCompra" }) %>');
        form.attr('method', 'post');
    });
});
$(window).load(function () {
    $('.table-responsive').on('show.bs.dropdown', function () {
        $('.table-responsive').css("overflow", "inherit");
    });

    $('.table-responsive').on('hide.bs.dropdown', function () {
        $('.table-responsive').css("overflow", "auto");
    })
});
