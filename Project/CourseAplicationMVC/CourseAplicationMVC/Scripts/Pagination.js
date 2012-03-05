function checkPaging(elem, e) {
    e.preventDefault();
    var value = $(elem).text();
    if (value == "Pagination Off") {
        paging($(elem).attr("href"), false);
        $('.order').click(function () { orderby($(this)); });
        accord();

    } else {
        paging($(elem).attr("href"), true);
        var totalp = Math.ceil(parseInt($('#totalelems').val()) / $('#DisplayNum').val());
        $('#totalp').text(totalp);
        events();
        refreshelems(window.location.href, 1);
    }

    return false;

}

function paging(myurl, flag) {
    var http = new XMLHttpRequest();
    if (flag == false) {

        $('#paging').html("Pagination On");
        var href = $('#paging').attr("href");
        href = href.replace("pagination=False", "pagination=true");
        $('#pages').remove();


        http.open("GET", myurl + "&partial=true", false);
        http.onreadystatechange = function() {
            if (http.readyState == 4 && http.status == 200) {
                var textout = http.responseText;
                $('#elems').html(textout);
                history.pushState(null, document.title, myurl);
            }
            return false;
        };
        http.send(null);

        
    } else {
        $('.order').unbind('click');
        http.open("GET", "/fuc/pagination", false);
        http.onreadystatechange = function() {
            if (http.readyState == 4 && http.status == 200) {
                var textout = http.responseText;
                $('#pager').html(textout);
            }
            return false;
        };
        
        http.send(null);
       
        $('#paging').html("Pagination Off");
        href = $('#paging').attr("href");
        href = href.replace("pagination=true", "pagination=False");
        href = href.replace("pagination=True", "pagination=False");
    }

    $('#paging').attr("href", href);
    return false;

}

function refreshelems(myurl, page) {

    if (page < 1 || page > parseInt($('#totalp').text())) return false;
    myurl = myurl.substring(0, myurl.indexOf("?", myurl));
    var itemsPerPage = $('#DisplayNum').val();

    var http = new XMLHttpRequest();
    http.open("GET", myurl + "?page=" + page + "&itemsnumber=" + itemsPerPage + "&partial=true", false);
    http.onreadystatechange = function() {
        if (http.readyState == 4 && http.status == 200) {
            var textout = http.responseText;
            //console.log(textout);
            $('#elems').html(textout);
            ($('#pageinput').val(page));

            $('#next').attr("href", replacehref(myurl, page + 1, itemsPerPage));
            $('#prev').attr("href", replacehref(myurl, page - 1, itemsPerPage));

            history.pushState(null, document.title, '?page=' + page + '&itemsnumber=' + itemsPerPage);

        }
        accord();
    };
    http.send(null);
    return false;

}
function replacehref(myurl, page, number) {
    var f = myurl + "?page=id&itemsnumber=id2";
    f = f.replace("id", page);
    f = f.replace("id2", number);
    return f;
}