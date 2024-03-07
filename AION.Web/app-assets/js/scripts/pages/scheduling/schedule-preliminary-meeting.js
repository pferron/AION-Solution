//global vars
var isPreliminaryMeeting = true;
var suggestedDate1 = $("#hdnProposedDate1").val();
var suggestedDate2 = $("#hdnProposedDate2").val();
var suggestedDate3 = $("#hdnProposedDate3").val();
//jcl- set this for the AutoSchedule, should always be false for Preliminary
var isActivateNAReview = false;

var holidays = [];

//windows onload event
$(function () {

    //init action buttons on _Actions.cshtml
    $("#btnSave").on("click", function () {
        SaveData();
    });

    $("#btnSubmit").on("click", function () {
        SubmitSearch();
    });

    $("#btnCancel").on("click", function () {
        ReloadData();
    });

    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").hide();
    $(".btnAutoSchedule").on("click", function () {
        if ($('#hdnHasAutoSchedule').val() == "true") {
            //stop user from closing dialog
            openInProgress("AutoSchedule in progress", false);
        }
    });

    $(".meetingroomnamelabel").text(" - " + $("#meetingroomname").val() + " - ");

    /******************* Validator *******************/
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "");
    $.validator.addMethod("valueGTZero", function (value, element) {
        if (value == "0" || value == "-1")
            return false;
        else
            return true;
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
            MeetingRoomRefIDSelected: { valueGTZero: true },
            AssignedFacilitator: { required: true, valueNotEquals: "-1" },
        },
        messages: {
            MeetingRoomRefIDSelected: "Meeting Room is required.",
            AssignedFacilitator: "Facilitator is required.",
            ScheduleDate: "Date is required.",
            StartTime: "Start Time is required.",
            EndTime: "End Time is required."

        },
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            $.each(validator.invalid, function (index, value) {
                if (index.toLowerCase().indexOf("fire") > 0) {
                    //highlight tab
                    $('.firereqcls').show();
                };
                if (index.toLowerCase().indexOf("zon") > 0) {
                    //highlight tab
                    $('.zoningreqcls').show();
                };
                if (index.toLowerCase().indexOf("backf") > 0) {
                    //highlight tab
                    $('.backfreqcls').show();
                };
                if (index.toLowerCase().indexOf("build") > 0
                    || index.toLowerCase().indexOf("elect") > 0
                    || index.toLowerCase().indexOf("mecha") > 0
                    || index.toLowerCase().indexOf("plumb") > 0
                    || index.toLowerCase().indexOf("facilitator") > 0
                ) {
                    //highlight tab
                    $('.bempreqcls').show();
                };
                if (index.toLowerCase().indexOf("food") > 0
                    || index.toLowerCase().indexOf("pool") > 0
                    || index.toLowerCase().indexOf("facil") > 0
                    || index.toLowerCase().indexOf("dayc") > 0
                ) {
                    //highlight tab
                    $('.healthreqcls').show();
                };

            });
            if (errors) {
                var message = errors == 1
                    ? 'You missed 1 field. It has been highlighted'
                    : 'You missed ' + errors + ' fields. They have been highlighted';
                $("div.error").html(message);
                $("div.error").show();
            } else {
                $('.bempreqcls').hide();
                $('.firereqcls').hide();
                $('.zoningreqcls').hide();
                $('.backfreqcls').hide();
                $('.healthreqcls').hide();
                $("div.error").hide();
            }

        },
        submitHandler: function (form) {
            var savemessage = "Saving Preliminary Meeting Appointment ...";
            var submitmessage = "Submitting Preliminary Meeting Appointment ...";
            var message = savemessage;
            var isSubmit = $("#IsSubmit").val();
            if (isSubmit == "true")
                message = submitmessage;
            openSuccess(message, false);
            $("#btnSubmit").addClass("disabled");
            $("#btnSave").addClass("disabled");
            $("div.error").html("");
            $("div.error").hide();
            $('.bempreqcls').hide();
            $('.firereqcls').hide();
            $('.zoningreqcls').hide();
            $('.backfreqcls').hide();
            $('.healthreqcls').hide();

            form.submit();
        }
    });
    /*************************end validator ****************************/
    

    //Date Format for Date Picker//
    var setDate = $("#hdnScheduleDate").val() == "" ? "" : $("#hdnScheduleDate").val();

    /******************************************************/
    $(".hrstextbox").on("focusout", function () {
        console.log("focusout hrstextbox");
        //if value > 0 then uncheck NA box
        SetNA(this);
    })

    SetTabPerms();


    ///Capture Reset and set refresh the datepickers on Create
    $('#SchedulingForm').on('reset', function (event) {
        setTimeout(function () {
            // executes after the form has been reset
            $("#startdatesel").datepicker('setDate', new Date());
        }, 1);

    });
    //**************************************************//


    /* During the first time GetWhatChanged is called for change of each dropdown box, we need
     * to know what is the first set of values on load. else the first set of change will not be reflected properly.
     * So just after the form load is done, add all the selected lists to related html element as custome data.*/

    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBuild");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsElectric");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsMech");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPlumb");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBackFlow");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFood");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPool");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsLodge");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsDayCare");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFire");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsZone");

    $(".chkDrpDnExPln").on("OnSelectionChange", function (e, eventData) {
        var evntArgs = {
            IsDeleted: false,
            IsAdded: false,
            AddedValues: [], //null if no change/None. Else changed value.
            DeletedValues: [] //null if no change/None. Else changed value.
        };
        var source = e;
        //evntArgs = $(this).data('SelectionChangeEventArgs');
        evntArgs = eventData;
        var elementnm = $(this).attr("id");
        if (evntArgs !== "undefined" && elementnm != "") {
            if (evntArgs.IsAdded == true) {
                //if excluded checked then remove.
                for (var i = 0; i < evntArgs.AddedValues.length; i++) {
                    DropDownListRemoveItem(GetPrimaryReviewerID(elementnm), evntArgs.AddedValues[i]);
                    DropDownListRemoveItem(GetSecondaryReviewerID(elementnm), evntArgs.AddedValues[i]);
                }
            }
            if (evntArgs.IsDeleted == true) {
                //if excluded checked then remove.
                for (var i = 0; i < evntArgs.DeletedValues.length; i++) {
                    var txt = $("#" + elementnm + " option[value=" + evntArgs.DeletedValues[i] + "]").text();
                    DropDownListAddItem(GetPrimaryReviewerID(elementnm), txt, evntArgs.DeletedValues[i]);
                    DropDownListAddItem(GetSecondaryReviewerID(elementnm), txt, evntArgs.DeletedValues[i]);
                }
            }
        }
    });

});
// ****** end on load event ***************//


