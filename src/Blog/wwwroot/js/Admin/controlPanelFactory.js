(function () {
    'use strict';

    
    angular.module('adminApp')
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

        function approvePost(post, isPublished) {
            //set isPublished status
            post.isPublished = isPublished;

            return $http.post("/api/admin/approve", post);
        }

        function banUser(user) {
            return $http.post("/api/admin/ban", user);
        }

        function unbanUser(user) {
            return $http.post("/api/admin/unban", user);
        }

        var service = {
            getUnpublishedPosts: getUnpublishedPosts,
            getPublishedPosts: getPublishedPosts,
            getUsers: getUsers,
            approvePost: approvePost,
            banUser: banUser,
            unbanUser: unbanUser
        };

        return service;
    };

})();