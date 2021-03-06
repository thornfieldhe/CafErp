﻿function changePassword() {
	$(form).data('bootstrapValidator').validate();
	if ($(form).data('bootstrapValidator').isValid()) {
			$.post("/Home/ChangePassword", $("form").serialize(), function(e) {
				if (e.Status === 0) {
					taf.notify.success(e.Data);
				} else {
					taf.notify.danger(e.Message);
				}
		});
	}
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
						stringLength: {
						min: 6,
						message: '密码不能少于6位'
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