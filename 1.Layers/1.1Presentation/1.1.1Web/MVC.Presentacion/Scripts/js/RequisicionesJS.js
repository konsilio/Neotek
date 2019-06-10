$(document).ready(function () {
    $("#txtNumRequisicion").keyup(Busqueda);
    $("#ddlEmpresas").on('change', Busqueda);

});//document.ready

var Busqueda = function () {
    var url = "/Requisicion/BuscarPorNumeroRequisicion";
    var clave = $('#txtNumRequisicion').val();
    var id = $('#ddlEmpresas').find('option:selected').val();
    if (typeof id === "undefined")
        id = 0;
    var str = '';
    $.getJSON(url, { numrequisicion: clave, idEmpresa: id }, function (data) {
        $("#contenido").empty();
        $.each(JSON.parse(data), function (index, value) {
            if (value.IdRequisicionEstatus == 1) {
                str = '<a id="lbDgOjo" href="">'
                    + '<i class="material-icons">picture_as_pdf</i>'
                    + '<span class="icon-name"></span></a><a id="lbDgPDF" href="'
                    + '@Url.Action("RequisicionRevision", "Requisicion", new { Id = value.IdRequisicion, estatus = value.IdRequisicionEstatus })">'
                    + '<i class="material-icons">content_paste</i>'
                    + '<span class="icon-name"></span></a>'
            }
            if (value.IdRequisicionEstatus == 3 || value.IdRequisicionEstatus == 4) {
                + '<a id="lbDgPDF" href="">'
                + '<i class="material-icons">picture_as_pdf</i>'
                + '<span class="icon-name"></span>'
                + '</a>'
                + '<a id="lbAutoriza" href="@Url.Action("RequisicionAutorizacion", "Requisicion", new { Id = req.IdRequisicion, estatus = req.IdRequisicionEstatus })">'
                + '<i class="material-icons">spellcheck</i>'
                + '<span class="icon-name"></span>'
                + '</a>'
            }
            $("#contenido").append("<tr><td>"
                + value.NombreComercial + "</td>"
                + "<td>" + value.NumeroRequisicion + "</td>"
                + "<td>" + value.FechaRequerida + "</td>"
                + "<td>" + value.UsuarioSolicitante + "</td>"
                + "<td>" + value.RequisicionEstatus + "</td>"
                + "<td>" + str + "</td></tr>");
        });
    });
};