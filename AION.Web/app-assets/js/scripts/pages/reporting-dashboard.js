$(function () {

    $(".generateleadtimebtn").on("click", function () {
        openSuccess({ text: "You will receive an email when the reporting data has been refreshed and is ready to run.", timer: 3000 });
        var url = "/Reporting/GenerateSchedulingLeadTimeData";

        $.ajax
        ({
            method: "GET",
            datatype: "json",
            url: url
        }).done(function (response, status, jqxhr) {

        });
    });
});