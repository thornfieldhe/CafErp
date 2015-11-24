	var vue = new Vue({
		el: '#main',
		data: {
		query: {
			storehouse: '',
			code:''
		},
		items:[]
		},
		methods: {
			addStock:function() {
				var $this = this;
				if ($this.query.storehouse === "") {
					Notify("请选择仓库！", 'top-right', '5000', 'danger', 'fa-times', true);
					$("#store").focus();
				} else {
					$.get("/Stock/GetProductInStore", { code: $this.query.code, store: $this.query.storehouse }, function(e) {
						if (e.Status === 0) {
							$.extend(true, e.Data, { EditModel:false });
							var item=_.find($this.items, function (p){return p.Code===e.Data.Code});
							console.log(item);
							if (item!==undefined) {
								item.Amount += 1;
							} else {
								$this.items.push(e.Data);
							}
							$this.query.code = "";
							$("#code").focus();
						} else {
							Notify(e.Message, 'top-right', '5000', 'danger', 'fa-times', true);
						}
					});
				}
			},
			edit(it,index) {
				it.EditModel = true;
				$('#row_'+index+' .spinbox').spinbox();
				$('#row_' + index + ' select').select2();
			},
			update(it,index) {
				it.EditModel = false;
				vue.items[index].Unit = $("#row_"+index+" select").val();
				vue.items[index].Amount = $("#row_"+index+" [type='text']").val();
			},
			delete(it) {
			var $this = this;
				bootbox.confirm({
					message: "确认删除产品["+it.Product+"]么？",
					className: "modal-darkorange",
					callback:function(result) {
						if (result) {
							var news=	_.reject($this.items, function(i) {return i.Id === it.Id; });
							$this.items = news;
						}
					},
					buttons: {
						cancel: {
							label: " <i class='fa   fa-mail-reply'></i> 取消",
							className: "btn-default",
							callback: function () { }
						},
						confirm: {
							label: "<i class='fa   fa-trash-o'></i>删除",
							className: "btn-danger"
						}
					}
				});
			}
		}
	});

	$("#store").select2().on("change", function(e) {vue.query.storehouse=$("#store").val()});
	$("#search").focus();
	