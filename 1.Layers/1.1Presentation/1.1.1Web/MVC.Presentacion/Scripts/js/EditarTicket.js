$(document).ready(function () {
    MVCxClientGlobalEvents.AddControlsInitializedEventHandler(AttachEditorValueChangedEvent);

    function AttachEditorValueChangedEvent() {
        var demoOptionNames = ["EditMode", "StartEditAction", "HighlightDeletedRows"];
        $.each(demoOptionNames, function (i, name) {
            var editor = ASPxClientControl.GetControlCollection().GetByName(name);
        });
    }
    function OnBatchEditEndEditing(s, e) {
        s.batchEditApi.SetCellValue(e.visibleIndex, null, true);
    }
    function OnBeginGridCallback(s, e) {
        e.customArgs = MVCxClientUtils.GetSerializedEditorValuesInContainer("options");
    }

    $("#iva").hide();
});


MVCxClientGlobalEvents.AddControlsInitializedEventHandler(AttachEditorValueChangedEvent);

function AttachEditorValueChangedEvent() {
    var demoOptionNames = ["EditMode", "StartEditAction", "HighlightDeletedRows"];
    $.each(demoOptionNames, function (i, name) {
        var editor = ASPxClientControl.GetControlCollection().GetByName(name);
    });
}
function OnBatchEditEndEditing(s, e) {
    s.batchEditApi.SetCellValue(e.visibleIndex, null, true);
}
function OnBeginGridCallback(s, e) {
    e.customArgs = MVCxClientUtils.GetSerializedEditorValuesInContainer("options");
}


