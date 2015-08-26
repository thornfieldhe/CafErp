(function () {
    angular.module('inspinia', [
        'ui.router',                    // Routing
        'oc.lazyLoad',                  // ocLazyLoad
        'ui.bootstrap',                 // Ui Bootstrap
        'pascalprecht.translate',       // Angular Translate
        'ngIdle'                        // Idle timer
    ])
})();


var appManage = angular.module('mainApp', ['ngMessages','ngRoute']);
appManage.config([
		'$locationProvider', '$routeProvider', function($routeProvider) {
			//            $locationProvider.html5Mode(false).hashPrefix('!');
		//$locationProvider.html5Mode(false).hashPrefix('!');
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