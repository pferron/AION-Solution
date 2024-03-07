//**************************************
// checks the capacity of the plan reviewer
// returns pop up to ask if the user
// would like to continue despite over capacity
// or change the scheduled reviewer
//*****************************************

$(function () {

    /**************** init Modal Box *****************/
    var $modal = $('#dialog-modal-reviewercapacity');
    $modal.find('.modal-content')
        .resizable({
            handles: 'n, e, s, w, ne, sw, se, nw',
        })
        .draggable({
            handle: '.modal-header'
        });

});
function AcceptMeeting() {
    $('#dialog-modal-reviewercapacity').modal('hide');
    $("#SchedulingForm").trigger("submit");
}
function AcceptPlanReview() {
    //collapse step 1 page
    $("#sectionPlanReview").collapse("hide");

    if (isFifoProject == false) {
        // step 2 - show the notes
        $("#sectionSchedulingNotes").collapse("show");
        $('#dialog-modal-reviewercapacity').modal('hide');
    }
    else {
        //if this is fifo, there is no step 2 so submit the page
        $('#dialog-modal-reviewercapacity').modal('hide');
        $("#SchedulingForm").trigger("submit");
    }
}
function PrelimReviewerCapacitySearch() {

    //get the plan reviewers
    //get the start date
    //get the end date
    //send ajax request to get availability
    //endpoint expects a list of string so make these singles into an array
    //need list of reviewer,startdate,enddate
    var vmlist = new Object();
    var objlist = [];

    //determine which are blank and do not add those objects
    //get the prelim date and time
    var scheduledate = $("#startdatesel").val();
    var starttime = $("#starttimesel").val();
    var endtime = $("#endtimesel").val();
    var scheduledatestarttime = "";
    var scheduledateendtime = "";
    var hasDatetime = (scheduledate != "" && starttime != "" && endtime != "");
    //get the reviewers
    var obj = new Object();
    if (hasDatetime) {

        scheduledatestarttime = scheduledate + " " + starttime;
        scheduledateendtime = scheduledate + " " + endtime;

        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleBuild").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.ElecReviewerSelected = [$("#DrpDnScheduleElectric").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.MechReviewerSelected = [$("#DrpDnScheduleMech").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.PlumReviewerSelected = [$("#DrpDnSchedulePlumb").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.ZoniReviewerSelected = [$("#DrpDnScheduleZone").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FireReviewerSelected = [$("#DrpDnScheduleFire").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BackReviewerSelected = [$("#DrpDnScheduleBackFlow").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FoodReviewerSelected = [$("#DrpDnScheduleFood").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.PoolReviewerSelected = [$("#DrpDnSchedulePool").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FaciReviewerSelected = [$("#DrpDnPScheduleLodge").val()] || [];
        objlist.push(obj);

        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.DayCReviewerSelected = [$("#DrpDnScheduleDayCare").val()] || [];
        objlist.push(obj);
    }

    //ScheduleCapacityListViewModel
    //logged in user email required to get Perms
    vmlist.ScheduleCapacityList = objlist;
    vmlist.LoggedInUserEmail = $("#LoggedInUserEmail").val();

    //indicate this is a reviewer search
    if (vmlist != null && vmlist.ScheduleCapacityList != null && vmlist.ScheduleCapacityList.length > 0) {
        openInProgress("Searching...", false)
        $.ajax({
            type: "POST",
            url: "/ScheduleCapacity/ReviewerCapacitySearch",
            data: vmlist,
            statusCode: {
                404: function () {
                    openError("page not found", true);
                }
            },
            success: function (response) {
                closeSuccess();
                buildResultTablePrelim(response);
                var icount = response.length;
                if (icount > 0) {
                    $('#dialog-modal-reviewercapacity').modal('show');

                } else {
                    $('#SchedulingForm').trigger("submit");
                }
            },
            failure: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); }
        });
    } else {
        openError("error", true);
    }

}

function buildResultTablePrelim(response) {
    var message = "";
    //start with a blank table
    $("#capacitysearchlist > tbody").html("");
    var dataobj = response;
    $.each(dataobj, function (i, data) {
        message += "<tr>";
        message += "<td>";
        message += data.ReviewerName;
        message += "</td>";
        message += "<td>";
        message += data.MeetingName;
        message += "</td>";
        message += "<td>";
        message += data.MeetingBeginDtTm;
        message += "</td>";
        message += "<td>";
        message += data.MeetingEndDtTm;
        message += "</td>";
        message += "<td>";
        message += data.MeetingTypeName;
        message += "</td>";
        message += "</tr>";
        $('#capacitysearchlist > tbody:last-child').append(message);
    });
    return message;
}

