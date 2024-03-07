
var o = new Object();

$(function () {
    //close the left navigation bar and footer
    $('.main-menu').hide();
    $('#navfooter').hide();
    //Init data table object
    $("#schedulecapacitylist").DataTable({
        "order": [],
        "ordering": false
    });

    //init div error 
        $("div.error").html("");
        $("div.error").hide();

    $("#filter").on("click", function () {
        GetScheduleCapacityDDLs();
    });

    GetScheduleCapacityDDLs();
    //***********************************
    ///////
    var dateFormat = "mm/dd/yy",
        from = $("#SCsearchstartdate")
            .datepicker({
                defaultDate: 0,
                changeMonth: true,
                changeYear: true,
                minDate: 0,
                numberOfMonths: 1,
                maxDate: "+2Y",
                yearRange: "0:+2"
            })
            .on("change", function () {
                to.datepicker("option", "minDate", getDate(this));
            })
            .datepicker("setDate", "0"),
        to = $("#SCsearchenddate").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            minDate: 0,
            maxDate: "+2Y",
            yearRange: "0:+2"
        })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            })
            .datepicker("setDate", "+1w");

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }

        return date;
    }

    $("#SCsearchenddate").on("change", function () {
        var today = new Date();
        var maxday = new Date(today.getFullYear() + 2, today.getMonth(), today.getDate());
        var currentval = getDate(this);
        if (currentval > maxday) {
            $(this).datepicker("setDate", maxday);
        }
    });

    $(".schcapsearch").on("click", function () {
        console.log("click search")
        searchSchedules();
    });
    //validate form
    $.validator.addMethod("isAfterStartDate", function (value, element) {
        return isAfterStartDate($('#SCsearchstartdate').val(), value);
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
    $('.SCselectdeptreviewer').on("change", function () {
        deptReviewerSelected();
    });


});
//**********end onload event **********************/

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
    $(".SCselectdeptreviewer option:selected").each(function () {
        if ($(this).val() != -1) {
            i = "Y";
        }
    });
    $("#SCdeptreviewersselected").val(i);
    return false;
}

function searchSchedules() {
    var o = new Object();
    o.StartDate = $("#SCsearchstartdate").val();
    o.EndDate = $("#SCsearchenddate").val();
    o.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    o.BldgReviewerSelected = $("#msSCBldgReviewers").val() || [];
    o.ElecReviewerSelected = $("#msSCElecReviewers").val() || [];
    o.MechReviewerSelected = $("#msSCMechReviewers").val() || [];
    o.PlumReviewerSelected = $("#msSCPlumReviewers").val() || [];
    o.ZoniReviewerSelected = $("#msSCZoniReviewers").val() || [];
    o.FireReviewerSelected = $("#msSCFireReviewers").val() || [];
    o.BackReviewerSelected = $("#msSCBackReviewers").val() || [];
    o.FoodReviewerSelected = $("#msSCFoodReviewers").val() || [];
    o.PoolReviewerSelected = $("#msSCPoolReviewers").val() || [];
    o.FaciReviewerSelected = $("#msSCFaciReviewers").val() || [];
    o.DayCReviewerSelected = $("#msSCDayCReviewers").val() || [];
    if (o != null) {
        openInProgress("Searching...", false)
        $.ajax({
            type: "POST",
            url: "/ScheduleCapacity/ScheduleCapacitySearch",
            data: o,


            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                closeSuccess();
                $('#searchtbody').html(response);
                $("#schedulecapacitylist").DataTable({
                    "order": [],
                    "ordering": false
                });

            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
        closeSuccess();
    }
}

function GetScheduleCapacityDDLs() {
    var args = new Object();
    args.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    $(".SCselectdeptreviewer").remove();
    //this dialog works as a modal
    openInProgress("Gathering data...", false);
    $("#scheduleCapDDLs").html("");
    $('#searchtbody').html("");
    var url = "/ScheduleCapacity/ScheduleCapacityDDLs";
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
            closeWarning();
            $("#scheduleCapDDLs").html(response);
            $(".SCselectdeptreviewer").multiselect({ buttonWidth: '200px', noneSelectedText: "None Selected", selectedList: 2 });
            console.log("ddls complete");
        });
}