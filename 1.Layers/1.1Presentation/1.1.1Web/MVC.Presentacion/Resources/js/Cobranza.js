function OnValueGotFocus(s, e, index, field){
    var text = s.GetValue();
}
function OnBeginGridCallback(s, e) {
    e.customArgs = MVCxClientUtils.GetSerializedEditorValuesInContainer("options");
}
function OnBatchEditEndEditing(s, e) {
  
    s.batchEditApi.SetCellValue(e.visibleIndex, null, true);
}