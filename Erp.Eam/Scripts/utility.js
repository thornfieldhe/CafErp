//taf基础工具
var taf= { 
    //订阅器
    subscriber: {
        //新增订阅
        addSubscriber: function(id, callBack) {
            erp.subscriber.subscribers[id] = callBack;
        },
        //注销订阅
        unSubscriber: function(id) {
            delete erp.subscriber.subscribers[id];
        },
        //发布订阅
        publish: function(id, data) {
            var item = erp.subscriber.subscribers[id];
            if (item == null || item === 'undefined') return;
            item(data);
        }
    },
    //通知
    notify: {
        danger:function(text) {
            Notify(text, 'top-right', '5000', 'danger', 'fa-times', true);
        },
        success:function(text) {
            Notify(text, 'top-right', '5000', 'success', 'fa-times', true);
        }
    },
    delete:
        {
        //弹出对话框删除对象
        delInDialogBase: function(title, calback) {
            bootbox.confirm({
                message: "确认删除" + title + "么？",
                className: "modal-darkorange",
                callback: function(result) {
                    if (result) {
                        calback(result);
                    }
                },
                buttons: {
                    cancel: {
                        label: " <i class='fa   fa-mail-reply'></i> 取消",
                        className: "btn-default",
                        callback: function() {}
                    },
                    confirm: {
                        label: "<i class='fa   fa-trash-o'></i>删除",
                        className: "btn-danger"
                    }
                }
            });
        },
        delInDialog:function (title, url, id, subscriber) {
            delInDialogBase(title, function(result) {
                $.ajax({
                    type: "post",
                    url: url,
                    data: { id: id },
                    async: false,
                    success: function(e) {
                        if (e.Status === 1) {
                            result = false;
                        } else {
                            erp.subscriber.publish(subscriber);
                        }
                    }
                });
            });
        }
    },
    model: {
        find: function(id) {
            
        },
        get: function (index, filter, model,callback) {
            $.get("/" + model + "/List?pageIndex=" + index + "&pageSize=10", filter, function(e) {
                if (e.Status === 1) {
                    taf.notify.danger(e.Message);
                }
                if (callback && callback instanceof Function) {
                    callback(e.Data);
                }
            });
        },
        edit: function (title, model, formDocument, isNew, callback) {
            var url = isNew ? "/" + model + "/Insert" : "/" + model + "/Update";
            bootbox.dialog({
                message: formDocument,
                title: title,
                className: " modal-primary",
                callback: function () {
                },
                buttons: {
                    cancel: {
                        label: "<i class='fa   fa-mail-reply'></i>取消",
                        className: "btn-default",
                        callback: function () { }
                    },
                    success: {
                        label: "<i class='fa    fa-floppy-o'></i>保存",
                        className: "btn-primary",
                        callback: function () {
                            $(form).data('bootstrapValidator').validate();
                            var result = false;
                            if ($(form).data('bootstrapValidator').isValid()) {
                                $.ajax({
                                    type: "post",
                                    url: url,
                                    data: $("form").serialize(),
                                    async: false,
                                    success: function (e) {
                                        if (e.Status === 1) {
                                            $("#unknownError").show().find(".help-block").html(e.Message);
                                        } else {
                                            result = true;
                                        }
                                    }
                                });
                            }
                            return result;
                        }
                    }
                }
            });
            if (callback && callback instanceof Function) {
                callback();
            }
        },
        delete:function(id, model, name,callback) {
            bootbox.confirm({
                message: "确认删除" + name + "么？",
                className: "modal-darkorange",
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            type: "post",
                            url: "/"+model+"/Delete",
                            data: { id: id },
                            async: false,
                            success: function (e) {
                                if (e.Status === 1) {
                                    result = false;
                                } else {
                                    if (callback && callback instanceof Function) {
                                        callback();
                                    }
                                }
                            }
                        });
                    }
                },
                buttons: {
                    cancel: {
                        label: " <i class='fa   fa-mail-reply'></i> 取消",
                        className: "btn-default",
                        callback: function () { }
                    },
                    confirm: {
                        label: "<i class='fa   fa-trash-o'></i>删除",
                        className: "btn-danger"
                    }
                }
            });
        },
       
    }
}

//订阅器集合
taf.subscriber.subscribers = new Array();