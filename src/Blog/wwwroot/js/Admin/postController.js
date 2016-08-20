(function() {

   'use strict';

    angular
        .module('adminApp')
        .controller('postController', ['controlPanelFactory', postController]);

    function postController(controlPanelFactory) {
        var vm = this;

        vm.posts = [];

        vm.error = "";

        vm.isBusy = true;
        vm.reverse = true;

        vm.predicate = 'title';
       
   
        vm.getUnpublished = function () {
            vm.isBusy = true;
            controlPanelFactory
                .getUnpublishedPosts().success(function (response) {
                    angular.copy(response, vm.posts);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to get unpublished posts.";
                })
        };

        vm.getPublished = function () {
            vm.isBusy = true;
            controlPanelFactory
                .getPublishedPosts().success(function (response) {
                    angular.copy(response, vm.posts);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to get published posts.";
                })
        };

        vm.order = function (predicate) {
            vm.reverse = (vm.predicate === predicate) ? !vm.reverse : false;
            vm.predicate = predicate;
        };
    
        vm.approve = function (index, isPublished) {
            vm.isBusy = true;
            controlPanelFactory
                .approvePost(vm.posts[index], isPublished).success(function (response) {
                    vm.posts.splice(index, 1);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to approve: " + post.title;
                });
        };
    
    };
})();