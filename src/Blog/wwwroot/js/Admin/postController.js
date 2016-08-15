(function() {

   

    angular.module('blogApp')
        .controller('postController', ['$scope', 'controlPanelFactory', postController]);

    function postController($scope, controlPanelFactory) {
        var vm = this;
     
        $scope.posts = [];

        vm.error = "";
        vm.isBusy = true;
      
        vm.predicate = 'author';
        vm.reverse = true;
        vm.currentPage = 1;

        controlPanelFactory
            .getUnpublishedPosts().success(function (response) {
                angular.copy(response, $scope.posts);
                vm.isBusy = false;
            }).error(function (error) {
                vm.error = "Failed to get data";
            });

        vm.order = function (predicate) {
            vm.reverse = (vm.predicate === predicate) ? !vm.reverse : false;
            vm.predicate = predicate;
        };
    
        $scope.approve = function (post, isPublished) {
            vm.isBusy = true;
            controlPanelFactory
                .approvePost(post, isPublished).success(function (response) {
                    $scope.posts.pop(response);
                    vm.isBusy = false;
                }).error(function (error) {
                    vm.error = "Failed to approve the post";
                });
        };
    };
})();