//************ ReviewerCapacitySearch.js ***************/
function ReviewerCapacitySearch() {
    //get the plan reviewers
    //get the start date
    //get the end date
    //send ajax request to get availability
    //endpoint expects a list of string so make these singles into an array
    //need list of reviewer,startdate,enddate
    var vmlist = new Object();
    var objlist = [];

    vmlist.AccelaProjectRefId = $("#AccelaProjectRefId").val();
    vmlist.ProjectId = $("#ProjectId").val();
    vmlist.Cycle = $("#CycleNbr").val();
    vmlist.BuildingIsPool = $("#buildpool").val();
    vmlist.ElectricIsPool = $("#electpool").val();
    vmlist.MechIsPool = $("#mechapool").val();
    vmlist.PlumbIsPool = $("#plumbpool").val();
    vmlist.ZoneIsPool = $("#zonepool").val();
    vmlist.FireIsPool = $("#firepool").val();
    vmlist.FoodServiceIsPool = $("#foodpool").val();
    vmlist.PoolIsPool = $("#poolpool").val();
    vmlist.FacilityIsPool = $("#facilpool").val();
    vmlist.DayCareIsPool = $("#daycpool").val();
    vmlist.BackFlowIsPool = $("#backfpool").val();

    vmlist.BuildingUserID = $("#DrpDnScheduleBuild").val();
    vmlist.ElectricUserID = $("#DrpDnScheduleElectric").val();
    vmlist.MechUserID = $("#DrpDnScheduleMech").val();
    vmlist.PlumbUserID = $("#DrpDnSchedulePlumb").val();
    vmlist.ZoneUserID = $("#DrpDnScheduleZone").val();
    vmlist.FireUserID = $("#DrpDnScheduleFire").val();
    vmlist.FoodServiceUserID = $("#DrpDnScheduleFood").val();
    vmlist.PoolUserID = $("#DrpDnSchedulePool").val();
    vmlist.FacilityUserID = $("#DrpDnPScheduleLodge").val();
    vmlist.DayCareUserID = $("#DrpDnScheduleDayCare").val();
    vmlist.BackFlowUserID = $("#DrpDnScheduleBackFlow").val();

    //determine which are blank and do not add those objects

    var obj = new Object();
    if ($("#buildstartdatesel").val() != "" && $("#buildenddatesel").val() != "") {
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#buildstartdatesel").val();
        obj.EndDate = $("#buildenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleBuild").val()] || [];
        objlist.push(obj);
    }

    if ($("#electstartdatesel").val() != "" && $("#electenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#electstartdatesel").val();
        obj.EndDate = $("#electenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.ElecReviewerSelected = [$("#DrpDnScheduleElectric").val()] || [];
        objlist.push(obj);
    }

    if ($("#mechastartdatesel").val() != "" && $("#mechaenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#mechastartdatesel").val();
        obj.EndDate = $("#mechaenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.MechReviewerSelected = [$("#DrpDnScheduleMech").val()] || [];
        objlist.push(obj);
    }

    if ($("#plumbstartdatesel").val() != "" && $("#plumbenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#plumbstartdatesel").val();
        obj.EndDate = $("#plumbenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.PlumReviewerSelected = [$("#DrpDnSchedulePlumb").val()] || [];
        objlist.push(obj);
    }

    if ($("#zonestartdatesel").val() != "" && $("#zoneenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#zonestartdatesel").val();
        obj.EndDate = $("#zoneenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.ZoniReviewerSelected = [$("#DrpDnScheduleZone").val()] || [];
        objlist.push(obj);
    }

    if ($("#firestartdatesel").val() != "" && $("#fireenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#firestartdatesel").val();
        obj.EndDate = $("#fireenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FireReviewerSelected = [$("#DrpDnScheduleFire").val()] || [];
        objlist.push(obj);
    }

    if ($("#backfstartdatesel").val() != "" && $("#backfenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#backfstartdatesel").val();
        obj.EndDate = $("#backfenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BackReviewerSelected = [$("#DrpDnScheduleBackFlow").val()] || [];
        objlist.push(obj);
    }

    if ($("#foodstartdatesel").val() != "" && $("#foodenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#foodstartdatesel").val();
        obj.EndDate = $("#foodenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FoodReviewerSelected = [$("#DrpDnScheduleFood").val()] || [];
        objlist.push(obj);
    }

    if ($("#poolstartdatesel").val() != "" && $("#poolenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#poolstartdatesel").val();
        obj.EndDate = $("#poolenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.PoolReviewerSelected = [$("#DrpDnSchedulePool").val()] || [];
        objlist.push(obj);
    }

    if ($("#facilstartdatesel").val() != "" && $("#facilenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#facilstartdatesel").val();
        obj.EndDate = $("#facilenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FaciReviewerSelected = [$("#DrpDnPScheduleLodge").val()] || [];
        objlist.push(obj);
    }

    if ($("#daycstartdatesel").val() != "" && $("#daycenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#daycstartdatesel").val();
        obj.EndDate = $("#daycenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.DayCReviewerSelected = [$("#DrpDnScheduleDayCare").val()] || [];
        objlist.push(obj);
    }

    //ScheduleCapacityListViewModel
    //logged in user email required to get Perms
    vmlist.ScheduleCapacityList = objlist;

    vmlist.BuildingHours = $("#hoursbuildingfinal").val();
    vmlist.ElectricHours = $("#hourselectricfinal").val();
    vmlist.MechanicalHours = $("#hoursmechfinal").val();
    vmlist.PlumbingHours = $("#hoursplumbfinal").val();
    vmlist.ZoningHours = $("#hourszoningfinal").val();
    vmlist.BackflowHours = $("#hoursbackflowfinal").val();
    vmlist.FoodHours = $("#hoursfoodfinal").val();
    vmlist.PoolHours = $("#hourspoolfinal").val();
    vmlist.FireHours = $("#hoursfirefinal").val();
    vmlist.LodgeHours = $("#hourslodgefinal").val();
    vmlist.DaycareHours = $("#hoursdaycarefinal").val();

    vmlist.PropertyType = $("#propertytype").val();

    vmlist.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    vmlist.RecIdTxt = $("#recidtxt").val();

    //indicate this is a reviewer search
    if (vmlist != null && vmlist.ScheduleCapacityList != null && vmlist.ScheduleCapacityList.length > 0) {
        openSuccess("Searching...", false)
        $.ajax({
            type: "POST",
            url: "/ScheduleCapacity/ReviewerCapacitySearchPlanReview",
            data: vmlist,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                closeSuccess();
                if (response.isActive == true) {
                    var message = getMessage(response);
                    $(".reviewercapacitymessage").html(message);
                    $('#dialog-modal-reviewercapacity').modal('show');
                } else {
                    $("#sectionPlanReview").collapse("hide");
                    showStep2();
                }
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    } else {
        //open schedule notes
        $("#sectionPlanReview").collapse("hide");
        showStep2();
    }

}

function showStep2() {
    $(".step1btn").hide();
    //show the action buttons
    $(".step2btn").show();
    $("#sectionSchedulingNotes").collapse("show");
}

function showStep1() {
    $(".step1btn").show();
    //Activate correct buttons
    // based on project statuses and state
    //if can schedule future cycle then activate button
    if ($("#hCanScheduleFutureCycle").val() == "false") {
        $("#schedulecyclebtn").hide();
    }

    //if has activate na review permission and this project has been scheduled then activate button
    if ($("#hIsNAActivate").val() == "false") {
        $("#activatenareviewbtn").hide();
    }

    if ($("#hIsCycleComparison").val() == "false") {
        $("#adjusthoursbtn").hide();
    }
    // hide step 2 buttons 
    $(".step2btn").hide();
}
function FIFOReviewerCapacitySearch() {
    //get the plan reviewers
    //get the start date
    //get the end date
    //send ajax request to get availability
    //endpoint expects a list of string so make these singles into an array
    //need list of reviewer,startdate,enddate
    var vmlist = new Object();
    var objlist = [];

    vmlist.AccelaProjectRefId = $("#AccelaProjectRefId").val();
    vmlist.ProjectId = $("#ProjectId").val();
    vmlist.Cycle = $("#CycleNbr").val();
    vmlist.BuildingIsPool = $("#buildpool").val();
    vmlist.ElectricIsPool = $("#electpool").val();
    vmlist.MechIsPool = $("#mechapool").val();
    vmlist.PlumbIsPool = $("#plumbpool").val();
    vmlist.ZoneIsPool = $("#zonepool").val();
    vmlist.FireIsPool = $("#firepool").val();
    vmlist.FoodServiceIsPool = $("#foodpool").val();
    vmlist.PoolIsPool = $("#poolpool").val();
    vmlist.FacilityIsPool = $("#facilpool").val();
    vmlist.DayCareIsPool = $("#daycpool").val();
    vmlist.BackFlowIsPool = $("#backfpool").val();

    vmlist.BuildingUserID = $("#DrpDnScheduleBuild").val();
    vmlist.ElectricUserID = $("#DrpDnScheduleElectric").val();
    vmlist.MechUserID = $("#DrpDnScheduleMech").val();
    vmlist.PlumbUserID = $("#DrpDnSchedulePlumb").val();
    vmlist.ZoneUserID = $("#DrpDnScheduleZone").val();
    vmlist.FireUserID = $("#DrpDnScheduleFire").val();
    vmlist.FoodServiceUserID = $("#DrpDnScheduleFood").val();
    vmlist.PoolUserID = $("#DrpDnSchedulePool").val();
    vmlist.FacilityUserID = $("#DrpDnPScheduleLodge").val();
    vmlist.DayCareUserID = $("#DrpDnScheduleDayCare").val();
    vmlist.BackFlowUserID = $("#DrpDnScheduleBackFlow").val();

    //determine which are blank and do not add those objects

    var obj = new Object();
    if ($("#buildstartdatesel").val() != "" && $("#buildenddatesel").val() != "") {
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#buildstartdatesel").val();
        obj.EndDate = $("#buildenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleBuild").val()] || [];
        objlist.push(obj);
    }

    if ($("#electstartdatesel").val() != "" && $("#electenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#electstartdatesel").val();
        obj.EndDate = $("#electenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.ElecReviewerSelected = [$("#DrpDnScheduleElectric").val()] || [];
        objlist.push(obj);
    }

    if ($("#mechastartdatesel").val() != "" && $("#mechaenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#mechastartdatesel").val();
        obj.EndDate = $("#mechaenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.MechReviewerSelected = [$("#DrpDnScheduleMech").val()] || [];
        objlist.push(obj);
    }

    if ($("#plumbstartdatesel").val() != "" && $("#plumbenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#plumbstartdatesel").val();
        obj.EndDate = $("#plumbenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.PlumReviewerSelected = [$("#DrpDnSchedulePlumb").val()] || [];
        objlist.push(obj);
    }

    if ($("#zonestartdatesel").val() != "" && $("#zoneenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#zonestartdatesel").val();
        obj.EndDate = $("#zoneenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.ZoniReviewerSelected = [$("#DrpDnScheduleZone").val()] || [];
        objlist.push(obj);
    }

    if ($("#firestartdatesel").val() != "" && $("#fireenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#firestartdatesel").val();
        obj.EndDate = $("#fireenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FireReviewerSelected = [$("#DrpDnScheduleFire").val()] || [];
        objlist.push(obj);
    }

    if ($("#backfstartdatesel").val() != "" && $("#backfenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#backfstartdatesel").val();
        obj.EndDate = $("#backfenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BackReviewerSelected = [$("#DrpDnScheduleBackFlow").val()] || [];
        objlist.push(obj);
    }

    if ($("#foodstartdatesel").val() != "" && $("#foodenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#foodstartdatesel").val();
        obj.EndDate = $("#foodenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FoodReviewerSelected = [$("#DrpDnScheduleFood").val()] || [];
        objlist.push(obj);
    }

    if ($("#poolstartdatesel").val() != "" && $("#poolenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#poolstartdatesel").val();
        obj.EndDate = $("#poolenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.PoolReviewerSelected = [$("#DrpDnSchedulePool").val()] || [];
        objlist.push(obj);
    }

    if ($("#facilstartdatesel").val() != "" && $("#facilenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#facilstartdatesel").val();
        obj.EndDate = $("#facilenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.FaciReviewerSelected = [$("#DrpDnPScheduleLodge").val()] || [];
        objlist.push(obj);
    }

    if ($("#daycstartdatesel").val() != "" && $("#daycenddatesel").val() != "") {
        obj = new Object();
        obj.IsReviewerCapacitySearch = true;
        obj.StartDate = $("#daycstartdatesel").val();
        obj.EndDate = $("#daycenddatesel").val();
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.DayCReviewerSelected = [$("#DrpDnScheduleDayCare").val()] || [];
        objlist.push(obj);
    }

    //ScheduleCapacityListViewModel
    //logged in user email required to get Perms
    vmlist.ScheduleCapacityList = objlist;

    vmlist.BuildingHours = $("#hoursbuildingfinal").val();
    vmlist.ElectricHours = $("#hourselectricfinal").val();
    vmlist.MechanicalHours = $("#hoursmechfinal").val();
    vmlist.PlumbingHours = $("#hoursplumbfinal").val();
    vmlist.ZoningHours = $("#hourszoningfinal").val();
    vmlist.BackflowHours = $("#hoursbackflowfinal").val();
    vmlist.FoodHours = $("#hoursfoodfinal").val();
    vmlist.PoolHours = $("#hourspoolfinal").val();
    vmlist.FireHours = $("#hoursfirefinal").val();
    vmlist.LodgeHours = $("#hourslodgefinal").val();
    vmlist.DaycareHours = $("#hoursdaycarefinal").val();

    vmlist.PropertyType = $("#propertytype").val();

    vmlist.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    //indicate this is a reviewer search
    if (vmlist != null && vmlist.ScheduleCapacityList != null && vmlist.ScheduleCapacityList.length > 0) {
        openSuccess("Searching...", false)
        $.ajax({
            type: "POST",
            url: "/ScheduleCapacity/ReviewerCapacitySearchPlanReview",
            data: vmlist,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                closeSuccess();
                if (response.isActive == true) {
                    var message = getMessage(response);
                    $(".reviewercapacitymessage").html(message);
                    $('#dialog-modal-reviewercapacity').modal('show');
                } else {
                    //submit form
                    $("#SchedulingForm").trigger("submit");
                }
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    } else {
        $("#SchedulingForm").trigger("submit");
    }

}

function getMessage(response) {
    var message = response.message + "<br/>";
    var dataobj = response.conflicts;
    $.each(dataobj, function (i, data) {

        message += data.ReviewerName;
        message += " ";
        message += data.MeetingName;
        message += " ";
        message += data.MeetingBeginDtTm;
        message += " ";
        message += data.MeetingEndDtTm;
        message += "<br/>";
    });
    return message;
}

//************ ReviewerCapacitySearchForMeeting.js ScheduleMeeting.cshtml ************************/

function ReviewerCapacityScheduleMeetingSearch() {
    //get the plan reviewers
    //get the start date
    //get the end date
    //send ajax request to get availability
    //endpoint expects a list of string so make these singles into an array
    //need list of reviewer,startdate,enddate
    var vmlist = new Object();
    var objlist = [];

    //determine which are blank and do not add those objects

    var obj = new Object();

    var scheduledate = $("#startdatesel").val();
    var starttime = $("#starttimesel").val();
    var endtime = $("#endtimesel").val();
    var scheduledatestarttime = "";
    var scheduledateendtime = "";
    var hasDatetime = (scheduledate != "" && starttime != "" && endtime != "");
    //get the reviewers
    var obj = new Object();
    if (hasDatetime) {

        scheduledatestarttime = scheduledate + " " + starttime;
        scheduledateendtime = scheduledate + " " + endtime;

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleBuild").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleElectric").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleMech").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnSchedulePlumb").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleZone").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleFire").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleBackFlow").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleFood").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnSchedulePool").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnPScheduleLodge").val()] || [];
        objlist.push(obj);

        obj = new Object();

        obj.StartDate = scheduledatestarttime;
        obj.EndDate = scheduledateendtime;
        obj.IsReviewerCapacitySearch = true;
        obj.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        obj.BldgReviewerSelected = [$("#DrpDnScheduleDayCare").val()] || [];
        objlist.push(obj);
    }


    //ScheduleCapacityListViewModel
    //logged in user email required to get Perms
    vmlist.ScheduleCapacityList = objlist;
    vmlist.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    vmlist.RecIdTxt = $("#recidtxt").val();
    //indicate this is a reviewer search
    if (vmlist != null && vmlist.ScheduleCapacityList != null && vmlist.ScheduleCapacityList.length > 0) {
        openSuccess("Searching...", false)
        $.ajax({
            type: "POST",
            url: "/ScheduleCapacity/ReviewerCapacitySearch",
            data: vmlist,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                var message = getMeetingMessage(response);
                closeSuccess();

                if (message != "") {
                    $(".reviewercapacitymessage").html(message);
                    $('#dialog-modal-reviewercapacity').modal('show');
                }
                else {
                    $("#SchedulingForm").trigger("submit");
                }
                closeSuccess();

            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    } else {
        $("#SchedulingForm").trigger("submit");
    }

}
function getMeetingMessage(response) {
    var message = "";
    var dataobj = response;
    $.each(dataobj, function (i, data) {

        message += data.ReviewerName;
        message += " ";
        message += data.MeetingName;
        message += " ";
        message += data.MeetingBeginDtTm;
        message += " ";
        message += data.MeetingEndDtTm;
        message += "<br/>";
    });
    return message;
}
