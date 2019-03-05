$(document).ready(function () {
    function OnSelectedIndexChanged(s, e) {
        var item = Id_Vehiculo.cphidenVehiculos[Id_Vehiculo.GetSelectedIndex()].split("|")
        if (item[0] == true) {
            $('#TipoVehiculo').val(1)
        }
        if (item[1] == true) {
            $('#TipoVehiculo').val(2)
        }
        if (item[2] == true) {
            $('#TipoVehiculo').val(3)
        }
    }
});