(function () {
    'use strict';

    angular
        .module('blogApp')
        .factory('postCreateFactory', ['$http', postCreateFactory]);


    function postCreateFactory($http) {

        function getPostCreateForm() {
            return $http.get('/api/post/create');
        }

        function sendNewPost(post) {
            return $http.post('/api/post/submit', post);
        }

        var service = {
            getPostCreateForm: getPostCreateForm,
            sendNewPost: sendNewPost

        };

        return service;
    }
})();