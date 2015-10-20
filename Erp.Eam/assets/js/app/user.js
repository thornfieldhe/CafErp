var columns=[];

	function bindUsers(index) {
		$.get("/Home/GetUserList?pageIndex="+index+"&pageSize=20", function(e) {
			columns = e.Datas;
			e = $.extend(true, e, { colspan: 3 ,pageChangeAction:"bindColumns" });
			var html =juicer($("#table").html(), { data: e });
			$('#columnGrid').html(html);
		});
	}

	function editUser(id) {
		var column = _.find(columns, function(r) {
			return r.Id === id;
		});
		var html =juicer($("#submitForm").html(),column);
		editInDialog("编辑栏目", "/Home/UpdateUser",html,onFormInit,"userChangedSubscriber");
	}

	function deleteUser(id,name) {
		delInDialog(name, "/Home/DeleteUser",id,"userChangedSubscriber");
	}

	function init() {
			bindUsers(1);
			erp.subscriber.addSubscriber("userChangedSubscriber", function(d) {
			bindColumns(1);
			});
	}

	$("#newUser").on('click', function () {
		var html =juicer($("#submitForm").html(),{Name:"",Order:0,Id:""});
		editInDialog("新增用户", "/Home/CreateUser",html,onFormInit,"userChangedSubscriber");
	});

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
	