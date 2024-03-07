//Admin Create Role tab

//onload event
$(function () {
    //onload, get the choices for whatever is default
    GetSystemRolePerms("#createsrparent", "#CreateRoleViewModel_", ".createroleperm");

    //on dropdown change, get the perms
    $("#createsrparent").on("change", function () {
        GetSystemRolePerms("#createsrparent", "#CreateRoleViewModel_", ".createroleperm");
    });
});

