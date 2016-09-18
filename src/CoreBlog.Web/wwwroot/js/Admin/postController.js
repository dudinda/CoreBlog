(function() {

   'use strict';

    angular
        .module('adminApp')
        .controller('postController', ['$routeParams',
                                       'controlPanelFactory',
                                       postController]);

    function postController($routeParams, controlPanelFactory) {
        var vm = this;

        vm.posts = [];
   
        vm.error = "";

        vm.isBusy = false;
        vm.reverse = true;

        vm.predicate = 'title';
       
   
        vm.getUnpublished = function () {
            vm.isBusy = true;
            controlPanelFactory
                .getUnpublishedPosts().success(function (response) {
                    angular.copy(response, vm.posts);
                }).error(function (error) {
                    vm.error = "Failed to get unpublished posts.";
                }).finally(function () {
                    vm.isBusy = false;
                })
        };

        vm.getPublished = function () {
            vm.isBusy = true;
            controlPanelFactory
                .getPublishedPosts().success(function (response) {
                    angular.copy(response, vm.posts);
                }).error(function (error) {
                    vm.error = "Failed to get published posts.";
                }).finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.preview = function () {           
            vm.isBusy = true;
            vm.openPost = {};
            controlPanelFactory
                .getPost($routeParams.id).success(function (response) {
                    angular.copy(response, vm.openPost);
                }).error(function () {
                    vm.error = "Failed to open post with id: " + $routeParams.id;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.order = function (predicate) {
            vm.reverse = (vm.predicate === predicate) ? !vm.reverse : false;
            vm.predicate = predicate;
        };

        vm.isDeleted = function (post) {
            return !post.deleted;
        };
    
        vm.delete = function (post) {
            vm.isBusy = true;
            controlPanelFactory
                .deletePost(post).success(function () {
                    post.deleted = true;
                }).error(function (error) {
                    vm.error = "Failed to delete: " + post.title;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.approve = function (post) {
            vm.isBusy = true;
            controlPanelFactory
                .approvePost(post).success(function () {
                    post.deleted = true;
                }).error(function (error) {
                    vm.error = "Failed to approve: " + post.title;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };
    
    };
})();