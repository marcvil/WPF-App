class Students
{
    constructor($http) {
        this.Http = $http;

        this.gridOptions =
        {
            enableSorting: true,
            enableColumnMenus: true,
            enableHorizontalScrollbar: 0,
            enableVerticalScrollbar: 0,
            enableRowSelection: true,
            enableRowHeaderSelection: true,
            multiSelect: false,
            enableGridMenu: false,
            enableColumnResizing: true,
            data: this.Students,
            selectedRows: [],
            showRowSelection: true,
            
            onRegisterApi: function (gridApi) {
                this.gridApi = gridApi;

            },
            columnDefs:
                [
                    {
                        name: 'Name', field: 'name', width: 90, enableCellEdit: true
                    } ,
                    {
                        name: 'DNI', field: 'dni', width: 90, enableCellEdit: true
                    } ,
                    {
                        name: 'LockerKeyNumber', field: 'lockerKeyNumber', width: 90, enableCellEdit: true
                    },
                    {
                        name: 'Mail', field: 'mail', width: 90, enableCellEdit: true
                    } 
                ],
            
            
        }
    }
    

    /* Fields */
    get Id() {
        return this.Id;
    }

    get Name() {
        return this.name;
    }
    set Name(value) {
        return this.name = value;
    }

    get Dni() {
        return this.dni;
    }
    set Dni(value) {
        return this.dni = value;
    }

    get LockerKeyNumber() {
        return this.lockerKeyNumber;
    }
    set LockerKeyNumber(value) {
        return this.lockerKeyNumber = value;
    }

    get Mail() {
        return this.mail;
    }
    set Mail(value) {
        return this.mail = value;
    }


    get Students() {
        return this._students;
    }
    set Students(value) {
        this._students = value;
    }

    get Errors() {
        var errors = this.Errors;
        return errors;
    }
    set Errors(value) {
        this._errors = value;
    }

    set SelectedRows(value) {
        this.selectedRows = value;
    }

    get SelectedRows() {
        var selectedRows = this.gridOptions.gridApi.selection.getSelectedRows();

        return selectedRows;
    }

    get IsEdit() {
        return this._isEdit;
    }
    set IsEdit(value) {

        this._isEdit = value;
    }


 


    /* Crud Methods */

    Students = [];

    SaveStudent()
    {
        var student = new Student(this.Dni, this.Name, this.Mail, this.LockerKeyNumber);
        this.Http.post("/api/Students",student).then(
            (response) =>
            {
                if (response.data.SaveValidationSuccesful = true)
                {
                    this.Dni = "";
                    this.Name = "";
                    this.Mail = "";
                    this.LockerKeyNumber = null;
                }
                else {

                    console.log("POST-ing of data failed");
                }
            }

        )
    }

    EditStudent()
    {
        if (this.SelectedRows != null)
        {
            var student = new Student();
            {
                student.Id = this.SelectedRows[0].id;
                student.Name = this.Name;
                student.Dni = this.Dni;
                student.Mail = this.Mail;
                student.LockerKeyNumber = this.LockerKeyNumber;

            }
           
            this.Http.put("/api/Students/" + student.Id ,student).then(
                (response) => {
                    if (response.data.SaveValidationSuccesful = true) {
                        this.Dni = "";
                        this.Name = "";
                        this.Mail = "";
                        this.LockerKeyNumber = null;
                    }
                    else {

                        console.log("POST-ing of data failed");
                    }
                })
            
        }
    }
    GetStudentsRequest()
    {
        this.Http.get("/api/Students").then(
            (response) => {
                this.Students.length = 0;
                for (let i in response.data) {
                    this.Students.push(response.data[i]);
                    
                }
            },
            function errorCallback(response) {
                console.log("POST-ing of data failed");
            }
        );
    }

    DeleteStudent()
    {

    }

   
  

}


Students.$inject = ['$http'];

ASPNETApp.
    component('students', {
        templateUrl: './App/Views/UniquePages/Students/StudentsView.html',
        controller: ('Students', Students),
        controllerAs: 'vm'
    });

