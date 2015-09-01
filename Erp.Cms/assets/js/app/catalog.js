var tree,treeNodes,selectNode,
isAddAction=false,
setting = {
			data: {
				simpleData: {
					enable: true
				}
			},
			callback: {
				onClick: onClick
				}
		},
setting2 = {
			data: {
				simpleData: {
					enable: true
				}
			},
			callback: {
				onClick: onClickDropdownList
				}
		}

function onClick(event, treeId, treeNode, clickFlag) {
	selectNode = treeNode;
	allowEdit();
}	

function onClickDropdownList(event, treeId, treeNode, clickFlag) {
	$("[name=parentName]").val(treeNode.name);
	$("[name=parentId]").val(treeNode.id);
	$("#menuContent").hide();
}	

function loadTree() {
		$.get("/Manage/GetCatalogList", function(e) {
			treeNodes = e;
			var datas=  _.map(e,function(n) {
				return  { name: n.Name, id:n.Id,pId:n.ParentId,levelCode:n.LevelCode};
			});
		tree=$.fn.zTree.init($("#catalogTree"), setting, datas);
		tree.expandAll(true);
	});
}

function allowEdit() {
	if (selectNode.pId!==null) {
		$("#deleteCatalog").attr("disabled", false);
		$("#editCatalog").attr("disabled", false);
	} else {
		$("#deleteCatalog").attr("disabled", true);
		$("#editCatalog").attr("disabled", true);
	}
}

function showCatalogs() {
		var datas;
	if (!isAddAction) {
		var filterDatas = _.filter(treeNodes, function(n) {
			return !_.startsWith(n.LevelCode, selectNode.levelCode);
		});
		datas = _.map(filterDatas, function(n) {
			return { name: n.Name, id: n.Id, pId: n.ParentId };
		});
	} else {
				datas = _.map(treeNodes, function(n) {
			return { name: n.Name, id: n.Id, pId: n.ParentId };
		});
	}


			var tree2=$.fn.zTree.init($("#treeCatalog2"), setting2, datas);
			tree2.expandAll(true);
			$("#menuContent").slideDown("fast");
			$("body").bind("mousedown", onBodyDown);
}

$("#editCatalog").on("click", function() {
		isAddAction = false;
		var html =juicer($("#submitForm").html(),{Name:selectNode.name,Order:0,Id:selectNode.id,ParentId:selectNode.pId,ParentName:selectNode.getParentNode().name});
		editInDialog("编辑目录", "/Manage/UpdateCatalog",html,onFormInit,"columnChangedSubscriber");
});

$("#newCatalog").on("click", function() {
		isAddAction = true;
		var html =juicer($("#submitForm").html(),{Name:"",Order:0,Id:"",ParentId:treeNodes[0].Id,ParentName:treeNodes[0].Name});
		editInDialog("新增目录", "/Manage/CreateCatalog",html,onFormInit,"columnChangedSubscriber");
		$('#treeCatalog2').slimScroll({
		height: '100px'
		});
});

$("#deleteCatalog").on("click", function() {
		isAddAction = false;
	delInDialog(selectNode.name, "/Manage/DeleteArticle", selectNode.id, "columnChangedSubscriber");
});

function onBodyDown(event) {
	if (!(event.target.id === "menuBtn" || event.target.id === "menuContent" || $(event.target).parents("#menuContent").length>0)) {
			$("#menuContent").hide();
	}
}

function validate() {
	$("#form").bootstrapValidator({
			message: '栏目验证未通过',
			fields: {
				parentName: {
					validators: {
						notEmpty: {
							message: '父目录不能为空'
						}
					}
				},
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
	$('#treeCatalog2').slimScroll({
	height: '100px'
	});
}


function init() {
	loadTree();
	erp.subscriber.addSubscriber("columnChangedSubscriber", function(d) {
		isAddAction = false;
		loadTree();
	});
}

init();

