var units=[],specifications=[];

	function bindUnits(index) {
		$.get("/Info/GetInfoList?category=0&pageIndex="+index+"&pageSize=10", function(e) {
			units = e.Datas;
			e = $.extend(true, e, { colspan: 3 ,pageChangeAction:"bindUnits" });
			var html =juicer($("#table").html(), { data: e });
			$('#unitGrid').html(html);
		});
	}

	function editInfo(id) {
		var column = _.find(units, function(r) {
			return r.Id === id;
		});
		var html =juicer($("#submitForm").html(),column);
		editInDialog("编辑单位", "/Info/UpdateInfo",html,onFormInit,"infoChangedSubscriber");
	}

	function deleteInfo(id,name) {
		delInDialog(name, "/Info/DeleteInfo",id,"infoChangedSubscriber");
	}

	function init() {
			bindUnits(1);
			erp.subscriber.addSubscriber("infoChangedSubscriber", function(d) {
			bindUnits(1);
			});
	}

	$("#newUnit").on('click', function () {
		var html =juicer($("#submitForm").html(),{Name:"",Id:"",Category:0});
		console.log(html,1);
		editInDialog("新增单位", "/Info/CreateInfo",html,onFormInit,"infoChangedSubscriber");
	});

	function validate(name) {
	$("#form").bootstrapValidator({
			message: name+'验证未通过',
			fields: {
				text: {
					validators: {
						notEmpty: {
							message: name+'名称不能为空'
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
	