 $(document).ready(function () {
 //criar os elementos de paginaçao em vez de ja estarem por omisao
     
        var totalp = Math.ceil(parseInt($('#totalelems').val()) / $('#DisplayNum').val());
        $('#DisplayNum').val(1);
        $('#pageinput').val(1);
        $('#totalp').text(totalp);
        //events:
        $('#pageinput').keyup(function () {

            refreshelems(window.location.href, parseInt($('#pageinput').val()));
            return false;
        });

        $('#DisplayNum').change(function () {
            var totalpages = 1;
            if (!($('#DisplayNum').val() == "All")) {

                totalpages = Math.ceil(parseInt($('#totalelems').val()) / $('#DisplayNum').val());
            }
            //else desativar paginaçao: fazer mostrar todos em x de fazer itemsnumber=All(erro)
            $('#totalp').text(totalpages);
            refreshelems(window.location.href, parseInt($('#pageinput').val()));
            return false;
        });

        $('#prev').click(function (e) {

            e.preventDefault();
            var page = parseInt($('#pageinput').val());
            refreshelems($(this).attr("href"), page - 1);
            return false;
        });
        $('#next').click(function (e) {
            e.preventDefault();
            var page = parseInt($('#pageinput').val());
            refreshelems($(this).attr("href"), page + 1);
            return false;
        });

       // refreshelems(window.location.href, 1);
    });

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
