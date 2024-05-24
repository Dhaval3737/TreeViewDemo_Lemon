function displayMessage(fsMessage, fiMessageType = 0) {
    toastr.options = {
        "closeButton": true,
        "debug": true,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    if (fiMessageType == 1)
        toastr.success(fsMessage);
    else if (fiMessageType == 2)
        toastr.info(fsMessage);
    else if (fiMessageType == 3)
        toastr.warning(fsMessage);
    else
        toastr.error(fsMessage);

    $('.toast-message').css("color", "white");
}

function isNumberKey(input, fiMaxDigits) {
    input.value = input.value.replace(/[^\d]/g, '');

    if (input.value.length > fiMaxDigits) {
        input.value = input.value.slice(0, fiMaxDigits);
    }
}

function deleteNode(NodeId) {
    $("#hdnNodeId").val(NodeId);
    $("#deleteNode").modal('show');
}