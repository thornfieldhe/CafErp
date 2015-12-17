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
	delete: {
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
	}
}

//订阅器集合
taf.subscriber.subscribers = new Array();