var color;
var utils = {
    properties: {
        mainControllerUrl: "~/Shared/Main"
    },
    fnResolveUrl: function (url) {
        if (url.indexOf("~/") == 0) {
            url = global_baseUrl + url.substring(2);
        }
        return url;
    },
    fnSerializeScope: function (scope) {
        var o = {};
        var a = scope.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    },
    fnJSONToDate: function (o) {
        var s = "a";
        for (var x in o) {
            if ((typeof o[x] === typeof s) && (o[x].substr(0, 5) === '/Date')) {
                try {
                    o[x] = new Date(parseInt(o[x].match(/\d+/)[0]));
                } catch (e) {
                }
            }
        }
    },
    fnSetInputsForMayus: function () {
        $('input:text').blur(function () {
            if (!$(this).hasClass('NoMayus')) {
                $(this).val($(this).val().toUpperCase());
            }
        });
    },
    fnDownloadFile: function (data) {
        if (data.IsCorrect) {
            window.location = data.Location;
        } else {
            utils.fnShowNotificacion(data.Message, "danger");
        }
    },
    fnProcessingButton: function (processing, text, button) {
        $(button).html(processing ? "Procesando Descarga..." : text);
        $(button).prop('disabled', processing);
    },
    fnDetectIE: function () {
        return /(msie|trident)/i.test(navigator.userAgent) ? parseInt(navigator.userAgent.match(/(msie |rv:)(\d+(.\d+)?)/i)[2]) : false;
    },
    fnCheckIE: function () {
        var ie = this.fnDetectIE();
        if (ie > 0 && ie < 11) {
            $(".browser-ie").fadeIn();
        }
    },
    fnCallMainController: function (method) {
        //debugger;
        window.location = this.fnResolveUrl(this.properties.mainControllerUrl) + "/" + method;

    },
    fnShowNotificacion: function (message, type, onUnblock)
    {
        $('#TextSapnDialog').text(message);
        $('#ModalDialogHeader').addClass("modal-headerMessage alert alert-" + type);
        $('body').block({
            message: $('#modalDialog'),
            onUnblock: onUnblock
        });
    },
    fnShowConfirmation: function (message, callback, data) {
        $('#TextSapnConfirmation').text(message);
        $('#modalConfirmation').on('click', '#btnAceptar', function (e) {
            callback(data);
        });
        $('body').block({
            message: $('#modalConfirmation'),
            onUnblock: function () {
                $('#modalConfirmation').off('click').on('click', '#btnAceptar', function () {
                    // function body
                });
            }
        });
    }
};

/*
// Prototipo String
String.prototype.removeSpecialCharacters = function () {

    var replacement = {
        '&acute;': "'",
        '&ldquo;': '"',
        '&rdquo;': '"',
        '&nbsp;': ' ',
        '&rsquo;': "'"
    };
    var result = this;
    for (var x in replacement) {
        result = result.replaceAll(x, replacement[x]);
    }
    return result;
};

String.prototype.fromJsonDate = function () {
    var dateString = this.substr(6);
    return new Date(parseInt(dateString));
};

// Prototipo Date

Date.prototype.toDDMMYYYY = function () {
    var month = this.getMonth() + 1;
    var day = this.getDate();
    var year = this.getFullYear();
    return ('00' + day.toString()).right(2) + "/" + ('00' + month.toString()).right(2) + "/" + year;
};
*/