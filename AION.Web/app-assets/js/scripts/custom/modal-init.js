$(function () {
    $("#dialog-modal-warning").dialog({
        modal: true,
        autoOpen: false,
        height: "auto",
        width: 800
    });
    $("#dialog-modal-success").dialog({
        modal: true,
        autoOpen: false,
        height: "auto",
        width: 800
    });

    var $modal = $('.modal');
    $modal.find('.modal-content')
        .resizable({
            handles: 'n, e, s, w, ne, sw, se, nw',
        })
        .draggable({
            handle: '.modal-header'
        });
})
function openWarning(message, canclose) {
    var showclosebutton = canclose == null ? true : canclose;
    if (!showclosebutton) {
        //disable close button
        swal(message, {
            button: false,
            closeOnClickOutside: false,
            closeOnEsc: false,
            icon: "warning"

        });
    } else {
        swal("", message, "warning", {button: "OK", closeOnClickOutside: false, closeOnEsc: false});
    }
}

function openSuccess(message, canclose) {
    var showclosebutton = canclose == null ? true : canclose;
    if (!showclosebutton) {
        //disable close button
        swal(message, {
            button: false,
            closeOnClickOutside: false,
            closeOnEsc: false,
            icon: "success"
        });
    } else {
        swal("", message, "success", { button: "OK", closeOnClickOutside: false, closeOnEsc: false });
    }
}
function openError(message, canclose) {
    var showclosebutton = canclose == null ? true : canclose;
    if (!showclosebutton) {
        //disable close button
        swal(message, {
            button: false,
            closeOnClickOutside: false,
            closeOnEsc: false,
            icon: "error"
        });
    } else {
        swal("", message, "error", { button: "OK", closeOnClickOutside: false, closeOnEsc: false });
    }
}

function openInProgress(message, canclose) {
    var showclosebutton = canclose == null ? true : canclose;
    if (!showclosebutton) {
        //disable close button
        swal(message, {
            button: false,
            closeOnClickOutside: false,
            closeOnEsc: false,
            icon: "info"
        });
    } else {
        swal("", message, "info", { button: "OK", closeOnClickOutside: false, closeOnEsc: false });
    }
}

function opendialog(divid) {
    $("#" + divid).dialog("open");
}
function closedialog(divid) {
    $("#" + divid).dialog("close");
}
function closeSuccess() {
    swal.close();
    closedialog("dialog-modal-success");
}
function closeWarning() {
    swal.close();
    closedialog("dialog-modal-warning");
}