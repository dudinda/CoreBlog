(function () {
    'use strict';

    angular
        .module('app')
        .factory('registrationFactory', ['$http', registrationFactory]);


    function registrationFactory($http) {

        function getRegistrationForm() {
            return $http.get('/api/reg/form');
        }

        function validateNewUser(user) {
            return $http.post('reg/registration', user);
        }

        var service = {
            getRegistrationForm: getRegistrationForm,
            validateNewUser: validateNewUser

        };

        return service;
    }
})();