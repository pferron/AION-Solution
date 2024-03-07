$(function () {
    $("#btn-reset").on("click", function (e) {
        e.preventDefault();
        $(".form-control").val("");
        $('#sectionResults').hide().html("");
    });

    $('.fixed-header').DataTable({
        fixedHeader: true
    });

    $("#btn-submit").on("click", function () {
        SearchProjects();
    });

    $('#sectionSearch').on("keypress", function (e) {
        if (e.key === 'Enter') {
            $("#btn-submit").trigger('click');
            return false;
        }
    });

    function SearchProjects() {
        openInProgress("Searching");

        $.ajax({
            url: "SearchProjects",
            type: "GET",
            data: $("#search-dashboard").serialize(),
            success: function json(result) {
                closeSuccess();

                $('#sectionResults').show().html(result);
                $("#projects").DataTable().destroy();
                $.fn.dataTable.moment('MM/dd/yyyy');
                $('#projects').DataTable({
                    order: [[0, 'desc']],
                    columnDefs: [
                        { targets: 0, type: 'date' }
                    ]
                });
            },
            error: function () { openWarning("There was an error."); }
        });
    }
});
