(function() {

    personApp.controller('personController', function ($http) {
        var vm = this;
     
        vm.posts = [];
        vm.error = "";
      
        $http.get("/api/admin").then(function (response) {
            angular.copy(response.data, vm.posts);
        }, function (error) {
            vm.error = "Failed to load data" + error;
        });

        sortByName = function () {
            posts.sort();
        };
    })
})();