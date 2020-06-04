class MainMenu {

    constructor($http) {
        this.Http = $http;
    }
}


MainMenu.$inject = ['$http'];
ASPNETApp.
    component('mainmenu', {
        templateUrl: './App/Views/UniquePages/Menu/MainMenuView.html',
        controller: ('MainMenu', MainMenu),
        controllerAs: 'vm'
    });