(function () {
    'use strict';
   
    angular
        .module('adminApp')
        .factory('controlPanelFactory', ['$http', controlPanelFactory]);

    function controlPanelFactory($http) {

        function getUnpublishedPosts() {
            return $http.get("/api/admin/unpublished");
        }

        function getPublishedPosts() {
            return $http.get("/api/admin/published");
        }

        function getUsers() {
            return $http.get("/api/admin/users");
        }

        function getPost(id) {
            return $http.get("/api/post/get" + id);
        }
        
        function deletePost(post) {
            return $http.post("/api/admin/deletepost", post);
        }

        function deleteUser(user) {
            return $http.post("/api/admin/deleteuser", user);
        }

        function approvePost(post) {
            return $http.post("/api/admin/approve", post);
        }

        function banUser(user) {
            return $http.post("/api/admin/ban", user);
        }

        function unbanUser(user) {
            return $http.post("/api/admin/unban", user);
        }

        function updatePost(post) {
            return $http.put("/api/post/update", post);
        }

        var service = {
            getUnpublishedPosts: getUnpublishedPosts,
            getPublishedPosts: getPublishedPosts,
            getUsers: getUsers,
            approvePost: approvePost,
            banUser: banUser,
            unbanUser: unbanUser,
            deleteUser: deleteUser,
            getPost: getPost,
            deletePost: deletePost,
            updatePost: updatePost            
        };
  
        return service;
    }; 
})();