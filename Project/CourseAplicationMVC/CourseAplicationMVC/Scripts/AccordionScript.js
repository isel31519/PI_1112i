$(document).ready(function () { accord(); });

function accord (){
    $('a.detailer').click(function(e) {
        e.preventDefault();
        $('#showDetail').slideDown();
        var http = new XMLHttpRequest();
        console.log($(this).attr("href"));
        http.open("GET", $(this).attr("href") + "?partial=true", false);
        http.onreadystatechange = window.useHttpResponse;
        http.send(null);

        if (http.readyState == 4 && http.status == 200) {
            var textout = http.responseText;
            console.log(textout);
            $('#showDetail').html(textout);
        }
    });
}