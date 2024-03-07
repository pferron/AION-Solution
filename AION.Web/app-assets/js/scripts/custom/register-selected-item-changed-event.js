

$(function () {

})

/**
    * Registers and event to idenitify the changes on each selection change and then stores that in the control in data element as SelectionChangeEventArgs.
    * This can be retrived with a seperate event argument as
    * $("#chkDrpDnExPlnRvrsBuild").change(function ()
           {

               var evntArgs = {
                   IsDeleted: false,
                   IsAdded: false,
                   AddedValues: [], //null if no change/None. Else changed value.
                   DeletedValues: [] //null if no change/None. Else changed value.
               };
    *          evntArgs = $(this).data('SelectionChangeEventArgs');
    *      }
    * paramater dropdownElementIDName = ID or class name of the element. This need to be unique otherwise the same value will be cross applied between controls..
    */
function RegisterSelectedItemChangeEvent(dropdownElementIDORClassName) {
    var dropdownElementRef = dropdownElementIDORClassName;
    //creates an option to let user select by both id and class.
    if ($("." + dropdownElementRef).length > 0) {
        dropdownElementRef = "." + dropdownElementRef;
    }
    else if ($("#" + dropdownElementRef).length > 0) {
        dropdownElementRef = "#" + dropdownElementRef;
    }

    //Intializes the first time data and stores the values back to control. So if any of the checkboxes in dropdown is selected then it will be processe and added to control.
    $(dropdownElementRef).data('lastsel', $(dropdownElementRef).val());
    var beforeval = $(dropdownElementRef).data('lastsel');
    var afterval = $(dropdownElementRef).val();
    //storing the last value for next time change.
    $(dropdownElementRef).data('lastsel', afterval);
    //get changes details
    var delta = GetWhatChanged(beforeval, afterval);
    //stores the change details back into same object so that it can be used from anywhere regarless of who is calling it.
    $(dropdownElementRef).data('SelectionChangeEventArgs', delta);
    //prepares the event so that the same operation can be done everytime the object is changed.
    $(dropdownElementRef).change(function () {
        var beforeval = $(dropdownElementRef).data('lastsel');
        var afterval = $(dropdownElementRef).val();
        //storing the last value for next time change.
        $(dropdownElementRef).data('lastsel', afterval);
        //get changes details
        var delta = GetWhatChanged(beforeval, afterval);
        //stores the change details into same object so that it can be used from anywhere regarless of who is calling it.
        $(dropdownElementRef).data('OnSelectionChangeEventArgs', delta);
        //fires the event
        $(dropdownElementRef).trigger('OnSelectionChange', [delta]);
        //$.event.trigger('OnSelectionChange', [delta]);
    });
}

//assume this will never be called with both added and removed at same time.
//console.log(GetWhatChanged("39,96,121,107", "39,96,106,107,109")); //This will not work correctly since there are values added and removed at same time.
function GetWhatChanged(lastVals, currentVals) {
    if (typeof lastVals === 'undefined')
        lastVals = '' //for the first time the last val will be empty in that case make both same.
    if (typeof currentVals === 'undefined')
        currentVals = ''
    var ret = {
        IsDeleted: false,
        IsAdded: false,
        AddedValues: [], //null if no change/None. Else changed value.
        DeletedValues: [] //null if no change/None. Else changed value.
    };
    var addedvals;
    var delvals;
    var lastValsArr, currentValsArr;
    if (Array.isArray(lastVals))
        lastValsArr = lastVals;
    else
        lastValsArr = lastVals.split(",");
    if (Array.isArray(currentVals))
        currentValsArr = currentVals;
    else
        currentValsArr = currentVals.split(",");
    delvals = $(lastValsArr).not(currentValsArr).get();
    if (delvals.length > 0) {
        //console.log("Deleted :" + delvals[0]);
        for (var i = 0; i < delvals.length; i++) {
            ret.DeletedValues.push(delvals[i]);
        }
        ret.IsDeleted = true;
    }
    addedvals = $(currentValsArr).not(lastValsArr).get();
    if (addedvals.length > 0) {
        //console.log("Added:" + addedvals[0]);
        for (var i = 0; i < addedvals.length; i++) {
            ret.AddedValues.push(addedvals[i]);
        }
        ret.IsAdded = true;
    }
    return ret;
};

function DropDownListRemoveItem(ctrlID, val) {
    $("#" + ctrlID + " option[value=" + val + "]").remove();
}

function DropDownListAddItem(ctrlID, text, val) {
    var elementObj = document.getElementById(ctrlID);
    if (elementObj != null) {
        var newOption = new Option(); // Create a new instance of ListItem
        newOption.text = text;
        newOption.value = val;

        elementObj.options[elementObj.length] = newOption;
    }
}