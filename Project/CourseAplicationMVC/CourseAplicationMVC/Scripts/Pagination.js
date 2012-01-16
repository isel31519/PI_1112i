$(document).ready(function () {

    var totalp = Math.ceil(parseInt($('#totalelems').val()) / $('#DisplayNum').val());
    $('#DisplayNum').val(1);
    $('#pageinput').val(1);
    $('#totalp').text(totalp);
    //events:
    $('#paging').click(function (e) {
        e.preventDefault();
        var value = $(this).text();
        if (value == "Pagination Off") {
            $('#paging').html("Pagination On");
            //esconder será melhor
            $('#prev').unbind("click");
            $('#next').unbind("click");
            $('#pageinput').unbind("keyup");
            $('#DisplayNum').unbind("change");

            $('#prev').click(function (e1) { e1.preventDefault(); });
            $('#next').click(function (e1) { e1.preventDefault(); });
            $('#pageinput').keyup(function (e1) { e1.preventDefault(); });
            $('#DisplayNum').change(function (e1) { e1.preventDefault(); });

            paging($(this).attr("href"));

        } else {
            $('#paging').html("Pagination Off");
            events();
            refreshelems(window.location.href, 1);
            //paging($(this).attr("href"));
        }


        return false;
    });

    events();




    // refreshelems(window.location.href, 1);
});

function events() {
    $('#pageinput').keyup(function () {

        refreshelems(window.location.href, parseInt($('#pageinput').val()));
        return false;
    });


    $('#DisplayNum').change(function () {
        var totalpages = Math.ceil(parseInt($('#totalelems').val()) / $('#DisplayNum').val());
        $('#totalp').text(totalpages);
        var page=1;
        if (parseInt($('#pageinput').val()) <= totalpages)
            page = parseInt($('#pageinput').val());
        refreshelems(window.location.href, page);
        return false;
    });

    $('#prev').click(function (e) {

        e.preventDefault();
        var page = parseInt($('#pageinput').val());
        refreshelems($(this).attr("href"), page - 1);
        accord();
        return false;
    });
    $('#next').click(function (e) {
        e.preventDefault();
        var page = parseInt($('#pageinput').val());
        refreshelems($(this).attr("href"), page + 1);
        accord();
        return false;
    });
}

function paging(myurl) {
    var http = new XMLHttpRequest();
    http.open("GET", myurl + "&partial=true", false);
    http.onreadystatechange = window.useHttpResponse;
    http.send(null);

    if (http.readyState == 4 && http.status == 200) {
        var textout = http.responseText;
        console.log(textout);
        $('#elems').html(textout);
    }
    return false;
    
}
function refreshelems(myurl, page) {

        if (page < 1 || page > parseInt($('#totalp').text())) return false;
        myurl = myurl.substring(0, myurl.indexOf("?", myurl));
        var itemsPerPage = $('#DisplayNum').val();
        var http = new XMLHttpRequest();
        http.open("GET", myurl + "?page=" + page + "&itemsnumber=" + itemsPerPage + "&partial=true", false);
        http.onreadystatechange = window.useHttpResponse;
        http.send(null);

        if (http.readyState == 4 && http.status == 200) {
            var textout = http.responseText;
            console.log(textout);
            $('#elems').html(textout);
            ($('#pageinput').val(page));

            $('#next').attr("href", replacehref(myurl, page + 1, itemsPerPage));
            $('#prev').attr("href", replacehref(myurl, page - 1, itemsPerPage));

        }
        return false;

    }
    function replacehref(myurl, page, number) {
        var f = myurl + "?page=id&itemsnumber=id2";
        f = f.replace("id", page);
        f = f.replace("id2", number);
        return f;
    }
