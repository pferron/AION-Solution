

/***
 * 
 * This js is for the scheduling standard notes.
 * 
 * 
 * 
 * 
 */
$(function () {
    ///LES-678////////////////////////////////
    $(".chkstandnote").change(function () {
        //add contents of text boxes to customer gate notes
        //rebuild any time the checkboxes change
        //array of checked boxes
        var arr = [];
        $('input.chkstandnote:checkbox:checked').each(function () {
            arr.push($(this).val());
        });
        GetStandardNotes(arr);
    });
    $(".openstandnotes").click(function () {
        //get the type of notes BEMP,Zoning, etc
        var standardnotetype = $(this).attr("data-gatenoteid");
        $("#StandardNoteType").val(standardnotetype);
        //get what's currently in the customer gate notes
        setNotesTitle(standardnotetype);
        //set the text box in the standard notes dialog
        $(".chkstandnote").change();
        openNotes();
    });
    $(".standnotesubmit").click(function () {
        SubmitStandardNote();
        closeNotes();
    });
    //////////////////////////////////////////
});

//**********************
function PutStandardNotes() {
    var gatenoteid = $("#StandardNoteType").val();
    var gatenotes = $("#" + gatenoteid).val();
    $("#StandardNoteText").val(gatenotes);
    $("#schedstandardnotes").val(gatenotes);
}
function GetStandardNotes(arr) {
    //PutStandardNotes();
    $("#StandardNoteText").val("");
    $("#schedstandardnotes").val("");
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
    $("#schedstandardnotes").val(gatenotes);
}
function SubmitStandardNote() {
    var gatenoteid = $("#StandardNoteType").val();
    var gatenotes = $("#StandardNoteText").val();
    $("#" + gatenoteid).val(gatenotes);
}
    //////////////