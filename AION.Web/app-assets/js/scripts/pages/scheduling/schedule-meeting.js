//****** onload ********************************///
$(function () {
    //init action buttons on _Actions.cshtml
    $("#btnSubmitFMA").on("click", function () {
        PrepareSubmitData();
    });

    $("#btnCancelFMA").on("click", function () {
        ReturnToDashboard();
    });

    $("#btnSaveFMA").on("click", function () {
        PrepareSaveData();
    });

    //init error div
    $("div.error").html("");
    $("div.error").hide();


    /************************Meeting Room ********************************/
    $(".meetingroomnamelabel").text("- " + $("#meetingroomname").val() + " -");

    /****************************Meeting Date***********************/

    var setDate = $("#hScheduleDate").val() == "" ? "" : $("#hScheduleDate").val();

 
    /******************* Validator *******************/
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "");

    var isValidDate = function (dateStr) {
        try {
            if ($.datepicker.parseDate("mm/dd/yy", dateStr))
                return true;
            return false;
        } catch (e) {
            //parse failed
            return false;
        }
    };

    $.validator.addMethod("isValidDate", function (value, element) {
        return isValidDate(value);
    }, "Enter a valid date MM/DD/YYYY");

    $('#SchedulingForm').validate({
        debug: false,
        ignore: ".ignore",
        rules: {
            ScheduleDate: { required: true, isValidDate: true },
            StartTime: { required: true },
            EndTime: { required: true },
            MeetingRoomRefIDSelected: { required: true, valueNotEquals: "0" }
        },
        messages: {
            MeetingRoomRefIDSelected: "Meeting Room is required.",
            ScheduleDate: "Date is required.",
            StartTime: "Start Time is required.",
            EndTime: "End Time is required.",
        },
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {
                var message = errors == 1
                    ? 'You missed 1 field. It has been highlighted'
                    : 'You missed ' + errors + ' fields. They have been highlighted';
                $("div.error").html(message);
                $("div.error").show();
            } else {
                $("div.error").hide();
            }
        },
        submitHandler: function (form) {
            var savemessage = "Saving Facilitator Meeting Appointment ...";
            var submitmessage = "Submitting Facilitator Meeting Appointment ...";
            var message = savemessage;
            var isSubmit = $("#IsSubmit").val();
            if (isSubmit == "true") {
                message = submitmessage;
            }
            openInProgress(message, false);

            $("#btnSubmit").addClass("disabled");
            $("#btnSave").addClass("disabled");
            $("div.error").html("");
            $("div.error").hide();
            $("div.error").html("");
            $("div.error").hide();
            form.submit();
        }
    });

    /***************   end validator *****************/
    var attendees = $("#hAttendeeIds").val();
    var array = attendees.split(',');

    var previousReviewer;

    $(".scheduledreviewer").on('focus', function () {
        previousReviewer = this.value;
    }).on("change", function () {
        var attIndex = array.findIndex(x => x === previousReviewer);

        var data = $.grep(array, function (n, i) {
            return i != attIndex;
        });
        array = data;

        var attendeesString = array.join();
        $("#AttendeeIds").val(attendeesString);
    });
});
//************* end onload **********************/

function PrepareSaveData() {
    $(".meetingroomrefid").rules("remove", "required");
    $(".meetingroomrefid").rules("remove", "valueNotEquals");

    $("#IsSubmit").val("false");
    $("#SchedulingForm").trigger("submit");
};

function PrepareSubmitData() {
    if (CheckSelectedReviewers() == true) {
        ValidateMeetingRoomAvailability();

        if ($('#SchedulingForm').valid()) {
            $("div.error").html("");
            $("div.error").hide();
            $("#SchedulingForm").trigger("submit");
        }

    } else {
        openWarning("At least one reviewer must be selected.", true);
    }

}

function ReturnToDashboard() {
    window.location = "/Scheduling/SchedulingDashboard";
}

function GetAutoScheduleValues() {
    var args = new Object();
    args.PerformAutoEstimation = "True";
    args.ProjectId = $("#AccelaProjectRefId").val();
    args.LoggedInUserEmail = $("#LoggedInUserEmail").val();

    args.MeetingTypeDesc = $("#MeetingTypeDesc").val();

    args.BuildingUserID = $("#DrpDnScheduleBuild").val();
    args.ElectricUserID = $("#DrpDnScheduleElectric").val();
    args.MechUserID = $("#DrpDnScheduleMech").val();
    args.PlumbUserID = $("#DrpDnSchedulePlumb").val();
    args.ZoneUserID = $("#DrpDnScheduleZone").val();
    args.FireUserID = $("#DrpDnScheduleFire").val();
    args.BackFlowUserID = $("#DrpDnScheduleBackFlow").val();
    args.FoodServiceUserID = $("#DrpDnScheduleFood").val();
    args.PoolUserID = $("#DrpDnSchedulePool").val();
    args.FacilityUserID = $("#DrpDnPScheduleLodge").val();
    args.DayCareUserID = $("#DrpDnScheduleDayCare").val();

    args.DurationHours = $("#DurationHours").val();
    args.DurationMinutes = $("#DurationMinutes").val();

    args.RequestedDate1 = $("#RequestedDate1").val();
    args.RequestedDate2 = $("#RequestedDate2").val();
    args.RequestedDate3 = $("#RequestedDate3").val();

    var atLeastOneReviewerSelected = CheckSelectedReviewers();

    if (atLeastOneReviewerSelected) {
        var url = "/Scheduling/ScheduleMeeting";
        if (args != null) {
            openInProgress("Preparing auto schedule values...", false)
            $.ajax
                ({
                    method: "POST",
                    datatype: "json",
                    url: url,
                    data: args,
                    statusCode: {
                        404: function () {
                            openError("page not found", true);
                            closeSuccess();
                        }
                    },
                    success: function (response) {
                        closeSuccess();
                        $("#auditAutoScheduleButton").val(true);
                    },
                    failure: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openError("Error: " + textStatus + " : " + errorThrown, true); },
                    error: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openError("Error: " + textStatus + " : " + errorThrown, true); }
                })
                .done(function (response, status, jqxhr) {
                    $("body").html(response);
                });
        }
    }
    else {
        openWarning("At least one reviewer must be selected for auto scheduling.", true);
    }
}

function CheckSelectedReviewers() {
    var selected = false;
    $(".scheduledreviewer").each(function () {
        if ($(this).val() != "0") {
            selected = true;
        }
    });
    return selected;
}
