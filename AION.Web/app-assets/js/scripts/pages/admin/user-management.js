$(function () {
    LoadUserConfigurations();

    ToggleIsSchedulable();

    $(document).on('show.bs.modal', '#occupancy-square-foot-modal', function (e) {
        var result = GetSelectedOccupancySquareFootage();
        if (result === false) {
            e.preventDefault();
            openWarning("Please search for or add a user.");
        };
    })

    $('#myModal').on('show.bs.modal', function (e) {
        var button = e.relatedTarget;
        if ($(button).hasClass('no-modal')) {
            e.stopPropagation();
        }
    });

    $(".apply-filter").on("click", function () {
        ApplyFilterToUsersUsers();
    });

    $("#btnOccupancySubmit").on("click", function () {
        var occupancy = new Array();
        var occId;
        var userId;
        var squareFootageIdSelected
        $(".chkBoxOccupancy").each(function () {
            if ($(this).is(':checked')) {
                //Occupency Id
                occId = $(this).val();
                //User Id
                var hdnUserId = "hdnUserId_" + occId;
                userId = $("#" + hdnUserId).val();
                //square footage selected id
                var hdnSquareFootageIdSelectedId = "hdnSquareFootageIdSelected_" + occId;
                squareFootageIdSelected = $('#' + hdnSquareFootageIdSelectedId).val();
                occupancy.push({ OccupancyIdSelected: occId, SquareFootageIdSelected: squareFootageIdSelected, UserId: userId });
            }
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: 'Createoccupancy',
            data: JSON.stringify(occupancy),
            success: function json(result) {
                $('#divsquarefootagebyoccupancy').html(result);
                $('#occupancySqFtModal').modal('hide');
                $('#drpDnLstLevel').val(" ");

            },
            error: function () { alert("error"); }
        });
    });

    $(".chkBoxOccupancy").each(function () {
        if ($(this).is(':checked')) {
            var ddId = "#occuDD_" + $(this).val();
            $(ddId).prop("disabled", false);
        } else {
            var ddId = "#occuDD_" + $(this).val();
            $(ddId).prop("disabled", true);
        }
    });

    $(".chkBoxOccupancy").on("change", function () {
        if ($(this).is(':checked')) {
            var ddId = "#occuDD_" + $(this).val();
            $(ddId).prop("disabled", false);
        } else {
            var ddId = "#occuDD_" + $(this).val();
            $(ddId).prop("disabled", true);
        }
    });
});

function validateCaseSensitiveEmail(email) {
    //var reg = /^([\w-\.]+)@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    //return reg.test(email);
    const validateEmail = (email) => {
        return String(email)
            .toLowerCase()
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    };
}

