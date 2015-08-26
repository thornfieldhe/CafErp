appManage.controller('manageController',
         function ($scope, manageService) {
             $scope.newColumn = function () {
                 console.log("xxx");
                 $scope.Column = manageService.newColumn();
             }
         });