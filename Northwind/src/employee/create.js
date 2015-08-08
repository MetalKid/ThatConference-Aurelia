import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import {EmployeeService} from './service';
import {Validation} from 'aurelia-validation';

@inject(Router, EmployeeService, Validation)
export class EmployeeCreate {
    
    showingCancelDialog = false;
    heading = 'Employee Create';
    employee = {};
    
    constructor(router, employeeService, validation) {
        this.router = router;
        this.employeeService = employeeService;
        this.validation = validation.on(this)
          .ensure('employee.firstName')
            .isNotEmpty()
            .hasMinLength(3)
            .hasMaxLength(20)
           .ensure('employee.lastName')
            .isNotEmpty()
            .hasMinLength(3)
            .hasMaxLength(10);
    }

    save() {
        this.validation.validate().then(() => {
            this.employeeService.create(this.employee).then(response => {
                if (response.content.isSuccessful) {
                   this.goBack();
                }
            });//.catch(response => {});
        }).catch(err => {
            console.log(err);
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
    
}
