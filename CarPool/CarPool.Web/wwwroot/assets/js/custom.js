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
                    if (res.html != "") {
                        $('#view-all').html(res.html)
                    }
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

ajaxId = (url, id) => {
    $.ajax({
        type: "POST",
        url: url,
        success: function (res) {
            $(`#${id}`).html(res.html)
        }
    })
}

function showDetails(duration, distance, driver, passengers, seats, vehicalModel, vehicleColor, price, comment) {
    Swal.fire({
        position: 'center',
        backdrop: false,
        customClass: 'swal-trip',
        title: '<h4 class="swal-title"> Details </h4>',
        showConfirmButton: true,
        html:
            `<div class='row' style='height: 400px;'>` +
            `<div class='col'>` +
            `<h5>Trip duration</h5>` +
            `<h6>${timeConvert(duration)}</h6>` +
            `<h5>Trip Distance</h5>` +
            `<h6>${distance} km</h6>` +
            `<h5>Driver Name</h5>` +
            `<h6>${driver}</h6>` +
            `<h5>Passengers Count</h5>` +
            `<h6>${passengers}</h6>` +
            `</div>` +
            `<div class='col'>` +
            `<h5>Free Seats</h5>` +
            `<h6>${seats}</h6>` +
            `<h5>Vehicle</h5>` +
            `<h6>${vehicalModel} ${vehicleColor}</h6>` +
            `<h5>Trip Price</h5>` +
            `<h6>${price} lev/person</h6>` +
            `<h5>Comment</h5>` +
            `<h6>${comment}</h6>` +
            `</div>` +
            `</div>`
    })
}

function showUserInfo(image, firstName, lastName, username, country, city, phone) {
    Swal.fire({
        position: 'center',
        backdrop: false,
        customClass: 'swal-trip',
        showConfirmButton: true,
        html:
            `<div>` +
                `<div class="rn-team team-style-default">` +
                    `<div class="inner">` +
                        `<div class="thumbnail">` +
                            `<img src="${image}" >` +
                        `</div>` +
                            `<div class="content">` +
                                `<h2 class="title">${firstName} ${lastName}</h2>` +
                                `<h6 class="subtitle theme-gradient">${username}</h6>` +
                                `<span class="team-form">` +
                                    `<i class="feather-map-pin"></i>` +
                                    `<span class="location">${city}, ${country}</span>` +
                                `</span>` +
                                `<span class="team-form">` +
                                    `<i class="fas fa-phone-alt"></i>` +
                                    `<span>${phone}</span>` +
                                `</span>` +
                            `</div>` +
                        `</div>` +
                    `</div>` +
               `</div>`
    })
}
function showPassangerInfo(image, firstName, lastName, username, phone) {
    Swal.fire({
        position: 'center',
        backdrop: false,
        customClass: 'swal-trip',
        showConfirmButton: true,
        html:
            `<div>` +
            `<div class="rn-team team-style-default">` +
            `<div class="inner">` +
            `<div class="thumbnail">` +
            `<img src="${image}" >` +
            `</div>` +
            `<div class="content">` +
            `<h2 class="title">${firstName} ${lastName}</h2>` +
            `<h6 class="subtitle theme-gradient">${username}</h6>` +            
            `<span class="team-form">` +
            `<i class="fas fa-phone-alt"></i>` +
            `<span>${phone}</span>` +
            `</span>` +
            `</div>` +
            `</div>` +
            `</div>` +
            `</div>`
    })
}
function timeConvert(n) {
    var num = n;
    var hours = (num / 60);
    var rhours = Math.floor(hours);
    var minutes = (hours - rhours) * 60;
    var rminutes = Math.round(minutes);
    return rhours + " h " + rminutes + " min";
}