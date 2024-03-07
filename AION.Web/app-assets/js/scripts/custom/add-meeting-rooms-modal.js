
//global variables
var meetingroomendpointurl = "/Scheduling/GetAllAvailableMeetingRoomsForDateRange";
var expressmeetingroomendpointurl = "/Scheduling/GetAllAvailableMeetingRoomsForDateRangeForExpressScheduling";

var validatemeetingroom = "/Scheduling/CheckMeetingRoomSelectedIsAvailable";
var validateexpressmeetingroom = "/Scheduling/CheckMeetingRoomSelectedIsAvailableForExpress";

$(function () {

    /**************** Add Meetings Modal Box *****************/

    $('#meeting-room-modal').on('shown.bs.modal', function () {
        OpenMeetingRooms();
        $('.meeting-room').iCheck({
            checkboxClass: 'icheckbox_minimal'
        });
    });

    $('#room-modal').on('hidden.bs.modal', function (e) {
        $("#meeting-rooms").empty();

        $('.meeting-room').iCheck('uncheck');
    });

    $("#btn-submit-manual-room").on("click", function () {
        SetMeetingRoomSelected();
    });

});

function OpenMeetingRooms() {
    ApplyMeetingRoomFilter();
}

//preliminary meeting
function ApplyMeetingRoomFilter() {
    //handle express meetings different endpoint
    //this element only exists on the express page
    if ($("#hIsExpress").length && $("#hIsExpress").val() == "true") {

        meetingroomendpointurl = expressmeetingroomendpointurl;

    }

    var seldate = $("#startdatesel").val();
    var starttime = $("#starttimesel").val();
    var endtime = $("#endtimesel").val();

    if (seldate == '' || starttime == '' || endtime == '') {
        $('.meeting-room').iCheck('uncheck');
        $(".meetingroomrefid").val(0);
        $(".meetingroomname").val("");
        $(".meetingroomnamelabel").text("- Select A Meeting Room -");
        //close the modal
        $('#meeting-room-modal').modal('hide');
        openWarning('Cannot decide meeting availablity without date and time range. Select date and time first');
        return false;
    }
    else {
        var data = new Object();
        data.selDate = seldate;
        data.selStartTime = starttime;
        data.selEndTime = endtime;
        $.ajax({
            type: "POST",
            url: meetingroomendpointurl,
            data: data,
            statusCode: {
                404: function () {
                    openWarning("page not found", true);
                },
                500: function () {
                    openWarning("500", true);
                }
            },
            success: function (response) {
                $(".searching-meeting-rooms").hide();
                $("#meeting-rooms").html(response);
            },
            failure: function (jqXHR, textStatus, errorThrown) {
                openWarning("Error: " + textStatus + " : " + errorThrown, true);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                openWarning("Error: " + textStatus + " : " + errorThrown, true);
            }
        });

        return true;
    }
}

function ValidateMeetingRoomAvailability() {
    //handle express meetings different endpoint
    //this element only exists on the express page
    if ($("#hIsExpress").length && $("#hIsExpress").val() == "true") {

        validatemeetingroom = validateexpressmeetingroom;

    }
    var seldate = $("#startdatesel").val();
    var starttime = $("#starttimesel").val();
    var endtime = $("#endtimesel").val();

    if (seldate == '' || starttime == '' || endtime == '') {
        var seldate = $(".startdatesel").val();
        var starttime = $(".starttimesel").val();
        var endtime = $(".endtimesel").val();
    }

    var meetingroom = $(".meetingroomrefid").val();

    if (seldate == '' || starttime == '' || endtime == '') {
        openWarning('Cannot decide meeting availablity without date and time range. Select date and time first');
        $('.meeting-room').iCheck('uncheck');
        $(".meetingroomrefid").val(0);
        $(".meetingroomname").val("");
        //close the modal
        $('#meeting-room-modal').modal('hide');
        $(".meetingroomnamelabel").text("- Select A Meeting Room -");
        return false;
    }
    else {
        var status = true;
        var data = new Object();
        data.selMeetingRoomRefID = meetingroom;
        data.selDate = seldate;
        data.selStartTime = starttime;
        data.selEndTime = endtime;
        var url = validatemeetingroom;
        $.ajax
            ({
                method: "POST",
                async: false,
                datatype: "json",
                url: url,
                data: data,
            })
            .done(function (response, status, jqxhr) {
                isRoomAvailable = response;
                if (isRoomAvailable == false) {
                    openWarning('Selected meeting room is no longer available, please select another room.');
                    $('.meeting-room').iCheck('uncheck');
                    $(".meetingroomrefid").val(0);
                    $(".meetingroomname").val("");
                    $(".meetingroomnamelabel").text("- Select A Meeting Room -");
                    status = false;
                }

                status = true;
            });
        return status;
    }
}

function SetMeetingRoomSelected() {
    var selectedRows = [];

    $('#meetingroomtable .meeting-room').each(function () {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var id = $(this).attr("data-id");
            var value = $(this).attr("data-meeting-room");
            selectedRows.push({ id: id, value: value });
        }
    });

    if (selectedRows.length > 0) {
        $.each(selectedRows, function (index, value) {
            var id = value.id;
            var meetingRoom = value.value;

            $(".meetingroomrefid").val(id);
            $(".meetingroomname").val(meetingRoom);
            $(".meetingroomnamelabel").text("-" + meetingRoom + "-");
            $("#manualmeetingroomid").val(id);

            var available = ValidateMeetingRoomAvailability();

            if (available) {
                $('#meeting-room-modal').modal('hide');
            }
        });
    } else {
        $(".meetingroomrefid").val(0);
        $(".meetingroomname").val("");
        $(".meetingroomnamelabel").text("- Select A Meeting Room -");
    }
}
//*********************************************//
