appManage.factory('manageService', [ function () {
	return {
		getColumns: function () { return [{Name:"规章制度",Order:0}]; },
		newColumn: function () {
			return {Name:"规章制度",Order:0}
		}
	}
}]);