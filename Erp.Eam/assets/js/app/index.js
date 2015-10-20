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
					loadPage("/Home/Dashboard",null, "主页","#menuHome",true);
		break;
		case 1:
					loadPage("/Manage/ColumnIndex", "基本信息", "商品类别","#menuColumn",false);
		break;
		case 2:
					loadPage("/Manage/CatalogsIndex", "基本信息", "商品资料","#menuCatalog",false);
		break;
		case 3:
					loadPage("/Manage/ArticlesIndex", "基本信息", "仓库设置","#menuArticle",false);
		break;
		case 4:
					loadPage("/Home/ChangePasswordIndex", "用户管理", "员工设置","#menuChangePass",false);
		break;		
		case 5:
					loadPage("/Home/SlideIndex", "基本信息", "仓库设置","#menuSlide",false);
		break;
		case 6:
					loadPage("/Home/UserIndex", "用户管理", "用户管理","#menuUsers",false);
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