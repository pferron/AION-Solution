$(function () {
    var activeDate = $("#MessageConfigAdminViewModel_ActiveDate").val();

    //if (activeDate != '') {
    //    $("#starttimesel").timepicker('setTime', activeDate);
    //}

    CKEDITOR.replace("FullDescription", {
        toolbar: [['Source', 'Bold', 'Italic', 'Font', 'FontSize', 'Link', 'Unlink', 'Anchor', 'TextColor']],
        width: 1200,
        height: 500,
        htmlEncodeOutput: true,
    });

    $("#messageTemplate").on("change", function () {
        var id = $(this).val();
        if (id > 0) {
            GetMessageTemplateById(id);
        }
    });

    $("#messageTemplateType").on("change", function () {
        var id = $(this).val();
        if (id > 0) {
            GetMessageTemplatesByTypeId(id);
        }
    });

    $(".dataelement").on("click", function () {
        InsertDataElement($(this).data("valtxt"));
    });

    $(".edittemplate").on("click", function () {
        StartEditTemplate();
    });
    $(".canceltemplate").on("click", function () {
        CancelEditTemplate();
    });
    $("#activeDateTime").removeAttr("readonly");
});

function GetMessageTemplatesByTypeId(id) {
    var o = new Object();
    o.id = id;
    if (id != null && id > 0) {
        //empty the templates dropdown
        $('#messageTemplate').find('option').not('[value=-1]').remove();
        CKEDITOR.instances['FullDescription'].setData("Please wait");
        //clear the edit button
        $(".editingtemplate").hide();
        $("#MessageConfigAdminViewModel_IsEdit").val("");
        $(".canceltemplate").hide();
        $(".edittemplate").show();
        //clear the template name
        $("#messageTemplateName").val("");
        $.ajax({
            type: "POST",
            url: "/Admin/GetMessageTemplatesByTypeId",
            data: o,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                CKEDITOR.instances['FullDescription'].setData("");

                //response is MessageTemplate list
                var messageTemplates = response;

                //fill the templates dropdown
                $.each(messageTemplates, function (i, v) {
                    $('#messageTemplate').append(new Option(v.TemplateName, v.TemplateId));
                });
                $(".edittemplate").prop("disabled", true);
            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }


}

function GetMessageTemplateById(id) {
    var o = new Object();
    o.id = id;
    if (id != null && id > 0) {
        CKEDITOR.instances['FullDescription'].setData("Please wait");
        $.ajax({
            type: "POST",
            url: "/Admin/GetMessageTemplateById",
            data: o,
            statusCode: {
                404: function () {
                    openSuccess("page not found", true);
                }
            },
            success: function (response) {
                //response is MessageTemplate
                var messageTemplatejson = response;
                SetTemplateVariables(messageTemplatejson)
                $(".edittemplate").prop("disabled", false);

            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }

}

function SetTemplateVariables(messageTemplatejson) {
    //set template name
    var templatename = messageTemplatejson.TemplateName;
    $("#messageTemplateName").val(templatename);
    //set activation date and time
    var activationdate = messageTemplatejson.DisplayActiveDate;
    $("#activeDate").val(activationdate);
    var activationtime = messageTemplatejson.DisplayActiveTime;
    var activedt = new Date(activationdate + " " + activationtime);
    $("#activeDateTime").pickatime('picker').set('select', new Date(activedt));
    //set editor text
    var messageTxt = messageTemplatejson.TemplateText;
    CKEDITOR.instances['FullDescription'].setData(messageTxt);
    //set the active/inactive
    var active = messageTemplatejson.ActiveInd;
    var activestr = "";
    if (active == null || active == false) {
        activestr = "false";
    } else {
        activestr = "true";
    }
    $("#drpDnLstActive").val(activestr);

}

function InsertDataElement(valtxt) {
    CKEDITOR.instances['FullDescription'].insertText(valtxt);
}

function StartEditTemplate() {
    $(".editingtemplate").show();
    $("#MessageConfigAdminViewModel_IsEdit").val("true");
    $(".canceltemplate").show();
    $(".edittemplate").hide();
}

function CancelEditTemplate() {
    $(".editingtemplate").hide();
    $("#MessageConfigAdminViewModel_IsEdit").val("false");
    $(".canceltemplate").hide();
    $(".edittemplate").show();
    GetMessageTemplateById($("#messageTemplate").val());
    $(".edittemplate").prop("disabled", false);

}
function IsDuplicateTemplateNameAjax() {
    var name = $("#messageTemplateName").val();
    var isDuplicateTemplateName = false;
    var o = new Object();
    o.name = name;
    if (name != null) {
        $.ajax({
            type: "POST",
            url: "/Admin/IsDuplicateTemplateName",
            data: o,
            statusCode: {
                404: function () {
                    openWarning("page not found", true);
                }
            },
            success: function (response) {
                isDuplicateTemplateName = response.isDuplicateTemplateName;
                $("#isDuplicateTemplateName").val(isDuplicateTemplateName);
                if (isDuplicateTemplateName) {
                    $("#errorlist").val("Please choose another name. Message Name exists.<br/>");
                    $("#errorlist").show();
                    openWarning("Please choose another name. Message Name exists.<br/>");
                } else {
                    messageConfigurationValidationComplete();
                }

            },
            failure: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); },
            error: function (jqXHR, textStatus, errorThrown) { openSuccess("Error: " + textStatus + " : " + errorThrown, true); }
        });
    }
}
function isTemplateNameValid(name) {
    var reg = /^[A-Za-z][A-Za-z0-9()\/\'\-.\s]+([\ A-Za-z][A-Za-z0-9()\/\'\-.\s]+)$/;
    return reg.test(name);
}
