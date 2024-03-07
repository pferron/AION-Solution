

$(function () {

    /**************** Add Attendees Modal Box *****************/
    var $modal = $('#dialog-modal-addattendees');
    $modal.find('.modal-content')
        .resizable({
            handles: 'n, e, s, w, ne, sw, se, nw',
        })
        .draggable({
            handle: '.modal-header'
        });

    $("#addremoveattendees").bootstrapTable();;
    $('#addremoveattendees').on('check.bs.table', function (e, row, $element) {
        // console.log(row, $element);
        var rval = $("#requiredattendees").val();
        if (rval == null) rval = "";
        $("#requiredattendees").val(rval + " " + row.UserName + "; ");
        //add id to input
        var sval = $(".selectedattendees").val();
        if (sval == null) sval = "";
        $(".selectedattendees").val(sval + row.UserId + ",");
        var rowId = $("#tableName >tbody >tr").length;
        rowId = rowId + 1;
        $('#currentattendees').bootstrapTable('insertRow', {
            index: rowId,
            row: row
        });
    });
    $('#addremoveattendees').on('uncheck.bs.table', function (e, row, $element) {
        // console.log(row, $element);
        var rval = $("#requiredattendees").val();
        if (rval == null) rval = "";
        var usernames = $("#requiredattendees").val().replace(row.UserName + "; ", '');
        $("#requiredattendees").val(usernames);
        //remove id from input
        var sval = $(".selectedattendees").val();
        if (sval == null) sval = "";
        var userids = $(".selectedattendees").val().replace(row.UserId + ",", '');
        $(".selectedattendees").val(userids);
        $('#currentattendees').bootstrapTable('remove', {
            field: "UserId",
            values: row.UserId
        });
    });

})