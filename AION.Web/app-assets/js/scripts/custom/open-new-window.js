/**
 * ScheduleCapacityMain and ConfigurationHistoryMain popup windows
 * @param {any} url
 * @param {any} width
 * @param {any} height
 */
function openNewWindow(url, width, height) {
    //var top = parseInt((screen.availHeight) - height - 100);
    //var left = parseInt((screen.availWidth) - (width / 2));
    var features = "width=" + width + ", height=" + height;
    window.open(url, "newwindow", features);
}

function isNullOrBlank(obj) {
    return obj == null || obj == "";
}