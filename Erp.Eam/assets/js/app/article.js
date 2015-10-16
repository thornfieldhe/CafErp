var tree,selectNode,
articles=[],
setting = {
			data: {
				simpleData: {
					enable: true
				}
			},
			callback: {
				onClick: onClick
				}
		}

function onClick(event, treeId, treeNode, clickFlag) {
	selectNode = treeNode;
	$("#newArticle").attr("disabled", false);
	bindArticles(1);
}	

function loadTree() {
		$.get("/Manage/GetCatalogList", function(e) {
			var datas=  _.map(e,function(n) {
				return  { name: n.Name, id:n.Id,pId:n.ParentId,levelCode:n.LevelCode};
			});
		tree=$.fn.zTree.init($("#catalogTree"), setting, datas);
		tree.expandAll(true);
	});
}

function editArticle(id) {
	$.get("/Manage/ArticleEdit?id=" + id, function(e) {
		editInDialog("编辑文章", "/Manage/UpdateArtile",e,onFormInit,"columnChangedSubscriber");
	});
}


$("#newArticle").on("click", function() {
	$.get("/Manage/ArticleEdit?parentId="+selectNode.id, function(e) {
		editInDialog("新增文章", "/Manage/CreateArticle",e,onFormInit,"columnChangedSubscriber");
	});
});

function deleteArticle(id,name) {
	delInDialog(name, "/Manage/DeleteArticle", id, "columnChangedSubscriber");
}

	function bindArticles(index) {
		$.get("/Manage/GetArticleList?catalogId="+selectNode.id+"&pageIndex="+index+"&pageSize=20", function(e) {
			articles = e.Datas;
			e = $.extend(true, e, { colspan: 3,pageChangeAction:"bindArticles" });
			var html =juicer($("#table").html(), { data: e });
			$('#articleGrid').html(html);
		});
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
				},
				content: {
					validators: {
						notEmpty: {
							message: '内容不能为空'
						}
					}
				}
			}});
	}
	
function onFormInit() {
	$('.spinbox').spinbox();
	$(".modal-dialog").addClass("modal-lg");
}


function init() {
	loadTree();
	erp.subscriber.addSubscriber("columnChangedSubscriber", function(d) {
		bindArticles(1);
	});
}

init();

