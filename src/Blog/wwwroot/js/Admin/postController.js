(function() {

   

    angular.module('adminApp')
        .controller('postController', ['controlPanelFactory', postController]);

    function postController(controlPanelFactory) {
        var vm = this;
     
        vm.posts = [];

        vm.error = "";
        vm.isBusy = true;
      
        vm.predicate = 'author';
        vm.reverse = true;
        vm.currentPage = 1;

        vm.getUnpublished = function () {
            vm.isBusy = true;
            controlPanelFactory
                .getUnpublishedPosts().success(function (response) {
                    angular.copy(response, vm.posts);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to get data";
                })
        };

        vm.getPublished = function () {
            vm.isBusy = true;
            controlPanelFactory
                .getPublishedPosts().success(function (response) {
                    angular.copy(response, vm.posts);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to get data";
                })
        };

        vm.order = function (predicate) {
            vm.reverse = (vm.predicate === predicate) ? !vm.reverse : false;
            vm.predicate = predicate;
        };
    
        vm.approve = function (post, isPublished) {
            vm.isBusy = true;
            controlPanelFactory
                .approvePost(post, isPublished).success(function (response) {
                    vm.posts.pop(response);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to approve the post";
                });
        };
    };
})();