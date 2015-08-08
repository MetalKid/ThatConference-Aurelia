import {ConventionalViewStrategy} from "aurelia-framework";
import toastr from 'toastr';

export function configure(aurelia) {
  //toastr.options.closeButton = true;

  aurelia.use
    .standardConfiguration()
    .developmentLogging()
    .plugin('aurelia-animator-css')
    .plugin('./resources/index')
    .plugin('aurelia-validation')
    .plugin('aurelia-bs-modal');

  //ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
  //    var moduleName = moduleId.replace("Scripts/", "");
  //    return `./Templates/${moduleName}`;
  //}

  aurelia.start().then(a => a.setRoot());
}
