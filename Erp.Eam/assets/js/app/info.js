var loadSpecification, loadColor, loadBrand=false;
	function bindInfos(index, category, callback, element, changeAction) {
		$.get("/Info/GetInfoList?category=" + category + "&pageIndex=" + index + "&pageSize=10", function(e) {
			e = $.extend(true, e, { colspan: 3, pageChangeAction: changeAction, tabName: "单位", callback: callback });
			var html = juicer($("#table").html(), { data: e });
			$(element).html(html);
		});
	}

	function bindUnits(index) {
		bindInfos(index, 0, "unitChangedSubscriber", "#unitGrid", "bindUnits");
	}

	function bindSpecifications(index) {
		bindInfos(index, 1, "specificationChangedSubscriber", "#specificationGrid", "bindSpecifications");
	}

	function bindColors(index) {
		bindInfos(index, 2, "colorChangedSubscriber", "#colorGrid", "bindColors");
	}

	function bindBrands(index) {
		bindInfos(index, 3, "brandChangedSubscriber", "#brandGrid", "bindBrands");
	}

	function editInfo(id, name, title, category, callback) {
		var html = juicer($("#submitForm").html(), { Id: id, Name: name, Category: category });
		editInDialog("编辑" + title, "/Info/UpdateInfo", html, onFormInit, callback);
	}

	function deleteInfo(id, name, callback) {
		delInDialog(name, "/Info/DeleteInfo", id, callback);
	}

	function init() {
		bindUnits(1);
		erp.subscriber.addSubscriber("unitChangedSubscriber", function(d) {
			bindUnits(1);
		});
		erp.subscriber.addSubscriber("specificationChangedSubscriber", function(d) {
			bindSpecifications(1);
		});
		erp.subscriber.addSubscriber("colorChangedSubscriber", function(d) {
			bindColors(1);
		});
		erp.subscriber.addSubscriber("brandChangedSubscriber", function(d) {
			bindBrands(1);
		});
	}

	function creatInfo(title, category, callback) {
		var html = juicer($("#submitForm").html(), { Name: "", Id: "", Category: category });
		editInDialog("新增" + title, "/Info/CreateInfo", html, onFormInit, callback);
	}

	$("#newUnit").on('click', function() {
		creatInfo("单位", 0, "unitChangedSubscriber");
	});
	$("#newSpecification").on('click', function() {
		creatInfo("规格", 1, "specificationChangedSubscriber");
	});
	$("#newColor").on('click', function() {
		creatInfo("颜色", 2, "colorChangedSubscriber");
	});
	$("#newBrand").on('click', function() {
		creatInfo("品牌", 3, "brandChangedSubscriber");
	});

	$('#myTab a').click(function(e) {
			console.log($(this),e);
		switch ($(this).attr("href")) {
					case "#specification":
					if(!loadSpecification)
						{
							bindSpecifications(1);
							loadSpecification = true;
						}
					break;
					case "#color":
						if (!loadColor) {
							bindColors(1);
							loadColor = true;
						}
						break;
					case "#brand":
						if (!loadBrand) {
							bindBrands(1);
							loadBrand = true;
						}
						break;
		}
	});

	function validate(name) {
		$("#form").bootstrapValidator({
			message: name + '验证未通过',
			fields: {
				text: {
					validators: {
						notEmpty: {
							message: name + '名称不能为空'
						}
					}
				}
			}
		});
	}

	function onFormInit() {
		validate();
		$('.spinbox').spinbox();
	}

	init();
	