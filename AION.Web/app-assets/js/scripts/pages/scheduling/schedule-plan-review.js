//global vars
var isFifoProject = $('#hIsFIFOProject').val() == "true";
var canPoolForPoorPerformer = $('#hCanPoolForPoorPerformer').val() == "true";

//jcl LES-186 Activate NA Review
var isScheduled = $("#hScheduled").val() == "true";
var isActivateNAReview = false;
var originalNAEstimate = $("#hOriginalEstimate").val();

/************ onload ******************/

$(function () {
    //init action buttons on _Actions.cshtml
    $("#btnSave").on("click", function () {
        SaveData();
    });

    $("#btnSubmit").on("click", function () {
        SubmitSearch();
    });

    $("#btnCancel").on("click", function () {
        ReloadPlanReview();
    });

    $("#btnSubmitPR").on("click", function () {
        $("#SchedulingForm").trigger("submit");
    });

    showStep1();
   
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
    var isadjusttoggle = $("#hIsAdjustHours").val() == "true";
    if (isadjusttoggle) {
        //set adjusted column
        $(".rereviewgroup").addClass("btn-success");
        $(".proposedgroup").removeClass("btn-success");
        $("#adjusthoursbtn").addClass("btn-success");
    }

    //******************************************************************/

    //***** init pool txt *****************************************/

    setPool('buildpooltxt', 'buildpool', $("#hBuildPool").val(), 'buildstartendsel', 'btnbuildpool');
    setPool('electpooltxt', 'electpool', $("#hElectPool").val(), 'electstartendsel', 'btnelectpool');
    setPool('mechapooltxt', 'mechapool', $("#hMechaPool").val(), 'mechastartendsel', 'btnmechapool');
    setPool('plumbpooltxt', 'plumbpool', $("#hPlumbPool").val(), 'plumbstartendsel', 'btnplumbpool');
    setPool('backfpooltxt', 'backfpool', $("#hBackfPool").val(), 'backfstartendsel', 'btnbackfpool');
    setPool('zonepooltxt', 'zonepool', $("#hZonePool").val(), 'zonestartendsel', 'btnzonepool');
    setPool('firepooltxt', 'firepool', $("#hFirePool").val(), 'firestartendsel', 'btnfirepool');
    setPool('foodpooltxt', 'foodpool', $("#hFoodPool").val(), 'foodstartendsel', 'btnfoodpool');
    setPool('poolpooltxt', 'poolpool', $("#hPoolPool").val(), 'poolstartendsel', 'btnpoolpool');
    setPool('facilpooltxt', 'facilpool', $("#hFacilPool").val(), 'facilstartendsel', 'btnfacilpool');
    setPool('daycpooltxt', 'daycpool', $("#hDaycPool").val(), 'daycstartendsel', 'btndaycpool');
    //******************************************************************/

    //******** init pool button ****************************************/
    initPoolBtns();


});
//************** end onload events **************************/

