$(document).ready(function () {

    function OnSelectedIndexChanged(s, e) {
        debugger
        var v = IdVehiculo.cpHiddenColumnValues[IdVehiculo.GetSelectedIndex()];
        var esCamioneta = s.GetSelectedItem().GetColumnText[0];
        var esPipa = s.GetSelectedItem().GetColumnText("EsPipa");
        var Esutilitario = s.GetSelectedItem().GetColumnText("Esutilitario");

        Price.SetValue(comboPrice);

        var spinAmount = amount.GetValue();
        var total = "";
        if (spinAmount != null)
            total = spinAmount * comboPrice;
        Total.SetValue(total);
    }
});