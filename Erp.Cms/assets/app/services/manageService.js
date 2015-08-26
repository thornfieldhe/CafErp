appManage.factory('manageService', ['Restangular', function (Restangular) {
    var restAngular = Restangular.withConfig(function (Configurer) {
        Configurer.setBaseUrl('/Api');
    });
    var _columnService = restAngular.all("Column");
    return {
        getColumns: function () { return [{Name:"规章制度",Order:0}]; },
        newColumn: function () {
            return {Name:"规章制度",Order:0}
        }
    }
}]);