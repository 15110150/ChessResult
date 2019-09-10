$(function () {
    $('#pageSubmenu').on('click', 'li a', function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $('#content-data').html(data);
        });
    });
});

$(function () {
    $('#staring-rank').click(function () {
        var url = $(this).data('request-url');
        $.get(url, function (data) {
            $('#content-data').html(data);
        });
    });
});