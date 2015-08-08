import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class Flickr{
  static inject() { return [HttpClient]; };

  heading = 'Flickr';
  images = [];
  url = 'http://api.flickr.com/services/feeds/photos_public.gne?tags=planet,pluto&tagmode=all&format=json';

  constructor(http) {
    this.http = http;
  }

  activate() {
      return this.http.jsonp(this.url).then(response => { 
        console.log("Response", response);
        console.log("Content", response.content);
        this.images = response.content.items;
    });
  }
}
