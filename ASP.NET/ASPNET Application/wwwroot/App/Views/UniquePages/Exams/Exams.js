class Exams
{
    constructor($http) {
        this.Http = $http;
    }


}
Exams.$inject = ['$http'];

ASPNETApp.
    component('exams', {
        templateUrl: './App/Views/UniquePages/Exams/ExamsView.html',
        controller: ('Exams', Exams),
        controllerAs: 'vm'
    });

