$(function () {
    $(".apptResponseSelect").on("change", function () {
        var tr = $(this).closest("tr");
        var response = $(this).val();
        if (response == 1) {
            var prProdDate = tr.find("#PlansReadyOnDate");
            prProdDate.val('');
        }
    });

    $(".updateProjectCycle").on("click", function (e) {
        e.preventDefault();
        var tr = $(this).closest("tr");
        var cycleId = $(this).attr("data-cycle-id");
        var reviewId = $(this).attr("data-review-id");
        var projectId = $(this).attr("data-project-id");
        var userId = $(this).attr("data-user");
        var prResponse = tr.find(".apptResponseSelect");
        var prStatusText = tr.find(".prStatusText").text();
        var prProdDate = tr.find("#PlansReadyOnDate").val();

        var responseValue = "-1";

        if (prResponse != null && prResponse.val() == "") {
            openSuccess("Please select a status");
            return;
        }

        if (prResponse != null && prResponse.length > 0) {
            responseValue = prResponse.val();
        }
        else {
            responseValue = "-1";
        }

        //Check for Self Schedule
        if (prResponse.val() == "3") {
            //set the relevant data for this plan review
            $("#prResponseEnum").val(prResponse.val());
            $("#prProdDateSelfSchedule").val(prProdDate);
            $("#prProjectCycleId").val(cycleId);
            $("#prUserId").val(userId);
            $("#prReviewId").val(reviewId);

            $("#SelfSchedule").modal("show");
            let minDate = Math.min(new Date(), Date.parse(prProdDate));
            $('#datepickerSS2').datepicker('option', 'minDate', minDate);
            return;
        }

        sendResponse(projectId, cycleId, reviewId, responseValue, prProdDate);

    });
});

function sendResponse(projectId, cycleId, reviewId, responseValue, prProdDate) {
    var model = {
        "projectId": projectId,
        "projectCycleId": cycleId,
        "planReviewScheduleId": reviewId,
        "response": responseValue,
        "prod": prProdDate,
    };

    var url = "/" + controller.value + "/UpdateProjectCycle";

    if ($("#isProdNotKnown").val() == "True") {
        url = "/" + controller.value + "/UpdateProjectCyclePROD";
    }

    openInProgress("Updating...", false);

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        statusCode: {
            404: function () {
                openError("page not found", true);
            }
        },
        success: function (response) {
            openSuccess("Updated", true);
            $("#planReviews").empty();
            $("#planReviews").html(response);

            GetProjectAudits(projectId);
        },
        failure: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); },
        error: function (jqXHR, textStatus, errorThrown) { openError("Error: " + textStatus + " : " + errorThrown, true); }
    });
}

function GetProjectAudits(projectId)
{
    var model = {
        "projectId": projectId
    };

    var url = "/" + controller.value + "/ProjectAudits";

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        statusCode: {
            404: function () {
                openSuccess("page not found", true);
            }
        },
        success: function (response) {
            $("#projectAudits").empty();
            $("#projectAudits").html(response);

            GetProjectStatus(projectId);

        },
        failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
        error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
    });
}

function GetProjectStatus(projectId) {
    var model = {
        "projectId": projectId
    };

    var url = "/" + controller.value + "/GetProjectStatus";

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        statusCode: {
            404: function () {
                openSuccess("page not found", true);
            }
        },
        success: function (response) {
            $("#projectStatus").empty();
            $("#projectStatus").html(response);

            if (response.indexOf("Not Scheduled") >= 0)
            {
                $("#schedulePlanReview").removeClass("disabled");
            }
        },
        failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
        error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
    });
}



