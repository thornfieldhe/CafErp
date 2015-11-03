var setting = {
	data: {
		simpleData: {
			enable: true
		}
	},
	callback: {
		onClick: onClickTreeDropdownList
	}
};
	function bindItems(index) {
		$.get("/Product/GetViews?pageIndex=" + index + "&pageSize=10", function(e) {
			e = $.extend(true, e, { colspan: 6, pageChangeAction:  "bindItems" });
			var html = juicer($("#table").html(), { data: e });
			$("#itemGrid").html(html);
			$("select[id^='e']").each(function(e) {
			$(this).select2();
			});

		});
	}

	function onClickTreeDropdownList(event, treeId, treeNode) {
	$("[name=searchProductCategory]").val(treeNode.name);
	$("[name=searchProductCategoryId]").val(treeNode.id);
	$(".dropdown-container").remove();
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

	function showCategories() {
			$.get("/ProductCategory/GetAll", function(e) {
			var datas=  _.map(e.Data,function(n) {
				return  { name: n.Name, id:n.Id,pId:n.ParentId,levelCode:n.LevelCode};
			});
			$("#searchCategoryTree").after('<div class="dropdown-container"><div class="dropdown-preview" style="width: 100%"><div id="menuContent" class="dropdown-menu" style="display: none; width: 100%;"><ul id="CategoryTree" class="ztree"></ul></div></div></div>');

			var tree=$.fn.zTree.init($("#CategoryTree"), setting, datas);
			tree.expandAll(true);
			$("#menuContent").slideDown("fast");
			$("body").bind("mousedown", onBodyDown);
		});
}

function onBodyDown(event) {
	if (!(event.target.id === "menuBtn" || event.target.id === "menuContent" || $(event.target).parents("#menuContent").length>0)) {
			$(".dropdown-container").remove();
	}
}

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
	