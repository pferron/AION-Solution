

//onload events
$(function () {
    //jcl LES-186 - set which hrs box shows based on NA
    hideHrsTextboxIfNA();

    $(".enddatesel").datepicker('option', 'minDate', "+1w");

    //hide the error notif on tabs
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").html("");
    $("div.error").hide();

    //scheduler dropdowns changed
    //if value is NA, pool button is disabled
    $(".scheduledreviewer").on("change",function () {
        var val = $(this).val();
        var id = $(this).attr("id");
        if (!$(this).hasClass("fifo")) initPoolBtn(id);
    })

    //init the back button on notes partial, goes back to step 1
    $("#btnBack").on("click",function () {
        $("div.error").html("");
        $("div.error").hide();
        //open schedule notes
        $("#sectionPlanReview").collapse("show");
        $("#sectionSchedulingNotes").collapse("hide");
        showStep1();

    });
});

//set pool indicator
function setPool(textboxid, hdnpoolid, ispool, datestartendclsid, btnid) {
    //console.log(ispool);
    if (ispool) {
        //show the pool txt
        $("#" + textboxid).show();
        //hide the date boxes
        $("." + datestartendclsid).hide();
        //make btn green btn-outline-primary
        $("#" + btnid).removeClass("btn-outline-chartreuse").addClass("btn-chartreuse");
        //set the value
        if ($("#" + hdnpoolid).val() != "true")
            $("#" + hdnpoolid).val("true");
    } else {
        //null is the same as false
        //hide the pool txt
        $("#" + textboxid).hide();
        //remove btn background
        $("#" + btnid).removeClass("btn-chartreuse").addClass("btn-outline-chartreuse");
        //show the date boxes
        $("." + datestartendclsid).show();
        //set the value
        if ($("#" + hdnpoolid).val() != "false")
            $("#" + hdnpoolid).val("false");
    }
}
function togglePool(textboxid, hdnpoolid, currentpoolval, datestartendclsid, btnid) {
    //toggle
    var ispool = false;
    if (currentpoolval == "false" || currentpoolval == "") ispool = true;
    setPool(textboxid, hdnpoolid, ispool, datestartendclsid, btnid)
}
function getPoolBtnId(drpdownid) {
    switch (drpdownid) {
        case "DrpDnScheduleBuild":
            return "btnbuildpool";
        case "DrpDnScheduleElectric":
            return "btnelectpool";
        case "DrpDnScheduleMech":
            return "btnmechapool";
        case "DrpDnSchedulePlumb":
            return "btnplumbpool";
        case "DrpDnScheduleZone":
            return "btnzonepool";
        case "DrpDnScheduleFire":
            return "btnfirepool";
        case "DrpDnScheduleBackFlow":
            return "btnbackfpool";
        case "DrpDnScheduleFood":
            return "btnfoodpool";
        case "DrpDnSchedulePool":
            return "btnpoolpool";
        case "DrpDnPScheduleLodge":
            return "btnfacilpool";
        case "DrpDnScheduleDayCare":
            return "btndaycpool";
        default:
    }
    return "";
}
function initPoolBtn(drpdownid) {
    var val = $("#" + drpdownid).val();
    //get the id so we can disable the correct button
    var id = getPoolBtnId(drpdownid);
    if (!canPoolForPoorPerformer) {
        $('#' + id).attr('disabled', 'disabled');
        $('#' + id).addClass("disabled");
    }
    else {
        if (val == -1) {
            $('#' + id).attr('disabled', 'disabled');
            $('#' + id).addClass("disabled");
        }
        else {
            $('#' + id).removeAttr('disabled');
            $('#' + id).removeClass("disabled");
        }
    }
}
function initPoolBtns() {
    initPoolBtn("DrpDnScheduleBuild");
    initPoolBtn("DrpDnScheduleElectric");
    initPoolBtn("DrpDnScheduleMech");
    initPoolBtn("DrpDnSchedulePlumb");
    initPoolBtn("DrpDnScheduleZone");
    initPoolBtn("DrpDnScheduleFire");
    initPoolBtn("DrpDnScheduleBackFlow");
    initPoolBtn("DrpDnScheduleFood");
    initPoolBtn("DrpDnSchedulePool");
    initPoolBtn("DrpDnPScheduleLodge");
    initPoolBtn("DrpDnScheduleDayCare");

}

