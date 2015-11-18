	var queryEntity={};

	function onFormInit() {
		$('.spinbox').spinbox('value', 1);
		$("#unit").select2();
		$("#unit2").select2();
		$("#categoryId").select2();
		$("#color").select2();
		$("#brand").select2();
		$("#specification").select2();
		validate();
	}

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
			$("#searchSpecification").val(queryEntity.specification).trigger("change");
		});
	}

	function filter() {
		queryEntity = {
			code: $("#searchCode").val(),
			name: $("#searchName").val(),
			unit: $("#searchUnit").val(),
			category: $("#searchCategory").val(),
			color: $("#searchColor").val(),
			specification: $("#searchSpecification").val()
		};
		bindItems(1);
	}

	function resetSearch() {
			queryEntity = {};
			$("#searchCode").val("");
			$("#searchName").val("");
			$("#searchCategory").select2().val("").trigger("change");
			$("#searchColor").select2().val("").trigger("change");
			$("#searchUnit").select2().val("").trigger("change");
			$("#searchSpecification").select2().val("").trigger("change");
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
						},
						numeric: {
							message: '单位换算必须是数字'
						}, 
							greaterThan: {
								value : 0,
								inclusive: false,
								message: '单位换算必须大于0'
						}
					}
				}
			}
		});
	}



	initIndexPage();
	