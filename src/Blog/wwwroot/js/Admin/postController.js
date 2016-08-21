(function() {

   'use strict';

    angular
        .module('adminApp')
        .controller('postController', ['controlPanelFactory', postController]);

    function postController(controlPanelFactory) {
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
                })
        };

        vm.order = function (predicate) {
            vm.reverse = (vm.predicate === predicate) ? !vm.reverse : false;
            vm.predicate = predicate;
        };
    
        vm.delete = function (index) {
            vm.isBusy = true;
            controlPanelFactory
                .deletePost(vm.posts[index]).success(function (response) {
                    vm.posts.splice(index, 1);
                }).error(function (error) {
                    vm.error = "Failed to delete: " + post.title;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.approve = function (index) {
            vm.isBusy = true;
            controlPanelFactory
                .approvePost(vm.posts[index]).success(function (response) {
                    vm.posts.splice(index, 1);
                }).error(function (error) {
                    vm.error = "Failed to approve: " + post.title;
                }).finally(function () {
                    vm.isBusy = false;
                });
        };
    
    };
})();