function SaveData() {
    //removed requirement for schedulereviewer class
    //so saved can be not selected
    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: false
    });
    //remove required and is valid date
    // so saved can be blank
    $("#buildstartdatesel").rules("remove", "required");
    $("#buildstartdatesel").rules("remove", "isValidDate");
    $("#buildenddatesel").rules("remove", "required");
    $("#buildenddatesel").rules("remove", "isValidDate");
    $("#electstartdatesel").rules("remove", "required");
    $("#electstartdatesel").rules("remove", "isValidDate");
    $("#electenddatesel").rules("remove", "required");
    $("#electenddatesel").rules("remove", "isValidDate");
    $("#mechastartdatesel").rules("remove", "required");
    $("#mechastartdatesel").rules("remove", "isValidDate");
    $("#mechaenddatesel").rules("remove", "required");
    $("#mechaenddatesel").rules("remove", "isValidDate");
    $("#plumbstartdatesel").rules("remove", "required");
    $("#plumbstartdatesel").rules("remove", "isValidDate");
    $("#plumbenddatesel").rules("remove", "required");
    $("#plumbenddatesel").rules("remove", "isValidDate");
    $("#backfstartdatesel").rules("remove", "required");
    $("#backfstartdatesel").rules("remove", "isValidDate");
    $("#backfenddatesel").rules("remove", "required");
    $("#backfenddatesel").rules("remove", "isValidDate");
    $("#firestartdatesel").rules("remove", "required");
    $("#firestartdatesel").rules("remove", "isValidDate");
    $("#fireenddatesel").rules("remove", "required");
    $("#fireenddatesel").rules("remove", "isValidDate");
    $("#zonestartdatesel").rules("remove", "required");
    $("#zonestartdatesel").rules("remove", "isValidDate");
    $("#zoneenddatesel").rules("remove", "required");
    $("#zoneenddatesel").rules("remove", "isValidDate");
    $("#poolstartdatesel").rules("remove", "required");
    $("#poolstartdatesel").rules("remove", "isValidDate");
    $("#poolenddatesel").rules("remove", "required");
    $("#poolenddatesel").rules("remove", "isValidDate");
    $("#foodstartdatesel").rules("remove", "required");
    $("#foodstartdatesel").rules("remove", "isValidDate");
    $("#foodenddatesel").rules("remove", "required");
    $("#foodenddatesel").rules("remove", "isValidDate");
    $("#facilstartdatesel").rules("remove", "required");
    $("#facilstartdatesel").rules("remove", "isValidDate");
    $("#facilenddatesel").rules("remove", "required");
    $("#facilenddatesel").rules("remove", "isValidDate");
    $("#daycstartdatesel").rules("remove", "required");
    $("#daycstartdatesel").rules("remove", "isValidDate");
    $("#daycenddatesel").rules("remove", "required");
    $("#daycenddatesel").rules("remove", "isValidDate");

    //remove required from facilitator
    $("#asndfacilitator").rules("remove", "required");
    $("#asndfacilitator").rules("remove", "valueNotEquals");
    //indicate this is not a submittted plan review
    $("#IsSubmit").val("false");

    //add submit for the form since this button is not a submit
    $("#SchedulingForm").trigger("submit");

};


