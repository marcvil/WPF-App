class Main {

    constructor($http) {
        this.Http = $http;
    }
}

Main.$inject = ['$http'];

ASPNETApp.
    component('main', {
        templateUrl: './App/Views/Main.html',
        controller: ('Main', Main),
        controllerAs: 'vm'
    });