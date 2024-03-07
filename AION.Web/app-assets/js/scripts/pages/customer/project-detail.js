//onload events
$(function () {
    $('#projects').DataTable();
    $('#PendingResponse').DataTable();
    $('#notes-view-table').DataTable();
    /*DatePicker settings in Request Express Modal & meeting date options*/
    $("#datepicker4,#datepicker5,#datepicker6, #datepickerPM1, #datepickerPM2, #datepickerPM3").datepicker({
        defaultDate: 0,
        changeMonth: true,
        changeYear: true,
        minDate: 0,
        numberOfMonths: 1,
        maxDate: "+2Y",
        yearRange: "0:+2",
        beforeShowDay: $.datepicker.noWeekends
    });
    /*form validation*/
    $('#CustomerProjectForm').validate({
        debug: false,
        ignore: ".ignore",
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
            $("#btnSubmit").addClass("disabled");
            $("div.error").html("");
            $("div.error").hide();
            form.submit();
        }
    });

    $(".apptResponseSelect.express").on("change", function () {
        var selectedMode = $(this).children("option:selected").text();

        var tr = $(this).closest("tr");
        var planReviewId = tr.find(".planReviewScheduleId").val();

        if (selectedMode == 'Reject') {
            $("#PlanReviewScheduleId").val(planReviewId);

            $("#RequestExpress").modal("show");
        }
    });

    $("#submitExpressRequestDateOptions").on("click", function () {
        openInProgress("Submitting...",false);
        var planReviewScheduleId = $("#PlanReviewScheduleId").val();
        var selectedDate = [];
        $('input.chkExpressDate:checked').each(function (index) {
            selectedDate.push($(this).val());
        });

        $.ajax({
            url: "SubmitExpressDateRequest",

            type: "POST",
            data: {
                "PlanReviewScheduleId": planReviewScheduleId,
                "requestDate1": selectedDate[0],
                "requestDate2": selectedDate[1],
                "requestDate3": selectedDate[2],
                "ProjectId": $('#ProjectId').val()
            },
            success: function () {
                closeSuccess();
                var tr = $('#tblPlanReviews tr:has(.planReviewScheduleId[value="' + planReviewScheduleId + '"])');
                var scheduledDate = tr.find(".scheduledDate");
                var responseDate = tr.find(".responseDate");
                var approvedText = tr.find(".response");

                scheduledDate.text("");
                responseDate.text("");
                approvedText.text("Rejected");
                $('.prStatusText').html("");
                $('.updateProjectCycle').addClass("disabled");
                $('#PRResponse').css("display", "none");
                openSuccess("Your requested dates have been submitted.");
            },
            error: function () {
                alert("error");
            }
        });

    });

    $("#searchForNewRequestDateOptions").on("click", function (e) {
        e.preventDefault();
        openInProgress("Searching...", false);

        var SearchStartDate = $('#datepicker_from').val();
        var SearchEndDate = $('#datepicker4_to').val();


        if (SearchStartDate.length == 0 || SearchEndDate.length == 0) {
            openWarning("Please fill out from and to dates.",false);
        }

        $.ajax({
            url: "/Customer/GetFiveAvailDatesForExpress",
            type: "POST",
            data: {
                "StartDate": SearchStartDate,
                "EndDate": SearchEndDate,
                "ProjectNumber": $("#AccelaProjectRefId").val()
            },
            success: function json(result) {
                $('#Express_AvailableDates').html("");

                var instruction = $('<div class="col"><h5>Select up to 3 dates</h5></div>');
                var divider = $('<div class="w-100"></div>');

                $('#Express_AvailableDates').append(instruction);
                $('#Express_AvailableDates').append(divider);
                closeSuccess();
                for (i = 0; i < result.length; i++) {
                    var formattedDate = new Date(parseInt(result[i].replace("/Date(", "").replace(")/", ""), 10)).toLocaleDateString();

                    var div = $('<div class="col"></div>');

                    var checkbox = $('<input type="checkbox" class="chkExpressDate" value="' + formattedDate + '" onchange="checkExpressChanged()" />');

                    var label = $('<label style="padding-left:10px"></label>').html(formattedDate);

                    div.append(checkbox);
                    div.append(label);

                    var divider = $('<div class="w-100"></div>');
                    $('#Express_AvailableDates').append(div);
                    $('#Express_AvailableDates').append(divider);
                }
                var h4 = $('<div class="col"><h4 class="modal-title"></h4></div>').html('Requested dates are not guaranteed to be scheduled on those dates');
                $('#Express_AvailableDates').append(h4);

                // Show or hide the dateSelectionContainer based on input
                if (result.length != 0 && SearchStartDate != "" && SearchEndDate != "") {
                    $("#NoResult").hide();
                    $("#dateSelectionContainer").show();
                    $("#submitExpressRequestDateOptions").prop('disabled', false);

                } else {
                    $("#dateSelectionContainer").hide();
                    $("#NoResult").show();
                    $("#submitExpressRequestDateOptions").prop('disabled', true);
                }
            },
            error: function () { alert("error"); }
        });
    });
});

function checkExpressChanged() {
    var checkedCount = $('input.chkExpressDate:checked').length;
    if (checkedCount < 3) {
        $("input.chkExpressDate").removeAttr("disabled");

    }
    else {
        var uncheckedList = $("input.chkExpressDate:not(:checked)");
        $('input.chkExpressDate:not(:checked)').each(function () { $(this).attr("disabled", true); });
    }
    //if ($(this).is(':checked')) {
    //    alert("this is checked");
    //}
    //else {
    //    alert("this is not checked");
}
