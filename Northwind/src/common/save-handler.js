import toastr from 'toastr';

export class SaveHandler {
    handle(response) {
        var result = response.content;
        if (result.isSuccessful) {
            toastr.success(result.messageText, result.messageTitle);
        } else {
            toastr.error(result.messageText, result.messageTitle);
        }
    }
}