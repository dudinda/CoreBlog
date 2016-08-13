(function () {
 
    blogApp.controller('userController', function ($scope, $http) {

        uservm = this;
        $scope.users = [];
        uservm.error = "";
        uservm.isBusy = true;


        $http.get("/api/admin/users").then(function (response) {
            angular.copy(response.data, $scope.users);
        }, function (error) {
            uservm.error = "Failed to load data" + error;
        }).finally(function () {
            uservm.isBusy = false;
        });

        $scope.unban = function (user) {
            $http.post("/api/admin/unban", user).then(function (response) {
                $scope.users.replace(user, response.data);
            }, function (error) {
                uservm.error = "Failed to unban user" + error;
            }).finally(function () {
                user.isBanned = false;
            });
        };

        $scope.ban = function (user) {
            $http.post("/api/admin/ban", user).then(function (response) {
                $scope.users.replace(user, response.data);
            }, function (error) {
                uservm.error = "Failed to ban user" + error;
            }).finally(function () {
                user.isBanned = true;
            });
        };
    });
})();
