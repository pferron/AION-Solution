$(function () {
 
    //**************************************************//


    /* During the first time GetWhatChanged is called for change of each dropdown box, we need
     * to know what is the first set of values on load. else the first set of change will not be reflected properly.
     * So just after the form load is done, add all the selected lists to related html element as custome data.*/

    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBuild");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsElectric");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsMech");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPlumb");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsBackFlow");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFood");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsPool");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsLodge");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsDayCare");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsFire");
    RegisterSelectedItemChangeEvent("chkDrpDnExPlnRvrsZone");

    $(".chkDrpDnExPln").on("OnSelectionChange", function (e, eventData) {
        var evntArgs = {
            IsDeleted: false,
            IsAdded: false,
            AddedValues: [], //null if no change/None. Else changed value.
            DeletedValues: [] //null if no change/None. Else changed value.
        };
        var source = e;
        //evntArgs = $(this).data('SelectionChangeEventArgs');
        evntArgs = eventData;
        var elementnm = $(this).attr("id");
        if (evntArgs !== "undefined" && elementnm != "") {
            if (evntArgs.IsAdded == true) {
                //if excluded checked then remove.
                for (var i = 0; i < evntArgs.AddedValues.length; i++) {
                    DropDownListRemoveItem(GetPrimaryReviewerID(elementnm), evntArgs.AddedValues[i]);
                    DropDownListRemoveItem(GetSecondaryReviewerID(elementnm), evntArgs.AddedValues[i]);
                }
            }
            if (evntArgs.IsDeleted == true) {
                //if excluded checked then remove.
                for (var i = 0; i < evntArgs.DeletedValues.length; i++) {
                    var txt = $("#" + elementnm + " option[value=" + evntArgs.DeletedValues[i] + "]").text();
                    DropDownListAddItem(GetPrimaryReviewerID(elementnm), txt, evntArgs.DeletedValues[i]);
                    DropDownListAddItem(GetSecondaryReviewerID(elementnm), txt, evntArgs.DeletedValues[i]);
                }
            }
        }
    });
});
// ****** end on load event ***************//
function GetPrimaryReviewerID(excludedCtrlID) {
    switch (excludedCtrlID) {
        case "chkDrpDnExPlnRvrsBuild":
            return "DrpDnPrimaryBuild";
        case "chkDrpDnExPlnRvrsElectric":
            return "DrpDnPrimaryElectric";
        case "chkDrpDnExPlnRvrsMech":
            return "DrpDnPrimaryMech";
        case "chkDrpDnExPlnRvrsPlumb":
            return "DrpDnPrimaryPlumb";
        case "chkDrpDnExPlnRvrsBackFlow":
            return "DrpDnPrimaryBackFlow";
        case "chkDrpDnExPlnRvrsFood":
            return "DrpDnPrimaryFood";
        case "chkDrpDnExPlnRvrsPool":
            return "DrpDnPrimaryPool";
        case "chkDrpDnExPlnRvrsLodge":
            return "DrpDnPrimaryLodge";
        case "chkDrpDnExPlnRvrsDayCare":
            return "DrpDnPrimaryDayCare";
        case "chkDrpDnExPlnRvrsFire":
            return "DrpDnPrimaryFire";
        case "chkDrpDnExPlnRvrsZone":
            return "DrpDnPrimaryZone";
        default:
            return "";
    }

}

function GetSecondaryReviewerID(excludedCtrlID) {
    switch (excludedCtrlID) {
        case "chkDrpDnExPlnRvrsBuild":
            return "DrpDnSecondaryBuild";
        case "chkDrpDnExPlnRvrsElectric":
            return "DrpDnSecondaryElectric";
        case "chkDrpDnExPlnRvrsMech":
            return "DrpDnSecondaryMech";
        case "chkDrpDnExPlnRvrsPlumb":
            return "DrpDnSecondaryPlumb";
        case "chkDrpDnExPlnRvrsBackFlow":
            return "DrpDnSecondaryBackFlow";
        case "chkDrpDnExPlnRvrsFood":
            return "DrpDnSecondaryFood";
        case "chkDrpDnExPlnRvrsPool":
            return "DrpDnSecondaryPool";
        case "chkDrpDnExPlnRvrsLodge":
            return "DrpDnSecondaryLodge";
        case "chkDrpDnExPlnRvrsDayCare":
            return "DrpDnSecondaryDayCare";
        case "chkDrpDnExPlnRvrsFire":
            return "DrpDnSecondaryFire";
        case "chkDrpDnExPlnRvrsZone":
            return "DrpDnSecondaryZone";
        default:
            return "";
    }
}
