
var o = new Object();
var holidays = [];

$(function () {
    initialize();

    var allreviewersjson = $("#hAllreviewersjson").val();

    arrAvailableReviewers = JSON.parse(allreviewersjson);

    //reset the form back to defaults
    ///Capture Reset and set refresh the datepickers on Create
    $("#resetCreateNPA").on("click", function () {
        $("#NPAForm").trigger("reset");
    });

    $('#NPAForm').on('reset', function () {
        setTimeout(function () {
            // executes after the form has been reset
            $(".selectalldeptreviewers").iCheck("uncheck");
            $(".selectalldeptreviewers").removeAttr('checked').iCheck('update');

        }, 1);
    });

    //capture start time change
    $("#starttimesel").on("change", function () {
        var time = $(this).pickatime('picker').get('select');
        var endtime = $("#endtimesel").pickatime('picker');
        endtime.set('select', time.time + 60);
    });

    $("#AllDay").on("ifChanged", function () {
        var checked = $(this).iCheck('update')[0].checked;

        if (checked) {
            $("#starttimesel").val("");
            $("#endtimesel").val("");
        }
    });

    var $modal = $('#modal-addattendees');
    $modal.find('.modal-content')
        .resizable({
            handles: 'n, e, s, w, ne, sw, se, nw',
        })
        .draggable({
            handle: '.modal-header'
        });

    $(".ddl-dept").on("change", function () {
        var procemessage = "<option value='0'> Please wait...</option>";
        $(".ddl-availdept").html(procemessage).show();
        var txt = $(this).val();
        var schid = parseInt($(".input-schid").val());
        //build available for Building as it's always first
        buildAvailableSel("ddl-availdept", txt, schid);
        //build attendees
        buildAttendeeSel("ddl-attendees", txt, schid);
        //update so there aren't duplicates
        updateSelectlists("ddl-attendees", "ddl-availdept");
    });

    //add click function for options in select lists
    $(".btn-rem").on("click", function () {
        console.log("click <<");
        var dept = $(".ddl-dept").val();
        var SchId = parseInt($(".input-schid").val());
        $("#ddl-attendees option:selected").each(function () {
            var $this = this;
            addtoselectlist("ddl-availdept", "", $this.value, $this.text);
            removefromselectlist("ddl-attendees", $this.value);
            //remove from array
            //get attendees array from objects
            var attid = parseInt($this.value);
            var arrAttendeeList = arrAttendees.find(x => x.id === SchId).dat;
            //Find index of specific object using findIndex method.
            var objIndex = arrAttendees.findIndex(x => x.id === SchId);
            //Find index of specific attendee
            var attIndex = arrAttendeeList.findIndex(x => x.AttendeeId === attid && x.DeptNameListId === dept);
            var data = $.grep(arrAttendeeList, function (n, i) {
                return i != attIndex;
            });
            arrAttendees[objIndex].dat = data;

        });
        sortsavedlist("ddl-attendees");
        sortsavedlist("ddl-availdept");
    });
    $(".btn-addto").on("click", function () {
        console.log("click >>");
        var dept = $(".ddl-dept").val();
        var SchId = parseInt($(".input-schid").val());
        $("#ddl-availdept option:selected").each(function () {
            var $this = this;
            addtoselectlist("ddl-attendees", "", $this.value, $this.text);
            removefromselectlist("ddl-availdept", $this.value);
            //add to array
            //get attendees array from objects
            var attid = parseInt($this.value);
            var arrAttendeeList = arrAttendees.find(x => x.id === SchId).dat;
            //Find index of specific object using findIndex method.
            var objIndex = arrAttendees.findIndex(x => x.id === SchId);
            //get this reviewer from the allreviewers list so we have the data
            var attendee = arrAvailableReviewers.find(x => x.AttendeeId === attid && x.DeptNameListId === dept);
            arrAttendeeList.push(attendee);
            arrAttendees[objIndex].dat = arrAttendeeList;

        });
        sortsavedlist("ddl-attendees");
        sortsavedlist("ddl-availdept");
    });

    $(".btn-addall").on("click", function () {
        var dept = $(".ddl-dept").val();
        var SchId = parseInt($(".input-schid").val());
        $("#ddl-availdept option").each(function () {
            var $this = this;
            addtoselectlist("ddl-attendees", "", $this.value, $this.text);
            removefromselectlist("ddl-availdept", $this.value);
            //add to array
            //get attendees array from objects
            var attid = parseInt($this.value);
            var arrAttendeeList = arrAttendees.find(x => x.id === SchId).dat;
            //Find index of specific object using findIndex method.
            var objIndex = arrAttendees.findIndex(x => x.id === SchId);
            //get this reviewer from the allreviewers list so we have the data
            var attendee = arrAvailableReviewers.find(x => x.AttendeeId === attid && x.DeptNameListId === dept);
            arrAttendeeList.push(attendee);
            arrAttendees[objIndex].dat = arrAttendeeList;

        });
        sortsavedlist("ddl-attendees");
        sortsavedlist("ddl-availdept");
    });
    $(".btn-remall").on("click", function () {
        console.log("click <<");
        var dept = $(".ddl-dept").val();
        var SchId = parseInt($(".input-schid").val());
        $("#ddl-attendees option").each(function () {
            var $this = this;
            addtoselectlist("ddl-availdept", "", $this.value, $this.text);
            removefromselectlist("ddl-attendees", $this.value);
            //remove from array
            //get attendees array from objects
            var attid = parseInt($this.value);
            var arrAttendeeList = arrAttendees.find(x => x.id === SchId).dat;
            //Find index of specific object using findIndex method.
            var objIndex = arrAttendees.findIndex(x => x.id === SchId);
            //Find index of specific attendee
            var attIndex = arrAttendeeList.findIndex(x => x.AttendeeId === attid && x.DeptNameListId === dept);
            var data = $.grep(arrAttendeeList, function (n, i) {
                return i != attIndex;
            });
            arrAttendees[objIndex].dat = data;

        });
        sortsavedlist("ddl-attendees");
        sortsavedlist("ddl-availdept");
    });

    $(".btn-submitattendees").on("click", function () {
        console.log("submit button click");
        var NPAId = parseInt($(".input-npaid").val());
        var SchId = parseInt($(".input-schid").val());
        console.log(NPAId);
        if (NPAId == null) {
            console.log("NPA id error");
            openWarning("ID error.");
            return;
        }
        //get attids as objects form arrAttend
        //get the attendees, get the array values, send object with business id
        var attids = arrAttendees.find(x => x.id === SchId).dat;
        console.log(attids);

        if (attids == null || attids.length == 0) {
            console.log("No attendees error");
            openWarning("At least 1 plan reviewer for NPA is required");
            return;
        }
        var wkrID = $("#wkrID").val();
        if (wkrID == null || wkrID <= 0) {
            console.log("Worker ID error");
            openWarning("Log in error.");
            return;
        }
        if (SchId == null || SchId <= 0) {
            console.log("Schedule ID error");
            openWarning("Schedule error.");
            return;
        }
        updateAttendees(attids, NPAId, wkrID, SchId);

    });

    initializeOpenAddRemove();
    //end //npa/addremoveattendees.js


    $(".checkAllMNPAs").on("ifChanged", function () {
        var checked = $(this).iCheck('update')[0].checked;
        if (checked) {
            $('.mChkBx').iCheck('check');
        }
        else {
            $('.mChkBx').iCheck('uncheck');
        }
    });
    $(".checkAllESNPAs").on("ifChanged", function () {
        var checked = $(this).iCheck('update')[0].checked;
        if (checked) {
            $('.esChkBx').iCheck('check');
        }
        else {
            $('.esChkBx').iCheck('uncheck');
        }
    });

    $("#deleteButton").on("click", function (e) {
        var scheduleIds = [];
        var npaRecurring = [];
        var npaType = $('#NPATypeSelectList').val();
        var searchString = $('#SearchString').val();
        var reviewer = $('#AllReviewersSelectList').val();
        var SearchStartDate = $('#searchstartdatesel').val();
        var SearchEndDate = $('#searchenddatesel').val();
        if (npaType == "") npaType = "0";
        if (reviewer == "") reviewer = "0";
        if (searchString == null || searchString == "") searchString = null;

        $("#dialog").dialog({
            modal: true,
            autoOpen: false,

            width: 400,
            height: 200,
            buttons: [
                {
                    id: "Yes",
                    text: "Cancel Recurring",
                    click: function () {
                        openWarning("Cancelling...", false);

                        $.ajax({
                            url: "DeleteAndSearchNPA",
                            type: "POST",
                            data: {
                                //"npaIds": npaIds,
                                "scheduleIds": scheduleIds,
                                "type": npaType,
                                "reviewerId": reviewer,
                                "searchtxt": searchString,
                                "startdate": SearchStartDate,
                                "enddate": SearchEndDate,
                                "cancelRecurringflag": true
                            },
                            success: function json(result) {
                                $('#divModifyNPAList').html(result);
                                closeSuccess();
                                //reinialize buttons/dropdowns for partial
                                initialize();
                                initializeOpenAddRemove();
                            },
                            error: function () { openError("error"); }
                        });
                        $(this).dialog("close");

                    }
                },
                {
                    id: "No",
                    text: "Cancel Single Event",
                    click: function () {
                        openWarning("Cancelling...", false);

                        cancelRecurringflag = false;
                        $.ajax({
                            url: "DeleteAndSearchNPA",
                            type: "POST",
                            data: {
                                "scheduleIds": scheduleIds,
                                "type": npaType,
                                "reviewerId": reviewer,
                                "searchtxt": searchString,
                                "startdate": SearchStartDate,
                                "enddate": SearchEndDate,
                                "cancelRecurringflag": false
                            },
                            success: function json(result) {
                                $('#divModifyNPAList').html(result);
                                closeSuccess();
                                //reinialize buttons/dropdowns for partial
                                initializeOpenAddRemove();
                            },
                            error: function () { openError("error"); }
                        });
                        $(this).dialog("close");
                    }
                }
            ]
        });

        $(".mChkBx").each(function () {
            if ($(this).is(':checked')) {
                var tempID = $(this).val();
                var splitString = tempID.split('-');
                scheduleIds.push(splitString[0]);
                npaRecurring.push(splitString[1]);
            }
        });

        // do not ask about
        if (scheduleIds.length == 1) {
            if (npaRecurring.indexOf('Y') != -1) {
                $('#dialog').dialog('open');
            }
            else {
                deleteModifySchedules(scheduleIds, npaType, reviewer, searchString, SearchStartDate, SearchEndDate, false);
            }
        }

        if (scheduleIds.length > 1) {

            if (!confirm("Are you sure you want to delete multiple NPAs")) {
                e.preventDefault(); //stop form submission
                return;
            }
            else {
                deleteModifySchedules(scheduleIds, npaType, reviewer, searchString, SearchStartDate, SearchEndDate, false);
            }
        }
        else if (scheduleIds.length < 1) {
            openWarning("Please select NPA to delete ");
            e.preventDefault(); //stop form submission
            return;
        }
    });

    $("#deleteButtonending").on("click", function (e) {
        var scheduleIds = [];

        var npaType = $('#npatypeselectlistending').val();
        var searchString = $('#SearchStringending').val();
        var reviewer = $('#allreviewersselectlistidending').val();
        var SearchStartDate = $('#searchstartdateselending').val();
        var SearchEndDate = $('#searchenddateselending').val();
        var cancelRecurring = false;

        if (npaType == "") npaType = "0";
        if (reviewer == "") reviewer = "0";
        if (searchString == null) searchString = "";
        if (SearchStartDate == null) SearchStartDate = "";
        if (SearchEndDate == null) SearchEndDate = "";

        $(".esChkBx").each(function () {
            if ($(this).is(':checked')) {
                scheduleIds.push($(this).val());
            }
        });

        if (scheduleIds.length > 1) {

            if (!confirm("Are you sure you want to delete multiple NPAs")) {
                e.preventDefault(); //stop form submission
                return;
            }
            else {
                deleteEndingSoonSchedules(scheduleIds, npaType, reviewer, searchString, SearchStartDate, SearchEndDate, cancelRecurring);
            }
        } else if (scheduleIds.length < 1) {
            openWarning("Please select NPA to delete ");
            e.preventDefault(); //stop form submission
            return;
        }
        else {
            deleteEndingSoonSchedules(scheduleIds, npaType, reviewer, searchString, SearchStartDate, SearchEndDate, cancelRecurring);
        }
    });

    function deleteModifySchedules(scheduleIds, npaType, reviewer, searchString, SearchStartDate, SearchEndDate, cancelRecurring) {
        openWarning("Deleting...", false);
        $.ajax({
            url: "DeleteAndSearchNPA",
            type: "POST",
            data: {
                "scheduleIds": scheduleIds,
                "type": npaType,
                "reviewerId": reviewer,
                "searchtxt": searchString,
                "startdate": SearchStartDate,
                "enddate": SearchEndDate,
                "cancelRecurringflag": cancelRecurring
            },
            success: function json(result) {
                $("#divModifyNPAList").html(result);
                closeSuccess();
                //reinialize buttons/dropdowns for partial
                initialize();
                initializeOpenAddRemove();
            },
            error: function () { openError("error"); }
        });
        $(this).dialog("close");
    }
    function deleteEndingSoonSchedules(scheduleIds, npaType, reviewer, searchString, SearchStartDate, SearchEndDate, cancelRecurring) {
        openWarning("Deleting...", false);
        $.ajax({
            url: "DeleteAndSearchNPAEnding",
            type: "POST",
            data: {
                "scheduleIds": scheduleIds,
                "type": npaType,
                "reviewerId": reviewer,
                "searchtxt": searchString,
                "startdate": SearchStartDate,
                "enddate": SearchEndDate,
                "cancelRecurringflag": cancelRecurring
            },
            success: function json(result) {
                $("#divEndingNPAList").html(result);
                closeSuccess();
                //reinialize buttons/dropdowns for partial
                initialize();
                initializeOpenAddRemove();
            },
            error: function () { openError("error"); }
        });
        $(this).dialog("close");
    }

    $(".searchbutton").on("click", function () {
        var npaType = $('#NPATypeSelectList').val();
        var searchString = $('#SearchString').val();
        var reviewer = $('#AllReviewersSelectList').val();

        var SearchStartDate = $('#searchstartdatesel').val();
        var SearchEndDate = $('#searchenddatesel').val();

        if (npaType == "") npaType = "0";
        if (reviewer == "") reviewer = "0";
        if (searchString == null) searchString = "";
        if (SearchStartDate == null) SearchStartDate = "";
        if (SearchEndDate == null) SearchEndDate = "";

        openWarning("Refreshing NPA Search...", false);
        $.ajax({
            url: "SearchNPA",
            type: "GET",
            data: {
                "type": npaType,
                "reviewerId": reviewer,
                "searchtxt": searchString,
                "startdate": SearchStartDate,
                "enddate": SearchEndDate
            },
            success: function json(result) {
                $('#divModifyNPAList').html(result);

                $('.skin-square input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                });
                closeSuccess();
                //reinialize buttons/dropdowns for partial
                initialize();
                initializeOpenAddRemove();
            },
            error: function () { openError("error"); }
        });
    });

    $(".Searchbuttonendingnpa").on("click", function () {
        var npaType = $('#npatypeselectlistending').val();
        var searchString = $('#SearchStringending').val();
        var reviewer = $('#allreviewersselectlistidending').val();
        var SearchStartDate = $('#searchstartdateselending').val();
        var SearchEndDate = $('#searchenddateselending').val();
        if (npaType == "") npaType = "0";
        if (reviewer == "") reviewer = "0";
        if (searchString == null) searchString = "";
        if (SearchStartDate == null) SearchStartDate = "";
        if (SearchEndDate == null) SearchEndDate = "";
        openWarning("Refreshing 'Ending Soon' NPA Search...", false);
        $.ajax({
            url: "EndingSearchNPA",
            type: "GET",
            data: {
                "type": npaType,
                "reviewerId": reviewer,
                "searchtxt": searchString,
                "startdate": SearchStartDate,
                "enddate": SearchEndDate
            },
            success: function json(result) {
                $('#divEndingNPAList').html(result);

                closeSuccess();
                //reinialize buttons/dropdowns for partial
                initialize();
                initializeOpenAddRemove();
            },
            error: function () { openWarning("error"); }
        });
    });

    $("#dialog-modal-conflicts").dialog({
        modal: true,
        autoOpen: false,
        height: "auto",
        width: 800,
        buttons: {
            Accept: function () {
                createNPA();
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        title: 'Express Reservation/Schedule Conflicts'
    });

    EnableDisableControls($("#drpdownReccurence"));

    $("#drpdownReccurence").on("change", function () {
        EnableDisableControls(this);
    });


    $(".createnpabutton").on("click", function () {
        console.log("click savebutton")
        if ($("#NPAForm").valid()) {
            expressConflicts();
        }
    });
    /************************Meeting Room ********************************/
    $(".meetingroomnamelabel").text(" - " + $("#hMeetingroomname").val() + " - ");

    //validate form for Create NPA
    //do not submit, use ajax CreateNPA function instead
    $("#NPAForm").on("submit", function (e) {
        e.preventDefault();
    })
        .validate({
            rules: {
                NPATypeSelected: "required",
                NPAName: "required",
                RecurrenceSelected: "required",
                DaySelected: "required",
                StartDate: { required: true, isValidDate: true },
                EndDate: {
                    required: {
                        depends: function (element) {
                            return $("#RecurrenceSelected").val() != "Once";
                        }
                    },
                    isAfterStartDate: true,
                    isValidDate: true
                },
                StartTime: {
                    required: {
                        depends: function (element) {
                            return $("#AllDay").is(":checked") == false;
                        }
                    }
                },
                EndTime: {
                    required: {
                        depends: function (element) {
                            return $("#AllDay").is(":checked") == false;
                        }
                    }
                },
                YNSelected: "required",
                deptreviewersselected: "required"
            },
            messages: {
                NPATypeSelected: "Type of NPA is required.  ",
                NPAName: "Name of NPA is required.  ",
                RecurrenceSelected: "Reccurence of NPA is required.  ",
                DaySelected: "Day of NPA is required.  ",
                StartDate: "Start Date of NPA is required.  ",
                EndDate: "End Date of NPA is required.  ",
                StartTime: "Either Time or all day of NPA is required.  ",
                EndTime: "Either Time or all day of NPA is required.  ",
                YNSelected: "All plan reviewers selection for NPA is required.  ",
                deptreviewersselected: "All plan reviewers selection or at least 1 plan reviewer for NPA is required.  "
            },
            ignore: ".ignore",
            invalidHandler: function (event, validator) {
                var nm = "";
                for (var i = 0; i < validator.errorList.length; i++) {
                    var obj = validator.errorList[i];
                    nm += obj.message;
                }
                // 'this' refers to the form
                var errors = validator.numberOfInvalids();
                if (errors) {
                    var message = errors == 1
                        ? 'You missed 1 field.  '
                        : 'You missed ' + errors + ' fields.  ';
                    openWarning(message + nm, true);
                }
            },
            submitHandler: function (form) {
                return false;
            },
            errorPlacement: function (error, element) {
                error.appendTo(element.parent("div").next("div"));
            },
        });
    /////////////
    var isAfterStartDate = function (startDateStr, endDateStr) {
        try {
            if ($.datepicker.parseDate("mm/dd/yy", startDateStr) > $.datepicker.parseDate("mm/dd/yy", endDateStr))
                return false;
            return true;
        } catch (e) {
            //parse failed
            return false;
        }
    };
    $.validator.addMethod("isAfterStartDate", function (value, element) {
        return isAfterStartDate($('#startdatesel').val(), value);
    }, "End date should be after start date");
    //////////////
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

    $('.selectalldeptreviewers').on("ifChanged", function () {
        deptReviewerSelected();
    });
    $('.selectdeptreviewer').on("change", function () {
        deptReviewerSelected();
    });
    $('.ynselectallreviewers').on("ifChanged", function () {
        //select all of everything if Y, deselect if N
        var selected = ($(this).val() == "Y");
        selectAllReviewers(selected);
        deptReviewerSelected();
    });
    $('.recurrenceSelected').on("change", function () {
        //if once, disable end date
        if ($(this).val() == "Once") {
            $("#enddatesel").prop("readonly", true);
        } else {
            $("#enddatesel").prop("readonly", false);
        }
    });

    $('.selectdeptreviewer').multiselect({ noneSelectedText: "None Selected", buttonWidth: '200px', selectedList: 2 });

    $(".bldgselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msBldgReviewers", selected);
    });
    $(".mechselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msMechReviewers", selected);
    });
    $(".elecselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msElecReviewers", selected);
    });
    $(".plumselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msPlumReviewers", selected);
    });
    $(".zoniselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msZoniReviewers", selected);
    });
    $(".fireselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msFireReviewers", selected);
    });
    $(".foodselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msFoodReviewers", selected);
    });
    $(".poolselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msPoolReviewers", selected);
    });
    $(".faciselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msFaciReviewers", selected);
    });
    $(".daycselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msDayCReviewers", selected);
    });
    $(".backselectall").on("ifChanged", function () {
        var selected = $(this).is(":checked");
        selectall("msBackReviewers", selected);
    });
});

