var catalogo = {
    scope: {
    },
    elements: {
        controller: "",
        Ascendente : true,
        filtro: {},
        scope: {}
    },
    validator: {
        onValid: function (event) {
            catalogo.fnEjecutar($(document.activeElement).attr('value'));
        },
        onError: function (event) {
            catalogo.fnEjecutar($(document.activeElement).attr('value'), 'Error');
        },
        validators: {
            regExp: {
                Email: {
                    pattern: /^([\w\-\.]+@[a-z]+\.([a-z]+\.){0,2}[a-z]{2,3}\s?)*[\w\-\.]+@[a-z]+\.([a-z]+\.){0,2}[a-z]{2,3}$/,
                    errorMessage: 'Invalid format email.'
                }
            }
        }
    },
    fngetOptions: function() 
    {
        var options = {};
        options.headers = [];
        options.headers.push({
            field: "__RequestVerificationToken",
            value: document.getElementsByName('__RequestVerificationToken').__RequestVerificationToken.defaultValue
        });
        return options;
    },
    fnConstructor: function (scopeCatalogo) {
        this.scope = scopeCatalogo;
        this.elements.controller = utils.fnResolveUrl("~/" + catalogo.scope.Controller);//+ catalogo.scope.Subfolder + "/"
    },
    fnBuscar: function (filtro) {
        var options = catalogo.fngetOptions();
        filtro.CantidadPorPagina = 10;
        options.message = 'Processing request...';
        options.url = catalogo.elements.controller + "/Buscar";
        options.data = filtro;
        options.dataType = 'text';
        options.done = function (data) {
            $('#GridView').html(data);
        };
        service.postProxy(options);
    },
    fnShowModal: function (filtro) {
        var options = catalogo.fngetOptions();
        options.message = 'Opening form...';
        options.url = catalogo.elements.controller + "/Show";
        options.data = filtro;
        options.dataType = 'text';
        options.done = function (data) {
            if (!data.IsCorrect) {
                $('div.page-container').block({ message: data});
                catalogo.fnSubmit();
                $("#frmSubmit").validetta(catalogo.validator);
            }
        };
        service.postProxy(options);
    },
    fnShowAction: function (action) {
        var options = catalogo.fngetOptions();
        options.message = 'Opening information';
        options.url = catalogo.elements.controller + "/" + action;
        options.dataType = 'text';
        options.done = function (data) {
            if (!data.IsCorrect) {
                $('div.page-container').block({ message: data});
            }
        };
        service.postProxy(options);
    },
    fnEliminar: function (filtro) {
        var options = catalogo.fngetOptions();
        options.message = 'Removing record...';
        options.url = catalogo.elements.controller + "/Baja";
        options.data = filtro;
        options.done = function (data) {
            if (!data.IsCorrect) {
                utils.fnShowNotificacion(data.Message, "danger");
            } else {
                utils.fnShowNotificacion(data.Message, "success", function () {
                    catalogo.fnEjecutar('Regresar', null);
                });
            }
        };
        service.postProxy(options);
    },
    fnEjecutar: function (tipo, valor) {
        switch (tipo) {
            case 'Regresar':
                $('#CampoFiltro').val('');
                catalogo.elements.filtro = {};
                catalogo.elements.filtro.Pagina = 1;
                catalogo.fnBuscar(catalogo.elements.filtro);
                break;
            case 'Ir':
                if (valor.value)
                    valor = parseInt(valor.value);
                if(valor == '-' || valor == '+')
                {
                    newPage = valor == '-' ? -1 : 1;
                    catalogo.elements.filtro.Pagina = catalogo.elements.filtro.Pagina + (valor == '-' ? -1 : 1);
                }
                else
                {
                    catalogo.elements.filtro.Pagina = valor;
                }

                catalogo.fnBuscar(catalogo.elements.filtro);
                break;
            case 'Ordenar':
                catalogo.elements.filtro.Ascendente = catalogo.elements.Ascendente;
                catalogo.elements.filtro.OrdenaPor = valor;
                catalogo.fnBuscar(catalogo.elements.filtro);
                catalogo.elements.Ascendente = !catalogo.elements.Ascendente;
                break;
            case 'Alta':
                var filtro = {};
                filtro.ValidarRequerido = true;
                filtro.ValidarExpresion = true;
                catalogo.fnShowModal(filtro);
                break;
            case 'Delete':
                utils.fnShowConfirmation('¿Are you sure to delete the record?'
                                        , function (valor) {
                                            catalogo.fnEliminar(valor);
                                        }
                                        , valor);
                break;
            case 'Edit':
                var filtro = valor;
                filtro.ValidarRequerido = true;
                filtro.ValidarExpresion = true;
                catalogo.fnShowModal(filtro);
                break;
            case 'Buscar':
                catalogo.elements.filtro = {};
                catalogo.elements.filtro[catalogo.scope.CampoFiltro] = $('#CampoFiltro').val();
                catalogo.elements.filtro.Pagina = 1;
                catalogo.fnBuscar(catalogo.elements.filtro);
                break;
            case 'BuscarAvanzado':
                var filtro = {};
                filtro.ValidarRequerido = false;
                filtro.ValidarExpresion = true;
                catalogo.fnShowModal(filtro);
                break;
            case 'Detail':
                var filtro = valor;
                filtro.ValidarRequerido = false;
                filtro.ValidarExpresion = false;
                catalogo.fnShowModal(filtro);
                break;
            case 'Check':
                break;
            case 'Aceptar':
                event.preventDefault();
                //Al dar click en el boton Aceptar de los modales de Agregar, Actualizar y Buscar avanzado.
                if (valor != 'Error') {
                    var f = catalogo.elements.scope;
                    if (f.prop('action').indexOf('Buscar') > -1) {
                        //catalogo.fnDefineCheckBox();
                        catalogo.elements.filtro = utils.fnSerializeScope(f);
                        catalogo.elements.filtro.Pagina = 1;
                        catalogo.fnBuscar(catalogo.elements.filtro);
                        $('div.page-container').unblock();
                    }
                    else {
                        var options = catalogo.fngetOptions();
                        options.message = 'Processing...';
                        options.url = f.prop('action');
                        //options.dataType = 'text';
                        catalogo.fnDefineCheckBox();
                        options.data = utils.fnSerializeScope(f);

                        if (catalogo.scope.Controller == "AdminRoles") {
                            catalogo.fnSerializeMenu(options.data);
                        }

                        if (f.prop('action').indexOf('Carga') > -1) {
                            var formdata = new FormData();
                            var file = $('#file')[0].files[0];
                            formdata.append('file', file);
                            options.contentType = false;
                            options.processData = false;
                            options.data = formdata;
                        }


                        options.done = function (data) {
                            if (!data.IsCorrect) {
                                if (data.View) {
                                    $('body').block({ message: data.View });
                                }
                                else {
                                    utils.fnShowNotificacion(data.Message, "danger");
                                }
                            } else {
                                utils.fnShowNotificacion(data.Message, "success"
                                    , function () {
                                                    $('div.page-container').unblock();
                                                    catalogo.fnEjecutar('Regresar', null);
                                                });
                            }
                        };
                        service.postProxy(options);
                    }
                }
                break;
            case 'Carga':
                var filtro = {};
                filtro.ValidarCarga = true;
                catalogo.fnShowModal(filtro);
                break;
        }
    },
    fnSubmit: function () {
        //  debugger;
        $('#frmSubmit').submit(function (evt) {
            catalogo.elements.scope = $(this);
        });
    },
    fnSerializeMenu: function (data) {
        data.RolMenus = [];
        //Se obtiene el valor de los checbox de los nodos y se agrega el elemento al modelo RolMenus
        $('input', $('#frmSubmit')).each(function (index) {
            if ($(this).attr("id") != undefined && $(this).attr("id").indexOf("Menu") >= 0) {
                data.RolMenus.push({
                    IdMenu: $(this).attr("id").replace('Menu', ''),
                    Seleccionado: $(this).prop('checked'),
                    Privilegios: { Alta: false, Baja: false, Cambio: false }
                });
            }
        });

        //Se obtienen los valores de los checkbox de privilegios y se actualizan los elemenetos del modelo RolMenus
        $('input', $('#frmSubmit')).each(function (index) {
            if ($(this).attr("id") != undefined && $(this).attr("id").indexOf("Privilegio") >= 0) {
                for (var x in data.RolMenus) {
                    if (data.RolMenus[x].IdMenu == $(this).attr("id").replace('PrivilegioAlta', '')) {
                        data.RolMenus[x].Privilegios.Alta = $(this).prop('checked');
                    }
                    if (data.RolMenus[x].IdMenu == $(this).attr("id").replace('PrivilegioBaja', '')) {
                        data.RolMenus[x].Privilegios.Baja = $(this).prop('checked');
                    }
                    if (data.RolMenus[x].IdMenu == $(this).attr("id").replace('PrivilegioCambio', '')) {
                        data.RolMenus[x].Privilegios.Cambio = $(this).prop('checked');
                    }
                }
            }
        });

    },
    fnDefineCheckBox: function()
    {
        $("input[type='hidden']", $('#frmSubmit')).each(function (index) {
            if ($('#' + $(this).attr("name")).prop("type") == 'checkbox')
            {
                if ($('#' + $(this).attr("name")).prop("checked")) {
                    $(this).remove();
                }
            }
        });
    }
}

$(function () {
    //debugger;
    catalogo.fnConstructor(scopeCatalogo);
    catalogo.fnEjecutar('Regresar', null);
});