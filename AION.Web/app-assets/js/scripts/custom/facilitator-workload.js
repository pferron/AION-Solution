//init
$(function () {
    $('#datepicker1').datepicker({
        beforeShowDay: $.datepicker.noWeekends,
        onSelect: function (dateText, inst) {
            $('#datepicker2').datepicker('option', 'minDate', new Date(dateText));
        },
    });

    $('#datepicker2').datepicker({
        beforeShowDay: $.datepicker.noWeekends,
        minDate: $('#datepicker1').val(),
        onSelect: function (dateText, inst) {
            $('#datepicker1').datepicker('option', 'maxDate', new Date(dateText));
        }
    });
});