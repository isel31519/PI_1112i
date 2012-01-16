$(document).ready(function () { accord(); });

function accord (){
    $('a.detailer').click(function (e) {
        e.preventDefault();
        console.log($('#showDetail>h2').text());
        console.log($(this).text());
        if ($('#showDetail>h2').text() == $(this).text()) {
            $('#showDetail').children().remove();
            return false;
        }

        $('#showDetail').slideDown();
        var http = new XMLHttpRequest();
        http.open("GET", $(this).attr("href") + "?partial=true", false);
        http.onreadystatechange = window.useHttpResponse;
        http.send(null);

        if (http.readyState == 4 && http.status == 200) {
            var textout = http.responseText;

            $('#showDetail').html(textout);
        }
    });
}