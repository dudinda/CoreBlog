(function () {
 
    blogApp.controller('userController', function ($scope, $http) {

        uservm = this;
        $scope.users = [];
        uservm.error = "";
        uservm.isBusy = true;

        $scope.getUsers = function () {
            console.log('request');
            $http.get("/api/admin/users").then(function (response) {
                angular.copy(response.data, $scope.users);
            }, function (error) {
                uservm.error = "Failed to load data" + error;
            }).finally(function () {
                uservm.isBusy = false;
            });
        };

    })
})();
