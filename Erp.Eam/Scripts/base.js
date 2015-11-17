
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
					var result=false;
					if ($(form).data('bootstrapValidator').isValid()) {
						$.ajax({
							type: "post",
							url: url,
							data: $("form").serialize(),
							async: false,
							success: function(e) {
								if (e.Status === 1) {
									$("#unknownError").show().find(".help-block").html(e.Message);
								} else {
									erp.subscriber.publish(subscriber);
									result= true;
								}
							}
						});
					}
					return result;
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

	function newItem(baseUrl,title) {
		$.get("/"+baseUrl+"/Edit", function(e) {
			editInDialog(title, "/"+baseUrl+"/Insert",e,onFormInit,"itemChangedSubscriber");
		});
	}
	
	function editItem(id,baseUrl,title) {
		$.get("/"+baseUrl+"/Edit/"+id, function(e) {
			editInDialog(title, "/"+baseUrl+"/Update",e,onFormInit,"itemChangedSubscriber");
		});
	}
	
	function deleteItem(id,baseUrl, name) {
		delInDialog(name, "/"+baseUrl+"/Delete/", id, "itemChangedSubscriber");
	}

	
	function initIndexPage() {
		bindItems(1);
		erp.subscriber.unSubscriber("itemChangedSubscriber");
		erp.subscriber.addSubscriber("itemChangedSubscriber", function(d) {
			bindItems(1);
		});
	}

	//juicer模板配置
	juicer.set(
	{
	'tag::operationOpen': "{%",
	'tag::operationClose': "}"
	});
//	//Vue注册组件
//	//新增按钮
//	Vue.component('add-button', {
//		props: ['title', 'model'],
//		template: '<a href="javascript:void(0);" class="btn btn-primary " onclick="newItem(\'{{model}}\',\'{{title}}\')"><i class="fa  fa-plus"></i> 新增</a>'
//	});
//	//行搜索按钮
//	Vue.component('row-search', {
//		props: ['title', 'model'],
//		template: '<a href="javascript:void(0)" class="btn btn-danger "   onclick="filter()"  onclick="resetSearch()"><i class="fa fa-search"></i> 搜索</a> <a href="javascript:void(0)" class="btn"><i class="fa fa-times"></i> 重置</a>'
//	});
//		//行编辑按钮1
//	Vue.component('row-edit1', {
//		props: ['title', 'model','id','name'],
//		template: '<a href="javascript:void(0)" class="btn   edit" onclick=" editItem(\'{{id}}\', \'{{model}}\', \'{{title}}\') "><i class="fa fa-edit"></i> 编辑</a> <a href="javascript:void(0)" class="btn btn-danger  " onclick=" deleteItem(\'{{id}}\', \'{{model}}\', \'[{{name}}]\') "><i class="fa fa-trash-o"></i> 删除</a>'
//	});
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


