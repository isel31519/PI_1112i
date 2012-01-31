var contentHeight = 800;
var pageHeight = document.documentElement.clientHeight;
var scrollPosition;
var n = 5;
var xmlhttp;

/*function putImages() {

    if (xmlhttp.readyState == 4) {
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
                    /j++;

                    if (j == 3 || j == 6)
                        document.getElementById("elems_table_first").innerHTML += files[i];
                    else if (j == 9) {
                        //document.getElementById("elems_table_first").innerHTML += '<p>' + (n - 1) + " Images Displayed | <a href='#header'>top</a></p><hr />";
                        j = 0;
                    }
                }
            }
        }
    }
}*/

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

        alert("Load more");
        
        if (window.XMLHttpRequest)
            xmlhttp = new XMLHttpRequest();
        else
            if (window.ActiveXObject)
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            else
                alert("Bummer! Your browser does not support XMLHTTP!");

        //var url = "getImages.php?n=" + n;

        xmlhttp.open("GET", "/Search/FindFucsAndAcrs", true);/*a cena está certa. só falta ir buscar + fucs*/
        xmlhttp.send();

        n += 5;
        //xmlhttp.onreadystatechange = putImages;
        contentHeight += 210;
    }
}  