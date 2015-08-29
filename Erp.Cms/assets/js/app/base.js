//弹出对话框编辑对象
	function editInDialog(title,url,formDocument,validate,subscriber) {
		bootbox.dialog({
			message:formDocument,
			title: title,
			className: "modal-darkorange",
			buttons: {
				cancel: {
					label: "取消",
					className: "btn-default",
					callback: function () { }
				},
				success: {
				label: "保存",
				className: "btn-danger",
				callback: function() {
					console.log(1);
					if ($(form).data('bootstrapValidator').isValid()) {
					console.log(3);
						var result=true;
						$.ajax({
							type: "post",
							url: url,
							data: $("form").serialize(),
							async: false,
							success: function(e) {
								if (e.Status === 1) {
									$("#unknownError").show().find(".help-block").html(e.Message);
									result = false;
								} else {
									erp.subscriber.publish(subscriber);
								}
							}
						});
						return result;
						}else {
						$(form).data('bootstrapValidator').validate();
							return false;
						}
					}
				}
			}
		});
		validate();
	}

	//模板配置
	juicer.set(
	{
	'tag::operationOpen': "{%",
	'tag::operationClose': "}"
	});

	//erp.subscriber
	erp = {
	subscriber :{
	addSubscriber: function (id, callBack) {
		erp.subscriber.subscribers[id] = callBack;
	},
	unSubscriber: function (id) {
		delete erp.subscriber.subscribers[id];
	},
	publish: function (id, data) {
		var item = erp.subscriber.subscribers[id];
		if (item == null || item === 'undefined') return;
		item(data);
	}
}
};
erp.subscriber.subscribers = new Array();

