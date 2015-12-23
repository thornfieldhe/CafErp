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
					loadPage("/Home/UserIndex", "用户管理", "用户管理","#menuUsers",false);
		break;
		case 2:
					loadPage("/Home/ChangePasswordIndex", "用户管理", "员工设置","#menuChangePass",false);
		break;	
		case 3:
		    loadPage("/Product/Index", "基本信息", "商品类别", "#menuProductCategory", false);
		break;
		case 4:
					loadPage("/Product/Index2", "基本信息", "商品资料","#menuProduct",false);
		break;
		case 5:
					loadPage("/Info/Index", "基本信息", "综合信息设置","#menuUsers",false);
		break;
		case 6:
					loadPage("/StockIn/Index", "库存管理", "入库单","#menuArticle",false);
		break;
		case 7:
					loadPage("/StockOut/Index", "库存管理", "出库单","#menuChangePass",false);
		break;	
		case 8:
					loadPage("/Home/SlideIndex", "库存管理", "盘点单","#menuSlide",false);
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
				$("#bodyTitle").html(parentTitle);
		    });
	}

	function init() {
		load(0);
	}