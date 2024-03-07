$(function () {

    var statusmessage = $("#StatusMessage").val();

    if (statusmessage != "") {
        openWarning(statusmessage);
    }

    $("#filter-dialog").dialog({
        autoOpen: false,
        minHeight: 600,
        minWidth: 900
    });

    var outerHeight = 0;
    outerHeight += $('.header-navbar').outerHeight();
    outerHeight += $('.sticky-wrapper').outerHeight();

    var customDefs = [];
    var pages = window.location.href.split('/').pop();
    switch (pages) {
        case 'MeetingsDashboard':
            customDefs.push({ targets: [6], type: 'date' });
            break;
        case 'SchedulingDashboard':
            customDefs.push({ targets: [8,9], type: 'date' });
            break;
        case 'EstimationDashboard':
            customDefs.push({ targets: [23], type: 'date' });
            break;
    }
    $.fn.dataTable.moment('MM/dd/yyyy');
    $('.fixed-header').DataTable({
        fixedHeader: {
            header: true,
            headerOffset: outerHeight
        },
        colReorder: true,
        scrollX: true,
        columnDefs: customDefs
    });

    // hack to get the fixed header to scroll horizontally with the body of the table.
    $('.dataTables_scrollBody').on("scroll", function () {
        if ($(".fixedHeader-floating").is(":visible")) {
            var outerWidth = 0;
            outerWidth += $('.main-menu').outerWidth() + 38;

            $('.fixedHeader-floating').css('left', -$(this).scrollLeft() + outerWidth + "px");
        }
    });
});