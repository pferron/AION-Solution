var arrProjects = [];

$(function () {
    $("#bulk-estimation").on("click", function () {
        if (arrProjects.length <= 0) {
            swal("", "There are no projects selected", "warning");
            $("#bulk-estimation-modal").modal('hide');
            return false;
        }
        else {
            swal({ text: "Preparing bulk estimation values...", timer: 1500 });
            $("#bulk-estimation-modal").modal('show');
            InitializeControls();
        }
    });

    $(document).on("click", "#btn-submit", function (e) {
        e.preventDefault();

        var $inputs = $('#form-bulk-estimation .form-control');

        var hasErrors = false;

        $inputs.each(function () {
            $(this).removeClass("alert-red");

            var requiredTag = $(this).parents('div').find('.block-tag');

            requiredTag.hide();

            var valRequired = $(this).attr("data-val-required");

            var value = $(this).val();

            if (valRequired != null && valRequired != "false") {
                var isRequired = true;
                if (isRequired  && (value == '' || value == 0 || value == "-1")) {
                    hasErrors = true;

                    $(this).addClass("alert-red");
                }
            }
        });

        if (hasErrors) {
            swal("", "Please complete required fields.", "error");
        }
        else {
            var data = $('#form-bulk-estimation').serializeArray();

            data.push({ name: 'ProjectIds', value: JSON.stringify(arrProjects) });

            swal({ text: "Submitting bulk estimation ...", timer: 2000 });

            $.ajax({
                url: "BulkEstimationUpdate",
                type: "POST",
                data: data,
                success: function (result) {
                    if (result == "success") {

                        $("#bulk-estimation-dialog").dialog("close");

                        swal({ text: "Refreshing project list ...", timer: 2000 });

                        var location = '/Estimation/EstimationDashboard?LoggedInUserEmail=' + $("#LoggedInUserEmail").val();

                        window.location = location;
                    }
                },
                error: function () { swal("", "There was an error.", "error"); }
            });
        }
    });

    function InitializeControls() {

        $("#AssignedFacilitator").attr("data-val-required", "true");

        $("#form-bulk-estimation").on('ifToggled', '.estimationna', function () {
            SetRequired(this);
        });

        $("#form-bulk-estimation").on('focusout', '.hrstextbox', function () {
            SetNA(this);
        });

        $(".multiselect").multiselect('refresh');

        $('select').on('change', function () {
            $(this).find('option[value="' + this.value + '"]').attr('selected', 'selected');
        });

        RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBuild");
        RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsElectric");
        RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsMech");
        RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPlumb");
        RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFire");

        $(".chkDrpDnExPln").on("OnSelectionChange", function (e, eventData) {
            var evntArgs = {
                IsDeleted: false,
                IsAdded: false,
                AddedValues: [], //null if no change/None. Else changed value.
                DeletedValues: [] //null if no change/None. Else changed value.
            };
            var source = e;
            //evntArgs = $(this).data('SelectionChangeEventArgs');
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
    }
  
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
            case "HrsNAFire":
                SetObjDataValReq("txtHoursFire", na);
                break;
            default:
                break;
        }
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
        $("#" + textboxid).attr("data-val-required", req);

        $("#" + textboxid).attr("min", minval);

        if (val != null) {
            $("#" + textboxid).val(val);
        }
    }

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
                case "txtHoursFire":
                    $("#HrsNAFire").prop("checked", false);
                    break;
                 default:
                    break;
            }
        }
    }
});

$('.bulk-project').on('ifToggled', function () {
    var id = $(this).attr('data-id');

    if ($(this).is(':checked')) {
        arrProjects.push({ id: id });
    }
    else {
        var objIndex = arrProjects.findIndex(x => x.id === id);

        if (objIndex > -1) {
            arrProjects.splice(objIndex, 1);
        }
    }
});