export class ChildRouter{
  heading = 'Child Router';

  configureRouter(config, router){
    config.map([
      { route: ['','home'], name: 'home',      moduleId: './home',      nav: false, title:'Home' },
      { route: 'employees',       name: 'employees',       moduleId: './employees',       nav: true, title:'Employees' },
      { route: 'flickr',       name: 'flickr',       moduleId: './flickr',       nav: true, title:'Flickr' },
      { route: 'child-router', name: 'child-router', moduleId: './child-router', nav: true, title:'Child Router' }
    ]);

    this.router = router;
  }
}
