$(function () {
    $.fn.dataTable.moment('MM/dd/yyyy');
    $("#holidaydetails").DataTable({
        order: [[1, 'desc']],
        columnDefs: [
            { targets: 1, type: 'date' }
        ]
    });
});

var deleteholconfig = function () {
    if ($('table tbody tr input[type="checkbox"]:checked').length == 0) {
        openWarning("Please make a selection in order to delete a holiday.");
    }
    else {
        swal({
            title: "Are you sure?",
            text: "The selected holidays will be deleted.",
            icon: "warning",
            buttons: {
                cancel: {
                    text: "No",
                    value: null,
                    visible: true,
                    className: "btn-warning",
                    closeModal: false,
                },
                confirm: {
                    text: "Yes",
                    value: true,
                    visible: true,
                    className: "",
                    closeModal: false
                }
            }
        }).then(isConfirm => {
            if (isConfirm) {
                var holid = $('table tbody tr input[type="checkbox"]:checked').map(function () {
                    return $(this).val();
                }).get();

                $.ajax({
                    type: 'POST',
                    url: '/Admin/Delete',
                    data: JSON.stringify({ HolidayIds: holid }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        window.location.reload();
                        swal("Deleted!", "The selected holidays have been deleted.", "success");
                    },
                    failure: function (response) { openWarning("Save Failure"); },
                    error: function (response) { openWarning("Save Error"); }
                });

            }
            else {
                swal("Cancelled", "The holidays will not be cancelled.", "error");
            }
        });
    }
}