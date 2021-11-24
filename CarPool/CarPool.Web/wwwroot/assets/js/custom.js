// Page loading animation
$(window).on('load', function () {
    if ($('.cover').length) {
        $('.cover').parallax({
            imageSrc: $('.cover').data('image'),
            zIndex: '1'
        });
    }

    $("#preloader").animate({
        'opacity': '0'
    }, 600, function () {
        setTimeout(function () {
            $("#preloader").css("visibility", "hidden").fadeOut();
        }, 300);
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal .show').remove();
                    $('#form-modal').css('display', 'none');
                    $('.modal-backdrop').remove();
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            }

        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

ExecuteEmptyAjax = (url) => {
    $.ajax({
        type: "POST",
        url: url,
        success: function (res) {
            $('#view-all').html(res.html)
        }
    })
}

$(".form-search").change(function () {
    $(this).attr('action', $('#sel').val());
});

SearchBarAjax = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                }
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
    }
}
