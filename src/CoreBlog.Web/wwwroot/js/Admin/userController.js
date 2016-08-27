(function () {
    'use strict';

    angular
        .module('adminApp')
        .controller('userController', ['$http', 'controlPanelFactory', userController]);

    function userController($http, controlPanelFactory) {

        var vm = this;

        vm.users = [];

        vm.error = "";

        vm.isBusy = true;

        controlPanelFactory
            .getUsers().success(function (response) {
                angular.copy(response, vm.users)
                vm.isBusy = false;
            }).error(function (error) {
                vm.error = "Failed to get users";
            });
        
    
        vm.unban = function (user) {
     
            vm.isBusy = true;
 
            controlPanelFactory
                .unbanUser(user).success(function(response){
                    user.isBanned = response;
                }).error(function (error) {
                    vm.error = "Failed to unban the user: " + user.userName;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };


        vm.ban = function (user) {

            vm.isBusy = true;
  
            controlPanelFactory
                .banUser(user).success(function(response){
                    user.isBanned = response;
                }).error(function (error) {
                    vm.error = "Failed to ban the user" + user.userName;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };
    };
})();
