import {computedFrom} from 'aurelia-framework';

export class Welcome{
  heading = 'Welcome to the Aurelia Northwind App!';
  firstName = 'John';
  lastName = 'Doe';
  previousValue = this.fullName;

  //Getters can't be observed with Object.observe, so they must be dirty checked.
  //However, if you tell Aurelia the dependencies, it no longer needs to dirty check the property.
  //To optimize by declaring the properties that this getter is computed from, uncomment the line below.
  @computedFrom('firstName', 'lastName')
  get fullName(){
    return `${this.firstName} ${this.lastName}`;
  }

  submit() {
    this.previousValue = this.fullName;
    alert(`Welcome, ${this.fullName}!`);
  }

  canActivate() {
  //  alert("canActivate!");
  }

  activate(params, routeConfig, navigationInstruction) {
    console.log(params);
    console.log(routeConfig);
    console.log(navigationInstruction);
   // alert("activate!");
  }

  canDeactivate() {
 //   alert("canDeactivate!");

    if (this.fullName !== this.previousValue) {
      return confirm('Are you sure you want to leave?');
    }
  }

  deactivate() {
   //   alert("deactivate!");
  }

}
