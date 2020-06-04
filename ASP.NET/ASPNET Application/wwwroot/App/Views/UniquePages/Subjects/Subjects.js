class Subjects
{
    constructor($http) {
        this.Http = $http;
    }

  

    /* Fields */
    get Id() {
        return this.Id;
    }

    get SubjectName() {
        return this.subjectName;
    }
    set SubjectName(value) {
        return this.subjectName = value;
    }

    get SubjectCode() {
        return this.subjectCode;
    }
    set SubjectCode(value) {
        return this.subjectCode = value;
    }

    get Subjects() {
        return this.subjects;
    }
    set Subjects(value) {
        this.subjects = value;
    }

    get Errors() {
        var errors = this.Errors;
        return errors;
    }
    set Errors(value) {
        this._errors = value;
    }


    /* Crud Methods */

    Subjects = [];

    SaveSubject() {
        var subject = new Subject(this.subjectName, this.SubjectCode);
        this.Http.post("/api/Subjects", subject).then(
            (response) => {
                if (response.data.SaveValidationSuccesful = true) {
                    this.SubjectName = "";
                    this.SubjectCode = "";
                    
                }
                else {

                    console.log("POST-ing of data failed");
                }
            }

        )
    }

    EditSubject() {

    }
    GetSubjectsRequest() {


    }

    DeleteSubject() {

    }


}
Subjects.$inject = ['$http'];

ASPNETApp.
    component('subjects', {
        templateUrl: './App/Views/UniquePages/Subjects/SubjectsView.html',
        controller: ('Subjects', Subjects),
        controllerAs: 'vm'
    });

