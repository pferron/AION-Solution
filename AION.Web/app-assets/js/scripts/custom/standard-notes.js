$(function () {
    ///LES-678////////////////////////////////
    $(".chkstandnote").on("change", function () {
        //add contents of text boxes to customer gate notes
        //rebuild any time the checkboxes change
        var arr = [];
        $('input.chkstandnote:checkbox:checked').each(function () {
            arr.push($(this).val());
        });
        GetStandardNotes(arr, $("#StandardNoteType").val());
    });

    $(".openstandnotes").on("click", function () {
        //get the type of notes BEMP,Zoning, etc
        var standardnotetype = $(this).attr("data-gatenoteid");
        $("#StandardNoteType").val(standardnotetype);
        //get what's currently in the customer gate notes
        setNotesTitle(standardnotetype);
        //set the text box in the standard notes dialog
        $(".chkstandnote").change();
    });

    $(".standnotesubmit").on("click", function () {
        SubmitStandardNote();
    });
    //////////////////////////////////////////
});

function setNotesTitle(standardnotetype) {
    var title = "";
    switch (standardnotetype) {
        case "BEMPGateNotes":
            title = "BEMP Customer/Gate Notes: ";
            break;
        case "ZoningGateNotes":
            title = "Zoning Customer/Gate Notes: ";
            break;
        case "FireGateNotes":
            title = "Fire Customer/Gate Notes: ";
            break;
        case "BackflowGateNotes":
            title = "Backflow Customer/Gate Notes: ";
            break;
        case "EHSGateNotes":
            title = "Health Customer/Gate Notes: ";
            break;
        default:
            title = "unknown";
    }
    $(".modal-title").html(title);
}
function PutStandardNotes() {
    var gatenoteid = $("#StandardNoteType").val();
    var gatenotes = $("#" + gatenoteid).val();
    $("#StandardNoteText").val(gatenotes);
}
function GetStandardNotes(arr) {
    PutStandardNotes();
    $.each(arr, function (index, value) {
        var txtid = value;
        var note = $("#" + txtid).val();
        InsertStandardNote(note);
    })
}
function InsertStandardNote(note) {
    var gatenotes = $("#StandardNoteText").val();
    gatenotes += "\n";
    gatenotes += "\n";
    gatenotes += note;
    $("#StandardNoteText").val(gatenotes);
}
function SubmitStandardNote() {
    var gatenoteid = $("#StandardNoteType").val();
    var gatenotes = $("#StandardNoteText").val();
    $("#" + gatenoteid).val(gatenotes);
}
    //////////////