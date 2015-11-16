	var queryEntity={};
	function bindItems(index) {
		console.log(queryEntity);
		$.get("/Product/List?pageIndex=" + index + "&pageSize=10",queryEntity, function(e) {
			e = $.extend(true, e, { colspan: 7, pageChangeAction:  "bindItems" });
			var html = juicer($("#table").html(), { data: e });
			$("#itemGrid").html(html);
			$("select").each(function(e) {
					$(this).select2();
			});
			$("#searchCode").val(queryEntity.code);
			$("#searchName").val(queryEntity.name);
			$("#searchUnit").val(queryEntity.unit).trigger("change");
			$("#searchCategory").val(queryEntity.category).trigger("change");
			$("#searchColor").val(queryEntity.color).trigger("change");
		});
	}

	function filter() {
		queryEntity = {
			code: $("#searchCode").val(),
			name: $("#searchName").val(),
			unit: $("#searchUnit").val(),
			category: $("#searchCategory").val(),
			color: $("#searchColor").val()
		};
		bindItems(1);
	}

	function resetSearch() {
			queryEntity = {};
			$("#searchCode").val("");
			$("#searchName").val("");
			$("#searchCategoryId").select2().val("").trigger("change");
			$("#searchColor").select2().val("").trigger("change");
			$("#searchUnit").select2().val("").trigger("change");
		}

	function validate() {
		$("#form").bootstrapValidator({
			message: '验证未通过',
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
		$('.spinbox').spinbox('value', 1);
		$("select").each(function(e) {
				$(this).select2();
		});
		validate();
	}

	initIndexPage();
	