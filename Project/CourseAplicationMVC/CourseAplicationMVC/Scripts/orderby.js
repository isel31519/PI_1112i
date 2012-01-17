function orderby() {

    var http = new XMLHttpRequest();
    console.log($(this));
    var href = window.location.href + "&orderby=" + $(this).text() + "&type=asc";
    http.open("GET", href + "&partial=true", false);
    http.onreadystatechange = window.useHttpResponse;
    http.send(null);

    if (http.readyState == 4 && http.status == 200) {
        var textout = http.responseText;
        //console.log(textout);
        $('#elems').html(textout);
        history.pushState(null, document.title, href);

    }
    accord();

}