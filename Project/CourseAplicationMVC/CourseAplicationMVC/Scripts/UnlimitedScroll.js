var contentHeight = 800;
var pageHeight = document.documentElement.clientHeight;
var scrollPosition;
var n = 5;
var xmlhttp;

function loadMoreFucs() {

    if (xmlhttp.readyState == 4) {
        if (xmlhttp.responseText) {
            {
                alert("Loading...");
                addFucsToTable();
                alert("Done!");
                console.log(xmlhttp.responseText);      /*ver porque é que o responseText retorna POO*/
            }
        }
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

function addFucsToTable() {

    var table;
    var row;
    var cell1;
    var cell2;
    
    for (var i = n - 4; i <= n; i++) {
        table = document.getElementById("unlimitedScrollTable");
        row = table.insertRow(i);
        cell1 = row.insertCell(0);
        cell2 = row.insertCell(1);
        cell1.innerHTML = "New";
        cell2.innerHTML = "New";
    }
}

//function Trim(str) { return str.replace(/^\s+|\s+$/g, ""); }

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

        n += 5;
        xmlhttp.onreadystatechange = loadMoreFucs;
        contentHeight += 210;
    }
}