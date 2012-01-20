
$(document).ready(function () {
    $('#totalp').text(Math.ceil(parseInt($('#totalelems').val()) / $('#DisplayNum').val()));
    
  
    $('#paging').click(function (e) {
        checkPaging(this, e);
    });
    events();
});



function events() {


    if ($('#paging').text() == "Pagination On") {
        $('.order').click(function () { orderby($(this)); return false; });
    }
   
    
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
        
        return false;
    });
    $('#next').click(function (e) {
        e.preventDefault();
        var page = parseInt($('#pageinput').val());
        refreshelems($(this).attr("href"), page + 1);
        
        return false;
    });
}



  