function changePassword() {
	$.post("/Home/ChangePassword", $("form").serialize(), function(e) {
		if (e.Status === 0) {
			Notify(e.Data, 'top-right', '5000', 'success', 'fa-check', true); 
		} else {
						Notify(e.Message, 'top-right', '5000', 'danger', 'fa-times', true); 
		}
	});
}


function validate() {
	$("#form").bootstrapValidator({
			message: '栏目验证未通过',
			fields: {
				currentPassword: {
					validators: {
						notEmpty: {
							message: '当前密码不能为空'
						}
					}
				},
				newPassword: {
					validators: {
						notEmpty: {
							message: '新密码不能为空'
						}, 
						identical: {
						field: 'confirmPassword',
						message: '新密码与确认密码不一致'
						}
					}
				},
				confirmPassword: {
					validators: {
						notEmpty: {
							message: '确认密码不能为空'
						}, 
						identical: {
						field: 'newPassword',
						message: '新密码与确认密码不一致'
						}
					}
				}
			}});
	}

validate();