(function () {
    'use strict';

    angular
        .module('blogApp')
        .controller('registrationController', ['$scope', 'registrationFactory', registrationController]);

    function registrationController($scope, registrationFactory) {
        var vm = this;


        vm.error = "";
        vm.registrationFrom = {};

        registrationFactory
            .getRegistrationForm().success(function (response) {
                angular.copy(response, vm.getRegistrationForm);
            }).error(function () {
                vm.error = "Oops. Something went wrong. Try again later!";
            });
        
        vm.approve = function () {
            registrationFactory
                .validateNewUser(registrationForm).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                });
        };
    }
})();
