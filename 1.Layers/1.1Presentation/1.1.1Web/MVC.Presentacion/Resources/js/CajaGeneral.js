$(document).ready(function () {
    $('#Chkboxcorrecto').click(function () {
        if (!$(this).is(':checked')) {
            $("#btnLiquidar").prop("disabled", true);
        }
        else {
            $("#btnLiquidar").prop("disabled", false);
        }

    });
});