$(document).ready(function () {
    $(".filterable tr:has(td)").each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
         .hide().text(t).appendTo(this);
    });
    $("#txtFiltroNumero").keyup(function () {
        var s = $(this).val().toLowerCase().split(" ");
        $(".filterable tr:hidden").show();
        $.each(s, function () {
            $(".filterable tr:visible .indexColumn:not(:contains('"
               + this + "'))").parent().hide();
        });//each
    });
});//document.ready