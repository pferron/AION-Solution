$(function () {
    var isEstimationComplete = $("#IsEstimationComplete").val();
    var isProjectPreliminary = $("#IsProjectPreliminary").val();
    var isViewOnly = $("#IsViewOnly").val();
    var isCEManager = $("#IsCeManager").val();
    var isAgencyEstimator = $("#IsAgencyEstimator").val();
    var isCeEstimator = $("#IsCeEstimator").val();
    var isBEMPEstimator = $("#IsBEMPEstimator").val();
    var isZoneEstimator = $("#IsZoneEstimator").val();
    var isFireEstimator = $("#IsFireEstimator").val();
    var isBackflowEstimator = $("#IsBackflowEstimator").val();
    var isHealthEstimator = $("#IsHealthEstimator").val();


    /* During the first time GetWhatChanged is called for change of each dropdown box, we need
         * to know what is the first set of values on load. else the first set of change will not be reflected properly.
         * So just after the form load is done, add all the selected lists to related html element as custome data.*/
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBuild");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsElectric");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsMech");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPlumb");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBackFlow");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFood");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPool");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsLodge");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsDayCare");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFire");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsZone");

    $('#notes-view-table').DataTable();

    $("div.error").hide();

    if (isProjectPreliminary == "True") {
        $(".application-details").hide();
    }

    $('.estimationna').on('ifToggled', function (event) {
        SetRequired(this);
    });

    $("#btn-submit").on("click", function () {
        CheckAndAssign();
        ValidateForm();
    });


    $("#btn-save").on("click", function () {
        PrepareSaveData();
        ValidateForm();
    });

    $('#EstimationForm').validate({
        debug: false,
        rules: {
            AssignedFacilitator: { required: true, valueNotEquals: "-1" }
        },
        messages: {
            AssignedFacilitator: "Facilitator is required."
        },
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            var activeTab = $("#ActiveTab").val();

            $.each(validator.invalid, function (index, value) {
                if ((index.toLowerCase().indexOf("fire") > 0) && (activeTab.indexOf("fire") > 0)) {
                    //highlight tab
                    $('.firereqcls').show();
                };
                if ((index.toLowerCase().indexOf("zon") > 0) && (activeTab.indexOf("zoning") > 0)) {
                    //highlight tab
                    $('.zoningreqcls').show();
                };
                if ((index.toLowerCase().indexOf("backf") > 0) && (activeTab.indexOf("backflow") > 0)) {
                    //highlight tab
                    $('.backfreqcls').show();
                };
                if ((index.toLowerCase().indexOf("build") > 0
                    || index.toLowerCase().indexOf("elect") > 0
                    || index.toLowerCase().indexOf("mecha") > 0
                    || index.toLowerCase().indexOf("plumb") > 0
                    || index.toLowerCase().indexOf("facilitator") > 0)
                    && (activeTab.indexOf("bemp") > 0)
                ) {
                    //highlight tab
                    $('.bempreqcls').show();
                };
                if ((index.toLowerCase().indexOf("food") > 0
                    || index.toLowerCase().indexOf("pool") > 0
                    || index.toLowerCase().indexOf("facil") > 0
                    || index.toLowerCase().indexOf("dayc") > 0)
                    && (activeTab.indexOf("health") > 0)
                ) {
                    //highlight tab
                    $('.healthreqcls').show();
                };

            });
            if (errors) {
                var message = errors == 1
                    ? 'You missed 1 field. It has been highlighted'
                    : 'You missed ' + errors + ' fields. They have been highlighted';
                $("div.error").html(message);
                $("div.error").show();
            }
            else {
                $('.bempreqcls').hide();
                $('.firereqcls').hide();
                $('.zoningreqcls').hide();
                $('.backfreqcls').hide();
                $('.healthreqcls').hide();

                $("div.error").hide();
            }
        }
    });

    function ValidateForm() {
        if ($('#EstimationForm').valid()) {
            SubmitForm();
        }
        else {
            return false;
        }
    }

    function SubmitForm() {
        openInProgress("Saving...");
        $("#btn-submit").addClass("disabled");
        $("#btn-save").addClass("disabled");
        $("div.error").html("");
        $("div.error").hide();
        $('.bempreqcls').hide();
        $('.firereqcls').hide();
        $('.zoningreqcls').hide();
        $('.backfreqcls').hide();
        $('.healthreqcls').hide();

        $("#EstimationForm").trigger("submit");
    }


    $(".chkDrpDnExPln").on("OnSelectionChange", function (e, eventData) {
        var evntArgs = {
            IsDeleted: false,
            IsAdded: false,
            AddedValues: [], //null if no change/None. Else changed value.
            DeletedValues: [] //null if no change/None. Else changed value.
        };
        var source = e;

        evntArgs = eventData;
        var elementnm = $(this).attr("id");
        if (evntArgs !== "undefined" && elementnm != "") {
            if (evntArgs.IsAdded == true) {
                //if excluded checked then remove.
                for (var i = 0; i < evntArgs.AddedValues.length; i++) {
                    DropDownListRemoveItem(GetPrimaryReviewerID(elementnm), evntArgs.AddedValues[i]);
                    DropDownListRemoveItem(GetSecondaryReviewerID(elementnm), evntArgs.AddedValues[i]);
                }
            }
            if (evntArgs.IsDeleted == true) {
                //if excluded checked then remove.
                for (var i = 0; i < evntArgs.DeletedValues.length; i++) {
                    var txt = $("#" + elementnm + " option[value=" + evntArgs.DeletedValues[i] + "]").text();
                    DropDownListAddItem(GetPrimaryReviewerID(elementnm), txt, evntArgs.DeletedValues[i]);
                    DropDownListAddItem(GetSecondaryReviewerID(elementnm), txt, evntArgs.DeletedValues[i]);
                }
            }
        }
    });

    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "");


    $(".hrstextbox").on("focusout", function () {
        //if value > 0 then uncheck NA box
        SetNA(this);
    })

    //set active tab on load
    var activeTab = $('a.nav-link.active').attr("id");

    $("#ActiveTab").val(activeTab);

    CheckHoursTextBoxesForRequired();

    //set active tab on click
    $('a[data-toggle="tab"]').on("click", function (e) {
        activeTab = e.target.id;
        $("#ActiveTab").val(activeTab);
        CheckHoursTextBoxesForRequired();
    });


    SetTabPerms();

    //show/hide group with expressYN and projecttypechg, depends on accela project/property type of Express
    ShowProjectTypeGroup("@Model.Project.AccelaPropertyType.ToStringValue()");

    //show/hide project type depends on type express
    $("#expressYNSelected").on("change", function () {
        ShowProjectTypeDDL($(this).val());
    });

    ShowProjectTypeDDL($("#expressYNSelected").val());

    //*************
    $('.bempreqcls').hide();
    $('.firereqcls').hide();
    $('.zoningreqcls').hide();
    $('.backfreqcls').hide();
    $('.healthreqcls').hide();

    //*************

    function GetPrimaryReviewerID(excludedCtrlID) {
        switch (excludedCtrlID) {
            case "chkDrpDnExPlnRvrsBuild":
                return "DrpDnPrimaryBuild";
            case "chkDrpDnExPlnRvrsElectric":
                return "DrpDnPrimaryElectric";
            case "chkDrpDnExPlnRvrsMech":
                return "DrpDnPrimaryMech";
            case "chkDrpDnExPlnRvrsPlumb":
                return "DrpDnPrimaryPlumb";
            case "chkDrpDnExPlnRvrsBackFlow":
                return "DrpDnPrimaryBackFlow";
            case "chkDrpDnExPlnRvrsFood":
                return "DrpDnPrimaryFood";
            case "chkDrpDnExPlnRvrsPool":
                return "DrpDnPrimaryPool";
            case "chkDrpDnExPlnRvrsLodge":
                return "DrpDnPrimaryLodge";
            case "chkDrpDnExPlnRvrsDayCare":
                return "DrpDnPrimaryDayCare";
            case "chkDrpDnExPlnRvrsFire":
                return "DrpDnPrimaryFire";
            case "chkDrpDnExPlnRvrsZone":
                return "DrpDnPrimaryZone";
            default:
                return "";
        }
    }

    function GetSecondaryReviewerID(excludedCtrlID) {
        switch (excludedCtrlID) {
            case "chkDrpDnExPlnRvrsBuild":
                return "DrpDnSecondaryBuild";
            case "chkDrpDnExPlnRvrsElectric":
                return "DrpDnSecondaryElectric";
            case "chkDrpDnExPlnRvrsMech":
                return "DrpDnSecondaryMech";
            case "chkDrpDnExPlnRvrsPlumb":
                return "DrpDnSecondaryPlumb";
            case "chkDrpDnExPlnRvrsBackFlow":
                return "DrpDnSecondaryBackFlow";
            case "chkDrpDnExPlnRvrsFood":
                return "DrpDnSecondaryFood";
            case "chkDrpDnExPlnRvrsPool":
                return "DrpDnSecondaryPool";
            case "chkDrpDnExPlnRvrsLodge":
                return "DrpDnSecondaryLodge";
            case "chkDrpDnExPlnRvrsDayCare":
                return "DrpDnSecondaryDayCare";
            case "chkDrpDnExPlnRvrsFire":
                return "DrpDnSecondaryFire";
            case "chkDrpDnExPlnRvrsZone":
                return "DrpDnSecondaryZone";
            default:
                return "";
        }
    }

    function CheckHoursTextBoxesForRequired() {
        $(".estimationna").each(function () {
            SetRequired(this);
        });
    }

    function ShowProjectTypeGroup(express) {
        //projecttypefrmgrp
        if (express == "Express") {
            $(".projecttypefrmgrp").show();
            return;
        }
        $(".projecttypefrmgrp").hide();
    }

    function ShowProjectTypeDDL(express) {
        if (express == "Y") {
            //hide the project type dropdown
            $(".projecttypechg").hide();
            return;
        }
        $(".projecttypechg").show();
    }

    function SetTabPerms() {
        //skip the perms if estimation is complete
        if (!isEstimationComplete) {
            //  only show the agencies they can estimate

            //arr for nav tabs in order: bemp, zone, fire, backflow, health
            var permarr = [{ "id": "nav-bemp-tab", "perm": isBEMPEstimator },
            { "id": "nav-zoning-tab", "perm": isZoneEstimator },
            { "id": "nav-fire-tab", "perm": isFireEstimator },
            { "id": "nav-backflow-tab", "perm": isBackflowEstimator },
            { "id": "nav-health-tab", "perm": isHealthEstimator }];

            //set the accessible tab
            if (!isBEMPEstimator) {
                SetPermsMinVal("bemp");
                $("#nav-bemp-tab").addClass("disabled").removeClass("active");
            }
            if (!isZoneEstimator) {
                SetPermsMinVal("zoning");
                $("#nav-zoning-tab").addClass("disabled");
            }
            if (!isFireEstimator) {
                SetPermsMinVal("fire");
                $("#nav-fire-tab").addClass("disabled");
            }
            if (!isBackflowEstimator) {
                SetPermsMinVal("backflow");
                $("#nav-backflow-tab").addClass("disabled");
            }
            if (!isHealthEstimator) {
                SetPermsMinVal("health");
                $("#nav-health-tab").addClass("disabled");
            }

            //decide which tab should be the active tab
            //just pick the first
            var first = false;
            $.each(permarr, function (i, item) {
                if (item.perm == true && first == false) {
                    first = true;
                    $("#" + item.id).tab("show");
                }
            });

        }
    }

    //these are the tabs that user does not have permission to alter
    //  set the min to 0.0 leaving the value and na as is
    function SetPermsMinVal(permname) {
        var minval = 0.0;
        switch (permname) {
            case "bemp":
                SetMinValReq("txtHoursBuilding", false, null, minval);
                SetMinValReq("txtHoursElectic", false, null, minval);
                SetMinValReq("txtHoursMech", false, null, minval);
                SetMinValReq("txtHoursPlumb", false, null, minval);

                $("#asndfacilitator").rules("remove", "required");
                $("#asndfacilitator").rules("remove", "valueNotEquals");

                //console.log("bemp perm");
                break;
            case "zoning":
                SetMinValReq("txtHoursZoning", false, null, minval);
                //console.log("zoning perm");
                break;
            case "fire":
                SetMinValReq("txtHoursFire", false, null, minval);
                //console.log("fire perm");
                break;
            case "backflow":
                SetMinValReq("txtHoursBackFlow", false, null, minval);
                //console.log("backflow perm");
                break;
            case "health":
                SetMinValReq("txtHoursFood", false, null, minval);
                SetMinValReq("txtHoursPool", false, null, minval);
                SetMinValReq("txtHoursLodge", false, null, minval);
                SetMinValReq("txtHoursDayCare", false, null, minval);
                //console.log("health perm");
                break;
            default:
                break;
        }
    }

    function PrepareSaveData() {
        $(".hrstextbox").each(function () {
            var textboxid = $(this).attr("id");
            SetMinValReq(textboxid, false, null, 0.0)
        });
        $("#IsSubmit").val("false");
        $("#SaveType").val("2");

        $("#asndfacilitator").rules("remove", "required");
        $("#asndfacilitator").rules("remove", "valueNotEquals");

        CheckAndAssign();
    };

    function SetRequired(obj) {
        //get the object id
        var objid = $(obj).attr("id");

        var na = $(obj).prop("checked");

        //if checked, make corr txt box not required
        //if not check, make corr txt box required
        switch (objid) {
            case "HrsNABuilding":
                SetObjDataValReq("txtHoursBuilding", na);
                break;
            case "HrsNAElectric":
                SetObjDataValReq("txtHoursElectic", na);
                break;
            case "HrsNAMech":
                SetObjDataValReq("txtHoursMech", na);
                break;
            case "HrsNAPlumbing":
                SetObjDataValReq("txtHoursPlumb", na);
                break;
            case "HrsNAZone":
                SetObjDataValReq("txtHoursZoning", na);
                break;
            case "HrsNAFire":
                SetObjDataValReq("txtHoursFire", na);
                break;
            case "HrsNABackFlow":
                SetObjDataValReq("txtHoursBackFlow", na);
                break;
            case "HrsNAFood":
                SetObjDataValReq("txtHoursFood", na);
                break;
            case "HrsNAPool":
                SetObjDataValReq("txtHoursPool", na);
                break;
            case "HrsNAFacility":
                SetObjDataValReq("txtHoursLodge", na);
                break;
            case "HrsNADayCare":
                SetObjDataValReq("txtHoursDayCare", na);
                break;
            default:
                break;
        }
    }

    function CheckIfObjectInActiveTab(textboxid) {
        var activeTab = $("#ActiveTab").val();

        switch (textboxid) {
            case "txtHoursBuilding":
                if (activeTab.indexOf("bemp") > 0) {
                    return true;
                }
                break;
            case "txtHoursElectic":
                if (activeTab.indexOf("bemp") > 0) {
                    return true;
                }
                break;
            case "txtHoursMech":
                if (activeTab.indexOf("bemp") > 0) {
                    return true;
                }
                break;
            case "txtHoursPlumb":
                if (activeTab.indexOf("bemp") > 0) {
                    return true;
                }
                break;
            case "txtHoursZoning":
                if (activeTab.indexOf("zoning") > 0) {
                    return true;
                }
                break;
            case "txtHoursFire":
                if (activeTab.indexOf("fire") > 0) {
                    return true;
                }
                break;
            case "txtHoursBackFlow":
                if (activeTab.indexOf("backflow") > 0) {
                    return true;
                }
                break;
            case "txtHoursFood":
                if (activeTab.indexOf("health") > 0) {
                    return true;
                }
                break;
            case "txtHoursPool":
                if (activeTab.indexOf("health") > 0) {
                    return true;
                }
                break;
            case "txtHoursLodge":
                if (activeTab.indexOf("health") > 0) {
                    return true;
                }
                break;
            case "txtHoursDayCare":
                if (activeTab.indexOf("health") > 0) {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    function SetObjDataValReq(textboxid, na) {
        if (na == true) {
            SetMinValReq(textboxid, false, 0.0, 0.0);
        } else {
            SetMinValReq(textboxid, true, null, 0.5);
        }
    }

    //send null to skip setting the val
    function SetMinValReq(textboxid, req, val, minval) {
        //console.log("SetMinValReq " + textboxid + " " + req + " " + val + " " + minval);

        var isObjectInActiveTab = CheckIfObjectInActiveTab(textboxid);

        if (req && isObjectInActiveTab) {
            $("#" + textboxid).rules("add", { required: true });
            $("#" + textboxid).data("val-required", req);
            $("#" + textboxid).attr("min", minval);
        }
        else {
            $("#" + textboxid).rules("remove", "required");
            $("#" + textboxid).attr("min", null);
            $("#" + textboxid).data("val-required", false);
        }

        if (val != null)
            $("#" + textboxid).val(val);
    }

    function CheckAndAssign() {
        $('.bempreqcls').hide();
        $('.firereqcls').hide();
        $('.zoningreqcls').hide();
        $('.backfreqcls').hide();
        $('.healthreqcls').hide();

        var checkedCheckBoxes = $("input.estimationna:checked").length;

        var allCheckBoxes = $("input.estimationna").length;

        if (allCheckBoxes === checkedCheckBoxes) {
            $("#hfIsAllNAChecked").val(true);
        }
    }

    function SetNA(obj) {
        //get the object id - hrstextbox class
        var textboxid = $(obj).attr("id");
        var hrs = $(obj).val();
        //if checked, make corr txt box not required
        //if not check, make corr txt box required
        if (hrs != null && hrs > 0) {
            switch (textboxid) {
                case "txtHoursBuilding":
                    //HrsNABuilding
                    $("#HrsNABuilding").prop("checked", false);
                    break;
                case "txtHoursElectic":
                    //HrsNAElectric
                    $("#HrsNAElectric").prop("checked", false);
                    break;
                case "txtHoursMech":
                    //HrsNAMech
                    $("#HrsNAMech").prop("checked", false);
                    break;
                case "txtHoursPlumb":
                    //HrsNAPlumbing
                    $("#HrsNAPlumbing").prop("checked", false);
                    break;
                case "txtHoursZoning":
                    $("#HrsNAZone").prop("checked", false);
                    break;
                case "txtHoursFire":
                    $("#HrsNAFire").prop("checked", false);
                    break;
                case "txtHoursBackFlow":
                    $("#HrsNABackFlow").prop("checked", false);
                    break;
                case "txtHoursFood":
                    $("#HrsNAFood").prop("checked", false);
                    break;
                case "txtHoursPool":
                    $("#HrsNAPool").prop("checked", false);
                    break;
                case "txtHoursLodge":
                    $("#HrsNAFacility").prop("checked", false);
                    break;
                case "txtHoursDayCare":
                    $("#HrsNADayCare").prop("checked", false);
                    break;
                default:
                    break;
            }
        }
    }

    ///LES-782
    function SendPendingEmail(deptid) {
        $("#PendingEmailType").val(deptid);
        PrepareSaveData();
        $("#SaveType").val("3");
    }
});