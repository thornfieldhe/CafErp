//弹出对话框编辑对象
	function editInDialog(title,url,formDocument,validate) {
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
					var isValidate = $(form).data('bootstrapValidator').isValid();
					if (isValidate) {
						var result=true;
						$.ajax({
							type: "post",
							url: url,
							data: $("form").serialize(),
							async: false,
							success: function(e) {
							if (e.Status === 1) {
										$("#unknownError").show().find(".help-block").html(e.Message);
										result= false;
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