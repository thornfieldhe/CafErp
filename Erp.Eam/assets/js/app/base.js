
//弹出对话框编辑对象
	function editInDialog(title,url,formDocument,callback,subscriber) {
		bootbox.dialog({
			message:formDocument,
			title: title,
			className: " modal-primary",
			callback:function() {
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
				callback: function() {
					$(form).data('bootstrapValidator').validate();
							if ($(form).data('bootstrapValidator').isValid()) {
								$.ajax({
									type: "post",
									url: url,
									data: $("form").serialize(),
									async: false,
									success: function(e) {
										if (e.Status === 1) {
											$("#unknownError").show().find(".help-block").html(e.Message);
											return false;
										} else {
											erp.subscriber.publish(subscriber);
											return true;
										}
									}
								});
							}
							return false;
					}
				}
			}
		});
		if (callback!==null) {
			callback();
		}
		
	}

	//弹出对话框删除对象
	function delInDialog(title,url,id,subscriber) {
bootbox.confirm({
			message: "确认删除"+title+"么？",
			className: "modal-darkorange",
			callback:function(result) {
				if (result) {
						$.ajax({
							type: "post",
							url: url,
							data: {id:id},
							async: false,
							success: function(e) {
								if (e.Status === 1) {
									
									result = false;
								} else {
									erp.subscriber.publish(subscriber);
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


