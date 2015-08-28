	$(document).ready(function() {
		init();
	

		$(".sidebar-menu a").click(function(e) {
				$(".sidebar-menu .active").each(function(r) {
					$(this).removeClass("active");
				});
				$(this).parent().addClass("active");
		});
	});

	function load(type) {
		console.log(type);
		switch (type) {
		case 0:
			$.get('/Manage/Dashboard',function(data){
				$(".page-body").html(data);
				$(".breadcrumb").html("<li><i class='fa fa-home'></i><a href='javascript:void(0)' onclick='load(0)'>主页</a></li>");
				$(".sidebar-menu .active").each(function(r) {
					$(this).removeClass("active");
				});
				$("#menuHome").addClass("active");
				$("#bodyTitle").html("主页");
			});
		break;
		case 1:
		    $.get('/Manage/ColumnIndex',function(data){
				$(".page-body").html(data);
			    $(".breadcrumb").html("<li><i class='fa fa-home'></i><a href='javascript:void(0)' onclick='load(0)'>主页</a></li><li >文章管理</li><li >栏目管理</li>");
				$(".sidebar-menu .active").each(function(r) {
					$(this).removeClass("active");
				});
				$("#menuColumn").addClass("active");
				$("#bodyTitle").html("栏目管理");
		    });
		break;
		default:
		}
	}

	function init() {
		load(0);
	}