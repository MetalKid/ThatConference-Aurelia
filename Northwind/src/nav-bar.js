import {bindable} from 'aurelia-framework';
import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class NavBar {
  @bindable router = null;
  constructor(http) {
        this.http = http;
    }
}