function SubmitSearch() {
    console.log("SubmitSearch");

    //reset previous errors
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").html("");
    $("div.error").hide();
    //get the correct required values for validation
    //NA can be changed in the process so always start from a baseline then check at submit
    //add rule for start and end date so it's always there
    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: true
    });
    $("#asndfacilitator").rules("add", "required");
    $("#asndfacilitator").rules("add", "valueNotEquals");

    //if NA is selected, isPool, isFifo remove dates rule
    if ($("#DrpDnScheduleBuild").val() == -1 || $("#buildpool").val() == "true") {
        $("#buildstartdatesel").rules("remove", "required");
        $("#buildstartdatesel").rules("remove", "isValidDate");
        $("#buildenddatesel").rules("remove", "required");
        $("#buildenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursBuilding").rules("remove", "required");
        $("#txtHoursBuilding").data("val-required", false);
        $("#txtHoursBuilding").attr("min", 0.0);

    }
    if ($("#DrpDnScheduleElectric").val() == -1 || $("#electpool").val() == "true") {
        $("#electstartdatesel").rules("remove", "required");
        $("#electstartdatesel").rules("remove", "isValidDate");
        $("#electenddatesel").rules("remove", "required");
        $("#electenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursElectic").rules("remove", "required");
        $("#txtHoursElectic").data("val-required", false);
        $("#txtHoursElectic").attr("min", 0.0);

    }

    if ($("#DrpDnScheduleMech").val() == -1 || $("#mechapool").val() == "true") {
        $("#mechastartdatesel").rules("remove", "required");
        $("#mechastartdatesel").rules("remove", "isValidDate");
        $("#mechaenddatesel").rules("remove", "required");
        $("#mechaenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursMech").rules("remove", "required");
        $("#txtHoursMech").data("val-required", false);
        $("#txtHoursMech").attr("min", 0.0);
    }

    if ($("#DrpDnSchedulePlumb").val() == -1 || $("#plumbpool").val() == "true") {
        $("#plumbstartdatesel").rules("remove", "required");
        $("#plumbstartdatesel").rules("remove", "isValidDate");
        $("#plumbenddatesel").rules("remove", "required");
        $("#plumbenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursPlumb").rules("remove", "required");
        $("#txtHoursPlumb").data("val-required", false);
        $("#txtHoursPlumb").attr("min", 0.0);

    }

    if ($("#DrpDnScheduleBackFlow").val() == -1 || $("#backfpool").val() == "true") {
        $("#backfstartdatesel").rules("remove", "required");
        $("#backfstartdatesel").rules("remove", "isValidDate");
        $("#backfenddatesel").rules("remove", "required");
        $("#backfenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursBackFlow").rules("remove", "required");
        $("#txtHoursBackFlow").data("val-required", false);
        $("#txtHoursBackFlow").attr("min", 0.0);
    }

    if ($("#DrpDnScheduleFire").val() == -1 || $("#firepool").val() == "true") {
        $("#firestartdatesel").rules("remove", "required");
        $("#firestartdatesel").rules("remove", "isValidDate");
        $("#fireenddatesel").rules("remove", "required");
        $("#fireenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursFire").rules("remove", "required");
        $("#txtHoursFire").data("val-required", false);
        $("#txtHoursFire").attr("min", 0.0);

    }

    if ($("#DrpDnScheduleZone").val() == -1 || $("#zonepool").val() == "true") {
        $("#zonestartdatesel").rules("remove", "required");
        $("#zonestartdatesel").rules("remove", "isValidDate");
        $("#zoneenddatesel").rules("remove", "required");
        $("#zoneenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursZoning").rules("remove", "required");
        $("#txtHoursZoning").data("val-required", false);
        $("#txtHoursZoning").attr("min", 0.0);

    }

    if ($("#DrpDnSchedulePool").val() == -1 || $("#poolpool").val() == "true") {
        $("#poolstartdatesel").rules("remove", "required");
        $("#poolstartdatesel").rules("remove", "isValidDate");
        $("#poolenddatesel").rules("remove", "required");
        $("#poolenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursPool").rules("remove", "required");
        $("#txtHoursPool").data("val-required", false);
        $("#txtHoursPool").attr("min", 0.0);
    }

    if ($("#DrpDnScheduleFood").val() == -1 || $("#foodpool").val() == "true") {
        $("#foodstartdatesel").rules("remove", "required");
        $("#foodstartdatesel").rules("remove", "isValidDate");
        $("#foodenddatesel").rules("remove", "required");
        $("#foodenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursFood").rules("remove", "required");
        $("#txtHoursFood").data("val-required", false);
        $("#txtHoursFood").attr("min", 0.0);
    }

    if ($("#DrpDnPScheduleLodge").val() == -1 || $("#facilpool").val() == "true") {
        $("#facilstartdatesel").rules("remove", "required");
        $("#facilstartdatesel").rules("remove", "isValidDate");
        $("#facilenddatesel").rules("remove", "required");
        $("#facilenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursLodge").rules("remove", "required");
        $("#txtHoursLodge").data("val-required", false);
        $("#txtHoursLodge").attr("min", 0.0);

    }

    if ($("#DrpDnScheduleDayCare").val() == -1 || $("#daycpool").val() == "true") {
        $("#daycstartdatesel").rules("remove", "required");
        $("#daycstartdatesel").rules("remove", "isValidDate");
        $("#daycenddatesel").rules("remove", "required");
        $("#daycenddatesel").rules("remove", "isValidDate");
        //jcl LES-186 remove rules for hrs text box
        $("#txtHoursDayCare").rules("remove", "required");
        $("#txtHoursDayCare").data("val-required", false);
        $("#txtHoursDayCare").attr("min", 0.0);
    }

    var isFutureCycle = $("#isfuturecycle").val();
    var prod = $("#plansreadyondate").val();
    var sad = $("#scheduleafterdate").val();
    if (isFutureCycle == "True") {
        if (!sad) {
            openWarning("Schedule After Date is required for future plan review scheduling.", true);
            return;
        }
    }

    //if the form is valid
    if ($('#SchedulingForm').valid()) {
        $("div.error").html("");
        $("div.error").hide();
        $("#SchedulingForm").trigger("submit");
    }
}

