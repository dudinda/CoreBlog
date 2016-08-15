(function () {
 
    angular.module('blogApp')
        .controller('userController', ['$scope', '$http', 'controlPanelFactory', userController]);

    function userController($scope, $http, controlPanelFactory) {

        uservm = this;
        $scope.users = [];
        uservm.error = "";
        uservm.isBusy = true;

        controlPanelFactory
            .getUsers().success(function (response) {
                angular.copy(response, $scope.users)
                uservm.isBusy = false;
            }).error(function (error) {
                uservm.error = "Failed to get users";
            });

    
        $scope.unban = function (user) {

            uservm.isBusy = true;
            user.isBanned = false;
            controlPanelFactory
                .unbanUser(user).error(function (error) {
                    uservm.error = "Failed to unban the user";
                    user.isBanned = true;
                }).finally(function () {
                    uservm.isBusy = false;
                });
        };


        $scope.ban = function (user) {

            uservm.isBusy = true;
            user.isBanned = true;
            controlPanelFactory
                .banUser(user).error(function (error) {
                    uservm.error = "Failed to ban the user";
                    user.isBanned = false;
                }).finally(function () {
                    uservm.isBusy = false;
                });
        };
    };
})();
