(function() {

    blogApp.controller('postController', function ($scope, $http) {
        var vm = this;
     
        $scope.posts = [];

        vm.error = "";
        vm.isBusy = true;
      
        vm.predicate = 'author';
        vm.reverse = true;
        vm.currentPage = 1;

      
        $http.get("/api/admin/unpublished").then(function (response) {
            angular.copy(response.data, $scope.posts);
        }, function (error) {
            vm.error = "Failed to load data" + error;
        }).finally(function () {
            vm.isBusy = false;
        });
       

        vm.order = function (predicate) {
            vm.reverse = (vm.predicate === predicate) ? !vm.reverse : false;
            vm.predicate = predicate;
        };

    
        $scope.approve = function (post, isPublished) {
            post.isPublished = isPublished;
            $http.post("/api/admin", post).then(function (response) {
                $scope.posts.pop(post);
            }, function (error) {
                vm.error = "Failed to update the post: " + error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };
    })
})();