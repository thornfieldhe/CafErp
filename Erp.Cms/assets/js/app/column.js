   var column = Backbone.Model.extend({
	   default: {
		   Name:"",
		   Order:0
		   }
   });
   var columns = new Backbone.Collection({ model: column });
   
	  
	$("#newColumn").on('click', function () {
		showDialog("新增栏目", "/Manage/CreateColumn");
	});

   $(".edit").each(function(e) {
	$(this).on('click', function () {
		showDialog("编辑栏目", "/Manage/CreateColumn");
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

	function showDialog(title,url,data) {
		console.log(data);
		bootbox.dialog({
			message: '<form id="form" name="form">' +
				'<div class="row"><div class="col-md-12"><div class="form-group">' +
				'<input type="text" class="form-control" placeholder="名称" name="name"/>' +
				'</div></div></div><div class="row"><div class="col-md-12">' +
				'<div class="form-group"><input type="text" class="form-control " placeholder="排序" required name="order"/>' +
				'</div></div></div><div class="row" id="unknownError" style="display: none;" ><div class="col-md-12"><div class="form-group  has-feedback has-error">' +
				'<small class="help-block" style="display: block;" ></small></div></div></div></form>',
			title: title,
			className: "modal-darkorange",
			buttons: {
				cancel: {
					label: "取消",
					className: "btn-default",
					callback: function () { }
				},
				success: {
				label: "保存",
				className: "btn-danger",
				callback: function() {
					var isValidate = $(form).data('bootstrapValidator').isValid();
					if (isValidate) {
						var result=true;
						$.ajax({
							type: "post",
							url: url,
							data: { Name: $("form [name='name']").val(), Order: $("form [name='order']").val() },
							async: false,
							success: function(e) {
							if (e.Status === 1) {
										$("#unknownError").show().find(".help-block").html(e.Message);
										result= false;
									} 
							}
						});
						return result;
						}else {
						$(form).data('bootstrapValidator').validate();
							return false;
						}
					}
				}
			}
		});
		validate();
	}