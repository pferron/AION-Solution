/****************
 * LES-186
 * jeanine lindsay
 * 
 * When activate na review is clicked
 * Enable hrs boxes for estimation
 * Enable plan reviewer drop downs
 * Enable date boxes
 * Disable ability to future schedule cycle
 * 
 * ********************/

//onload events
$(function () {
    //set this value from the global var isScheduled on _SchedulePlanReview
    setDisabledForScheduled(isScheduled);
});

function setDisabledForScheduled(isscheduled) {
    var warning = "";
    if (isscheduled) {
        warning = "is scheduled";
        //disable NA dropdowns
        $(".scheduledreviewer").each(function (index) {
            var ddl = $(this);
            ddl.addClass("disabled");
            if (ddl.val() == -1) {
                ddl.find('option').each(function () {
                    if ($(this).val() != -1) {
                        $(this).prop("disabled", true);
                    }
                });
            };
        })
    } else {
        warning = "is scheduled = false";
    }
    //openWarning(warning, true);

}

function ToggleNAReview() {
    //use global var to toggle isActivateNAReview
    //toggle
    if (isActivateNAReview == false) {
        isActivateNAReview = true;
        $("#IsActivateNAReview").val("true");
    }
    else {
        isActivateNAReview = false;
        $("#IsActivateNAReview").val("false");
    }

    if (isActivateNAReview == true) {
        $("#schedulecyclebtn").addClass("disabled");
        $("#schedulecyclebtn").prop("disabled", true);

        $('#btn-save').hide();
        //make btn green btn-outline-primary
        $("#activatenareviewbtn").addClass("btn-danger");
        //enable estimation hrs textboxes
        $(".hrsna").each(function () {
            //if the value is True, then show the hours textbox enabled
            //enable reviewer dropdown
            if ($(this).val() == "True") {
                $(this).next(".na-txt").hide();
                //get the next object with class .na-txt and show
                var hrstextbox = $(this).next(".na-txt").next(".hrstextbox");
                hrstextbox.show();
                hrstextbox.prop("readonly", false);
                hrstextbox.removeClass("disabled");
                var textboxid = hrstextbox.attr("id");
                $("#" + textboxid).data("val-required", true);
                $("#" + textboxid).attr("min", 0.5);

                //scheduledreviewer class
                var scheduledreviewer = parseHrstextbox(textboxid);
                if ($("#" + scheduledreviewer).val() == -1) {
                    $("#" + scheduledreviewer).val(0);
                    $("#" + scheduledreviewer).find('option').each(function () {
                        $(this).prop("disabled", false);
                    });
                };

                var startdatesel = parseDatetextbox(textboxid);
                $("#" + startdatesel).rules("add", required = true);
                $("#" + startdatesel).rules("add", isValidDate = true);
                //send in the word end to get the end date sel
                var enddatesel = parseDatetextbox(textboxid + "end");
                $("#" + enddatesel).rules("add", required = true);
                $("#" + enddatesel).rules("add", isValidDate = true);
            }
        })
        initPoolBtns();
    } else {
        $("#schedulecyclebtn").removeClass("disabled");
        $("#schedulecyclebtn").prop("disabled", false);
        //null is the same as false
        $('#btnsave').show();
        //remove btn background
        $("#activatenareviewbtn").removeClass("btn-danger");

        $(".hrsna").each(function () {
            //if the value is True, then show the hours textbox enabled
            //enable reviewer dropdown
            if ($(this).val() == "True") {
                $(this).next(".na-txt").show();
                //get the next object with class .na-txt and show
                var hrstextbox = $(this).next(".na-txt").next(".hrstextbox");
                hrstextbox.hide();
                hrstextbox.prop("readonly", true);
                hrstextbox.addClass("disabled");
                hrstextbox.val(0.0);
                var textboxid = hrstextbox.attr("id");
                $("#" + textboxid).data("val-required", false);
                $("#" + textboxid).attr("min", 0.0);
                $("#" + textboxid).rules("remove", "required");

                //scheduledreviewer class
                var scheduledreviewer = parseHrstextbox(textboxid);

                $("#" + scheduledreviewer).val(-1);
                $("#" + scheduledreviewer).find('option').each(function () {
                    if ($(this).val() != -1) {
                        $(this).prop("disabled", true);
                    }
                });
            }
        })
        initPoolBtns();

        $('#SchedulingForm').validate();

    }
}

//parse the hrstextbox class element name so we can get the right scheduledreviewer element
function parseHrstextbox(textboxid) {
    if (textboxid.toLowerCase().indexOf("fire") > 0) {
        return "DrpDnScheduleFire";
    };
    if (textboxid.toLowerCase().indexOf("zon") > 0) {
        return "DrpDnScheduleZone";
    };
    if (textboxid.toLowerCase().indexOf("backf") > 0) {
        return "DrpDnScheduleBackFlow";
    };
    if (textboxid.toLowerCase().indexOf("build") > 0) {
        return "DrpDnScheduleBuild";
    };
    if (textboxid.toLowerCase().indexOf("elect") > 0) {
        return "DrpDnScheduleElectric";
    };
    if (textboxid.toLowerCase().indexOf("mech") > 0) {
        return "DrpDnScheduleMech";
    };
    if (textboxid.toLowerCase().indexOf("plumb") > 0) {
        return "DrpDnSchedulePlumb";
    };

    if (textboxid.toLowerCase().indexOf("food") > 0) {
        return "DrpDnScheduleFood";
    };
    if (textboxid.toLowerCase().indexOf("pool") > 0) {
        return "DrpDnSchedulePool";
    };
    if (textboxid.toLowerCase().indexOf("lodg") > 0) {
        return "DrpDnPScheduleLodge";
    };
    if (textboxid.toLowerCase().indexOf("dayc") > 0) {
        return "DrpDnScheduleDayCare";
    };

}
//parse the startdatesel & enddatesel class element name so we can get the right date element
function parseDatetextbox(textboxid) {
    if (textboxid.toLowerCase().indexOf("fire") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "fireenddatesel";
        } else {
            return "firestartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("zon") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "zoneenddatesel";
        } else {
            return "zonestartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("backf") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "backfenddatesel";
        } else {
            return "backfstartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("build") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "buildenddatesel";
        } else {
            return "buildstartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("elect") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "electenddatesel";
        } else {
            return "electstartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("mech") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "mechaenddatesel";
        } else {
            return "mechastartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("plumb") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "plumbenddatesel";
        } else {
            return "plumbstartdatesel";
        }
    };

    if (textboxid.toLowerCase().indexOf("food") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "foodenddatesel";
        } else {
            return "foodstartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("pool") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "poolenddatesel";
        } else {
            return "poolstartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("lodg") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "facilenddatesel";
        } else {
            return "facilstartdatesel";
        }
    };
    if (textboxid.toLowerCase().indexOf("dayc") > 0) {
        if (textboxid.toLowerCase().indexOf("end") > 0) {
            return "daycenddatesel";
        } else {
            return "daycstartdatesel";
        }
    };
}