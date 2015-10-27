var users=[];

	function bindUsers(index) {
		$.get("/Home/GetUserList?pageIndex="+index+"&pageSize=10", function(e) {
			users = e.Datas;
			console.log(users);
			e = $.extend(true, e, { colspan: 4 ,pageChangeAction:"bindUsers" });
			var html =juicer($("#table").html(), { data: e });
			$('#userGrid').html(html);
		});
	}

	function editUser(id) {
		$.get("/Home/EditUser?userId="+id, function(e) {
				editInDialog("编辑用户", "/Home/UpdateUser",e,onFormInit,"userChangedSubscriber");
		});
	}

	function deleteUser(id,name) {
		delInDialog(name, "/Home/DeleteUser",id,"userChangedSubscriber");
	}

	function init() {
			bindUsers(1);
			erp.subscriber.addSubscriber("userChangedSubscriber", function(d) {
			bindUsers(1);
			});
	}

	function createUser() {
		$.get("/Home/EditUser", function(e) {
				editInDialog("新增用户", "/Home/CreateUser",e,onFormInit,"userChangedSubscriber");
		});
	}

	function validate() {
	$("#form").bootstrapValidator({
			message: '用户验证未通过',
			fields: {
				loginName: {
					validators: {
						notEmpty: {
							message: '用户名不能为空'
						}
					}
				},
				fullName: {
					validators: {
						notEmpty: {
							message: '全名不能为空'
						}
					}
				},
				password: {
					validators: {
						notEmpty: {
							message: '密码不能为空'
						}, 
						stringLength: {
						min: 6,
						message: '密码不能少于6位'
						},
						identical: {
						field: 'confirmPassword',
						message: '密码与确认密码不一致'
						}
					}
				},
				confirmPassword: {
					validators: {
						notEmpty: {
							message: '确认密码不能为空'
						}, 
						identical: {
						field: 'password',
						message: '密码与确认密码不一致'
						}
					}
				}
			}});
	}

	function onFormInit() {
	validate();
	$('.spinbox').spinbox();
}

init();
	