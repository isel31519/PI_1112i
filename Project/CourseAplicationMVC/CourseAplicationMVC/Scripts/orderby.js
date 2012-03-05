function orderby(elem) {

    var http = new XMLHttpRequest();
    console.log(elem);
    var type="asc";
    if ($(elem).hasClass('order_asc')) {
        //fazer desc
        $(elem).removeClass('order_asc');
        $(elem).addClass('order_dsc');
        type = "dsc";
    } else {
        if ($(elem).hasClass('order_dsc')) {
            //fazer desc
            $(elem).removeClass('order_dsc');
            $(elem).addClass('order_asc');
        } else {
            $(elem).addClass('order_asc');
        }
    }
    
    var href = window.location.href;
    href = href.substring(0, href.indexOf("?", href));
    href = href + "?orderby=" + elem.text() + "&type=" + type;
    
    http.open("GET", href + "&partial=true", false);
    http.onreadystatechange = function() {
        if (http.readyState == 4 && http.status == 200) {
            var textout = http.responseText;
            //console.log(textout);
            $('#elems').html(textout);
            history.pushState(null, document.title, href);

        }
        accord();
    };
    http.send(null);



}

