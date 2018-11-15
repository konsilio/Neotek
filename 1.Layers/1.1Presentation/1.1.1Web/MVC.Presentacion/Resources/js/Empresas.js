$("#RegresaIndex").click(function () {
    var form = $(this).parent("form");
    form.attr('action', '<%= Url.RouteUrl(new { Controller = "Empresas", Action = "Index" }) %>');
    form.attr('method', 'post');
});
$(document).ready(function () {

    $("#RegresaIndexNE").click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Empresas", Action = "Index" }) %>');
        form.attr('method', 'post');
    }); 

});
