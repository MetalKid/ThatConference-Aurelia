import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import {EmployeeService} from './service';

@inject(Router, EmployeeService)
export class EmployeeList {
  heading = 'Employees';
  employees = [];
  showingDeleteDialog = false;
  deleteEmployee = null;
  
  constructor(rouer, employeeService){
    this.router = rouer;
    this.employeeService = employeeService;
  }

  create() {
     this.router.navigateToRoute('employee-create');
  }

  removeConfirm(employee) {
      this.deleteEmployee = employee;
      this.showingDeleteDialog = true;
  }

  deleteEmployeeConfirmed() {
    this.showingDeleteDialog = false;
    this.employeeService.delete(this.deleteEmployee.employeeID).then(response => {
      if (response.content.isSuccessful) {
          this.loadEmployees();
      }
    }).catch(response => {});
    this.deleteEmployee = null;
  }
 
  get deleteFullName(){
    if (this.deleteEmployee == null) {
      return null;
    }
    return `${this.deleteEmployee.firstName} ${this.deleteEmployee.lastName}`;
  }
  
  activate(){
    return this.loadEmployees();
  }

  closeDeleteModal() {
      this.showingDeleteDialog = false;
  }

  select(employee) {
	$.each(this.employees, function(i, emp) {
		emp.isSelected = false;
	});
	employee.isSelected = true;
    return true;
  }

  loadEmployees() {
      return this.employeeService.getAll().then(response => {
          var data = response.content;
          $.each(data, function(i, item) {
              item.isSelected = false;
          });
          this.employees = data;		
      });
   }
}
