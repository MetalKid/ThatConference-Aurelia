import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';
import {Redirect} from 'aurelia-router';

export class App {
  configureRouter(config, router) {
    router.title = 'Northwind';
    //config.addPipelineStep('authorize', AuthorizeStep); // Add a route filter to the authorize extensibility point.
    config.map([
      { route: ['','home'], name: 'home',      moduleId: './home',      nav: false, title:'Home' },
      { route: 'flickr',       name: 'flickr',       moduleId: './flickr',       nav: true, title:'Flickr' },
      { route: 'child-router', name: 'child-router', moduleId: './child-router', nav: true, title:'Child Router' },
      { route: 'employees',       name: 'employees',       moduleId: './employee/list',       nav: true, title:'Employees' },
      { route: 'employee-create',       name: 'employee-create',       moduleId: './employee/create',       nav: false, title:'Employee Create' },
      { route: 'employee-edit',       name: 'employee-edit',       moduleId: './employee/update',       nav: false, title:'Employee Edit' }
    ]);

    this.router = router;
  };
}

class AuthorizeStep {
    run(routingContext, next) {
        return next();

        // alert('I run every time!');
        //console.log(routingContext.nextInstruction)
        if (routingContext.nextInstruction.config.route == 'home') {
            return next();
        }
        return next.cancel(new Redirect('home'));
    }
}