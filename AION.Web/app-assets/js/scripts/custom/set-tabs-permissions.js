
//global vars - these hidden objects must be on the pages this is added to
var isEstimationComplete = $('#hIsestimationcomplete').val() == "true";

var isagencyestimator = $("#hIsagencyestimator").val() == "true";
var isceestimator = $("#hIsceestimator").val() == "true";
var isbempestimator = $("#hIsbempestimator").val() == "true";
var iszoneestimator = $("#hIszoneestimator").val() == "true";
var isfireestimator = $("#hIsfireestimator").val() == "true";
var isbackflowestimator = $("#hIsbackflowestimator").val() == "true";
var ishealthestimator = $("#hIshealthestimator").val() == "true";

//arr for nav tabs in order: bemp, zone, fire, backflow, health
var permarr = [{ "id": "nav-bemp-tab", "perm": isbempestimator },
{ "id": "nav-zoning-tab", "perm": iszoneestimator },
{ "id": "nav-fire-tab", "perm": isfireestimator },
{ "id": "nav-backflow-tab", "perm": isbackflowestimator },
{ "id": "nav-health-tab", "perm": ishealthestimator }];


function SetTabPerms() {
    //skip the perms if estimation is complete
    if (!isEstimationComplete) {
        //  only show the agencies they can estimate
        //hide everything if agency estimator
        if (isagencyestimator) {
            console.log("isagencyestimator");

            if (!isbempestimator) {
                SetPermsMinVal("bemp");
                $("#nav-bemp-tab").addClass("disabled").removeClass("active");
            }
            if (!iszoneestimator) {
                SetPermsMinVal("zoning");
                $("#nav-zoning-tab").addClass("disabled");
            }
            if (!isfireestimator) {
                SetPermsMinVal("fire");
                $("#nav-fire-tab").addClass("disabled");
            }
            if (!isbackflowestimator) {
                SetPermsMinVal("backflow");
                $("#nav-backflow-tab").addClass("disabled");
            }
            if (!ishealthestimator) {
                SetPermsMinVal("health");
                $("#nav-health-tab").addClass("disabled");
            }

            //decide which tab should be the active tab
            //just pick the first
            var first = false;
            $.each(permarr, function (i, item) {
                if (item.perm == true && first == false) {
                    first = true;
                    console.log(item.id);
                    $("#" + item.id).tab("show");
                }
            });

        }
        //LES - 782 JL if ce estimator, agency tabs (zoning, fire, backflow, health) are not required
        if (isceestimator) {
            SetPermsMinVal("zoning");
            SetPermsMinVal("fire");
            SetPermsMinVal("backflow");
            SetPermsMinVal("health");
        }
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
            console.log("bemp perm");
            break;
        case "zoning":
            SetMinValReq("txtHoursZoning", false, null, minval);
            console.log("zoning perm");
            break;
        case "fire":
            SetMinValReq("txtHoursFire", false, null, minval);
            console.log("fire perm");
            break;
        case "backflow":
            SetMinValReq("txtHoursBackFlow", false, null, minval);
            console.log("backflow perm");
            break;
        case "health":
            SetMinValReq("txtHoursFood", false, null, minval);
            SetMinValReq("txtHoursPool", false, null, minval);
            SetMinValReq("txtHoursLodge", false, null, minval);
            SetMinValReq("txtHoursDayCare", false, null, minval);
            console.log("health perm");
            break;
        default:
            break;
    }
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

function SetObjDataValReq(textboxid, na) {
    if (na == true) {
        SetMinValReq(textboxid, false, 0.0, 0.0);
    } else {
        SetMinValReq(textboxid, true, null, 0.5);
    }
}
//send null to skip setting the val
function SetMinValReq(textboxid, req, val, minval) {
    console.log("SetMinValReq " + textboxid + " " + req + " " + val + " " + minval);
    $("#" + textboxid).data("val-required", req);
    if (req) {
        $("#" + textboxid).rules("add", { required: true });
    } else {
        $("#" + textboxid).rules("remove", "required");
    }
    $("#" + textboxid).attr("min", minval);

    if (val != null)
        $("#" + textboxid).val(val);
}




