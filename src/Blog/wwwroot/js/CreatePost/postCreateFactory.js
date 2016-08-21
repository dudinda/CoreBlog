(function () {
    'use strict';

    angular
        .module('postCreateApp')
        .factory('postCreateFactory', ['$http', postCreateFactory]);


    function postCreateFactory($http) {

        function getPostForm() {
            return $http.get('/api/post/create');
        }

        function sendNewPost(post) {
            return $http.post('/api/post/submit', post);
        }


        function getPost(id) {
            return $http.get("/api/admin/post/" + id);
        }
        
        function updatePost(post) {
            return $http.post("/api/post/update", post);
        }

        var service = {
            getPostForm: getPostForm,
            sendNewPost: sendNewPost,
            updatePost: updatePost,
            getPost: getPost       
        };

        return service;
    }
})();