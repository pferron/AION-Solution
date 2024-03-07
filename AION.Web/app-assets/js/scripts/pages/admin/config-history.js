
//Global vars
var configurationHistory = new Object();

//onload event
$(function () {
    $('.main-menu').hide();
    $('#navfooter').hide();
 
    $(".searchtableaudit").on("click", function () {
        SearchHistory();
    });
});

function SearchHistory() {
    BuildObject();
    GetTableAuditLog();
}

function BuildObject() {
    configurationHistory.SearchType = $('#SearchType').val();
    configurationHistory.SearchRange = $('#SearchRange').val();
}

function GetTableAuditLog() {

    if (configurationHistory != null) {
        openSuccess("Searching...", false)

        $.ajax({
            type: "POST",
            url: "GetTableAuditLogListWDetails",
            data: configurationHistory,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                $("#confighistorytable").empty();

                $("#confighistorytable").html(response);

                $('#confighistorytable').DataTable();

                closeSuccess();
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}