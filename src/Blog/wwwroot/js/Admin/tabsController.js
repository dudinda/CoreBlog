(function () {
    'use strict';

    angular
        .module('adminApp')
        .controller('tabsController', [tabsController]);

    function tabsController() {
        var vm = this;
        vm.tabs = [
            { link: '#/unpublished', label: 'New' },
            { link: '#/published', label: 'Published' },
            { link: '#/users', label: 'Users' }
        ];

        vm.selectedTab = vm.tabs[0];

        vm.setSelectedTab = function (tab) {
            vm.selectedTab = tab;
        };

        vm.tabClass = function (tab) {
            return vm.selectedTab === tab ? "active" : "";
        };
    };
})();
