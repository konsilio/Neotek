//funcion para redondiar
//By Jaime Santillan
function redondeo(numero, decimales) {
    //var flotante = parseFloat(numero);
    //var resultado = Math.round(flotante * Math.pow(10, decimales)) / Math.pow(10, decimales);
    //return resultado;
    var power = Math.pow(10, decimales);
    return Math.round(numero * power) / power;
}

//funcion generica para redirecionamiento en MVC
//By Celia Ambriz
$(document).ready(function (btnName, controllerName, actionName) {
    $(btnName).click(function () {
        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "' + controllerName + '", Action = "' + actionName + '" }) %>');
        form.attr('method', 'post');
    });
});