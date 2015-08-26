var appManage = angular.module('mainApp', ['ngMessages','ngRoute']);
appManage.config([
		"$routeProvider", function($routeProvider) {

		$routeProvider.when("/Index", {
				controller: "manageController",
				templateUrl: "/Manage/Index"
			})
			.when("/Column", {
				controller: "manageController",
				templateUrl : "/Manage/ColumnIndex",
				reloadOnSearch: false
			})
			.when("/Catalog", {
				controller: "manageController",
				templateUrl: "/Manage/CatalogsIndex"
			})
			.when("/Article", {
				controller: "manageController",
				templateUrl: "/Manage/ArticleIndex"
			});
	}
	]);