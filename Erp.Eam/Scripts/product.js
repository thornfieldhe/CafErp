	var queryEntity={};

	
	function bindItems(index) {
		$.get("/Product/List?pageIndex=" + index + "&pageSize=10",queryEntity, function(e) {
		    e.Data = $.extend(true, e.Data, { colspan: 7, pageChangeAction: "bindItems" });
			var html = juicer($("#table").html(), { data: e.Data });
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

	initIndexPage();
	