function FIFOSubmitSearch() {
    //reset previous errors
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").html("");
    $("div.error").hide();
    //get the correct required values for validation
    //NA can be changed in the process so always start from a baseline then check at submit
    //add rule for start and end date so it's always there
    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: true
    });
    $("#asndfacilitator").rules("add", "required");
    $("#asndfacilitator").rules("add", "valueNotEquals");

    //if NA is selected, isPool, isFifo remove dates rule
    if ($("#DrpDnScheduleBuild").val() == -1 || $("#buildpool").val() == "true" || $("#buildfifo").val() == "true") {
        $("#buildstartdatesel").rules("remove", "required");
        $("#buildstartdatesel").rules("remove", "isValidDate");
        $("#buildenddatesel").rules("remove", "required");
        $("#buildenddatesel").rules("remove", "isValidDate");

    }
    if ($("#DrpDnScheduleElectric").val() == -1 || $("#electpool").val() == "true" || $("#electfifo").val() == "true") {
        $("#electstartdatesel").rules("remove", "required");
        $("#electstartdatesel").rules("remove", "isValidDate");
        $("#electenddatesel").rules("remove", "required");
        $("#electenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnScheduleMech").val() == -1 || $("#mechapool").val() == "true" || $("#mechafifo").val() == "true") {
        $("#mechastartdatesel").rules("remove", "required");
        $("#mechastartdatesel").rules("remove", "isValidDate");
        $("#mechaenddatesel").rules("remove", "required");
        $("#mechaenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnSchedulePlumb").val() == -1 || $("#plumbpool").val() == "true" || $("#plumbfifo").val() == "true") {
        $("#plumbstartdatesel").rules("remove", "required");
        $("#plumbstartdatesel").rules("remove", "isValidDate");
        $("#plumbenddatesel").rules("remove", "required");
        $("#plumbenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnScheduleBackFlow").val() == -1 || $("#backfpool").val() == "true" || $("#backffifo").val() == "true") {
        $("#backfstartdatesel").rules("remove", "required");
        $("#backfstartdatesel").rules("remove", "isValidDate");
        $("#backfenddatesel").rules("remove", "required");
        $("#backfenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnScheduleFire").val() == -1 || $("#firepool").val() == "true" || $("#firefifo").val() == "true") {
        $("#firestartdatesel").rules("remove", "required");
        $("#firestartdatesel").rules("remove", "isValidDate");
        $("#fireenddatesel").rules("remove", "required");
        $("#fireenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnScheduleZone").val() == -1 || $("#zonepool").val() == "true" || $("#zonefifo").val() == "true") {
        $("#zonestartdatesel").rules("remove", "required");
        $("#zonestartdatesel").rules("remove", "isValidDate");
        $("#zoneenddatesel").rules("remove", "required");
        $("#zoneenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnSchedulePool").val() == -1 || $("#poolpool").val() == "true" || $("#poolfifo").val() == "true") {
        $("#poolstartdatesel").rules("remove", "required");
        $("#poolstartdatesel").rules("remove", "isValidDate");
        $("#poolenddatesel").rules("remove", "required");
        $("#poolenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnScheduleFood").val() == -1 || $("#foodpool").val() == "true" || $("#foodfifo").val() == "true") {
        $("#foodstartdatesel").rules("remove", "required");
        $("#foodstartdatesel").rules("remove", "isValidDate");
        $("#foodenddatesel").rules("remove", "required");
        $("#foodenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnPScheduleLodge").val() == -1 || $("#facilpool").val() == "true" || $("#facilfifo").val() == "true") {
        $("#facilstartdatesel").rules("remove", "required");
        $("#facilstartdatesel").rules("remove", "isValidDate");
        $("#facilenddatesel").rules("remove", "required");
        $("#facilenddatesel").rules("remove", "isValidDate");

    }

    if ($("#DrpDnScheduleDayCare").val() == -1 || $("#daycpool").val() == "true" || $("#daycfifo").val() == "true") {
        $("#daycstartdatesel").rules("remove", "required");
        $("#daycstartdatesel").rules("remove", "isValidDate");
        $("#daycenddatesel").rules("remove", "required");
        $("#daycenddatesel").rules("remove", "isValidDate");

    }

    //if the form is valid
    if ($('#SchedulingForm').valid()) {
        $("div.error").html("");
        $("div.error").hide();
        $("#SchedulingForm").trigger("submit");
    }
}


