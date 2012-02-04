var contentHeight = 800;//210
var pageHeight = document.documentElement.clientHeight;
var scrollPosition;
var n = 10;//5
var xmlhttp;

function loadMoreFucs() {

    if (xmlhttp.readyState == 4) {
        if (xmlhttp.responseText) {
            {
                var fucs = xmlhttp.responseText.split(",");
                var fuc;
                var fucName;
                var fucAcr;
                           /*n - 4*/
                for (var i = n - 9, idx = 0; i <= n; i++, idx += 10) {
                    fuc = fucs[idx].split(":");
                    fucName = fuc[1];
                    fuc = fucs[idx+1].split(":");
                    fucAcr = fuc[1];
                    
                    fucName = removeQuoteAnnotation(fucName, "\"");
                    fucAcr = removeQuoteAnnotation(fucAcr, "\"");
                    addFucsToTable(i, fucName, fucAcr);
                }
            }
        }
    }
}

function removeQuoteAnnotation(str, str1) {
    var ret = "";
    for (var i = 0, j = 0; i < str.length; i++) {
        if (str[i] != str1) {
            ret += str[i];
            j++;
        }
    }
    return ret;
}

function addFucsToTable(i, fucName, fucAcr) {
    var table = document.getElementById("unlimitedScrollTable");
    var row = table.insertRow(i);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    cell1.innerHTML = '<a href="Fuc/Detail/' + fucAcr + '">' + fucName + '</a>';
    cell2.innerHTML = '<a href="Fuc/Detail/' + fucAcr + '">' + fucAcr + '</a>';
}

//function Trim(str) { return str.replace(/^\s+|\s+$/g, ""); }
//function Trim(str, str1, str2) { return str.replace(str1, str2); }

function scroll()
{
    if (navigator.appName == "Microsoft Internet Explorer")
        scrollPosition = document.documentElement.scrollTop;
    else
        scrollPosition = window.pageYOffset;

    /*console.log("contentHeight = " + contentHeight);
    console.log("pageHeight = " + pageHeight);
    console.log("scrollPosition = " + scrollPosition);
    var sum = contentHeight - pageHeight - scrollPosition;
    console.log("sum = " + sum);*/

    if ((contentHeight - pageHeight - scrollPosition) < 0) {
        if (window.XMLHttpRequest)
            xmlhttp = new XMLHttpRequest();
        else
            if (window.ActiveXObject)
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            else
                alert("Bummer! Your browser does not support XMLHTTP!");

        xmlhttp.open("GET", "/UnlimitedScroll/GetMoreFucs", true);
        xmlhttp.send();

        n += 10;//5
        xmlhttp.onreadystatechange = loadMoreFucs;
        contentHeight += 420;//210
    }
}