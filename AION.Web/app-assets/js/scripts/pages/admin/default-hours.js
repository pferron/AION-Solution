$(function () {
    DefaultHoursInitialization();

    BindProjectTypeChange();
});

function BindProjectTypeChange() {
    $("#ProjectTypesSelect").on("change", function () {
        openWarning("Updating...", false);

        var selectedval = new Object();
        selectedval.LoggedInUserEmail = $("#LoggedInUserEmail").val();
        selectedval.propertyType = $("#ProjectTypesSelect").children("option:selected").val();
        var url = "/Admin/DefaultHoursSwitchProjectType";
        $.ajax
            ({
                method: "POST",
                datatype: "json",
                url: url,
                data: selectedval,
            })
            .done(function (response, status, jqxhr) {
                $("#nav-hoursconf").html(response);
                DefaultHoursInitialization();
                BindProjectTypeChange();
                closeSuccess();
            });
    });
}

function DefaultHoursInitialization() {
    EnableDisable('Building');
    EnableDisable('Electrical');
    EnableDisable('Mechanical');
    EnableDisable('Plumbing');
    EnableDisable('CountyZoning');
    EnableDisable('CityZoning');
    EnableDisable('CountyFire');
    EnableDisable('CityFire');
    EnableDisable('EHSDayCare');
    EnableDisable('EHSFoodService');
    EnableDisable('EHSPublicPool');
    EnableDisable('EHSLodging');
    EnableDisable('Backflow');
    EnableDisable('Cornelius');
    EnableDisable('Davidson');
    EnableDisable('Matthews');
    EnableDisable('Pineville');

    //LES-3407 jcl - hide irrelevant zone towns, only show Huntersville and Mint Hill
    initTownDefaultHours();
}

function EnableDisable(classname) {
    var selectedMode = $('select.' + classname).children("option:selected").text();

    if (selectedMode == 'Manual' || selectedMode == 'Auto' || selectedMode == 'NA' || selectedMode == 'County') {
        $('label.' + classname).addClass("disabledefaulthours");
        $('input.' + classname).addClass("disabledefaulthours");
        $('input.' + classname).attr('readonly', true);
        $('.Hidden' + classname).val('0');
        SetObjDataValReq("txt" + classname, true);
    }
    else {
        $('label.' + classname).removeClass("disabledefaulthours");
        $('input.' + classname).removeClass("disabledefaulthours");
        $('input.' + classname).attr('readonly', false);
        $('.Hidden' + classname).val('1');
    }

}
//LES-3407 hide all zoning configuration except Huntersville and Mint Hill
function initTownDefaultHours() {

    $(".hiddenzone").hide();

    var classname = "Huntersville";
    $('label.' + classname).addClass("disabledefaulthours");
    $('input.' + classname).addClass("disabledefaulthours");
    $('input.' + classname).attr('readonly', true);
    SetObjDataValReq("txt" + classname, false);

    classname = "MintHill";
    $('label.' + classname).addClass("disabledefaulthours");
    $('input.' + classname).addClass("disabledefaulthours");
    $('input.' + classname).attr('readonly', true);
    SetObjDataValReq("txt" + classname, false);
}

function SetObjDataValReq(textboxid, na) {
    if (na == true) {
        SetMinValReq(textboxid, false, 0.0, 0.0);
    }
    else {
        SetMinValReq(textboxid, true, null, 0.5);
    }
}

//send null to skip setting the val
function SetMinValReq(textboxid, req, val, minval) {
    //console.log("SetMinValReq " + textboxid + " " + req + " " + val + " " + minval);
    $("#" + textboxid).data("val-required", req);

    if (req == true) {
        $("#" + textboxid).rules("add", { required: true });
    }
    else {
        $("#" + textboxid).rules("remove", "required");
    }
    $("#" + textboxid).attr("min", minval);

    if (val != null)
        $("#" + textboxid).val(val);
}