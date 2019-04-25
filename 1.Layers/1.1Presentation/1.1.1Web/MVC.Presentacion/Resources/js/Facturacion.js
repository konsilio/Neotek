function OnSelectedChangeCountry(s, e) {
    if (s.GetValue() !== 1) {
        $('#ddlEdo').hide();
        $('#txtEdoP').show();
    }
    else {
        $('#ddlEdo').show();
        $('#txtEdoP').hide();
    }
};
function OnSelectedChangeFilter(s, e) {
    if (s.GetValue() == 2) {//($('#ddlTipopersona').find('option:selected').text() == 'Moral') {
        $('#div_hideRP').show();
        $('#div_hidePM').hide();
        $('#hide_razonS').show();
    }

    if (s.GetValue() == 1) {// ($('#ddlTipopersona').find('option:selected').text() == 'Física') {
        $('#div_hideRP').hide();
        $('#hide_razonS').hide();
        $('#div_hidePM').show();
    }
    if (s.GetValue() == 0) {//($('#ddlTipopersona').find('option:selected').text() == 'Seleccione') {
        $('#div_hidePM').show();
        $('#div_hideRP').show();
        $('#hide_razonS').show();
    }
}