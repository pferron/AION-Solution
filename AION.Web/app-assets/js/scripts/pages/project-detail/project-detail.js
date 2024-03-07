
/*Partial view DataTable initialization*/
$(function () {

    /*_ProjectAudit*/
    $('#confighistory').DataTable();
    /*_PlanReviews*/
    /*$('#planreviews').DataTable();*/
    /*_SendNotifications*/
    $('#tblsendnotifs').DataTable();
    /*_MeetingRooms*/
    $('#meetingroomtable').DataTable();
    /*_Notes*/
    $('#notes-view-table').DataTable();
    
    /*$('#projects').DataTable();*/
})

/*OnClick events*/
$(function () {
    $("#Submit_ChangeFacilitator").on("click",
        function () {
            if ($("#Project_AssignedFacilitator").val() > 0) {
                if (ChangeFacilitator()) {
                    $("#change-facilitator-message").hide();
                    $("#dialog-modal-change-facilitator").modal('hide');
                }

            } else {
                $("#change-facilitator-message").show();
                $("#change-facilitator-message").html("Select a facilitator.");
            }
        });

    $("#Submit_CancelMeeting").on("click",
        function () {
            if ($("#CancelMeetingNotes").val() != "") {
                if (CancelMeeting()) {
                    $("#cancel-status-message").hide();
                    $("dialog-modal-cancel-meeting").modal('hide');
                }

            } else {
                $("#cancel-status-message").show();
                $("#cancel-status-message").html("Comments are required.");
            }
        });

    $(".cancel-meeting").on("click", function (e) {
        e.preventDefault();
        var apptId = $(this).attr("data-id");
        $("#appointmentId").val(apptId);
        $("#dialog-modal-cancel-meeting").modal();
    });

    $("#yes_confirmmodal").on("click",
        function () {
            var link = $("#schedulePlanReview").attr('href');
            window.location.href = link;
            $("#yes_confirmmodal").hide();
        });

    $("#schedulePlanReview").on("click", function (e) {
        e.preventDefault();

        if ($("#RescheduleWarning") == "True") {
            $("#dialog-modal-confirmation").modal();
            $(".confirmationmessage").html("Plan Review Start Date Is Less Than 5 Business Days From Today.  Are You Sure You Want To Reschedule?");
        }
        else {
            var link = $(this).attr('href');
            window.location.href = link;
        }
    });
});


function CancelMeeting() {
    var notes = $("#CancelMeetingNotes").val();
    var apptId = $("#appointmentId").val();
    var projectId = $("#Project_AccelaProjectRefId").val();
    var recIdTxt = $("#Project_RecIdTxt").val();
    var wkrId = $("#LoggedInUser_ID").val();

    var model = {
        "AppointmentId": apptId,
        "ProjectId": projectId,
        "RecIdTxt": recIdTxt,
        "UserId": wkrId,
        "Notes": notes
    };

    $.ajax({
        type: "POST",
        url: "/ProjectDetail/CancelMeeting",
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var tr = $('.meetings tr:has(.appointment-id[value="' + apptId + '"])');
            var td = tr.find('td.meetingStatus');
            td.text(data);
        }
    });

    return true;
}

function ChangeFacilitator() {
    var facilitatorId = $("#Project_AssignedFacilitator").val();
    var projectId = $("#Project_ID").val();

    var model = {
        "FacilitatorId": facilitatorId,
        "ProjectId": projectId
    };

    $.ajax({
        type: "POST",
        url: "/ProjectDetail/ChangeFacilitator",
        data: JSON.stringify(model),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#facilitatorPartial").empty();
            $("#facilitatorPartial").html(data);

            UpdateAccela();
        }
    });

    return true;
}

function UpdateAccela() {
    var facilitatorId = $("#Project_AssignedFacilitator").val();
    var projectId = $('#Project_ID').val();

    var model = {
        "FacilitatorId": facilitatorId,
        "ProjectId": projectId
    };

    $.ajax({
        type: "POST",
        url: "/ProjectDetail/UpdateAccelaFacilitator",
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != "Success") {
                $("#dialog-modal-warning").dialog("open");
                $(".warningmessage").html("The was an issue changing the facilitator with Accela");
            }
        }
    });

    return true;
}
