(function () {
    'use strict';
    angular.module('adminApp', ['ngRoute'])
       .config(function ($routeProvider) {
           $routeProvider
               .when("/published", {
                   controller: "postController",
                   controllerAs: "vm",
                   templateUrl: "/views/managePublishedView.html"
               })
               .when("/unpublished", {
                   controller: "postController",
                   controllerAs: "vm",
                   templateUrl: "/views/manageUnpublishedView.html"
               })
                .when("/users", {
                    controller: "userController",
                    controllerAs: "vm",
                    templateUrl: "/views/manageUsersView.html"
                });

           $routeProvider.otherwise({ redirectTo: "/unpublished" });
       });

})();