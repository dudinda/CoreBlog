(function () {
    'use strict';

    angular
        .module('postCreateApp')
        .controller('postCreateController', ['postCreateFactory', postCreateController]);

    function postCreateController(postCreateFactory) {

        var vm = this;
        
        vm.error = "";
        vm.isReady = false;
    
        vm.newPost = {};

        vm.isFull = false;

        postCreateFactory
            .getPostCreateForm().success(function (response) {
                angular.copy(response, vm.newPost);
            }).error(function () {
                vm.error = "Oops. Something went wrong. Try again later!";
            });


        vm.addTag = function () {
            if (vm.newPost.tags.length < 5) {
                vm.newPost.tags.push({
                    name: ''
                });
            } 

            if(vm.newPost.tags.length === 5) {
                vm.isFull = true;
            }
        };
        
        vm.removeTag = function (index) {
            vm.isFull = false;
            vm.newPost.tags.splice(index, 1);
        };

        vm.approve = function () {
            postCreateFactory
                .sendNewPost(vm.newPost).success(function () {
                    vm.isReady = true;
                }).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                });
        };
    }
})();
