import toastr from 'toastr';

export class ErrorHandler {
    handle(response) {
        var data = response.content;
        if (data.BrokenRules != undefined) {
            this.displayBrokenRules(data.BrokenRules);
        } else {
            toastr.error(data.Message, "Error");
        }
    }

    displayBrokenRules(rules) {
        var message = "<ul style='padding-left: 20px'>";
        $.each(rules, function(i, br) {
            message += "<li>" + br.RuleMessage + "</li>";
        });
        message += "</ul>";
        toastr.error(message, "Broken Rules");
    }
}