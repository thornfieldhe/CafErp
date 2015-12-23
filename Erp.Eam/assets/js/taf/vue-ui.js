    //新增按钮
    Vue.component('add-button', {
        props: ['title', 'model'],
        template: '#addButton',
        methods:{
            newItem: function (model, title) {

                taf.model.edit(title, model, $("#submitForm").html(), true, onFormInit);
            }
        }
    });

//行搜索按钮
Vue.component('row-search', {
    props: ['index', 'filter', 'model'],
    template: '#searchButton',
    methods: {
        search: function (index, filter, model) {
            var $this = this;
             taf.model.get(index, filter, model, function (result) { $this.$dispatch('bindItems', result); });
        }
    }
});

//行命令按钮
Vue.component('row-command', {
    props: ['id', 'model', 'title','name'],
    template: '#scommanButton',
    methods: {
        editItem: function (id, title, model) {
  
        },
        deleteItem: function (id, model,name) {
            taf.delete(id, model, name, function() {
                taf.model.query(1, null, model, function (result) { $this.$dispatch('bindItems', result); });
            });
        }
    }
});
