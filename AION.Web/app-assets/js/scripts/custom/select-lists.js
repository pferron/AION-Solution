function addtoselectlist(objname, objcls, objval, objtxt,) {
    $("#" + objname).append('<option class="' + objcls + '" value="' + objval + '">' + objtxt + "</option>");
}
function removefromselectlist(objname, objval) {
    $("#" + objname + ' option[value="' + objval + '"]').remove();
}
function selectall(objname, selected) {
    $('#' + objname + ' option').prop('selected', selected);
    $('#' + objname).multiselect('refresh');
}