
	function bindItems(index) {
		$.get("/Info/GetInfoList?pageIndex=" + index + "&pageSize=10", function(e) {
			e = $.extend(true, e, { colspan: 3, pageChangeAction:  "bindItems" });
			var html = juicer($("#table").html(), { data: e });
			$(element).html(html);
		});
	}

	function editItem(id) {
		$.get("/Product/Edit", function(e) {
				editInDialog("编辑商品", "/Product/Update/"+id,e,onFormInit,"itemChangedSubscriber");
		});
	}

	function deleteItem(id, name, callback) {
		delInDialog(name, "/Product/Delete", id, callback);
	}

	function init() {
		bindItems(1);
		erp.subscriber.addSubscriber("itemChangedSubscriber", function(d) {
			bindItems(1);
		});
	}


	$("#newItem").on('click', function() {
			$.get("/Product/Edit", function(e) {
				editInDialog("编辑商品", "/Product/Insert",e,onFormInit,"itemChangedSubscriber");
		});
	});

	function validate() {
		$("#form").bootstrapValidator({
			message: name + '验证未通过',
			fields: {
				name: {
					validators: {
						notEmpty: {
							message: '名称不能为空'
						}
					}
				}
			}
		});
	}

	function onFormInit() {
		validate();
		$('.spinbox').spinbox();
	}

	init();
	