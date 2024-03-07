$(function () {

    $(".updateScheduledMeeting").on("click", function (e) {
        e.preventDefault();
        var tr = $(this).closest("tr");
        var meetingId = $(this).attr("data-id");
        var projectId = $(this).attr("data-project-id");
        var userId = $(this).attr("data-user");
        var meetingResponse = tr.find(".meetingApptResponseSelect");
        var meetingType = tr.find(".scheduledMeetingType");

        var projectIdNumber = $("#ProjectId").val();

        if (meetingResponse == null || meetingResponse.val() == "") {
            openSuccess("Please select a status");
            return;
        }

        var model = {
            "projectId": projectId,
            "meetingId": meetingId,
            "meetingType": meetingType.text(),
            "loggedInUserId": userId
        };

        var url = "/Customer/UpdateScheduledMeeting";

        openSuccess("Updating...", false);

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                closeSuccess();
                $("#scheduledMeetings").empty();
                $("#scheduledMeetings").html(response);
                $('#projectStatus').html("Project Status: " + $('#newUpdateMeetingScheduleProjectStatus').val());

                GetProjectAudits(projectIdNumber);
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    });

    $(".meetingApptResponseSelect").on("change", function () {
        var selectedModeText = $(this).children("option:selected").text();

        var tr = $(this).closest("tr");

        var meetingId = tr.attr("data-id");
        var meetingType = tr.find('td.scheduledMeetingType').html();

        $("#selectedMeetingId").val(meetingId);
        $("#selectedMeetingType").val(meetingType);

        if (selectedModeText == 'Reject') {
            PrepareRequestDatesModal(meetingType);
        }
    });

    $("#submitMeetingRequestDateOptions").on("click", function () {
        $("#errorMessage").hide();
        $("#submitMeetingRequestDateOptions").prop('disabled', true);
        $("#meetingDateOptionsClosebtn").prop('disabled', true);
        $("#submitMessage").show();
        var obj = new Object();
        obj.MeetingApptId = $('#selectedMeetingId').val();
        obj.MeetingType = $('#selectedMeetingType').val();
        obj.requestDate1 = $('#datepickerPM1').val();
        obj.requestDate2 = $('#datepickerPM2').val();
        obj.requestDate3 = $('#datepickerPM3').val();
        obj.ProjectId = $(this).attr("data-id");
        obj.LoggedInUserId = $(this).attr("data-user-id");
        if (isNullOrBlank(obj.requestDate1) || isNullOrBlank(obj.requestDate2) || isNullOrBlank(obj.requestDate3)) {
            $("#submitMessage").hide();
            $("#errorMessage").show();
            $("#submitMeetingRequestDateOptions").prop('disabled', false);
            $("#meetingDateOptionsClosebtn").prop('disabled', false);

            //exit
            return;
        }
        if (obj != null) {

            $.ajax({
                url: "SubmitMeetingDateRequest",
                type: "POST",
                data: obj,
                success: function (response) {
                    $("#scheduledMeetings").empty();
                    $("#scheduledMeetings").html(response);

                    $("#MeetingDateOptions").modal("hide");

                },
                error: function () {
                    $("#meetingDateOptionsClosebtn").prop('disabled', false);
                    alert("error");
                }
            });
        }

    });
});

function UpdateAcceptArray(selectedModeText, meetingId, meetingType) {
    if (selectedModeText == "Accept") {
        arrMeetingAcceptances.push({ id: meetingId, type: meetingType });
    }
    else {
        var objIndex = arrMeetingAcceptances.findIndex(x => x.id === meetingId);

        if (objIndex > -1) {
            var returnedData = $.grep(arrMeetingAcceptances, function (element, index) {
                return index !== objIndex;
            });

            arrMeetingAcceptances = returnedData;
        }
    }

    $("#AcceptedMeetings").val(JSON.stringify(arrMeetingAcceptances));
}

function PrepareRequestDatesModal(meetingType) {
    var modalTitle = "";

    if (meetingType == "Preliminary") {
        modalTitle = "Preliminary Meeting Date Options";
    }
    else {
        modalTitle = "Request " + meetingType + " Meeting Date";
    }
    $("#meetingDateOptionsTitle").html(modalTitle);
    $("#MeetingDateOptions").modal("show");
}

