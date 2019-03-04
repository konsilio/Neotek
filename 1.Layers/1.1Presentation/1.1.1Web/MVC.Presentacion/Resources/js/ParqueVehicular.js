$(document).ready(function () {

    //Bloquear tamaños maximos
    $('#NumMotor').keypress(function () {
        if (this.value.length >= 10)
            return false;
    })
    $('#Placas').keypress(function () {
        if (this.value.length >= 8)
            return false;
    })
    $('#NumIdVehicular').keypress(function () {
        if (this.value.length >= 8)
            return false;
    })
})


function OnSelectedChange(s, e) {
    debugger
    var value = s.GetValue();
    if (value == 1)//Camioneta
    {
        $('.capKilos').show();
        $('.capLitos').show();
        $('.medidor').hide();
    }
    if (value == 2)//Pipa
    {
        $('.capKilos').show();
        $('.capLitos').show();
        $('.medidor').show();
    }
    if (value == 0 || value == 3)//utilitario
    {
        $('.capKilos').hide();
        $('.capLitos').hide();
        $('.medidor').hide();      
    }
}
