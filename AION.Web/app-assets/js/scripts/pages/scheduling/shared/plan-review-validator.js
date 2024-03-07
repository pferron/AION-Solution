$(function () {
    /******************* Validator *******************/
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg != value;
    }, "");

    $.validator.addMethod("reqselectedreviewer",
        function (value, element) {
            if (value == "0")
                return false;
            else
                return true;
        },
        "Please select a value");

    $.validator.addClassRules("scheduledreviewer", {
        reqselectedreviewer: true
    });

    var isValidDate = function (dateStr) {
        try {
            if ($.datepicker.parseDate("mm/dd/yy", dateStr))
                return true;
            return false;
        } catch (e) {
            //parse failed
            return false;
        }
    };

    $.validator.addMethod("isValidDate", function (value, element) {
        return isValidDate(value);
    }, "Enter a valid date MM/DD/YYYY");

    /**************** Schedule Plan Review Dialog Box *****************/
    $("#dialog-modal-confirmation").dialog({
        modal: true,
        autoOpen: false,
        height: 300,
        width: 400,
        buttons: [
            {
                text: "Yes", click: function () {
                    submitForm();
                    $(this).dialog("close");
                }
            },
            {
                text: "No", click: function () {
                    $(this).dialog("close");
                }
            }
        ],
        resizable: true
    });

    $('#SchedulingForm').validate({
        debug: false,
        ignore: ".ignore",
        rules: {
            BackfStartDate: { required: true, isValidDate: true },
            BackfEndDate: { required: true, isValidDate: true },

            BuildStartDate: { required: true, isValidDate: true },
            BuildEndDate: { required: true, isValidDate: true },
            ElectStartDate: { required: true, isValidDate: true },
            ElectEndDate: { required: true, isValidDate: true },
            MechaStartDate: { required: true, isValidDate: true },
            MechaEndDate: { required: true, isValidDate: true },
            PlumbStartDate: { required: true, isValidDate: true },
            PlumbEndDate: { required: true, isValidDate: true },

            FireStartDate: { required: true, isValidDate: true },
            FireEndDate: { required: true, isValidDate: true },

            ZoneStartDate: { required: true, isValidDate: true },
            ZoneEndDate: { required: true, isValidDate: true },

            FoodStartDate: { required: true, isValidDate: true },
            FoodEndDate: { required: true, isValidDate: true },
            PoolStartDate: { required: true, isValidDate: true },
            PoolEndDate: { required: true, isValidDate: true },
            FacilStartDate: { required: true, isValidDate: true },
            FacilEndDate: { required: true, isValidDate: true },
            DaycStartDate: { required: true, isValidDate: true },
            DaycEndDate: { required: true, isValidDate: true },

            AssignedFacilitator: { required: true, valueNotEquals: "-1" }
        },
        messages: {
            AssignedFacilitator: "Facilitator is required."
        },
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            $.each(validator.invalid, function (index, value) {
                if (index.toLowerCase().indexOf("fire") > 0) {
                    //highlight tab
                    $('.firereqcls').show();
                };
                if (index.toLowerCase().indexOf("zon") > 0) {
                    //highlight tab
                    $('.zoningreqcls').show();
                };
                if (index.toLowerCase().indexOf("backf") > 0) {
                    //highlight tab
                    $('.backfreqcls').show();
                };
                if (index.toLowerCase().indexOf("build") > 0
                    || index.toLowerCase().indexOf("elect") > 0
                    || index.toLowerCase().indexOf("mecha") > 0
                    || index.toLowerCase().indexOf("plumb") > 0
                    || index.toLowerCase().indexOf("facilitator") > 0
                ) {
                    //highlight tab
                    $('.bempreqcls').show();
                };
                if (index.toLowerCase().indexOf("food") > 0
                    || index.toLowerCase().indexOf("pool") > 0
                    || index.toLowerCase().indexOf("facil") > 0
                    || index.toLowerCase().indexOf("dayc") > 0
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
            } else {
                $('.bempreqcls').hide();
                $('.firereqcls').hide();
                $('.zoningreqcls').hide();
                $('.backfreqcls').hide();
                $('.healthreqcls').hide();
                $("div.error").hide();
            }
        },
        submitHandler: function (form) {

            var minDate = getMinDateOfPlanReviews();

            var gateDate = new Date();
            var gateDate = new Date($("#GateDate").val());

            var isReschedule = $("#IsReschedule").val();
            var isGateAccepted = $("#IsGateAccepted").val();

            var oneDay = 24 * 60 * 60 * 1000;
            var dateDiff = (gateDate - minDate) / oneDay;

            if (isReschedule == "True" && isGateAccepted == "False" && dateDiff >= 1) {
                $("#dialog-modal-confirmation").dialog("open");
                $(".confirmationmessage").html("Warning message: this is going to change the gate date, have you notified the customer?");
            }
            else {
                submitForm();
            }
            return false;
        }
    });

    function getMinDateOfPlanReviews() {
        var dates = [];
        dates.push($("#buildstartdatesel").val());
        dates.push($("#electstartdatesel").val());
        dates.push($("#mechastartdatesel").val());
        dates.push($("#plumbstartdatesel").val());
        dates.push($("#backfstartdatesel").val());
        dates.push($("#firestartdatesel").val());
        dates.push($("#zonestartdatesel").val());
        dates.push($("#poolstartdatesel").val());
        dates.push($("#foodstartdatesel").val());
        dates.push($("#facilstartdatesel").val());
        dates.push($("#daycstartdatesel").val());
        var data = $.grep(dates, function (i) {
            return i != '';
        });
        var fdates = [];
        $.each(data, function (index, value) {
            fdates.push(new Date(value));
        });
        var minDate = Math.min.apply(null, fdates)
        //var minDate = new Date();

        return minDate;
    }

    function submitForm() {
        var form = $('#SchedulingForm')[0];
        var savemessage = "Saving Plan Review ...";
        var submitmessage = "Submitting Plan Review ...";
        var message = savemessage;
        var isSubmit = $("#IsSubmit").val();
        if (isSubmit == "true")
            message = submitmessage;
        openSuccess(message, false);
        $("#btnSubmit").addClass("disabled");
        $("#btnSave").addClass("disabled");
        $("div.error").html("");
        $("div.error").hide();
        $('.bempreqcls').hide();
        $('.firereqcls').hide();
        $('.zoningreqcls').hide();
        $('.backfreqcls').hide();
        $('.healthreqcls').hide();
        form.submit();
    }
});


