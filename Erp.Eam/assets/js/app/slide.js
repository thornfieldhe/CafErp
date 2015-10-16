function upload() {
		try {
			  $(".dropzone").dropzone({
			    paramName: "file", // The name that will be used to transfer the file
			    maxFilesize: 0.8, // MB
				uploadMultiple:true,
				acceptedFiles:".png,.jpg,.jpeg,.gif",
				dictInvalidInputType:"文件格式仅支持png,jpg,jpeg,gif",
				dictFileTooBig:"文件大小不超过500KB",
				dictDefaultMessage :
				'<span class="bigger-150 bolder"><i class="icon-caret-right red"></i> 拖曳文件</span>上传 \
				<span class="smaller-80 grey">(或者 点击)</span> <br /> \
				<i class="upload-icon icon-cloud-upload blue icon-3x"></i>',
				dictResponseError: '上传失败！',
				//change the previewTemplate to use Bootstrap progress bars
				previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n  <div class=\"dz-details\">\n    <div class=\"dz-filename\"><span data-dz-name></span></div>\n    <div class=\"dz-size\" data-dz-size></div>\n    <img data-dz-thumbnail />\n  </div>\n  <div class=\"progress progress-small progress-striped active\"><div class=\"progress-bar progress-bar-success\" data-dz-uploadprogress></div></div>\n  <div class=\"dz-success-mark\"><span></span></div>\n  <div class=\"dz-error-mark\"><span></span></div>\n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n</div>",
			 success:function(e) {
				 console.log(e);
				 bindSlides(1);
			 },error:function(e) {
				    console.log(e,"111");
			    }
			  });
			} catch(e) {
			  alert('你的浏览器不支持上传插件!');
			}
}

function bindSlides(index) {
			$.get("/Manage/GetSlideList?pageIndex="+index+"&pageSize=20", function(e) {
			e = $.extend(true, e, { colspan: 4,pageChangeAction:"bindSlides"});
			var html =juicer($("#table").html(), { data: e });
			$('#slideGrid').html(html);
			$('.spinbox').spinbox();
				bindRateSelector();
			});
}


	function deleteSlide(id,name) {
		delInDialog(name, "/Manage/DeleteSlide",id,"columnChangedSubscriber");
	}

	function init() {
	upload();
	bindSlides(1);
	erp.subscriber.addSubscriber("columnChangedSubscriber", function(d) {
	bindSlides(1);
	});
	}

	function bindRateSelector() {
		$(".editRow").off('click').on('click',function(e) {
			var _this = $(this);
			_this.find(".disp").hide();
			_this.find(".edit").show();
			_this.find("input").focus();
		});
		$("[name=rate]").off("blur").on("blur", function(e) {
			var _this = $(this);
			var topParent = _this.parent().parent().parent();
			topParent.find(".edit").hide();
			topParent.find(".disp").html(_this.val()).show();
			$.post("/Manage/UpdateRate",{id:_this.prop("id"),rate:_this.val()});
		});
	}

	function updateRate(id, rate) {
		
	}
init();

