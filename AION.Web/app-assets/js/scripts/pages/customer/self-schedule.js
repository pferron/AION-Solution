$(function () {
    var dateFormat = "mm/dd/yy",
        from = $("#datepickerSS1")
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
                
                let minDate = $('#datepickerSS1').datepicker('getDate')
                let maxDate = new Date(minDate)
                maxDate.setDate(minDate.getDate() + 14)
                $('#datepickerSS2').datepicker('destroy')
                console.log(minDate)
                $('#datepickerSS2').datepicker('option', 'minDate', minDate)
                $('#datepickerSS2').datepicker('option', 'maxDate', maxDate)

                if ($('#datepickerSS1').val() > $('#datepickerSS2').val()) {
                    $('#datepickerSS2').val($('#datepickerSS1').val())
                }
                $('#datepickerSS2').datepicker({ minDate: minDate, maxDate: maxDate, beforeShowDay: $.datepicker.noWeekends })

            })
            .datepicker("setDate", "0"),
        to = $("#datepickerSS2").datepicker({
            defaultDate: "+2w",
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            minDate: 0,
            maxDate: "+2W",
            yearRange: "0:+2"
        })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            })
            .datepicker("setDate", "+2W");

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }

        return date;
    }
    //Self-schedule
    if ($("#aionProjectStatus").html() == "Scheduled") {
        $("#apptResponseselect", "#expressApptResponseselect").prop("disabled", true);
    }

    $('.proactive').each(function () {
        $(this).prop("disabled", false);
    });

    $("#searchSelfScheduleDates").on("click", function () {
        let startSearchDate = new Date(Date.parse($('#datepickerSS1').datepicker('getDate')))
        let endSearchDate = new Date(Date.parse($('#datepickerSS2').datepicker('getDate')))

        $('#SelfScheduleDatesList').html('')
        $("#selfSchStatusMsg").html("Searching...");
        $('#searchSelfScheduleDates').addClass('disabled')

        $.ajax({
            url: "SearchSelfScheduleDates",
            type: "POST",
            data: {
                "EarliestDate": $('#datepickerSS1').val(),
                "UpdatedDate": $('#datepickerSS2').val(),
                "ApptResponseStatusEnum": $('#prResponseEnum').val(),
                "ProjectId": $('#ProjectId').val(),
                "AccelaProjectRefId": $("#AccelaProjectRefId").val(),
                "ProjectCycleId": $("#prProjectCycleId").val(),
                "ProdDate": $("#prProdDateSelfSchedule").val()
            },
            success: function (res) {
                if (res.dates.length <= 0) {
                    $("#selfSchStatusMsg").html("No dates found, please try searching again.");
                } else {
                    $("#selfSchStatusMsg").html("");
                }
                res.dates.forEach(function (date) {
                    let tmpdate = new Date(Date.parse(date))
                    $('#SelfScheduleDatesList').append(`<input type="radio" id="${tmpdate.toDateString()}" name="selfScheduleDate" value="${tmpdate.toDateString()}"><label for="${tmpdate.toDateString()}">${tmpdate.toDateString()}</label><br>`)
                })
                $('#searchSelfScheduleDates').removeClass('disabled')
            },
            error: function (res) {
                $('#SelfSchedule').modal("hide");
                $('#selfScheduleError').modal("show");
                $('#searchSelfScheduleDates').removeClass('disabled');
                $('#selfScheduleError > .modal-body').html(res.responseText);
                
            }
        })
    });

    $('#submitSelfSchedule').on("click", function () {
        console.log($('input[name="selfScheduleDate"]:checked').val());
        if ($('input[name="selfScheduleDate"]:checked').val() == null) {
            $("#selfSchStatusMsg").html("Please select a date");
            $("#selfSchStatusMsg").focus();
        } else {

            SubmitSelfSchedule();
        }
    });

});

//Schedule the selected date
function SubmitSelfSchedule() {
    $("#selfSchStatusMsg").removeClass("alert-danger");
    $("#selfSchStatusMsg").addClass("alert-info");
    $("#selfSchStatusMsg").html("Submitting Self Schedule...");
    $("#submitSelfSchedule").addClass("disabled");
    $(".closeSelfSchedule").addClass("disabled");

    let earliestDate = new Date(Date.parse($('input[name="selfScheduleDate"]:checked').val()));

    let formattedDate = ((earliestDate.getMonth() > 8) ? (earliestDate.getMonth() + 1) : ('0' + (earliestDate.getMonth() + 1))) + '/' + ((earliestDate.getDate() > 9) ? earliestDate.getDate() : ('0' + earliestDate.getDate())) + '/' + earliestDate.getFullYear()

    var projectId = $('#ProjectId').val();

    $("#SelfSchedule").scrollTop(0);

    var id = $("#prProjectCycleId").val();
    //set to Accept so if success we can set to scheduled
    var successResponseValue = "2";
    var prProdDate = $('#prProdDateSelfSchedule').val();
    var userId = $("#prUserId").val();
    var reviewId = $("#prReviewId").val();

    var data = {
        "EarliestDate": formattedDate,
        "ApptResponseStatusEnum": $('#prResponseEnum').val(),
        "ProjectId": projectId,
        "AccelaProjectRefId": $("#AccelaProjectRefId").val(),
        "ProdDate": prProdDate,
        "ProjectCycleId": id
    };
    $.ajax({
        url: "SubmitSelfScheduleDate",
        dataType: "json",
        type: "POST",
        data: data,
        success: function (responseobj) {
            //if there's an error response, don't close the modal
            //display the status message in the status message
            //change status message to
            var success = responseobj.success;
            var status = responseobj.status;
            if (success == true) {
                $('#aionProjectStatus').html(status);
                $("#SelfSchedule").modal("hide");
                $("#selfSchStatusMsg").html("");
                $("#submitSelfSchedule").removeClass("disabled");
                $(".closeSelfSchedule").removeClass("disabled");
                $("#apptResponseselect").prop("disabled", true);
                $("#apptResponseselect option:contains('Accept')").attr('selected', 'selected');

                var date = new Date(earliestDate);
                var scheduledDate = getFormattedDate(date);
                $("#" + projectId).find('.scheduledDate').text(scheduledDate);
                $('#apptResponseselect').addClass('d-none')
                $('#acceptedLabel').removeClass('d-none')

                sendResponse(projectId, id, reviewId, successResponseValue, prProdDate);

            } else {

                $("#selfSchStatusMsg").focus();
                $("#selfSchStatusMsg").html(status);
                $("#selfSchStatusMsg").removeClass("alert-info");
                $("#selfSchStatusMsg").addClass("alert-danger");
                $("#submitSelfSchedule").removeClass("disabled");
                $(".closeSelfSchedule").removeClass("disabled");
                $("#apptResponseselect").prop("disabled", false);

            }
        },
        error: function (res) {

            $("#selfSchStatusMsg").focus();
            $("#selfSchStatusMsg").html("Error:" + res.responseText);
            $("#selfSchStatusMsg").removeClass("alert-info");
            $("#selfSchStatusMsg").addClass("alert-danger");
            $("#submitSelfSchedule").removeClass("disabled");
            $(".closeSelfSchedule").removeClass("disabled");
            $("#apptResponseselect").prop("disabled", false);

        }

    });


    function getFormattedDate(date) {
        var year = date.getFullYear();
        var month = (1 + date.getMonth()).toString();
        month = month.length > 1 ? month : '0' + month;
        var day = date.getDate().toString();
        day = day.length > 1 ? day : '0' + day;
        return month + '/' + day + '/' + year;
    }

}