$(document).ready(function () {
    $("#btnGuardaP").click(function () {

        var form = $(this).parent("form");
        form.attr('action', '<%= Url.RouteUrl(new { Controller = "Roles", Action = "GuardarPermisos" }) %>');
        form.attr('method', 'post');

        //var arrItem = [];
        //var commaSeparated = "";
        //$("#ItemList td input[type=checkbox]").each(function (index, val) {
        //    alert(index);
        //    alert(val);
        //     debugger
        //    var checkdId = $(val).atrr("Id");
        //    var arr = checkdId.split('_');
        //    var currentCheckboxId = arr[1];
        //    var IsChecked = $("#" + checkId).is(":checked", true);
        //    if (Ischecked) {
        //        arrItem.push(currentCheckboxId);
        //    }
        //    alert(arrItem.values);
        //})
        //if (arrItem.lenght != 0)
        //{
        //    $.ajax({
        //        url: "/Roles/SaveList",
        //        type:"POST",
        //        data: { ItemList: commaSeparated },
        //        success: function (response)
        //        {
        //        }
        //    })
        //}
    });
    $(".hideControl").hide();
    $("#homeRequisicion").click(function () {
        $('#MovilCompra').css("display", "none");
        $('#MovilRequsicion').css("display", "");
        $('#SistemaRequisicion').css("display", "");
       // $('#gvwSistemaReq').css("display", "");
        $('#window_Requsicion').css("display", "");
        $('#SistemaCompra').css("display", "none");
        $('#SistemaCatalogo').css("display", "none");
        $('#window_Compra').css("display", "none");
        $('#window_Ventas').css("display", "none");
        $('#window_Inventario').css("display", "none");
        $('#window_Catalogos').css("display", "none");
        $('#hide_divCat').css("display", "none");
        $('#catalogos_tabs').css("display", "none");
        $('#MovilCatalogo').css("display", "none");
        $('#MovilVentas').css("display", "none");
        //$('#SistemaVentas').css("display", "none");
        //$('div#SistemaVentas').css("display", "none");
        $('#SistemaInventario').css("display", "none");
        $('#MovilInventario').css("display", "none");
        $('#btnCatalogo').css("display", "none");
        $('#btnCompra').css("display", "none");
        $('#btnMovilCompra').css("display", "none");
        $('#btnMovilVenta').css("display", "none");
        $('#btnRrequisicion').css("display", "");
        $('#divReq').css("display", "");
        /**************************************/
        $('#DivgvwMovilVenta').css("display", "none");
        $('#tabSisVenta').css("display", "none");
    });
    $("#homeCatalogos").click(function () {
        $('#MovilCompra').css("display", "none");
        $('#SistemaCompra').css("display", "none");
        $('#SistemaCatalogo').css("display", "");
        $('#SistemaInventario').css("display", "none");
        $('#MovilInventario').css("display", "none");
        $('#btnCatalogo').css("display", "");
        $('#window_Compra').css("display", "none");
        $('#window_Ventas').css("display", "none");
        $('#window_Inventario').css("display", "none");
        $('#window_Catalogos').css("display", "");
        $('#hide_divCat').css("display", "");
        $('#tabSisCompra').css("display", "none");
        $('#tabSisVenta').css("display", "none");
        //$('#gvwSistemaReq').css("display", "none");
        $('#catalogos_tabs').css("display", "");
        $('#MovilCatalogo').css("display", "");
        $('#MovilRequsicion').css("display", "none");
        $('#SistemaVentas').css("display", "none");
        $('#MovilVentas').css("display", "none");
        $('#btnCompra').css("display", "none");
        $('#btnRrequisicion').css("display", "none");
    });

    $("#homeCompra").click(function () {
        $('#MovilCompra').css("display", "");
        $('#SistemaCompra').css("display", "");
        $('#divgvwCompra').css("display", "");
        $('#DivgvwMovilCompra').css("display", "");
        $('#DivgvwMovilVenta').css("display", "");
        $('#tabSisCompra').css("display", "");
        $('#tabSisVenta').css("display", "none");
        $('#SistemaCatalogo').css("display", "none");
        $('#window_Compra').css("display", "");
        $('#window_Ventas').css("display", "none");
        $('#SistemaVentas').css("display", "none");
        $('#window_Inventario').css("display", "none");
        $('#window_Catalogos').css("display", "none");
        $('#hide_divCat').css("display", "none");
        $('#catalogos_tabs').css("display", "none");
        $('#MovilCatalogo').css("display", "none");
        $('#SistemaInventario').css("display", "none");
        $('#MovilInventario').css("display", "none");
        $('#btnCatalogo').css("display", "none");
        $('#btnCompra').css("display", "");
        //$('#gvwSistemaReq').css("display", "none");
        $('#divReq').css("display", "none");
        $('#MovilRequsicion').css("display", "none");
        $('#btnMovilCompra').css("display", "");
        $('#btnMovilVenta').css("display", "none");
        $('#btnRrequisicion').css("display", "none");
        $('#MovilVentas').css("display", "none");
    });

    $("#homeInventario").click(function () {
        $('#window_Inventario').css("display", "");
        $('#SistemaInventario').css("display", "");
        $('#MovilInventario').css("display", "");
        $('#MovilCompra').css("display", "none");
        $('#SistemaCompra').css("display", "none");
        $('#SistemaCatalogo').css("display", "none");
        $('#window_Compra').css("display", "none");
        $('#window_Ventas').css("display", "none");
        $('#SistemaVentas').css("display", "none");
        $('#MovilVentas').css("display", "none");
        $('#window_Catalogos').css("display", "none");
        $('#hide_divCat').css("display", "none");
        $('#catalogos_tabs').css("display", "none");
        $('#MovilCatalogo').css("display", "none");
        $('#tabSisCompra').css("display", "none");
        $('#tabSisVenta').css("display", "none");
        //$('#gvwSistemaReq').css("display", "none");
        $('#MovilRequsicion').css("display", "none");
        $('#divgvwCompra').css("display", "none");
        $('#DivgvwMovilCompra').css("display", "none");
        $('#DivgvwMovilVenta').css("display", "none");
        $('#MovilVentas').css("display", "none");
        $('#btnCatalogo').css("display", "none");
        $('#btnCompra').css("display", "none");
        $('#btnMovilCompra').css("display", "none");
        $('#btnMovilVenta').css("display", "none");
        $('#btnRrequisicion').css("display", "none");
    });

    $("#homeVentas").click(function () {
        $('#window_Ventas').css("display", "");
        $('#SistemaVentas').css("display", "");
        $('#MovilVentas').css("display", "");
        $('#tabSisVenta').css("display", "");
        $('#window_Inventario').css("display", "none");
        $('#SistemaInventario').css("display", "none");
        $('#MovilInventario').css("display", "none");
        $('#MovilCompra').css("display", "none");
        $('#SistemaCompra').css("display", "none");
        $('#SistemaCatalogo').css("display", "none");
        $('#window_Compra').css("display", "none");
        $('#window_Catalogos').css("display", "none");
        $('#window_Requsicion').css("display", "none");
        $('#hide_divCat').css("display", "none");
        $('#catalogos_tabs').css("display", "none");
        $('#MovilCatalogo').css("display", "none");
        $('#tabSisCompra').css("display", "none");
        //$('#gvwSistemaReq').css("display", "none");
        $('#MovilRequsicion').css("display", "none");
        $('#divgvwCompra').css("display", "none");
        $('#DivgvwMovilCompra').css("display", "none");
        $('#DivgvwMovilVenta').css("display", "");
        $('#btnCatalogo').css("display", "none");
        $('#btnCompra').css("display", "none");
        $('#btnMovilCompra').css("display", "none");
        $('#btnMovilVenta').css("display", "");
        $('#btnRrequisicion').css("display", "none");
        $('#divReq').css("display", "none");
        $('#SistemaRequisicion').css("display", "none");        
    });
    $("#MovilRequsicion").click(function () {
        $('#window_Inventario').css("display", "none");
        $('#SistemaInventario').css("display", "none");
        $('#MovilInventario').css("display", "none");
        $('#MovilCompra').css("display", "none");
        $('#SistemaCompra').css("display", "none");
        $('#SistemaCatalogo').css("display", "none");
        $('#window_Compra').css("display", "none");
        $('#window_Catalogos').css("display", "none");
        $('#hide_divCat').css("display", "none");
        $('#catalogos_tabs').css("display", "none");
        $('#MovilCatalogo').css("display", "none");
        $('#tabSisCompra').css("display", "none");
        $('#tabSisVenta').css("display", "none");
        $('#divgvwCompra').css("display", "none");
        $('#DivgvwMovilCompra').css("display", "none");
        $('#DivgvwMovilVenta').css("display", "none");
        $('#MovilVentas').css("display", "none");
        $('#btnCatalogo').css("display", "none");
        $('#btnCompra').css("display", "none");
        $('#btnMovilCompra').css("display", "none");
        $('#btnMovilVenta').css("display", "none");
        $('#window_Ventas').css("display", "none");
        $('#SistemaVentas').css("display", "none");
        $('#MovilVentas').css("display", "none");

    });
});