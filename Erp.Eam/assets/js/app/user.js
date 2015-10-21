var users=[];

	function bindUsers(index) {
		$.get("/Home/GetUserList?pageIndex="+index+"&pageSize=20", function(e) {
			users = e.Datas;
			e = $.extend(true, e, { colspan: 4 ,pageChangeAction:"bindUsers" });
			var html =juicer($("#table").html(), { data: e });
			$('#userGrid').html(html);
		});
	}

	function editUser(id) {
		var user = _.find(users, function(r) {
			return r.Id === id;
		});
		var html =juicer($("#submitForm").html(),user);
		editInDialog("编辑栏目", "/Home/UpdateUser",html,onFormInit,"userChangedSubscriber");
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

	$("#newUser").on('click', function () {
		var html =juicer($("#submitForm").html(),{LoginName:"",FullName:"",RoleIds:[],Id:""});
		console.log();
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
				}
			}});
	}

	function onFormInit() {
	validate();
	$('.spinbox').spinbox();
}

init();
	