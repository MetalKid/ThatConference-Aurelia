import {inject} from 'aurelia-framework';
import {AppSettings} from '../app-settings';
import {HttpClient} from 'aurelia-http-client';
import {SaveHandler} from '../common/save-handler';
import {ErrorHandler} from '../common/error-handler';

@inject(AppSettings, HttpClient, SaveHandler, ErrorHandler)
export class EmployeeService {

   url = null;
   
   constructor(appSettings, http, saveHandler, errorHandler) {
      this.appSettings = appSettings;
      this.http = http;
      this.saveHandler = saveHandler;
      this.errorHandler = errorHandler;
      this.url = this.appSettings.baseUrl + "employee/";
   }

   getAll() {
      var result = new Promise((resolve, reject) => {
            this.http.get(this.url + "getall").then(response => {
               resolve(response);
            }).catch(response => {
               reject(response);
            });
         }
      );
      return result;
   }
   
   get(id) {
      var result = new Promise((resolve, reject) => {
            this.http.get(this.url + "?id=" + id).then(response => {
               resolve(response);
            }).catch(response => {
               reject(response);
            });
         }
      );
      return result;
   }

   create(employee) {
      var result = new Promise((resolve, reject) => {
            this.http.post(this.url, employee).then(response => {
               this.saveHandler.handle(response);
               resolve(response);
            }).catch(response => {
               this.errorHandler.handle(response);
               //reject(response);
           });
         }
      );
      return result;
   }

   update(employee) {
      var result = new Promise((resolve, reject) => {
            this.http.put(this.url, employee).then(response => {
               this.saveHandler.handle(response);
               resolve(response);
            }).catch(response => {
               this.errorHandler.handle(response);
               //reject(response);
           });
         }
      );
      return result;
   }

   delete(id) {
      var result = new Promise((resolve, reject) => {
            this.http.delete(this.url + "?id=" + id).then(response => {
               this.saveHandler.handle(response);
               resolve(response);
            }).catch(response => {
               this.errorHandler.handle(response);
               //reject(response);
           });
         }
      );
      return result;
    }
}