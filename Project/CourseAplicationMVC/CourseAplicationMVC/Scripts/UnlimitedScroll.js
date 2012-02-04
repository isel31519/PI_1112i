var contentHeight = 210;
var pageHeight = document.documentElement.clientHeight;
var scrollPosition;
var n = 5;
var xmlhttp;

function loadMoreFucs() {

    if (xmlhttp.readyState == 4) {
        if (xmlhttp.responseText) {
            {
                var fucs = xmlhttp.responseText.split(",");
                var fuc;
                var fucName;
                var fucAcr;

                for (var i = n - 4, idx = 0; i <= n; i++, idx += 10) {
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

    /*if (xmlhttp.readyState == 4) {
        if (xmlhttp.responseText) {
        var resp = xmlhttp.responseText.replace("\r\n", "");
        resp.replace("\"", "");
        var files = resp.split(",");
        var j = 0;
        for (i = 0; i < files.length; i++) {
        if (files[i] != "") {
        //document.getElementById("container").innerHTML += '<a href="img/' + files[i] + '"><img src="thumb/' + files[i] + '" /></a>';
        console.log(i);
        console.log(files[i]);
        console.log("j = " + j);
        var aux = files[i];
        aux=aux.trim();
        console.log(aux);
        document.getElementById("elems_table_first").innerHTML += '<a href="/fuc/detail/'+files[i]+'">'+files[i]+'</a>';
        j++;

        if (j == 3 || j == 6)
        document.getElementById("elems_table_first").innerHTML += files[i];
        else if (j == 9) {
        //document.getElementById("elems_table_first").innerHTML += '<p>' + (n - 1) + " Images Displayed | <a href='#header'>top</a></p><hr />";
        j = 0;
        }
        }
        }
        }
        }*/
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

    console.log("contentHeight = " + contentHeight);
    console.log("pageHeight = " + pageHeight);
    console.log("scrollPosition = " + scrollPosition);
    var sum = contentHeight - pageHeight - scrollPosition;
    console.log("sum = " + sum);

    if ((contentHeight - pageHeight - scrollPosition) < -600) {
        console.log("ENTROU");
        if (window.XMLHttpRequest)
            xmlhttp = new XMLHttpRequest();
        else
            if (window.ActiveXObject)
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            else
                alert("Bummer! Your browser does not support XMLHTTP!");

        xmlhttp.open("GET", "/UnlimitedScroll/GetMoreFucs", true);
        xmlhttp.send();

        n += 5;
        xmlhttp.onreadystatechange = loadMoreFucs;
        contentHeight += 210;
    }
}