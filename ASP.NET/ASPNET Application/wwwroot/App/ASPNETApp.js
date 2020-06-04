
var ASPNETApp = angular.module("Module1", ['ngRoute', 'ui.bootstrap',
    'ui.grid', 'ui.grid.selection']);

ASPNETApp.config(["$routeProvider", function ($routeProvider) {

    $routeProvider
        .when("/mainmenu", {
         template: "<mainmenu></mainmenu>",
        
        })
        .when("/students", {
        template: "<students></students>",
      
        })
        .when("/subjects", {
            template: "<subjects></subjects>",

        })
        .when("/exams", {
            template: "<exams></exams>",

        })
        .otherwise({
            redirectTo: "/mainmenu"
        });
}]).config(function ($locationProvider) {
    $locationProvider.html5Mode(true).hashPrefix('!');
})
