(function () {
    'use strict';

    angular
        .module('adminApp')
        .controller('postEditorController', ['$routeParams','controlPanelFactory', postEditorController]);

    function postEditorController($routeParams, controlPanelFactory) {

        var vm = this;
        vm.post = {};
        vm.error = "";
        vm.id = $routeParams.id;

        controlPanelFactory.getPost(vm.id).success(function (response) {
            angular.copy(response, vm.post);
        }).error(function () {
            vm.error = "Failed to get the post with id: " + vm.id;
        });
        

    };
})();