function LoadUserConfigurations() {
    $("#drpDnLstUsers").on("change", function () {
        UpdateUserManagementTabUserFilter();
    });
}
function ClearData() {

    $('option', $('#chkDrpDnLstRoles')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnLstRoles").multiselect('refresh');
    $('option', $('#chkDrpDnLstProjectTypes')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnLstProjectTypes").multiselect('refresh');
    $('option', $('#chkDrpDnLstTradeAgency')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnLstTradeAgency").multiselect('refresh');
    $('option', $('#chkDrpDnProjectTypesSelect')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnProjectTypesSelect").multiselect('refresh');

    $("#txtNotes").val('');
    $("#ttPlanReviewHoursOverride").val('');
    $("#txtLastName").val('');
    $("#txtFirstName").val('');
    $("#txtEmail").val('');
    $("#txtAdAccount").val('');
    $("#txtUserName").val('');
    $('#divsquarefootagebyoccupancy').html('');
    $('#drpDnLstExpress').prop('selectedIndex', 0);
    $('#drpDnLstLevel').prop('selectedIndex', 0);
    $('#drpDnLstPrilimMtg').prop('selectedIndex', 0);
    $('#drpDnLstSchedulable').prop('selectedIndex', 0);
    $('#drpDnLstActiveUSER').prop('selectedIndex', 0);
    $('#drpDnLstCityUSER').prop('selectedIndex', 0);
    $("#hrsEstimationLst").val('All');
}

function AddUserClick() {
    var userSelected = $("#drpDnLstUsers").children("option:selected").val();
    $('option', $('#chkDrpDnLstRoles')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnLstRoles").multiselect('refresh');

    $('option', $('#chkDrpDnLstProjectTypes')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnLstProjectTypes").multiselect('refresh');


    $('option', $('#chkDrpDnLstTradeAgency')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnLstTradeAgency").multiselect('refresh');


    $('option', $('#chkDrpDnProjectTypesSelect')).each(function (element) {
        $(this).removeAttr('selected').prop('selected', false);
    });
    $("#chkDrpDnProjectTypesSelect").multiselect('refresh');

    $('#drpDnLstUsers').empty().append($('<option>', {
        value: 0,
        text: 'Search for a User'
    }));

    $("#txtNotes").val('');
    $("#ttPlanReviewHoursOverride").val('');
    $("input:text").val("");
    $('#divsquarefootagebyoccupancy').html('');
    $('#drpDnLstExpress').prop('selectedIndex', 0);
    $('#drpDnLstUserType').prop('selectedIndex', 0);
    $('#drpDnLstLevel').prop('selectedIndex', 0);
    $('#drpDnLstPrilimMtg').prop('selectedIndex', 0);
    $('#drpDnLstSchedulable').prop('selectedIndex', 0);
    $('#drpDnLstActiveUSER').prop('selectedIndex', 0);
    $('#drpDnLstCityUSER').prop('selectedIndex', 0);
    $("#hrsEstimationLst").val('All');
}

function openUserPermission() {
    $('#nav-modifyrole-tab').tab('show');
    $("#permissionroledropdown").hide();
    $("#permissionusername").show();
    var userid = $("#drpDnLstUsers").val();
    $("#modifyrolesel").val(0);
    console.log(userid);
    if (userid <= 0) {
        $("#permissionnouser").show();
        console.log("show");
    } else {
        console.log("hide");
        $("#permissionnouser").hide();
        $("#permsusername").html($("#drpDnLstUsers option:selected").text());
        $("#PermissionUserId").val(userid);
        //load the permissions for this user
        //get the json back and load the permissions checkboxes
        GetUserPerms("#drpDnLstUsers", "#ModifyRoleViewModel_", ".modifyroleperm");
    }
}

function showRoleDropdown() {
    $("#permissionroledropdown").show();
    $("#permissionusername").hide();
}

function GetUserPerms(ddl, checkboxvm, checkboxclass) {
    var o = new Object();
    var userid = $(ddl).val();
    o.userid = userid;
    if (userid != null && userid > 0 && o != null) {
        openSuccess("Searching...", false)
        $.ajax({
            type: "POST",
            url: "/Admin/GetUserRolePermissions",
            data: o,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                setPermissionsCheckboxes(response, checkboxvm, checkboxclass);
                closeSuccess();
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}

// Last name can have dashes, dots and spaces but must be more than three letters.
function isLastNameValid(name) {
    var reg = /^[A-Za-z][A-Za-z\'\-.]+([\ A-Za-z][A-Za-z\'\-.]?)$/;
    return reg.test(name);
}
// First name is A-Za-z only and can be one letter
function isFirstNameValid(name) {
    var reg = /^(?=.{1,}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$/;
    return reg.test(name);
}
function CheckIfUserExists() {
    var args = new Object();
    args.LastName = $("#txtLastName").val();
    args.FirstName = $("#txtFirstName").val();
    args.Email = $("#txtEmail").val();
    args.ADAccount = $("#txtAdAccount").val();
    args.UserName = $("#txtUserName").val();

    var url = "/Admin/CheckIfUserExists";
    $.ajax
        ({
            method: "POST",
            datatype: "json",
            url: url,
            data: args,
        })
        .done(function (response, status, jqxhr) {
            var isDuplicate = response.success;
            if (isDuplicate == true) {
                $("#errorlist").val("User already exists!");
                $("#errorlist").show();
                openWarning("User already exists!")
            } else {
                userManagementValidationComplete();
            }
        });
}

function ValidateEmailAndUserName() {
    var args = new Object();
    args.Email = $("#txtEmail").val();
    args.UserName = $("#txtUserName").val();
    args.UserId = $("#drpDnLstUsers").children("option:selected").val();

    var userid = args.UserId;

    var url = "/Admin/ValidateEmailAndUserName";
    $.ajax
        ({
            method: "POST",
            datatype: "json",
            url: url,
            data: args
        })
        .done(function (response) {
            if (response != "") {
                $("#errorlist").val(response);
                $("#errorlist").show();
                openWarning(response);
            }
            else {
                if (userid == false) { // this is a new user
                    CheckIfUserExists();
                } else {
                    userManagementValidationComplete();
                }
            }
        });
}


window.onload = function () {
    /* During the first time GetWhatChanged is called for change of each dropdown box, we need
     * to know what is the first set of values on load. else the first set of change will not be reflected properly.
     * So just after the form load is done, add all the selected lists to related html element as custome data.*/

    $("#drpDnLstActiveUSER").on("change", this.ToggleIsSchedulable);
    $("#chkDrpDnLstTradeAgency").on("OnSelectionChange", HandleSelectionChangeEvent);
    RegisterSelectedItemChangeEvent("#chkDrpDnLstTradeAgency");

    $(".chkDrpDn").multiselect({ buttonWidth: '200px', selectedList: 2, noneSelectedText: "None Selected" });

};

function ToggleIsSchedulable() {

    var active = $("#drpDnLstActiveUSER").children("option:selected").val();

    if (active == 'N') {
        $('#drpDnLstSchedulable').prop('selectedIndex', 0);

    }
}

function HandleSelectionChangeEvent(e, eventData) {
    var tradevals = new Array('Building', 'Electrical', 'Mechanical', 'Plumbing');
    var ehsvals = new Array('EH_Facilities', 'EH_Food', 'EH_Day_Care', 'EH_Pool');
    var zoneval = "Zone";
    var fireval = "Fire";
    var backflow = "Backflow";

    var evntArgs = {
        IsDeleted: false,
        IsAdded: false,
        AddedValues: [], //null if no change/None. Else changed value.
        DeletedValues: [] //null if no change/None. Else changed value.
    };
    evntArgs = eventData;

    var elementnm = $(this).attr("id");
    if (evntArgs !== "undefined" && elementnm != "") {
        if (evntArgs.IsAdded == true) {
            var rest = [];
            var txt = $("#" + elementnm + " option[value=" + evntArgs.AddedValues[0] + "]").text();
            if (txt == 'Building' || txt == "Electrical" || txt == 'Mechanical' || txt == 'Plumbing') {
                rest = ehsvals.concat(zoneval).concat(fireval).concat(backflow);
            }
            else if (txt == 'EH_Facilities' || txt == "EH_Food" || txt == 'EH_Day_Care' || txt == 'EH_Pool') {
                rest = tradevals.concat(zoneval).concat(fireval).concat(backflow);
            }
            else if (txt == 'Zone') {
                rest = tradevals.concat(ehsvals).concat(fireval).concat(backflow);
            }
            else if (txt == 'Fire') {
                rest = tradevals.concat(zoneval).concat(ehsvals).concat(backflow);
            }
            else // if (txt == 'Backflow')
            {
                rest = tradevals.concat(zoneval).concat(fireval).concat(ehsvals);
            }
            rest.forEach(currenttext => {
                var addedval = $('#DrpDnLstTradeAgency-container option:contains(' + currenttext + ')').val(); //select by text and then get value. Done to make readable code.
                var option = $('#DrpDnLstTradeAgency-container option[value="' + addedval + '"]'); //select by val
                var input = $('#DrpDnLstTradeAgency-container input[value="' + addedval + '"]'); //select by val
                option.prop('disabled', true);
                input.prop('disabled', true);
                input.parent('label').parent('a').parent('li').addClass('disabled');
            });
        }
        if (evntArgs.IsDeleted == true) {
            var currentsel = $(this).val();
            if (currentsel.length == 0) {
                var rest = ehsvals.concat(zoneval).concat(fireval).concat(backflow).concat(tradevals);
                rest.forEach(currenttext => {
                    var addedval = $('#DrpDnLstTradeAgency-container option:contains(' + currenttext + ')').val(); //select by text and then get value. Done to make readable code.
                    var option = $('#DrpDnLstTradeAgency-container option[value="' + addedval + '"]'); //select by val
                    var input = $('#DrpDnLstTradeAgency-container input[value="' + addedval + '"]'); //select by val
                    option.removeProp('disabled');
                    input.removeProp('disabled');
                    option.removeAttr('disabled');
                    input.removeAttr('disabled');
                    input.parent('label').parent('a').parent('li').removeClass('disabled');
                });
            }
        }
    }
}

function IsTextMappingExists(searchtext, currentValues) {
    if (currentValues.length == 0)
        return false;
    var KeyValpair =
    {
        1: "Building", 2: 'Electrical', 3: 'Mechanical', 4: 'Plumbing', 6: "Zone", 14: "Fire", 25: "Backflow", 24: 'EH_Facilities', 22: 'EH_Food', 21: 'EH_Day_Care', 23: 'EH_Pool'
    };

    var srch = currentValues;

    for (var key in srch) {

        if (KeyValpair[srch[key]] == searchtext)
            return true;
    }
    return false;
}

function GetSelectedFacilitatorProjectTypes() {
    var ret = [];
    var i = 0;
    $("#chkDrpDnProjectTypesSelect option:selected").each(function () {
        ret[i] = $(this).val();
        i++
    });
    return ret;
}

function GetSelectedRoles() {
    var ret = [];
    var i = 0;
    $("#chkDrpDnLstRoles option:selected").each(function () {
        ret[i] = $(this).val();
        i++
    });
    return ret;
}

function SaveItem() {
    var selectedUser = $("#drpDnLstUsers").children("option:selected").val();
    if (selectedUser.val() > 0) {
        var model = {
            SelectedUser: "",
            SelectedFacilitatorProjectTypes: []
        };
        openInProgress("Updating...", false);
        model.SelectedUser = selectedUser;
        model.SelectedFacilitatorProjectTypes = GetSelectedFacilitatorProjectTypes();
        var url = "/Admin/SaveUserAutoFacilitatorSelections";
        $.ajax
            ({
                method: "POST",
                datatype: "json",
                url: url,
                data: model,
            }).done(function (response, status, jqxhr) {
                $("#dialog-modal-warning").dialog("close");
            });;
    }
    else {
        openWarning("Please search for or add a user.");
        return false;

    }
}

function phoneFormatter() {
    $('.phone').on('input', function () {
        var number = $(this).val().replace(/[^\d]/g, '')
        if (number.length == 7) {
            number = number.replace(/(\d{3})(\d{4})/, "$1-$2");
        } else if (number.length == 10) {
            number = number.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
        }
        $(this).val(number)
    });
};

function GetSelectedOccupancySquareFootage() {

    var SelectedUser = $("#drpDnLstUsers").children("option:selected").val();

    if (SelectedUser > 0) {

        $.ajax({
            url: "GetOccupancySquareFootage",
            type: "POST",
            data: {
                "userId": SelectedUser
            },
            dataType: "html",
            success: function(result) {
                $('#divSquareFootageByOccupancy').html(result);
            },
            error: function () { alert("error"); }
        });
    }
    else {
        return false;
    }
};

function RegisterSelectedItemChangeEvent(selector) {
        var dropdownElementRef = selector;
        ////creates an option to let user select by both id and class.
        //if ($("." + dropdownElementRef).length > 0)
        //{
        //    dropdownElementRef = "." + dropdownElementRef;
        //}
        //else if ($("#" + dropdownElementRef).length > 0)
        //{
        //    dropdownElementRef = "#" + dropdownElementRef;
        //}

        //Intializes the first time data and stores the values back to control. So if any of the checkboxes in dropdown is selected then it will be processe and added to control.
        $(dropdownElementRef).data('lastsel', $(dropdownElementRef).val());
        var beforeval = $(dropdownElementRef).data('lastsel');
        var afterval = $(dropdownElementRef).val();
        //storing the last value for next time change.
        $(dropdownElementRef).data('lastsel', afterval);
        //get changes details
        var delta = GetWhatChanged(beforeval, afterval);
        //stores the change details back into same object so that it can be used from anywhere regarless of who is calling it.
        $(dropdownElementRef).data('SelectionChangeEventArgs', delta);
        //prepares the event so that the same operation can be done everytime the object is changed.
        $(dropdownElementRef).change(function () {
            var beforeval = $(dropdownElementRef).data('lastsel');
            var afterval = $(dropdownElementRef).val();
            //storing the last value for next time change.
            $(dropdownElementRef).data('lastsel', afterval);
            //get changes details
            var delta = GetWhatChanged(beforeval, afterval);
            //stores the change details into same object so that it can be used from anywhere regarless of who is calling it.
            $(dropdownElementRef).data('OnSelectionChangeEventArgs', delta);
            //fires the event
            $(dropdownElementRef).trigger('OnSelectionChange', [delta]);
            //$.event.trigger('OnSelectionChange', [delta]);
        });
        var initdummy = [];
        var firstval = GetWhatChanged(initdummy, afterval);
        //fires the event to enable or disable the control on load itself based on current selection
        $(dropdownElementRef).trigger('OnSelectionChange', [firstval]);
    }

    function GetWhatChanged(lastVals, currentVals) {
        if (typeof lastVals === 'undefined')
            lastVals = '' //for the first time the last val will be empty in that case make both same.
        if (typeof currentVals === 'undefined')
            currentVals = ''
        var ret = {
            IsDeleted: false,
            IsAdded: false,
            AddedValues: [], //null if no change/None. Else changed value.
            DeletedValues: [] //null if no change/None. Else changed value.
        };
        var addedvals;
        var delvals;
        var lastValsArr, currentValsArr;
        if (Array.isArray(lastVals))
            lastValsArr = lastVals;
        else
            lastValsArr = lastVals.split(",");
        if (Array.isArray(currentVals))
            currentValsArr = currentVals;
        else
            currentValsArr = currentVals.split(",");
        delvals = $(lastValsArr).not(currentValsArr).get();
        if (delvals.length > 0) {
            //console.log("Deleted :" + delvals[0]);
            for (var i = 0; i < delvals.length; i++) {
                ret.DeletedValues.push(delvals[i]);
            }
            ret.IsDeleted = true;
        }
        addedvals = $(currentValsArr).not(lastValsArr).get();
        if (addedvals.length > 0) {
            //console.log("Added:" + addedvals[0]);
            for (var i = 0; i < addedvals.length; i++) {
                ret.AddedValues.push(addedvals[i]);
            }
            ret.IsAdded = true;
        }
        return ret;
}

function SquareFootageSelected(squareFootageIdSelected, occId) {
    var hdnSquareFootageIdSelectedId = "hdnSquareFootageIdSelected_" + occId;
    $("#" + hdnSquareFootageIdSelectedId).val(squareFootageIdSelected.value);
}

function UpdateUserManagementTabUserFilter() {
    var selData = new Object();
    selData.userID = $("#drpDnLstUsers").children("option:selected").val();
    selData.LoggedInUserEmail = $("#LoggedInUserEmail").val();
    selData.FilterByType = $("#drpDnLstUserType").children("option:selected").val();
    selData.SelectedUserSearchFilter = $("#txtSearchUsers").val();
    if (selData.userID != '' && selData.userID != '0') {
        openWarning("Searching...", false);
        var url = "/Admin/GetSelectedUserConfigurationDetails";
        $.ajax
            ({
                method: "POST",
                datatype: "json",
                url: url,
                data: selData,
            })
            .done(function (response, status, jqxhr) {
                $("#nav-usermgmt").html(response);
                LoadUserConfigurations();
                $("select[multiple]").multiselect({ selectedList: 2, noneSelectedText: "None Selected" });
                closeSuccess();
            });
    }
    else {
        openWarning("Please enter a user to search for.");

        return false;
    }
}

function ApplyFilterToUsersUsers() {
    ClearData();
    var searchbyddl = $("#drpDnLstUserType").val() != "All";

    var searchbytxt = $("#txtSearchUsers").val();
    var isSearchable = false;

    if (searchbyddl || !searchbyddl && $("#txtSearchUsers").val() != '') {
        openInProgress("Searching...", false);
        var args = new Object();
        args.FilterType = $("#drpDnLstUserType").val();
        args.SearchText = $("#txtSearchUsers").val();
        var url = "/Admin/ApplyFilterToUsersUsers";
        $.ajax
            ({
                method: "POST",
                datatype: "json",
                url: url,
                data: args,
                statusCode: {
                    404: function () {
                        openSuccess("page not found", true);
                    }
                },
                failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); $("#dialog-modal-warning").dialog("close"); },
                error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); $("#dialog-modal-warning").dialog("close"); }
            })
            .done(function (response, status, jqxhr) {

                $("#sectionUserDropdown").html(response);
                $('#drpDnLstUsers').prop('selectedIndex', 0);
                LoadUserConfigurations();
                closeSuccess();
            });
    }
    else {
        openWarning("Please enter a user to search for.");
        return false;
    }
}
