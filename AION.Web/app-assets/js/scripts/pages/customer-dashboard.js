$(function () {
    $("#projects").dataTable();

    $(".loggedinuseremail").on("change", function () {
        console.log("loggedinuseremail");
        $("#LoggedInUserEmail").val($(this).val());
    });
});