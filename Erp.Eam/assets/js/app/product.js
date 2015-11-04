﻿	function bindItems(index) {
		$.get("/Product/GetViews?pageIndex=" + index + "&pageSize=10", function(e) {
			e = $.extend(true, e, { colspan: 6, pageChangeAction:  "bindItems" });
			var html = juicer($("#table").html(), { data: e });
			$("#itemGrid").html(html);
			$("select[id^='e']").each(function(e) {
					$(this).select2();
			});
		});
	}

	function editItem(id) {
		$.get("/Product/Edit", function(e) {
				editInDialog("编辑商品", "/Product/Update/"+id,e,onFormInit,"itemChangedSubscriber");
		});
	}

	function deleteItem(id, name) {
		delInDialog(name, "/Product/Delete", id, "itemChangedSubscriber");
	}

	function init() {
		bindItems(1);
		erp.subscriber.unSubscriber("itemChangedSubscriber");
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
				code: {
					validators: {
						notEmpty: {
							message: '商品编号不能为空'
						}
					}
				},
				name: {
					validators: {
						notEmpty: {
							message: '商品名称不能为空'
						}
					}
				},
				unit: {
					validators: {
						notEmpty: {
							message: '单位不能为空'
						}
					}
				},
				unit2: {
					validators: {
						notEmpty: {
							message: '辅助单位不能为空'
						}
					}
				},
				categoryId: {
					validators: {
						notEmpty: {
							message: '商品类别不能为空'
						}
					}
				},				
				colorId: {
					validators: {
						notEmpty: {
							message: '商品颜色不能为空'
						}
					}
				},				
				conversion: {
					validators: {
						notEmpty: {
							message: '单位换算不能为空'
						}
					}
				},
			}
		});
	}

	function onFormInit() {
		validate();
		$('.spinbox').spinbox();
	}
	init();
	