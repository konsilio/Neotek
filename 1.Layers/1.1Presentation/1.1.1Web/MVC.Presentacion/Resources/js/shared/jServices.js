var service = {
    proxy: function (options) {
      //  debugger;
        var settings = $.extend({
            url: '',
            method: 'POST',
            data: options.data,
            contentType: 'application/json; charset=utf-8',
            crossDomain: false,
            processData: true,
            dataType: 'JSON',
            headers: [],
            timeout: 300000,
            before: function (data) { },
            after: function (data) { },
            done: function (data) { },
            fail: function (data) { },
            then: function () { }
        }, options);

        var errorHandler = function (data) {
            //debugger;
            if (data.readyState == 0) {
                window.location.reload(true);
            } else if (data.responseText != "") {
                var o = JSON.parse(data.responseText);
                if ($('#modalDialog')) {
                    utils.fnShowNotificacion(o.Message, "danger");
                } else {
                    console.log(data);
                }
            }
        };

        this.execute = function () {
            if (settings.processData != false) {
                settings.data = JSON.stringify(settings.data);
            }
            //debugger;
            $.when($.ajax({
                cache: false,
                url: settings.url,
                method: settings.method,
                data: settings.data,
                contentType: settings.contentType,
                crossDomain: settings.crossDomain,
                processData: settings.processData,
                dataType: settings.dataType,
                timeout: settings.timeout,
                beforeSend: function (request) {
                    if (settings.headers.length > 0) {
                        for (var x in settings.headers) {
                            request.setRequestHeader(settings.headers[x].field, settings.headers[x].value);
                        }
                    }
                    settings.before(request);
                    service.fnShowLoading(options.message);
                },
                statusCode: {
                    404: function () {
                        //alert('404');
                    },
                    302: function () {
                        //murio la session
                        window.location.reload();
                    },
                    999: function () { //error handled
                        //alert("status999");
                    }
                    //,else: function () {
                    //}
                }
            })
                .done(function (data) {
                    service.fnEndLoading();
                    settings.done(data);
                })
                .fail(function (data, a, e) {
                    service.fnEndLoading();
                    if ((data.status !== undefined) && (data.status === 302))
                        window.location.reload();
                    errorHandler(data);
                    settings.fail(data);
                })
                .always(function (data) {
                    settings.after(data);
                }))
            .then(settings.then);
        };
    },
    getProxy: function (options) {
       // debugger;
        var settings = $.extend({
            method: ''
        }, options);
        settings.method = 'GET';
        var p = new service.proxy(settings);
        p.execute();
    },
    postProxy: function (options) {
        // debugger;
        var settings = $.extend({
            method: ''
        }, options);
        settings.method = 'POST';
        if (options.processData != false) {
            options.data = utils.fnJSONToDate(options.data);
        }
        var p = new service.proxy(settings);
        p.execute();
    },
    fnShowLoading: function (message) {
        //debugger;
        $('#TextSapnLoading').text(message);
        $('body').block({
            message: $('#modalLoading'),
            css: { backgroundColor: 'transparent', border: 'transparent' }
        });
    },
    fnEndLoading: function () {
        $('body').unblock();
    }
}