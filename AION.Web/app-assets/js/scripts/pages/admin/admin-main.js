$(function () {
    
    var dt = new Date();
    if (dt.getMonth() == 0) {
        $("#configureid").css("color", "Red");
        $("#configureid").show();
    }
    else {
        $("#configureid").hide();

    }

    $("#permissionusername").hide();

    $("#btn-submit").on("click", function () { ValidateAdminMain(); });
});


