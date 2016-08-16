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
 
            controlPanelFactory
                .unbanUser(user).success(function(response){
                    user.isBanned = response;
                }).error(function (error) {
                    uservm.error = "Failed to unban the user";
                }).finally(function () {
                    uservm.isBusy = false;
                });
        };


        $scope.ban = function (user) {

            uservm.isBusy = true;
  
            controlPanelFactory
                .banUser(user).success(function(response){
                    user.isBanned = response;
                }).error(function (error) {
                    uservm.error = "Failed to ban the user";
                }).finally(function () {
                    uservm.isBusy = false;
                });
        };
    };
})();
