//Admin Modify Role tab
//modifyrolesel

//onload event
$(function () {
    //onload, get the choices for whatever is default
    GetSystemRolePerms("#modifyrolesel", "#ModifyRoleViewModel_", ".modifyroleperm");

    //on dropdown change, get the perms
    $("#modifyrolesel").on("change", function () {
        GetSystemRolePerms("#modifyrolesel", "#ModifyRoleViewModel_", ".modifyroleperm");
    });
});
