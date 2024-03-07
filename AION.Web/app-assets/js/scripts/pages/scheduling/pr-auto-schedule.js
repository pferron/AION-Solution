
function AutoSchedule(autoscheduletype) {

    var url = getURLByType(autoscheduletype);

    var o = buildPlanReviewParms();
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
    if (o != null && url != null) {
        openSuccess("Auto Scheduling Plan Review ...", false)
        $.ajax({
            type: "POST",
            url: url,
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
                setAutoScheduledFromResponse(response);
                setPlanReviewAutoScheduledAuditFromResponse(response);
                closeSuccess();
            },
            failure: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openWarning("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { closeSuccess(); openWarning("Error: " + textStatus + " : " + errorThrown, true); }
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

    //if not 0/-1, then set the dates, otherwise, set them to null
    setDateValue("buildstartdatesel", "buildenddatesel", ret.BuildingUserID, ret.BuildingScheduleStartTxt, ret.BuildingScheduleEndTxt);
    setDateValue("electstartdatesel", "electenddatesel", ret.ElectricUserID, ret.ElectricScheduleStartTxt, ret.ElectricScheduleEndTxt);
    setDateValue("mechastartdatesel", "mechaenddatesel", ret.MechUserID, ret.MechScheduleStartTxt, ret.MechScheduleEndTxt);
    setDateValue("plumbstartdatesel", "plumbenddatesel", ret.PlumbUserID, ret.PlumbScheduleStartTxt, ret.PlumbScheduleEndTxt);
    setDateValue("zonestartdatesel", "zoneenddatesel", ret.ZoneUserID, ret.ZoneScheduleStartTxt, ret.ZoneScheduleEndTxt);
    setDateValue("firestartdatesel", "fireenddatesel", ret.FireUserID, ret.FireScheduleStartTxt, ret.FireScheduleEndTxt);
    setDateValue("poolstartdatesel", "poolenddatesel", ret.PoolUserID, ret.PoolScheduleStartTxt, ret.PoolScheduleEndTxt);
    setDateValue("facilstartdatesel", "facilenddatesel", ret.FacilityUserID, ret.FacilityScheduleStartTxt, ret.FacilityScheduleEndTxt);
    setDateValue("daycstartdatesel", "daycenddatesel", ret.DayCareUserID, ret.DayCareScheduleStartTxt, ret.DayCareScheduleEndTxt);
    setDateValue("backfstartdatesel", "backfenddatesel", ret.BackFlowUserID, ret.BackFlowScheduleStartTxt, ret.BackFlowScheduleEndTxt);
    setDateValue("foodstartdatesel", "foodenddatesel", ret.FoodServiceUserID, ret.FoodScheduleStartTxt, ret.FoodScheduleEndTxt);

    //jcl LES-3407 Set pool for Zoning
    if (ret.ZoneIsPool == true) {
        //init pool btn
        setPool('zonepooltxt', 'zonepool', true, 'zonestartendsel', 'btnzonepool');
    }
}

function getURLByType(autoscheduletype) {
    switch (autoscheduletype) {
        //Schedule Plan Review
        case 1:
            return "/Scheduling/GetAutoSchedulePlanReviewData"
        default:
            return null;
    }
}

function buildPlanReviewParms() {
    var obj = new Object();
    obj.AccelaProjectIDRef = $("#AccelaProjectRefId").val();
    obj.ProjectID = $("#ProjectId").val();

    obj.BuildingIsPool = $("#buildpool").val();
    obj.ElectricIsPool = $("#electpool").val();
    obj.MechIsPool = $("#mechapool").val();
    obj.PlumbIsPool = $("#plumbpool").val();
    obj.ZoneIsPool = $("#zonepool").val();
    obj.FireIsPool = $("#firepool").val();
    obj.FoodServiceIsPool = $("#foodpool").val();
    obj.PoolIsPool = $("#poolpool").val();
    obj.FacilityIsPool = $("#facilpool").val();
    obj.DayCareIsPool = $("#daycpool").val();
    obj.BackFlowIsPool = $("#backfpool").val();

    obj.BuildingUserID = $("#DrpDnScheduleBuild").val();
    obj.ElectricUserID = $("#DrpDnScheduleElectric").val();
    obj.MechUserID = $("#DrpDnScheduleMech").val();
    obj.PlumbUserID = $("#DrpDnSchedulePlumb").val();
    obj.ZoneUserID = $("#DrpDnScheduleZone").val();
    obj.FireUserID = $("#DrpDnScheduleFire").val();
    obj.FoodServiceUserID = $("#DrpDnScheduleFood").val();
    obj.PoolUserID = $("#DrpDnSchedulePool").val();
    obj.FacilityUserID = $("#DrpDnPScheduleLodge").val();
    obj.DayCareUserID = $("#DrpDnScheduleDayCare").val();
    obj.BackFlowUserID = $("#DrpDnScheduleBackFlow").val();

    obj.Cycle = $("#CycleNbr").val();

    obj.BuildingIsFIFO = "";
    obj.ElectricIsFIFO = "";
    obj.MechIsFIFO = "";
    obj.PlumbIsFIFO = "";
    obj.ZoneIsFIFO = "";
    obj.FireIsFIFO = "";
    obj.FoodServiceIsFIFO = "";
    obj.PoolIsFIFO = "";
    obj.FacilityIsFIFO = "";
    obj.DayCareIsFIFO = "";
    obj.BackFlowIsFIFO = "";

    obj.ScheduleAfterDate = $("#scheduleafterdate").val();
    obj.PlansReadyOnDate = $("#plansreadyondate").val();
    obj.IsFutureCycle = $("#isfuturecycle").val();
    obj.IsCycleComparison = $("#isCycleComparison").val();
    obj.IsAdjustHours = $("#isadjusthours").val();
    obj.isActivateNAReview = isActivateNAReview;

    GetTheHours(obj);

    obj.isSelfSchedule = "";
    obj.selfScheduleDate = "";

    obj.recidtxt = $("#recidtxt").val();
    return obj;
}

function GetTheHours(obj) {
    if (obj.IsFutureCycle == "True") {
        obj.UpdatedBuildingHours = $("#hoursproposedbuilding").val();
        obj.UpdatedElectricHours = $("#hoursproposedelectric").val();
        obj.UpdatedMechHours = $("#hoursproposedmech").val();
        obj.UpdatedPlumbHours = $("#hoursproposedplumb").val();
        obj.UpdatedDayCareHours = $("#hoursproposeddaycare").val();
        obj.UpdatedFoodHours = $("#hoursproposedfood").val();
        obj.UpdatedPoolHours = $("#hoursproposedpool").val();
        obj.UpdatedLodgeHours = $("#hoursproposedlodge").val();
        obj.UpdatedBackflowHours = $("#hoursproposedbackflow").val();
        obj.UpdatedZoneHours = $("#hoursproposedzoning").val();
        obj.UpdatedFireHours = $("#hoursproposedfire").val();

    } else {
        //jcl LES-186 get the estimation hours in case of na activation
        obj.UpdatedBuildingHours = $("#txtHoursBuilding").val();
        obj.UpdatedElectricHours = $("#txtHoursElectic").val();
        obj.UpdatedMechHours = $("#txtHoursMech").val();
        obj.UpdatedPlumbHours = $("#txtHoursPlumb").val();
        obj.UpdatedDayCareHours = $("#txtHoursDayCare").val();
        obj.UpdatedFoodHours = $("#txtHoursFood").val();
        obj.UpdatedPoolHours = $("#txtHoursPool").val();
        obj.UpdatedLodgeHours = $("#txtHoursLodge").val();
        obj.UpdatedBackflowHours = $("#txtHoursBackFlow").val();
        obj.UpdatedZoneHours = $("#txtHoursZoning").val();
        obj.UpdatedFireHours = $("#txtHoursFire").val();

    }
}

/// jcl LES-186 if this is ActivateNAReview
///     only change the dropdowns that weren't originally estimated
function setReviewerValue(obj, newval) {
    var objid = obj.attr("id");
    if (isActivateNAReview) {
        //if this obj wasn't part of the original estimation
        if (originalNAEstimate.indexOf(objid) == -1) {
            obj.val(newval);
        }
    } else {
        obj.val(newval);
    }
}

/// jcl LES-186 set the date values based on the returned autoschedule
function setDateValue(startobjid, endobjid, reviewerid, newstartval, newendval) {
    var startdateobj = $("#" + startobjid);
    var enddateobj = $("#" + endobjid);
    if (reviewerid == "0" || reviewerid == "-1") {
        newstartval = "";
        newendval = "";
    }
    if (isActivateNAReview) {
        //if this object wasn't part of the original estimation
        if (originalNAEstimate.indexOf(startobjid) == -1 && originalNAEstimate.indexOf(endobjid) == -1) {
            startdateobj.datepicker("setDate", newstartval);
            enddateobj.datepicker("setDate", newendval);
        }
    } else {
        startdateobj.datepicker("setDate", newstartval);
        enddateobj.datepicker("setDate", newendval);
    }
}