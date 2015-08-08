import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import {EmployeeService} from './service';

@inject(Router, EmployeeService)
export class EmployeeUpdate {
    
    showingCancelDialog = false;
    heading = 'Employee Edit - ';
    employee = { firstName: '', lastName: ''};

    get fullName(){
        return `${this.employee.firstName} ${this.employee.lastName}`;
    }
    
    constructor(router, employeeService) {
        this.router = router;
        this.employeeService = employeeService;
    }

    save() {
        this.employeeService.update(this.employee).then(response => {
           if (response.content.isSuccessful) {
              this.goBack();
           }
        });
    }

    cancel() {
        this.showingCancelDialog = true;
    }

    closeCancelModal(){
        this.showingCancelDialog = false;
    }
    
    goBack(){
        this.showingCancelDialog = false;
        if (this.router.isRoot) {
            this.router.navigateToRoute('employees');
        } else {
            this.router.navigateBack();
        }
    }
    
    activate(params) {
        return this.employeeService.get(params.id).then(response => {
            this.employee = response.content;
        });
    }

}
