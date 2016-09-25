(function () {
    'use strict';

    angular
      .module('postCreateApp', ['ngRoute', 'textAngular', 'naif.base64'])
      .config(function ($routeProvider) {
          $routeProvider
            .when("/", {
                controller: "postCreateController",
                controllerAs: "vm",
                templateUrl: "/views/postCreateView.html"
            })
            .when("/Edit/:id", {
                controller: "postCreateController",
                controllerAs: "vm",
                templateUrl: "/views/postCreateView.html"
            });
           
          $routeProvider.otherwise({ redirectTo: "/" });
      });
})();