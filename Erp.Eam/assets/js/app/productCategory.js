
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

function onClick(event, treeId, treeNode) {
	selectNode = treeNode;
	allowEdit();
}	

function onClickDropdownList(event, treeId, treeNode) {
	$("[name=parentName]").val(treeNode.name);
	$("[name=parentId]").val(treeNode.id);
	$("#menuContent").hide();
}	

function loadTree() {
		$.get("/ProductCategory/GetAll", function(e) {
			treeNodes = e.Data;
			var datas=  _.map(e.Data,function(n) {
				return  { name: n.Name, id:n.Id,pId:n.ParentId,levelCode:n.LevelCode};
			});
		tree=$.fn.zTree.init($("#CategoryTree"), setting, datas);
		tree.expandAll(true);
	});
}

function allowEdit() {
	console.log(selectNode);
	if (selectNode!==null) {
		$("#deleteCategory").attr("disabled", false);
		$("#editCategory").attr("disabled", false);
	} else {
		$("#deleteCategory").attr("disabled", true);
		$("#editCategory").attr("disabled", true);
	}
}

function showCategories() {
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


			var tree2=$.fn.zTree.init($("#treeCategory2"), setting2, datas);
			tree2.expandAll(true);
			$("#menuContent").slideDown("fast");
			$("body").bind("mousedown", onBodyDown);
}

 function editCategory() {
		isAddAction = false;
		var html =juicer($("#submitForm").html(),{Name:selectNode.name,Id:selectNode.id,ParentId:selectNode.pId,ParentName:selectNode.getParentNode()===null?"":selectNode.getParentNode().name});
		editInDialog("编辑目录", "/ProductCategory/Update",html,onFormInit,"columnChangedSubscriber");
}

 function newCategory() {
		isAddAction = true;
		var levelOneNodes=_.filter(treeNodes,function(r){return r.LevelCode.length===2});
		var minNode =_.min(levelOneNodes,function(r){return r.Order});
		var html =juicer($("#submitForm").html(),{Name:"",Id:"",ParentId:minNode.Id,ParentName:minNode.Name});
		editInDialog("新增产品分类", "/ProductCategory/Insert",html,onFormInit,"columnChangedSubscriber");
		$('#treeCategory2').slimScroll({
		height: '100px'
		});
}

function deleteCategory() {
		isAddAction = false;
	delInDialog(selectNode.name, "/ProductCategory/Delete", selectNode.id, "columnChangedSubscriber");
}

function onBodyDown(event) {
	if (!(event.target.id === "menuBtn" || event.target.id === "menuContent" || $(event.target).parents("#menuContent").length>0)) {
			$("#menuContent").hide();
	}
}

function validate() {
	$("#form").bootstrapValidator({
			message: '栏目验证未通过',
			fields: {
				name: {
					validators: {
						notEmpty: {
							message: '商品分类名称不能为空'
						}
					}
				}
			}});
	}
	
function onFormInit() {
	validate();
	$('.spinbox').spinbox();
	$('#treeCategory2').slimScroll({
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

