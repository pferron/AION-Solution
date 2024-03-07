
//This checks the inner text of anchor tag (<a href...)
//on page load, the inner text should be +
function togglePlus(obj) {
    //console.log(obj.innerText);
    var innerText = obj.innerText;
    if (innerText == '+') {
        obj.innerText = '-';
    } else {
        obj.innerText = '+';
    }
}
