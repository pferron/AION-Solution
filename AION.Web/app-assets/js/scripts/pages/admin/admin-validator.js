$(function () {

    var validator = $("#AdminForm").validate(
        {
            invalidHandler: function (form, validator) {
                var errors = validator.numberOfInvalids();
                if (errors) {
                    var message = errors == 1
                        ? 'You missed 1 field. It has been highlighted'
                        : 'You missed ' + errors + ' fields. They have been highlighted';
                    openWarning(message);
                }

            }
        });
});

function ValidateAdminMain() {
    openWarning("Validating...", false);
    $("#errorlist").val("");
    $("#errorlist").hide();
    var email = $("#txtEmail").val() != "";
    var userid = $("#drpDnLstUsers").children("option:selected").val() != "0";
    if (email || userid) {
        //check for missing values

        var warning = '';

        if ($("#txtFirstName").val() == "") {
            warning += "*Please enter a First Name.<br/>";
        }
        else if (!isFirstNameValid($("#txtFirstName").val())) {
            warning += "*Please enter a valid First Name.<br/>";
        }
        if ($("#txtLastName").val() == "") {
            warning += "*Please enter a Last Name.<br/>";

        }
        else if (!isLastNameValid($("#txtLastName").val())) {
            warning += "*Please enter a valid Last Name.<br/>";
        }

        if ($("#txtUserName").val() == "") {
            warning += "*Please enter an User Name.<br/>";

        } else if (validateCaseSensitiveEmail($("#txtUserName").val()) == false) {
            warning += "*Please enter a valid User Name.<br/>";
        }
        if ($("#txtAdAccount").val() == "") {
            warning += "*Please enter an AD Account.<br/>";
        }
        if ($("#txtEmail").val() == "") {
            warning += "*Please enter an Email Address.<br/>";
        }
        else if (validateCaseSensitiveEmail($("#txtEmail").val()) == false) {
            warning += "*Please enter a valid Email Address.<br/>";
        }

        if (GetSelectedRoles() == "") {
            warning += "*Please select a Role.<br/>";
        }

        if (warning != '') {
            $("#errorlist").val(warning);
            $("#errorlist").show();
            openWarning(warning);
        }
        else {
            //ajax cascade of validation occurs after this check.
            //Checks for user, then checks Message Configuration, if no errors, then the form is validated and submitted.
            //if this is an edit, user id > 0, then don't check.

            closeWarning();

            ValidateEmailAndUserName();
        }
    } else {
        userManagementValidationComplete();
    }
}

function userManagementValidationComplete() {
    //do the message configuration validation
    if ($("#isEdit").val() == "true") {

        var mcwarning = "";
        if (!isTemplateNameValid($("#messageTemplateName").val())) {
            mcwarning += "Please enter a valid Message Name.";
            $("#errorlist").val(mcwarning);
        }
        if (mcwarning != "") {
            openWarning(mcwarning);
        } else {
            IsDuplicateTemplateNameAjax();
        }

    } else {
        allValidationComplete();
    }
};

function messageConfigurationValidationComplete() {

    allValidationComplete();
}

function allValidationComplete() {
    //check if there are any errors, if not, if form is valid, submit
    var errors = $("#errorlist").val() != "";

    if (errors == false && $("#AdminForm").valid()) {
        openWarning("Saving...", false);
        $("#AdminForm").submit();
    }
}