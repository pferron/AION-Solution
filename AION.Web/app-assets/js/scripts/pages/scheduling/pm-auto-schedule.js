
function AutoSchedule(autoscheduletype) {
    var url = "/Scheduling/GetAutoSchedulePreliminaryMeetingData";

    var o = buildPreliminaryMeetingsParms();

    sendRequest(o, url);
}

function autoSchedulePlanReview() {
    var url = getURLByType(1);

    var o = getObjByType(1);

    var prod = $("#plansreadyondate").val();
    var sad = $("#scheduleafterdate").val();
    if (o.IsFutureCycle == "True") {
        if (!sad) {
            openWarning("Schedule After Date is required for future plan review scheduling.", true);
            return;
        }
    }
    else {
        if (!prod) {
            openWarning("Plans Ready On Date is required for auto scheduling.  Please fill this out on the Project Details page.", true);
            return;
        }
    }

    sendRequest(o, url);
}

function autoSchedulePreliminaryMeeting() {
    var url = getURLByType(2);

    var o = getObjByType(2);

    sendRequest(o, url);
}

function sendRequest(o, url) {
    if (o != null && url != null) {
        openSuccess("Preparing auto schedule values...", false)
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            statusCode: {
                404: function () {
                    openError("page not found", true);
                    closeSuccess();
                }
            },
            success: function (response) {
                setAutoScheduledFromResponse(response);
                setMeetingAutoScheduledAuditFromResponse(response);
                closeSuccess();
            },
            failure: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openError("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openError("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }

}
function setAutoScheduledFromResponse(ret) {
    //jcl LES-186 set -1 to 0 so UI retains error handling
    //  only change the ones that didn't already have values for activate NA review
    setReviewerValue($("#DrpDnScheduleBuild"), ret.BuildingUserID);
    setReviewerValue($("#DrpDnScheduleElectric"), ret.ElectricUserID);
    setReviewerValue($("#DrpDnScheduleMech"), ret.MechUserID);
    setReviewerValue($("#DrpDnSchedulePlumb"), ret.PlumbUserID);
    setReviewerValue($("#DrpDnScheduleZone"), ret.ZoneUserID);
    setReviewerValue($("#DrpDnScheduleFire"), ret.FireUserID);
    setReviewerValue($("#DrpDnScheduleBackFlow"), ret.BackFlowUserID);
    setReviewerValue($("#DrpDnScheduleFood"), ret.FoodServiceUserID);
    setReviewerValue($("#DrpDnSchedulePool"), ret.PoolUserID);
    setReviewerValue($("#DrpDnPScheduleLodge"), ret.FacilityUserID);
    setReviewerValue($("#DrpDnScheduleDayCare"), ret.DayCareUserID);

    //set date and times
    setDateValue("startdatesel", ret.ScheduleStartTxt);
    setTimeValue("starttimesel", ret.ScheduleStartTxt);
    setTimeValue("endtimesel", ret.ScheduleEndTxt);

}

function setMeetingAutoScheduledAuditFromResponse(ret) {
    $("#auditAutoScheduleButton").val(true);
    $("#auditBuildingUserID").val(ret.BuildingUserID);
    $("#auditElectricUserID").val(ret.ElectricUserID);
    $("#auditMechUserID").val(ret.MechUserID);
    $("#auditPlumbUserID").val(ret.PlumbUserID);
    $("#auditZoneUserID").val(ret.ZoneUserID);
    $("#auditFireUserID").val(ret.FireUserID);
    $("#auditFoodServiceUserID").val(ret.FoodServiceUserID);
    $("#auditPoolUserID").val(ret.PoolUserID);
    $("#auditFacilityUserID").val(ret.FacilityUserID);
    $("#auditDayCareUserID").val(ret.DayCareUserID);
    $("#auditBackFlowUserID").val(ret.BackFlowUserID);

    $("#auditSelectedStartDateTime").val(ret.ScheduleStartTxt);
    $("#auditSelectedEndDateTime").val(ret.ScheduleEndTxt);
    $("#auditScheduleDate").val(ret.ScheduleStartTxt);

}


function buildPreliminaryMeetingsParms() {
    var obj = new Object();
    obj.AccelaProjectIDRef = $("#AccelaProjectRefId").val();
    obj.RecIdTxt = $("#recidtxt").val();

    obj.SuggestedDate1 = suggestedDate1;
    obj.SuggestedDate2 = suggestedDate2;
    obj.SuggestedDate3 = suggestedDate3;

    return obj;
}

function setReviewerValue(obj, newval) {
    obj.val(newval);
}

function setDateValue(objid, newval) {
    var startdateobj = $("#" + objid);
    startdateobj.datepicker("setDate", newval);
}
function setTimeValue(objid, newval) {
    //$("#" + objid).timepicker('setTime', new Date(newval));

}