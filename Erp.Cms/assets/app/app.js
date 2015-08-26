var appManage = angular.module('RouteApp', ['ngMessages','ngRoute', 'restangular']);
appManage.config([
		'$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
			//            $locationProvider.html5Mode(false).hashPrefix('!');
		$locationProvider.html5Mode(true);
		$routeProvider.when("/Index", {
				controller: "manageController",
				templateUrl: "/Manage/Index"
			})
			.when("/Column", {
				controller: "manageController",
				templateUrl: "/Manage/ColumnIndex",
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