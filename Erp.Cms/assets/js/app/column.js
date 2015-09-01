var columns=[];

	function bindColumns(index) {
		$.get("/Manage/GetColumnList?pageIndex="+index+"&pageSize=20", function(e) {
			columns = e.Datas;
			var html =juicer($("#table").html(), { data: e });
			$('#columnGrid').html(html);
		});
	}

	function editColumn(id) {
		var column = _.find(columns, function(r) {
			return r.Id === id;
		});
		var html =juicer($("#submitForm").html(),column);
		editInDialog("编辑栏目", "/Manage/UpdateColumn",html,onFormInit,"columnChangedSubscriber");
	}

	function deleteColumn(id,name) {
		delInDialog(name, "/Manage/DeleteArticle",id,"columnChangedSubscriber");
	}

	function init() {
			bindColumns(1);
			erp.subscriber.addSubscriber("columnChangedSubscriber", function(d) {
			bindColumns(1);
			});
	}

	$("#newColumn").on('click', function () {
		var html =juicer($("#submitForm").html(),{Name:"",Order:0,Id:""});
		editInDialog("新增栏目", "/Manage/CreateColumn",html,onFormInit,"columnChangedSubscriber");
	});

	function validate() {
	$("#form").bootstrapValidator({
			message: '栏目验证未通过',
			fields: {
				name: {
					validators: {
						notEmpty: {
							message: '栏目名称不能为空'
						}
					}
				},
				order: {
					validators: {
						notEmpty: {
							message: '排序不能为空'
						},
						digits: {
							message: '排序必须为数字'
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
	