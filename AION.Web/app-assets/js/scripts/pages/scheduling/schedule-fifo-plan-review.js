//global vars
var holidays = [];
var isFifoProject = $("#hIsFIFOProject").val() == "true";

/************ onload ******************/

$(function () {
    //init action buttons on _Actions.cshtml
    $("#btnSave").on("click", function () {
        SaveData();
    });

    $("#btnSubmit").on("click", function () {
        FIFOSubmitSearch();
    });

    $("#btnCancel").on("click", function () {
        ReloadPlanReview();
    });

    $("#btnSubmitPR").on("click", function () {
        $("#SchedulingForm").trigger("submit");
    });

    //initialize future cycle toggle
    var istoggle = $("#hIsFutureCycle").val() == "true";
    if (istoggle) {
        //show schedule after date
        $('#sadcontainer').show();
        $('#btnSave').hide();
        //make btn green btn-outline-primary
        $("#schedulecyclebtn").addClass("btn-success");
        //set the value
        if ($('#isfuturecycle').val() != "True")
            $('#isfuturecycle').val("True");
    }

    //initialize adjust hours toggle
    var selectedcyclegroup = $("#hIsCycleComparison").val() == "true";
    if (selectedcyclegroup) {
        //make btn green btn-outline-primary
        $(".proposedgroup").addClass("btn-success");
        $(".rereviewgroup").removeClass("btn-success");
    }

    //initialize adjusted hours
    var isadjusttoggle = $("#hIsAdjustHours").val();
    if (isadjusttoggle) {
        //set adjusted column
        $(".rereviewgroup").addClass("btn-success");
        $(".proposedgroup").removeClass("btn-success");
        $("#adjusthoursbtn").addClass("btn-success");
    }


    //**************** init datepickers with any saved dates ***********/
    if ($("#hBuildStartDate").val() != "")
        $("#buildstartdatesel").datepicker("setDate", $("#hBuildStartDate").val());
    if ($("#hBuildEndDate").val() != "")
        $("#buildenddatesel").datepicker("setDate", $("#hBuildEndDate").val());

    if ($("#hElectStartDate").val() != "")
        $("#electstartdatesel").datepicker("setDate", $("#hElectStartDate").val());
    if ($("#hElectEndDate").val() != "")
        $("#electenddatesel").datepicker("setDate", $("#hElectEndDate").val());

    if ($("#hMechaStartDate").val() != "")
        $("#mechastartdatesel").datepicker("setDate", $("#hMechaStartDate").val());
    if ($("#hMechaEndDate").val() != "")
        $("#mechaenddatesel").datepicker("setDate", $("#hMechaEndDate").val());

    if ($("#hPlumbStartDate").val() != "")
        $("#plumbstartdatesel").datepicker("setDate", $("#hPlumbStartDate").val());
    if ($("#hPlumbEndDate").val() != "")
        $("#plumbenddatesel").datepicker("setDate", $("#hPlumbEndDate").val());

    if ($("#hBackfStartDate").val() != "")
        $("#backfstartdatesel").datepicker("setDate", $("#hBackfStartDate").val());
    if ($("#hBackfEndDate").val() != "")
        $("#backfenddatesel").datepicker("setDate", $("#hBackfEndDate").val());

    if ($("#hFireStartDate").val() != "")
        $("#firestartdatesel").datepicker("setDate", $("#hFireStartDate").val());
    if ($("#hFireEndDate").val() != "")
        $("#fireenddatesel").datepicker("setDate", $("#hFireEndDate").val());

    if ($("#hZoneStartDate").val() != "")
        $("#zonestartdatesel").datepicker("setDate", $("#hZoneStartDate").val());
    if ($("#hZoneEndDate").val() != "")
        $("#zoneenddatesel").datepicker("setDate", $("#hZoneEndDate").val());

    if ($("#hPoolStartDate").val() != "")
        $("#poolstartdatesel").datepicker("setDate", $("#hPoolStartDate").val());
    if ($("#hPoolEndDate").val() != "")
        $("#poolenddatesel").datepicker("setDate", $("#hPoolEndDate").val());

    if ($("#hFoodStartDate").val() != "")
        $("#foodstartdatesel").datepicker("setDate", $("#hFoodStartDate").val());
    if ($("#hFoodEndDate").val() != "")
        $("#foodenddatesel").datepicker("setDate", $("#hFoodEndDate").val());

    if ($("#hFacilStartDate").val() != "")
        $("#facilstartdatesel").datepicker("setDate", $("#hFacilStartDate").val());
    if ($("#hFacilEndDate").val() != "")
        $("#facilenddatesel").datepicker("setDate", $("#hFacilEndDate").val());

    if ($("#hDaycStartDate").val() != "")
        $("#daycstartdatesel").datepicker("setDate", $("#hDaycStartDate").val());
    if ($("#hDaycEndDate").val() != "")
        $("#daycenddatesel").datepicker("setDate", $("#hDaycEndDate").val());

    //******************************************************************/


});
//************** end onload events **************************/




