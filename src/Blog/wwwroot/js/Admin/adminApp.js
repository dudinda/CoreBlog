﻿(function () {
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
                })
                .when("/open/:id", {
                    controller: "postEditorController",
                    controllerAs: "vm",
                    templateUrl: "/views/editPostView.html"
                })
               .when("/edit/:id", {
                   controller: "postEditorController",
                   controllerAs: "vm",
                   templateUrl: "/views/editPostView.html"
                });    
           $routeProvider.otherwise({ redirectTo: "/unpublished" });
       })

})();