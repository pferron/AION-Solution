
//get the system role perms to set permissions checkboxes

function GetSystemRolePerms(parentsystemrole, checkboxvm, checkboxclass) {
    var o = new Object();
    var systemroleid = $(parentsystemrole).val();
    o.systemroleid = systemroleid;
    if (systemroleid != null && systemroleid > 0) {
      /*  openSuccess("Searching...", false)*/
        $.ajax({
            type: "POST",
            url: "/Admin/GetSystemRolePermissions",
            data: o,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                setPermissionsCheckboxes(response, checkboxvm, checkboxclass);
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}
function setPermissionsCheckboxes(dataobj, checkboxvm, checkboxclass) {
    //list of object with names of true permissions
    //clear checkbox list
    clearPermCkbxs(checkboxclass);
    //for each permission that is true, check the box
    $.each(dataobj, function (i, data) {
        var checkboxid = checkboxvm + data;
        $(checkboxid).iCheck("check");
    });
}

//clear all checks in perm checkboxes on create role tab
function clearPermCkbxs(checkboxclass) {
    $(checkboxclass).each(function (i, item) {
        $(this).iCheck("uncheck");
    });

}