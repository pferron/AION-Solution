
//****** onload ********************************///
$(function () {
    $(".reviewerddl").attr("disabled", true);

    showStep1();
    //init action buttons on _Actions.cshtml
    $("#btnSave").on("click", function () {
        SaveData();
    });

    $("#btnSubmit").on("click", function () {
        SubmitData();
    });

    $("#btnCancel").on("click", function () {
        ReturnToDashboard();
    });

    $("#btnSubmitPR").on("click", function () {
        $("#SchedulingForm").trigger("submit");
    });

    //init the back button on notes partial
    $("#btnBack").on("click", function () {
        $("div.error").html("");
        $("div.error").hide();
        //open schedule notes
        $("#sectionPlanReview").collapse("show");
        $("#sectionSchedulingNotes").collapse("hide");

    });

    /******************** Init Manual Schedule button ********************/
    $(".manschedbtn").on("click", function () {
        //run the manual schedule stuff here
        getManualSchedule();
    });
    /*******************Init errors on tabs ******************************/
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").hide();
    $("div.error").html("");

    /************************Meeting Room ********************************/
    $(".meetingroomnamelabel").text(" - " + $("#meetingroomname").val() + " - ");

    /******************* Validator *******************/
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "");

    $.validator.addMethod("reqselectedreviewer",
        function (value, element) {
            if (value == "0")
                return false;
            else
                return true;
        },
        "Please select a value");

    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: true
    });

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
            MeetingRoomRefIDSelected: { required: true, valueNotEquals: "0", valueNotEquals: "-1" },
            AssignedFacilitator: { required: true, valueNotEquals: "-1" }
        },
        messages: {
            MeetingRoomRefIDSelected: "Meeting Room is required.",
            AssignedFacilitator: "Facilitator is required.",
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
            var message = "Saving Express Meeting Appointment ...";
            var submitmessage = "Submitting Express Meeting Appointment ...";
            var isSubmit = $("#IsSubmit").val();
            if (isSubmit == "true")
                message = submitmessage;
            openSuccess(message, false);
            $("#btnSubmit").addClass("disabled");
            $("#btnSave").addClass("disabled");
            $("div.error").html("");
            $("div.error").hide();
            form.submit();
        }
    });
    /************ end validator **********************/
});

/***********************************************************/

function GetAutoScheduleValues() {
    var args = new Object();
    args.PerformAutoEstimation = "True";
    args.ProjectId = $("#AccelaProjectRefId").val();
    args.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    args.RecIdTxt = $("#recidtxt").val();
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

    var url = "/Scheduling/ScheduleExpressMeeting";
    if (args != null) {
        openSuccess("Preparing auto schedule values...", false)
        $.ajax
            ({
                method: "POST",
                datatype: "json",
                url: url,
                data: args,
                statusCode: {
                    404: function () {
                        openWarning("page not found", true);
                        closeSuccess();
                    }
                },
                success: function (response) {
                    closeSuccess();
                },
                failure: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openWarning("Error: " + textStatus + " : " + errorThrown, true); },
                error: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openWarning("Error: " + textStatus + " : " + errorThrown, true); }
            })
            .done(function (response, status, jqxhr) {
                $("body").html(response);
                $("#auditAutoScheduleButton").val(true);
            });

    }
}

function SaveData() {
    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: false
    });
    $("#startdatesel").rules("remove", "required");
    $("#startdatesel").rules("remove", "isValidDate");
    $("#starttimesel").rules("remove", "required");
    $("#endtimesel").rules("remove", "required");
    $("#meetingroomrefid").rules("remove", "required");
    $("#meetingroomrefid").rules("remove", "valueNotEquals");
    $("#asndfacilitator").rules("remove", "required");
    $("#asndfacilitator").rules("remove", "valueNotEquals");

    $("#IsSubmit").val("false");
    $("#SchedulingForm").trigger("submit");
};
function SubmitData() {
    //reset previous errors
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").hide();
    $("div.error").html("");
    //if the form is valid
    if ($('#SchedulingForm').valid()) {
        $("#SchedulingForm").trigger("submit");
        $("div.error").html("");
        $("div.error").hide();
        //open schedule notes
        $("#sectionPlanReview").collapse("hide");
        $("#sectionSchedulingNotes").collapse("show");
        showStep2();
    }
}

