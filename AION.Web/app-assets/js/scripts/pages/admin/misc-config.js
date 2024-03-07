$(function () {

    $('form').on("submit", function () {
        // list box by default will pass only selected items back to server in model object.
        // So use jquery to force to send all items by selecting all the items in lists.
        selectallMISCs();
    });
});

function toggleSteps(element) {
    $("#txtSchedulingMultiplierFactor").val('');
    var use = element.value;
    if (use == "Percentage") {
        $("#txtSchedulingMultiplierFactor").attr('step', "0.1");
    } else if (use == "Hours") {
        $("#txtSchedulingMultiplierFactor").attr('step', "0.5");
    }
}  

function selectallMISCs() {
    $("#MISC").attr("multiple", "multiple");
    $("#MISC").find("option").each(function () {
        $(this).prop('selected', true);
    });
    $("#MISC_to").attr("multiple", "multiple");
    $("#MISC_to").find("option").each(function () {
        $(this).prop('selected', true);
    });
}