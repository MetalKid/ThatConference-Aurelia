import {inject, customElement, bindable} from 'aurelia-framework';
import moment from 'moment';
import {datepicker} from 'eonasdan/bootstrap-datetimepicker';
import 'eonasdan/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css!';

@inject(Element)
@customElement("datepicker")
@bindable("value")
export class DatePicker {

    @bindable format = "MM/DD/YYYY";
    @bindable enabled = "true";

    constructor(element) {
        this.element = element;
    }

    attached() {
        this.datePicker = $(this.element).find('.input-group.date')
            .datetimepicker({
                format: this.format,
                showClose: true,
                showTodayButton: true
            });
        
        if (this.enabled != "true") {
           $(this.element).find("input").attr('readonly', 'true');
        }

        this.datePicker.on("dp.change", (e) => {
            this.value = moment(e.date).format(this.format);
        });
    }

    valueChanged(newValue, oldValue) {
        //alert(newValue);
       // alert(value);
    }
}