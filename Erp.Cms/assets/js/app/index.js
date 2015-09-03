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
		switch (type) {
		case 0:
					loadPage("/Manage/Dashboard",null, "主页","#menuHome",true);
		break;
		case 1:
					loadPage("/Manage/ColumnIndex", "文章管理", "栏目管理","#menuColumn",false);
		break;
		case 2:
					loadPage("/Manage/CatalogsIndex", "文章管理", "目录管理","#menuCatalog",false);
		break;
		case 3:
					loadPage("/Manage/ArticlesIndex", "文章管理", "文章管理","#menuArticle",false);
		break;
		case 4:
					loadPage("/Manage/ChangePasswordIndex", "文章管理", "修改密码","#menuChangePass",false);
		break;		
		case 5:
					loadPage("/Manage/SlideIndex", "轮播图设置", "轮播图设置","#menuSlide",false);
		break;
		default:
		}
	}

	function loadPage(url,parentTitle,title,document,isHomePage) {
			$.get(url,function(data){
				$(".page-body").html(data);
				if (!isHomePage) {
					$(".breadcrumb").html("<li><i class='fa fa-home'></i><a href='javascript:void(0)' onclick='load(0)'>主页</a></li><li >" + parentTitle + "</li><li >" + title + "</li>");
				} else {
			    $(".breadcrumb").html("<li><i class='fa fa-home'></i><a href='javascript:void(0)' onclick='load(0)'>主页</a></li>");
				}
				$(".sidebar-menu .active").each(function(r) {
					$(this).removeClass("active");
				});
				$(document).addClass("active");
				$("#bodyTitle").html(title);
		    });
	}

	function init() {
		load(0);
	}