function initialize() {
    $("#npamodifylist").DataTable();
    $("#npaendinglist").DataTable();

    $('.skin-square input').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    if ($("#hHolidayslist").length) {
    var holidayslist = $("#hHolidayslist").val();
    var holidayarr = holidayslist.split(",");

    for (var i = 0; i < holidayarr.length; i++) {
        var date = new Date(holidayarr[i]);
        var year = date.getFullYear();
        var month = date.getMonth();
        var day = date.getDate();
        var formattedDate = new Date(year, month, day);
        holidays.push(formattedDate);
        }
    }
}
function initializeOpenAddRemove() {
    $(document).on("click", ".openaddrem", function () {
        //get projectscheduleid
        var schid = $(this).data("id");
        var npaid = $(this).data("npaid");
        $(".input-schid").val(schid);
        $(".input-npaid").val(npaid);
        $(".ddl-dept").val("Building");
        //build available for Building as it's always first
        buildAvailableSel("ddl-availdept", "Building", schid);
        //build attendees
        buildAttendeeSel("ddl-attendees", "Building", schid);
        //update so there aren't duplicates
        updateSelectlists("ddl-attendees", "ddl-availdept");
        //$(".modal-attendees").dialog("open");

        //reset the arrowsbutton behavior everytime clicking on it
        $('.multiselect-add, .multiselect-remove').prop("disabled", true);
        var isFromListEmpty = $('.multiselect-from').children('option').length == 0;
        var isToListEmpty = $('.multiselect-to').children('option').length == 0;

        $('.multiselect-addall').prop("disabled", isFromListEmpty);
        $('.multiselect-removeall').prop("disabled", isToListEmpty);
    });

}
//Sends Ajax request to save attendees
function updateAttendees(attids, npaid, wkrid, schid) {
    var a = new Object();
    a.wrkrId = wkrid;
    a.NPAID = npaid;
    a.attIds = attids;
    a.schId = schid;
    if (a != null) {
        openWarning("Updating attendees...", false)
        $.ajax({
            type: "POST",
            url: "/NPA/UpdateAttendees",
            data: JSON.stringify(a),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            statusCode: {
                404: function () {
                    openError("page not found", true);
                }
            },
            success: function (response) { openWarning(response, true); },
            failure: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}

//npa/addremoveattendees.js
//removes already selected attendees from Available list
function updateSelectlists(comparefromid, comparetoid) {
    $('#' + comparefromid + ' option').each(function () {
        $('#' + comparetoid).find('[value="' + $(this).val() + '"]').remove();
    });

}

//rebuild select list based on name list
function buildAttendeeSel(objname, namelistid, schid) {
    var procemessage = "<option value='0'> Please wait...</option>";
    $("#" + objname).html(procemessage).show();

    var dataobj = arrAttendees.find(x => x.id === schid).dat;
    var data = $.grep(dataobj, function (n, i) { return (n.DeptNameListId === namelistid); });

    var markup = "";
    for (var x = 0; x < data.length; x++) {

        markup += "<option value=" + data[x].AttendeeId + ">" + data[x].FirstName + " " + data[x].LastName + "</option>";
    }
    $("#" + objname).html(markup).show();
}

//rebuild select list based on name list
function buildAvailableSel(objname, namelistid, schid) {
    var procemessage = "<option value='0'> Please wait...</option>";
    $("#" + objname).html(procemessage).show();
    var data = $.grep(arrAvailableReviewers, function (n, i) { return (n.DeptNameListId === namelistid); });

    var markup = "";
    for (var x = 0; x < data.length; x++) {

        markup += "<option value=" + data[x].AttendeeId + ">" + data[x].FirstName + " " + data[x].LastName + "</option>";
    }
    $("#" + objname).html(markup).show();
}

//_CreateNPA.cshtml
function EnableDisableControls(obj) {
    var value = $(obj).val();
    if (value == "Once") {
        $("#drpdownDay").prop('disabled', true);
        $("#enddatesel").prop('disabled', true);

    }
    else if (value == "Yearly" || value == "Daily") {
        $("#drpdownDay").prop('disabled', true);
        $("#enddatesel").prop('disabled', false);
        $("#enddatesel").removeProp('disabled');
    }
    else {
        $("#drpdownDay").prop('disabled', false);
        $("#drpdownDay").removeProp('disabled');
        $("#enddatesel").prop('disabled', false);
        $("#enddatesel").removeProp('disabled');
    }
}
function deptReviewerSelected() {
    var i = "";
    //if any checkbox
    if ($("input.selectalldeptreviewers:checked").length > 0) {
        i = "Y";
    }
    //check all plan reviewers dropdown "Y","N"
    if ($(".ynselectallreviewers").val() == "Y") {
        i = "Y";
    }
    //if any dropdown
    $(".selectdeptreviewer option:selected").each(function () {
        if ($(this).val() != -1) {
            i = "Y";
        }
    });
    $("#deptreviewersselected").val(i);
    return false;
}

function createModelObject() {
    o.ID = $("#wkrID").val();
    o.LoggedInUserEmail = $("#wkrEmail").val();
    o.LoggedInUserID = $("#wkrID").val();
    o.NPAID = $("#NPAID").val();
    o.NPATypeSelected = $("#NPATypeSelected").val();
    o.NPAName = $("#NPAName").val();
    o.RecurrenceSelected = $("#drpdownReccurence").val();
    o.DaySelected = $("#drpdownDay").val();
    o.StartDate = $("#startdatesel").val();
    o.EndDate = $("#enddatesel").val();
    o.StartTime = $("#starttimesel").val();
    o.EndTime = $("#endtimesel").val();
    o.AllDay = $("#AllDay").is(':checked');
    o.YNSelected = $("#YNSelected").val();
    o.MeetingRoomRefIDSelected = $("#MeetingRoomRefIDSelected").val();
    o.MeetingRoomNameSelected = $("#MeetingRoomNameSelected").val();
    o.BldgSelectAll = $("#BldgSelectAll").is(':checked');
    o.ElecSelectAll = $("#ElecSelectAll").is(':checked');
    o.MechSelectAll = $("#MechSelectAll").is(':checked');
    o.PlumSelectAll = $("#PlumSelectAll").is(':checked');
    o.ZoniSelectAll = $("#ZoniSelectAll").is(':checked');
    o.FireSelectAll = $("#FireSelectAll").is(':checked');
    o.BackSelectAll = $("#BackSelectAll").is(':checked');
    o.FoodSelectAll = $("#FoodSelectAll").is(':checked');
    o.PoolSelectAll = $("#PoolSelectAll").is(':checked');
    o.FaciSelectAll = $("#FaciSelectAll").is(':checked');
    o.DayCSelectAll = $("#DayCSelectAll").is(':checked');
    o.BldgReviewerSelected = $("#msBldgReviewers").val() || [];
    o.ElecReviewerSelected = $("#msElecReviewers").val() || [];
    o.MechReviewerSelected = $("#msMechReviewers").val() || [];
    o.PlumReviewerSelected = $("#msPlumReviewers").val() || [];
    o.ZoniReviewerSelected = $("#msZoniReviewers").val() || [];
    o.FireReviewerSelected = $("#msFireReviewers").val() || [];
    o.BackReviewerSelected = $("#msBackReviewers").val() || [];
    o.FoodReviewerSelected = $("#msFoodReviewers").val() || [];
    o.PoolReviewerSelected = $("#msPoolReviewers").val() || [];
    o.FaciReviewerSelected = $("#msFaciReviewers").val() || [];
    o.DayCReviewerSelected = $("#msDayCReviewers").val() || [];
}

function createNPA() {
    createModelObject();
    verifyObj();
    if (o != null) {
        openInProgress("Creating NPA...", false)
        $.ajax({
            type: "POST",
            url: "/NPA/CreateNPA",
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            statusCode: {
                404: function () {
                    openError("page not found", true);
                }
            },
            success: function (response) {
                o = new Object();
                openSuccess(response, true);
                resetCreateNPAMultiSelects();
                EnableDisableControls($("#drpdownReccurence"));
            },
            failure: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}

function expressConflicts() {
    createModelObject();
    if (o != null) {
        openInProgress("Fetching schedules...", false)
        $.ajax({
            type: "POST",
            url: "/NPA/ExpressConflicts",
            data: JSON.stringify(o),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            statusCode: {
                404: function () {
                    openConflicts("page not found", true);
                }
            },
            success: function (response) {
                var message = getConflictMessage(response);
                if (message != "") {
                    closeSuccess();
                    openWarning(message, true);
                }
                else {
                    createNPA();
                }
            },
            failure: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}

function verifyObj() {
    if (o != null) {
        if (o.RecurrenceSelected == "Once") {
            //set end date
            o.EndDate = o.StartDate;
            $("#enddatesel").val(o.StartDate);

        }
        //check if dates are more than 2 years in advance
        if (o.RecurrenceSelected != "Once") {

        }

    }
}

function getConflictMessage(response) {
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

function selectAllReviewers(selected) {
    $(".bldgselectall").prop("checked", selected);
    selectall("msBldgReviewers", selected);
    $(".mechselectall").prop("checked", selected);
    selectall("msMechReviewers", selected);
    $(".elecselectall").prop("checked", selected);
    selectall("msElecReviewers", selected);
    $(".plumselectall").prop("checked", selected);
    selectall("msPlumReviewers", selected);
    $(".zoniselectall").prop("checked", selected);
    selectall("msZoniReviewers", selected);
    $(".fireselectall").prop("checked", selected);
    selectall("msFireReviewers", selected);
    $(".foodselectall").prop("checked", selected);
    selectall("msFoodReviewers", selected);
    $(".poolselectall").prop("checked", selected);
    selectall("msPoolReviewers", selected);
    $(".faciselectall").prop("checked", selected);
    selectall("msFaciReviewers", selected);
    $(".daycselectall").prop("checked", selected);
    selectall("msDayCReviewers", selected);
    $(".backselectall").prop("checked", selected);
    selectall("msBackReviewers", selected);
}
function resetCreateNPAMultiSelects() {
    $('.selectdeptreviewer').val('').multiselect('refresh');
}
