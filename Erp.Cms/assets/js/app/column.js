   var column = Backbone.Model.extend({
	   default: {
		   Name:"",
		   Order:0
		   }
   });
   var columns = new Backbone.Collection({ model: column });
	  
	$("#newColumn").on('click', function () {
		editInDialog("新增栏目", "/Manage/CreateColumn",$("#submitForm").html(),validate);
	});

   $(".edit").each(function(e) {
	$(this).on('click', function () {
		editInDialog("编辑栏目", "/Manage/CreateColumn",$("#submitForm").html(),validate);
	});
   });

	function validate() {
	$("#form").bootstrapValidator({
			message: '栏目验证未通过',
			feedbackIcons: {
				valid: 'glyphicon glyphicon-ok',
				invalid: 'glyphicon glyphicon-remove',
				validating: 'glyphicon glyphicon-refresh'
			},
			fields: {
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

	