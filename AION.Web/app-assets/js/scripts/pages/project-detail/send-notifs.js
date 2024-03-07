//onload event
$(function () {

    $("#Submit_notifs").on("click",
        function () {
            SendEmailNotifReq();
        });


    $(".opensendnotifs").on("click", function () {
        $("#dialog-modal-sendnotifs").modal();

    });

});
//end onload event

function SendEmailNotifReq() {
    var selectedRows = $('#tblsendnotifs').bootstrapTable('getSelections');
    var o = new Object();
    var arr = [];
    if (selectedRows.length > 0) {
        $.each(selectedRows, function (index, value) {
            var addrecips = $(addtorecipsid).val();
            if (addrecips != null && DoesStringEndWith(addrecips, ";") == false)
                addrecips += ";";
            var sendtoallid = "#sendtoall" + value.NotifId;
            var recipsid = "#recips" + value.NotifId;
            var addtorecipsid = "#addtorecips" + value.NotifId;
            var obj = new Object();
            obj.ProjectNotifEmailId = value.NotifId;
            obj.ResendAll = $(sendtoallid).is(":checked");
            obj.SelectedRecipient = $(recipsid).val();
            obj.AddRecipients = addrecips;
            arr.push(obj);
        });
        o.items = arr;
        //send to ajax endpoint
        if (o != null) {
            openSuccess("Sending...", false)
            $.ajax({
                type: "POST",
                url: "/ProjectDetail/ResendProjectNotifications",
                data: o,
                statusCode: {
                    404: function () {
                        openSuccess("page not found", true);
                    }
                },
                success: function (response) {
                    closeSuccess();
                    openSuccess(response, true);

                },
                failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
                error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
            });
        }
    }
}
function DoesStringEndWith(myString, stringCheck) {
    if (myString == null)
        return false;
    var foundIt = (myString.lastIndexOf(stringCheck) === myString.length - stringCheck.length) > 0;
    return foundIt;
}

