(function (window, undefined) {
    'use strict';

    // breadcrumbs
    // ****************************************************************************************
    //
    $(".sticky-breadcrumbs").sticky(
        {
            topSpacing: 0 - $(this).height(),
            zIndex: 900
        });

    $(document).on('show.bs.modal', '.modal', function () {
        $(".sticky-breadcrumbs").css({ 'position': '' })
    });
    $(document).on('hide.bs.modal', '.modal', function () {
        $("body").removeClass('modal-open');
        $("div.modal-backdrop").remove();
    });

    $('.dropdown-user-link').on('click', function () {
        $(".sticky-breadcrumbs").css({ 'position': '' })
    });

    $(".multiselect").multiselect({ selectedList: 2, noneSelectedText: "None Selected" });

    // datepicker
    // *******************************************************************************************
    //

    /****************************Express Date***********************/
    var expressdates = [];

    if ($("#hReserverdExpressDatesList").length) {
        var expressdatesList = $("#hReserverdExpressDatesList").val();
        var expressdatearr = expressdatesList.split(",");

        for (var i = 0; i < expressdatearr.length; i++) {

            expressdates.push(expressdatearr[i]);
        }
    }
    
    /****************************Holidays***********************/
    var holidays = [];

    if ($("#hHolidayslist").length) {
        var holidayslist = $("#hHolidayslist").val();
        var holidayarr = holidayslist.split(",");

        for (var i = 0; i < holidayarr.length; i++) {
            holidays.push(holidayarr[i]);
        }
    }

    // datepicker no date limitations
    $('.datepicker').datepicker({
        selectOtherMonths: true,
        changeMonth: true,
        changeYear: true,
        onSelect: function (dateText) {
            if ($(this).hasClass('start-date')) {
                $(".end-date").datepicker("option", "minDate", this.value);
            };
        }
    });

    $('.datepicker-future').datepicker({
        selectOtherMonths: true,
        changeMonth: true,
        changeYear: true,
        minDate: 0,
        onSelect: function (dateText) {
            if ($(this).hasClass('start-date')) {
                $(".end-date").datepicker("option", "minDate", this.value);
            };
        },
    });

    // datepicker no weekends or holidays and limited to next two years
    $('.datepicker-restricted').datepicker({
        selectOtherMonths: true,
        changeMonth: true,
        changeYear: true,
        minDate: 0,
        maxDate: "+2y",
        onSelect: function () {
            if ($(this).hasClass('start-date')) {
                $(".end-date").datepicker("option", "minDate", this.value);
            };
        },
        beforeShowDay: function (date) {
            var reformattedDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
            if (holidays.length) {
                if ($.inArray(reformattedDate, holidays) != -1) {
                    return [false, ''];
                }
            }
            var weekend_dates = $.datepicker.noWeekends(date);
            return weekend_dates; 
        }
    });

    // datepicker no weekends or holidays and limited to next two years
    $('.datepicker-express').datepicker({
        selectOtherMonths: true,
        changeMonth: true,
        changeYear: true,
        minDate: 0,
        maxDate: "+2y",
        onSelect: function () {
            if ($(this).hasClass('start-date')) {
                $(".end-date").datepicker("option", "minDate", this.value);
            };
        },
        beforeShowDay: function (date) {
            var reformattedDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
            if (expressdates.length) {
                if ($.inArray(reformattedDate, expressdates) != -1) {
                    return [true, ''];
                }
                else {
                    return [false, ''];
                }
            }
        }
    });

    $('.hasDatepicker').on('change', function () {
        if ($(this).hasClass('end-date')) {
            var startDate = $('.start-date');
            if ($(this).val() < startDate.val()) {
                $(this).val("");
                openWarning("Please fix the date range error.");
            }
        };
    });

    // timepicker
    // ******************************************************************************************************************
    //

    $('.pickatime').pickatime({ interval: 15 });

    $('.pickatime-minmax').pickatime({
        interval: 15,
        min: new Date(2015, 3, 20, 8, 0),
        max: new Date(2015, 7, 14, 17, 0),
        disable: [
            12
        ]
    });

})(window);