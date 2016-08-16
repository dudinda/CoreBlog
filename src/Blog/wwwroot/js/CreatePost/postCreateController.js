(function () {
    'use strict';

    angular
        .module('blogApp')
        .controller('postCreateController', ['$scope', 'postCreateFactory', postCreateController]);

    function postCreateController($scope, postCreateFactory) {

        var vm = this;
        vm.error = "";

        $scope.newPost = {};

        vm.isFull = false;

        postCreateFactory
            .getPostCreateForm().success(function (response) {
                angular.copy(response, $scope.newPost);
            }).error(function () {
                vm.error = "Oops. Something went wrong. Try again later!";
            });


        $scope.addTag = function () {
            if ($scope.newPost.tags.length < 5) {
                $scope.newPost.tags.push({
                    name: ''
                });
            } else {
                isFull = true;
            }
        };
        
        $scope.removeTag = function (index) {
            vm.isFull = false;
            $scope.newPost.tags.splice(index, 1);
        };

        vm.approve = function () {
            postCreateFactory
                .sendNewPost($scope.newPost).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                });
        };
    }
})();
