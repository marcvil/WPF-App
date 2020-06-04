class StudentsService {
    constructor($http) {
        this.Http = $http;
    }


    GetAllAsync(callbackAction) {
        this.Http.get("api/Students")
            .then((response) => {
                callbackAction(response.data);
            },
                function errorCallback(response) {
                    console.log("GET-ing of data failed");
                }
            );
        console.log("iniciando");
    }

    AddElementAsync(element, callbackAction) {
        this.Http.post("/api/Students", element)
            .then((response) => {
                callbackAction(response.data);
            },
                function errorCallback(response) {
                    console.log("POST-ing of data failed");
                }
            );
    }

    UpdateElementAsync(element, callbackAction) {
        this.Http.put("api/Students/" + element.id, element)

            .then((response) => {
                callbackAction(response.data);
            },
                function errorCallback(response) {
                    console.log("PUT-ing of data failed");
                }
            );
    }

    DeleteElementAsync(element, callbackAction) {
        this.Http.delete("api/Students/" + element.id, element)

            .then((response) => {
                callbackAction(response.data);
            },
                function errorCallback(response) {
                    console.log("DELETE-ing of data failed");
                }
            );
    }

}

StudentsService.$inject = ['$http'];
ASPNETApp.service('StudentsService', StudentsService);
