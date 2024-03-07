$(function () {
    $("#add-npa").on("click", function (e) {
        e.preventDefault();
        AddNewItemToList();
    });

    $('form').on("submit", function () {
        // list box by default will pass only selected items back to server in model object.
        // So use jquery to force to send all items by selecting all the items in lists.
        selectallNPAs();
    });

 });

function selectallNPAs() {
    $("#NPA").attr("multiple", "multiple");
    $("#NPA").find("option").each(function () {
        $(this).prop('selected', true);
    });
    $("#NPA_to").attr("multiple", "multiple");
    $("#NPA_to").find("option").each(function () {
        $(this).prop('selected', true);
    });
}

function AddNewItemToList() {
    var data = "";
    data = $("#txtAddNew").val();
    var warning = Validate();
    if (warning == "") {
        var timeallocationtype = $("#timeAllocationType option:selected").text();
        timeallocationtype = " ( " + timeallocationtype + " )";
        if (data != "") {
            var Target = document.getElementById("NPA");
            var newOption = new Option(); // Create a new instance of ListItem
            newOption.text = data + timeallocationtype;
            newOption.value = "-1," + data + timeallocationtype;
            Target.options[Target.length] = newOption; //Append the item in Target
            $("#txtAddNew").val(""); //removes the text box.
        }
    } else {
        //show errors
        openWarning(warning);
    }
}
function isNPATypeNameValid(name) {
    var reg = /^[A-Za-z][A-Za-z0-9\'\-.]+([\ A-Za-z][A-Za-z0-9\'\-.\s]+)$/;
    return reg.test(name);
}

function Validate() {
    //only allow letters and numbers
    //require at least 3 characters
    var data = $("#txtAddNew").val();
    var warning = '';

    if (data == "") {
        warning += "*Please enter a NPA Type. NPA Type must have at least 4 characters, Aa-Zz, 0-9.<br/>";
    }
    else if (!isNPATypeNameValid(data)) {
        warning += "*Please enter a valid NPA Type. NPA Type must have at least 4 characters, Aa-Zz, 0-9.<br/>";
    }

    return warning;
}