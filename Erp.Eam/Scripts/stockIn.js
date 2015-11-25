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
			add:function() {
				var $this = this;
				if ($this.query.storehouse === "") {
					Notify("请选择仓库！", 'top-right', '5000', 'danger', 'fa-times', true);
					$("#store").focus();
				} else {
					$.get("/Stock/GetProductInStore", { code: $this.query.code, store: $this.query.storehouse }, function(e) {
						if (e.Status === 0) {
							$.extend(true, e.Data, { EditModel:false });
							var item=_.find($this.items, function (p){return p.Code===e.Data.Code});
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
			cancel(it,index) {
				it.EditModel = false;
				$("#row_"+index+" select").val(vue.items[index].Unit).trigger("change");
				$("#row_"+index+" [type='text']").val(vue.items[index].Amount);
			},
			delete(it) {
				var $this = this;
				delInDialogBase(it.Product, function(result) {
					var news = _.reject($this.items, function(i) { return i.Id === it.Id; });
					$this.items = news;
				});
			},
			submit() {
				$.post("/StockIn/BatchAdding", { list: this.items }, function(e) {
					if (e.Status === 1) {
						Notify(e.Message, 'top-right', '5000', 'success', 'fa-times', true);
					} else {
						Notify("入库单新增成功！", 'top-right', '5000', 'danger', 'fa-times', true);
						this.items=[];
					}
				});
			}
		}
	});

	$("#store").select2().on("change", function(e) {vue.query.storehouse=$("#store").val()});
	$("#search").focus();
	