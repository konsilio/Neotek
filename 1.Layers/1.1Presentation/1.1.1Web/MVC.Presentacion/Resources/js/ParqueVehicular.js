function OnSelectedChange(s, e) {
    debugger
    var value = s.GetValue();
    if (value == 1)//Pipa
    {
        $('.selPipa').show();
        $('.selCamioneta').hide();
        IdCamioneta.SetSelectedIndex(0);
      
    }
    if (value == 2)//Camioneta
    {
        $('.selPipa').hide();
        $('.selCamioneta').show();
        //clear all form Pipas        
        IdPipa.SetSelectedIndex(0);
        $("#Cantidad").val(0);
    }
}