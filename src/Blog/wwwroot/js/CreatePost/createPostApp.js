(function () {
    'use strict';

    angular.module('postCreateApp', ['ngRoute'])
      .config(function ($routeProvider) {
          $routeProvider.when("/", {
              controller: "postCreateController",
              controllerAs: "vm",
              templateUrl: "/views/postCreateView.html"
          });

          $routeProvider.otherwise({ redirectTo: "/" });
      });
})();