//assume this will never be called with both added and removed at same time.
//console.log(GetWhatChanged("39,96,121,107", "39,96,106,107,109")); //This will not work correctly since there are values added and removed at same time.

function GetPrimaryReviewerID(excludedCtrlID) {
    switch (excludedCtrlID) {
        case "chkDrpDnExPlnRvrsBuild":
            return "DrpDnPrimaryBuild";
        case "chkDrpDnExPlnRvrsElectric":
            return "DrpDnPrimaryElectric";
        case "chkDrpDnExPlnRvrsMech":
            return "DrpDnPrimaryMech";
        case "chkDrpDnExPlnRvrsPlumb":
            return "DrpDnPrimaryPlumb";
        case "chkDrpDnExPlnRvrsBackFlow":
            return "DrpDnPrimaryBackFlow";
        case "chkDrpDnExPlnRvrsFood":
            return "DrpDnPrimaryFood";
        case "chkDrpDnExPlnRvrsPool":
            return "DrpDnPrimaryPool";
        case "chkDrpDnExPlnRvrsLodge":
            return "DrpDnPrimaryLodge";
        case "chkDrpDnExPlnRvrsDayCare":
            return "DrpDnPrimaryDayCare";
        case "chkDrpDnExPlnRvrsFire":
            return "DrpDnPrimaryFire";
        case "chkDrpDnExPlnRvrsZone":
            return "DrpDnPrimaryZone";
        default:
            return "";
    }

}

function GetSecondaryReviewerID(excludedCtrlID) {
    switch (excludedCtrlID) {
        case "chkDrpDnExPlnRvrsBuild":
            return "DrpDnSecondaryBuild";
        case "chkDrpDnExPlnRvrsElectric":
            return "DrpDnSecondaryElectric";
        case "chkDrpDnExPlnRvrsMech":
            return "DrpDnSecondaryMech";
        case "chkDrpDnExPlnRvrsPlumb":
            return "DrpDnSecondaryPlumb";
        case "chkDrpDnExPlnRvrsBackFlow":
            return "DrpDnSecondaryBackFlow";
        case "chkDrpDnExPlnRvrsFood":
            return "DrpDnSecondaryFood";
        case "chkDrpDnExPlnRvrsPool":
            return "DrpDnSecondaryPool";
        case "chkDrpDnExPlnRvrsLodge":
            return "DrpDnSecondaryLodge";
        case "chkDrpDnExPlnRvrsDayCare":
            return "DrpDnSecondaryDayCare";
        case "chkDrpDnExPlnRvrsFire":
            return "DrpDnSecondaryFire";
        case "chkDrpDnExPlnRvrsZone":
            return "DrpDnSecondaryZone";
        default:
            return "";
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
    $(".meetingroomrefid").rules("remove", "required");
    $(".meetingroomrefid").rules("remove", "valueNotEquals");
    $("#asndfacilitator").rules("remove", "required");
    $("#asndfacilitator").rules("remove", "valueNotEquals");
    $("#IsSubmit").val("false");

    $("#SchedulingForm").trigger("submit");

};
function SubmitData() {
    ValidateMeetingRoomAvailability();
}
function ReloadData() {
    var newurl = window.location.href.replace("PerformAutoEstimation=True", "PerformAutoEstimation=False");
    window.location.href = newurl;
    window.location = newurl;
    window.location.search = newurl.split("?")[1];
    var cururl = window.location.href;
}
function SubmitSearch() {
    //reset previous errors
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();
    $("div.error").hide();
    //get the correct required values for validation
    //NA can be changed in the process so always start from a baseline then check at submit
    //add rule for start and end date so it's always there
    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: true
    });
    $("#asndfacilitator").rules("add", "required");
    $("#asndfacilitator").rules("add", "valueNotEquals");

    $("#startdatesel").rules("add", "required");
    $("#startdatesel").rules("add", "isValidDate");
    $("#starttimesel").rules("add", "required");
    $("#endtimesel").rules("add", "required");
    $(".meetingroomrefid").rules("add", "required");
    $(".meetingroomrefid").rules("add", "valueGTZero");
    $("#asndfacilitator").rules("add", "required");
    $("#asndfacilitator").rules("add", "valueNotEquals");
    $("#IsSubmit").val("true");

    //if the form is valid
    if ($('#SchedulingForm').valid()) {
        $("div.error").html("");
        $("div.error").hide();
        $('.bempreqcls').hide();
        $('.firereqcls').hide();
        $('.zoningreqcls').hide();
        $('.backfreqcls').hide();
        $('.healthreqcls').hide();
        $("#SchedulingForm").trigger("submit");
    }
}