function ReloadPlanReview() {
    //var newurl = window.location.href.replace("PerformAutoEstimation=True", "PerformAutoEstimation=False");
    //window.location.href = newurl;
    //window.location = newurl;
    //window.location.search = newurl.split("?")[1];
    //var cururl = window.location.href;
    var args = new Object();
    args.PerformAutoEstimation = "False";
    args.ProjectId = $("#AccelaProjectRefId").val();
    args.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    openWarning("...", false);

    var url = "/Scheduling/SchedulePlanReview";
    $.ajax({
        type: "POST",
        url: url,
        data: args,
        statusCode: {
            404: function () {
                openWarning("page not found", true);
            }
        },
        success: function (response) {
            $("body").html(response);
        },
        failure: function (jqXHR, textStatus, errorThrown) { openWarning("Error: " + textStatus + " : " + errorThrown, true); },
        error: function (jqXHR, textStatus, errorThrown) { openWarning("Error: " + textStatus + " : " + errorThrown, true); }
    });
}

function GetAutoScheduleValues() {
    var args = new Object();
    args.PerformAutoEstimation = "True";
    args.ProjectId = $("#AccelaProjectRefId").val();
    args.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    args.BuildingIsPool = $("#buildpool").val();
    args.ElectricIsPool = $("#electpool").val();
    args.MechIsPool = $("#mechapool").val();
    args.PlumbIsPool = $("#plumbpool").val();
    args.ZoneIsPool = $("#zonepool").val();
    args.FireIsPool = $("#firepool").val();
    args.FoodServiceIsPool = $("#foodpool").val();
    args.PoolIsPool = $("#poolpool").val();
    args.FacilityIsPool = $("#facilpool").val();
    args.DayCareIsPool = $("#daycpool").val();
    args.BackFlowIsPool = $("#backfpool").val();

    args.BuildingUserID = $("#DrpDnScheduleBuild").val();
    args.ElectricUserID = $("#DrpDnScheduleElectric").val();
    args.MechUserID = $("#DrpDnScheduleMech").val();
    args.PlumbUserID = $("#DrpDnSchedulePlumb").val();
    args.ZoneUserID = $("#DrpDnScheduleZone").val();
    args.FireUserID = $("#DrpDnScheduleFire").val();
    args.FoodServiceUserID = $("#DrpDnScheduleFood").val();
    args.PoolUserID = $("#DrpDnSchedulePool").val();
    args.FacilityUserID = $("#DrpDnPScheduleLodge").val();
    args.DayCareUserID = $("#DrpDnScheduleDayCare").val();
    args.BackFlowUserID = $("#DrpDnScheduleBackFlow").val();

    args.IsFutureCycle = $("#isfuturecycle").val();
    args.IsAdjustHours = $("#isadjusthours").val();


    var prod = $("#plansreadyondate").val();
    var sad = $("#scheduleafterdate").val();
    if (args.IsFutureCycle == "True") {
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

    //this dialog works as a modal
    openWarning("AutoSchedule in Progress...", false);
    var url = "/Scheduling/SchedulePlanReview";
    $.ajax
        ({
            type: "POST",
            url: url,
            data: args,
            statusCode: {
                404: function () {
                    openWarning("page not found", true);
                }
            },
            failure: function (jqXHR, textStatus, errorThrown) { openWarning("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openWarning("Error: " + textStatus + " : " + errorThrown, true); }
        })
        .done(function (response, status, jqxhr) {
            $("body").html(response);
            console.log("ajax complete");
        });
}

function ToggleScheduleCycle() {
    //toggle
    var isfuturecycle = $('#isfuturecycle').val();
    if (isfuturecycle == "False") {
        isfuturecycle = "True";
    }
    else {
        isfuturecycle = "False";
    }

    if (isfuturecycle == "True") {
        //show schedule after date
        $('#sadcontainer').show();
        $('#btnSave').hide();
        //make btn green btn-outline-primary
        $("#schedulecyclebtn").addClass("btn-success");
        //set the value
        if ($('#isfuturecycle').val() != "True")
            $('#isfuturecycle').val("True");
    } else {
        //null is the same as false
        //hide schedule after date
        $('#sadcontainer').hide();
        $('#btnSave').show();
        //remove btn background
        $("#schedulecyclebtn").removeClass("btn-success");
        //set the value
        if ($('#isfuturecycle').val() != "False")
            $('#isfuturecycle').val("False");
    }
}

function ToggleAdjustHours() {
    //toggle
    var isadjusthours = $('#isadjusthours').val();
    if (isadjusthours == "False") {
        isadjusthours = "True";
    }
    else {
        isadjusthours = "False";
    }

    if (isadjusthours == "True") {
        //make btn green btn-outline-primary
        $(".rereviewgroup").addClass("btn-success");
        $(".proposedgroup").removeClass("btn-success");
        $("#adjusthoursbtn").addClass("btn-success");

        //set new hours selected
        if ($("#hoursrereviewbuilding") != null)
            $("#hoursbuildingfinal").val($("#hoursrereviewbuilding").val());
        else
            $("#hoursbuildingfinal").val() = 0;
        if ($("#hoursrereviewelectric") != null)
            $("#hourselectricfinal").val($("#hoursrereviewelectric").val());
        else
            $("#hourselectricfinal").val() = 0;
        if ($("#hoursrereviewmech") != null)
            $("#hoursmechfinal").val($("#hoursrereviewmech").val());
        else
            $("#hoursmechfinal").val() = 0;
        if ($("#hoursrereviewplumb") != null)
            $("#hoursplumbfinal").val($("#hoursrereviewplumb").val());
        else
            $("#hoursplumbfinal").val() = 0;
        if ($("#hoursrereviewfire") != null)
            $("#hoursfirefinal").val($("#hoursrereviewfire").val());
        else
            $("#hoursfirefinal").val() = 0;
        if ($("#hoursrereviewzoning") != null)
            $("#hourszoningfinal").val($("#hoursrereviewzoning").val());
        else
            $("#hourszoningfinal").val() = 0;
        if ($("#hoursrereviewbackflow") != null)
            $("#hoursbackflowfinal").val($("#hoursrereviewbackflow").val());
        else
            $("#hoursbackflowfinal").val() = 0;
        if ($("#hoursrereviewpool") != null)
            $("#hourspoolfinal").val($("#hoursrereviewpool").val());
        else
            $("#hourspoolfinal").val() = 0;
        if ($("#hoursrereviewfood") != null)
            $("#hoursfoodfinal").val($("#hoursrereviewfood").val());
        else
            $("#hoursfoodfinal").val() = 0;
        if ($("#hoursrereviewlodge") != null)
            $("#hourslodgefinal").val($("#hoursrereviewlodge").val());
        else
            $("#hourslodgefinal").val() = 0;
        if ($("#hoursrereviewdaycare") != null)
            $("#hoursdaycarefinal").val($("#hoursrereviewdaycare").val());
        else
            $("#hoursdaycarefinal").val(0);

        //set the value
        if ($('#isadjusthours').val() != "True")
            $('#isadjusthours').val("True");
    } else {
        //null is the same as false
        //remove btn background
        $(".rereviewgroup").removeClass("btn-success");
        $(".proposedgroup").addClass("btn-success");
        $("#adjusthoursbtn").removeClass("btn-success");

        //set new hours selected
        if ($("#hoursproposedbuilding") != null)
            $("#hoursbuildingfinal").val($("#hoursproposedbuilding").val());
        else
            $("#hoursbuildingfinal").val() = 0;
        if ($("#hoursproposedelectric") != null)
            $("#hourselectricfinal").val($("#hoursproposedelectric").val());
        else
            $("#hourselectricfinal").val() = 0;
        if ($("#hoursproposedmech") != null)
            $("#hoursmechfinal").val($("#hoursproposedmech").val());
        else
            $("#hoursmechfinal").val() = 0;
        if ($("#hoursproposedplumb") != null)
            $("#hoursplumbfinal").val($("#hoursproposedplumb").val());
        else
            $("#hoursplumbfinal").val() = 0;
        if ($("#hoursproposedfire") != null)
            $("#hoursfirefinal").val($("#hoursproposedfire").val());
        else
            $("#hoursfirefinal").val() = 0;
        if ($("#hoursproposedzoning") != null)
            $("#hourszoningfinal").val($("#hoursproposedzoning").val());
        else
            $("#hourszoningfinal").val() = 0;
        if ($("#hoursproposedbackflow") != null)
            $("#hoursbackflowfinal").val($("#hoursproposedbackflow").val());
        else
            $("#hoursbackflowfinal").val() = 0;
        if ($("#hoursproposedpool") != null)
            $("#hourspoolfinal").val($("#hoursproposedpool").val());
        else
            $("#hourspoolfinal").val() = 0;
        if ($("#hoursproposedfood") != null)
            $("#hoursfoodfinal").val($("#hoursproposedfood").val());
        else
            $("#hoursfoodfinal").val() = 0;
        if ($("#hoursproposedlodge") != null)
            $("#hourslodgefinal").val($("#hoursproposedlodge").val());
        else
            $("#hourslodgefinal").val() = 0;
        if ($("#hoursproposeddaycare") != null)
            $("#hoursdaycarefinal").val($("#hoursproposeddaycare").val());
        else
            $("#hoursdaycarefinal").val(0);

        //set the value
        if ($('#isadjusthours').val() != "False")
            $('#isadjusthours').val("False");
    }
}

function hideHrsTextboxIfNA() {
    //hide all hrs
    //na-txt
    //hrstextbox
    $(".na-txt").each(function () {
        $(this).hide();
    })
    $(".hrstextbox").each(function () {
        //make all not required since these only change if Activate NA Review is clicked
        var textboxid = $(this).attr("id");
        $("#" + textboxid).data("val-required", false);
        $("#" + textboxid).attr("min", 0.0);
        $("#" + textboxid).prop('readonly', true);
        $("#" + textboxid).addClass("disabled");
        $(this).hide();
    })
    $(".hrsna").each(function () {
        //if the value is True, then show the NA textbox
        if ($(this).val() == "True") {
            //get the next object with class .na-txt and show
            $(this).next(".na-txt").show();
            $(this).next(".na-txt").next(".hrstextbox").hide();
        } else {
            //get the next object with class .hrstextbox and show
            $(this).next(".na-txt").next(".hrstextbox").show();
        }
    })
}
