    $(document).ready(function () {
        $("#btnPagoExpedidor").click(function () {
            $("form").attr("action", "/OrdenCompra/SolicitarPagoExpedidor");
        });
        $("#btnPagoPorteador").click(function () {
            $("form").attr("action", "/OrdenCompra/SolicitarPagoPorteador");
        });
        $("#btnGuardarDatosExpedidor").click(function () {
            $("form").attr("action", "/OrdenCompra/GuardarDatosExpedidor");
        });
        $("#btnGuardarDatosPorteador").click(function () {
            $("form").attr("action", "/OrdenCompra/GuardarDatosPorteador");
        });
        $("#btnGuardarDatosPapeleta").click(function () {
            $("form").attr("action", "/OrdenCompra/GuardarDatosPapeleta");
        });
    });

//kilos finales
    var ObtenerLitrosDesdeKilos = function (kilogramos, factor) {
        return kilogramos / factor;
    };
    var ObtenerLitrosEnElTanque = function (capacidadTanqueLt, porcentaje) {
        return capacidadTanqueLt * (porcentaje / 100);
    };
    var ObtenerKilogramosDesdeLitros = function (litros, factor) {
        return litros * factor;
    };
    var ObtenerDiferenciaKilogramos = function (cantidadMayor, cantidadMenor) {
        if (cantidadMayor < cantidadMenor)
            return cantidadMenor - cantidadMayor;
        return (cantidadMayor - cantidadMenor);
    };

    //Expedidor
    var ObtenerPrecioXGalon = function (RPMMNTPG, TSG, TC) {
        return (parseFloat(RPMMNTPG) + parseFloat(TSG)) * TC;
    };
    var ObtenerImporteLitros = function (PrecioXGalon, FactorCGalLtr) {
        return PrecioXGalon / FactorCGalLtr;
    };
    var ObtenerPVPM = function (ImporteLitros, FactorCaKg) {
        return ImporteLitros / FactorCaKg;
    };
    var ObtenerPVIva = function (PVPM, IVA) {
        return (PVPM * (IVA / 100) + parseFloat(PVPM));
    };
    var ObtenerImportePagar = function (kilogramosPapeleta, PVIva) {
        return kilogramosPapeleta * PVIva;
    };

    //Porteador
    var ObtenerPrecioTransporte = function (Factor, KilosPapeleta) {
        return (Factor * KilosPapeleta)
    };
    var ObtenerSubtotalTransporte = function (PrecioTransporte, Casetas) {
        return (parseFloat(PrecioTransporte) + parseFloat(Casetas))
    };
    var ObtenerImporteTransporte = function (Subtotal, Iva) {
        return (Subtotal * (Iva / 100)) + parseFloat(Subtotal);
    };


    var CalcularKilosFinales = function () {
        var Factor = $("#FactorLitrosAKilos").val();
        var kilogramosPapeletaTractor = $("#KilosPapeleta").val();
        var litrosPapeletaTractor = ObtenerLitrosDesdeKilos(kilogramosPapeletaTractor, Factor);
        var litrosRealesTractor = ObtenerLitrosEnElTanque($("#CapacidadTanque").val(), $("#PorcenMagnatelOcularTractorINI").val());
        var kilogramosRealesTractor = ObtenerKilogramosDesdeLitros(litrosRealesTractor, Factor);
        var kilogramosRemanentes = ObtenerDiferenciaKilogramos(kilogramosRealesTractor, kilogramosPapeletaTractor);

        $("#KilosDescargados").val(redondeo(kilogramosRealesTractor, 5));
        $("#KilosDiferencia").val(redondeo(kilogramosRemanentes, 5));
    };

    var CalcularImporteExpedidor = function () {
        var RPMMNTPG = $("#OrdenCompraExpedidor_MontBelvieuDlls").val();
        var TSG = $("#OrdenCompraExpedidor_TarifaServicioPorGalonDlls").val();
        var TC = $("#OrdenCompraExpedidor_TipoDeCambioDOF").val();
        var FactorCGalLtr = $("#OrdenCompraExpedidor_FactorGalonALitros").val();
        var FactorCaKg = $("#OrdenCompraExpedidor_FactorCompraLitrosAKilos").val();
        var kilogramosPapeleta = $("#KilosPapeleta").val();
        var Iva = $("#IVAExpedidor option:selected").val();

        var PrecioXGalon = ObtenerPrecioXGalon(RPMMNTPG, TSG, TC);
        var ImporteLitros = ObtenerImporteLitros(PrecioXGalon, FactorCGalLtr);
        var PVPM = ObtenerPVPM(ImporteLitros, FactorCaKg);
        var PVIva = redondeo(ObtenerPVIva(PVPM, Iva), 5);
        var ImportePagar = ObtenerImportePagar(kilogramosPapeleta, PVIva);

        $("#PrecioPorGalon")[0].textContent = Number.isNaN(PrecioXGalon) ? "0" : redondeo(PrecioXGalon, 5);
        $("#ImporteEnLitros")[0].textContent = Number.isNaN(ImporteLitros) ? "0" : redondeo(ImporteLitros, 5);
        $("#PVPM")[0].textContent = Number.isNaN(PVPM) ? "0" : redondeo(PVPM, 5);
        $("#PrecioConIVA")[0].textContent = Number.isNaN(PVIva) ? "0" : redondeo(PVIva, 5);
        $("#ImporteExpedidor")[0].textContent = new Intl.NumberFormat("es-MX").format(Number.isNaN(ImportePagar) ? "0" : ImportePagar);
    };

    var CalcularImportePortador = function () {
        var FactorConv = $("#OrdenCompraPorteador_FactorConvTransporte").val();
        var kilogramosPapeleta = $("#KilosPapeleta").val();
        var Casetas = $("#OrdenCompraPorteador_Casetas").val();
        var Iva = $("#IVAPorteador option:selected").val();

        var PrecioTransporte = ObtenerPrecioTransporte(FactorConv, kilogramosPapeleta);
        var Subtotal = ObtenerSubtotalTransporte(PrecioTransporte, Casetas);
        var ImportePagar = ObtenerImporteTransporte(Subtotal, Iva);

        $("#PrecioTransporte")[0].textContent = Number.isNaN(PrecioTransporte) ? "0" : redondeo(PrecioTransporte, 5);
        $("#SubTotoalPorteador")[0].textContent = Number.isNaN(Subtotal) ? "0" : redondeo(Subtotal, 5);
        $("#ImportePorteador")[0].textContent = new Intl.NumberFormat("es-MX").format(Number.isNaN(ImportePagar) ? "0" : redondeo(ImportePagar, 2));
    };

    window.onload = function () {
        CalcularKilosFinales();
    }


    MVCxClientGlobalEvents.AddControlsInitializedEventHandler(AttachEditorValueChangedEvent);

    function AttachEditorValueChangedEvent() {
        var demoOptionNames = ["EditMode", "StartEditAction", "HighlightDeletedRows"];
        $.each(demoOptionNames, function (i, name) {
            var editor = ASPxClientControl.GetControlCollection().GetByName(name);
        });
    }
    function OnBatchEditEndEditing(s, e) {
        //var precioindex = s.GetColumnByField("Precio").index;
        //var cantidadindex = s.GetColumnByField("CantidadAComprar").index;
        //var descuentoindex = s.GetColumnByField("Descuento").index;
        //var ivaindex = s.GetColumnByField("IVA").index;
        //var iepsindex = s.GetColumnByField("IEPS").index;

        //var precio = e.rowValues[precioindex].value;
        //var cantidad = e.rowValues[cantidadindex].value;
        //var descuento = ((precio * cantidad) * (e.rowValues[descuentoindex].value / 100));
        //var subtotal = (precio * cantidad) - (descuento);
        //var iva = ((subtotal) * (e.rowValues[ivaindex].value / 100));
        //var ieps = ((subtotal) * (e.rowValues[iepsindex].value / 100));
        //var total = subtotal + iva + ieps;
        s.batchEditApi.SetCellValue(e.visibleIndex, null, true);
    }
    function OnBeginGridCallback(s, e) {
        e.customArgs = MVCxClientUtils.GetSerializedEditorValuesInContainer("options");
    }