function ReturnToDashboard() {
    window.location = "/Scheduling/SchedulingDashboard";
}

function getManualSchedule() {
    //reset previous errors
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").hide();
    $("div.error").html("");

    var seldate = $("#startdatesel").val();
    var starttime = $("#starttimesel").val();
    var endtime = $("#endtimesel").val();
    if (seldate == '' || starttime == '' || endtime == '') {
        openSuccess('Cannot decide meeting availablity without date and time range. Select date and time first.', true);
        return false;
    }

    getManuallyScheduled();

}
function setVal(id, val) {
    //if the original id is -1, don't change it.
    if ($("#" + id).val() != -1)
        $("#" + id).val(val);
}
function setValName(id, val) {
    //if the original id is -1, don't change it.
    if ($("#" + id).val() != "NA")
        $("#" + id).val(val);
}
function setManuallyScheduled(response) {
    //var jsonobj = {
    //    "scheduledReviewerBuilding": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerElectrical": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerMechanical": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerPlumbing": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerZone": { "reviewername": "Holli Redner", "reviewerid": "106" },
    //    "scheduledReviewerFire": { "reviewername": "Ginger Adams", "reviewerid": "109" },
    //    "scheduledReviewerBackflow": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerFood": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerPool": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerFacilities": { "reviewername": "NA", "reviewerid": "-1" },
    //    "scheduledReviewerDayCare": { "reviewername": "NA", "reviewerid": "-1" },
    //    "meetingRoomRefID": "1",
    //    "meetingRoomName": "Woodsonbargdorflam"
    //};
    //set the hidden fields with the meeting room id and name
    $(".meetingroomrefid").val(response.meetingRoomRefID);
    $(".meetingroomname").val(response.meetingRoomName);
    $(".meetingroomnamelabel").text("-" + response.meetingRoomName + "-");

    initMeetingTable();
    $.each(response, function (key, val) {
        if (key != "MeetingRoomRefID" && key != "MeetingRoomName") {

            if (val != null && val.reviewerid != null && val.reviewerid != '') {
                //set the hidden id linked to Model
                setVal(key, val.reviewerid);
                //set the name for view only
                setValName(key + "Name", val.reviewername);

            }
        }
    });

}
//Sends Ajax request to get the manual schedule data
function getManuallyScheduled() {
    var o = new Object();
    o.wrkrId = $("#LoggedInUser_ID").val();
    o.selDate = $("#startdatesel").val();
    o.selStartTime = $("#starttimesel").val();
    o.selEndTime = $("#endtimesel").val();
    o.accelaProjectIdRef = $("#Project_AccelaProjectRefId").val();
    o.recidtxt = $("#recidtxt").val();
    o.cycle = $("#cycleNumber").val();
    if (o != null) {
        openInProgress("Manual Scheduling In Progress ...", false)
        $.ajax({
            type: "POST",
            url: "/Scheduling/GetManuallyScheduledExpressData",
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            statusCode: {
                404: function () {
                    openWarning("page not found", true);
                    closeSuccess();
                }
            },
            success: function (response) {
                closeSuccess();
                if (response.errorMessage != null && response.errorMessage != "") {
                    openWarning(response.errorMessage, true);
                }
                setManuallyScheduled(response);
                $("#auditAutoScheduleButton").val(false);
            },
            failure: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openWarning("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openWarning("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}

function showStep1() {
    $(".step1btn").show();
    // hide step 2 buttons 
    $(".step2btn").hide();
}
function showStep2() {
    $(".step1btn").hide();
    //show the action buttons
    $(".step2btn").show();
    $("#sectionSchedulingNotes").collapse("show");
}
