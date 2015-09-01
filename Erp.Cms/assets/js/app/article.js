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

function editArticle(id){
			var article = _.find(articles, function(r) {
			return r.Id === id;
		});
		var html =juicer($("#submitForm").html(),article);
		editInDialog("编辑文章", "/Manage/UpdateArtile",html,onFormInit,"columnChangedSubscriber");
}


$("#newArticle").on("click", function() {
		var html =juicer($("#submitForm").html(),{Name:"",Order:0,Id:"",ParentId:selectNode.id});
		editInDialog("新增文章", "/Manage/CreateArticle",html,onFormInit,"columnChangedSubscriber");
});

function deleteArticle(id,name) {
	delInDialog(name, "/Manage/DeleteArticle", id, "columnChangedSubscriber");
}

	function bindArticles(index) {
		$.get("/Manage/GetArticleList?catalogId="+selectNode.id+"&pageIndex="+index+"&pageSize=20", function(e) {
			articles = e.Datas;
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
	validate();
	$('.spinbox').spinbox();
}


function init() {
	loadTree();
	erp.subscriber.addSubscriber("columnChangedSubscriber", function(d) {
		bindArticles(1);
	});
}

init();

