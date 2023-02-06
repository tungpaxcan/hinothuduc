product_cat()
function product_cat() {
    $.ajax({
        url: '/cateproducts/CateProduct',
        type: 'get',
        success: function (data) {
            $('#menu-item').empty();
            $('#menu-mobile').empty();
            if (data.code == 200) {
                let b = '<a href="/" class="nav-top-link">TRANG CHỦ</a>'
                let c ='<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/">TRANG CHỦ</a></li>'
                $.each(data.a, function (k, v) {
                    c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/danh-muc/' + v.meta + '/' + v.id + '" >' + v.name + '</a><li>'
                })
                $.each(data.c, function (k, v) {
                    b += '<a class="dropdown nav-top-link">' + v.name + '<div class="dd"><ul id="' + v.id + '"></ul></div> </a>'
                    IDCATECAR(v.id)
                })
                b += '<a href="/dich-vu/" class="nav-top-link">PHỤ TÙNG VÀ PHỤ KIỆN</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/dich-vu/" class="nav-top-link">PHỤ TÙNG VÀ PHỤ KIỆN</a></li>'
                b += '<a href="/dich-vu-cuu-ho/" class="nav-top-link">DỊCH VỤ CỨU HỘ</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/dich-vu-cuu-ho/" class="nav-top-link">DỊCH VỤ CỨU HỘ</a></li>'

                b += '<a href="/ho-tro-tra-gop/" class="nav-top-link">HỖ TRỢ TRẢ GÓP</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/ho-tro-tra-gop/" class="nav-top-link">HỖ TRỢ TRẢ GÓP</a></li>'
                b += '<a href="/tin-tuc/" class="nav-top-link">TIN TỨC</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/tin-tuc/" class="nav-top-link">TIN TỨC</a></li>'
                b += '<a href="/lien-he/" class="nav-top-link">LIÊN HỆ</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/lien-he/" class="nav-top-link">LIÊN HỆ</a></li>'
                
                $('#menu-item').append(b);
                $('#menu-mobile').append(c);
        
            }
        }
    })
}

function IDCATECAR(id) {
    $.ajax({
        url: '/cateproducts/IDCATECAR',
        type: 'get',
        data: {
            id
        },
        success: function (data) {
            $('#' + id + '').empty();
            if (data.code == 200) {
                $.each(data.a, function (k, v) {
                    let li = '<li onclick="Href('+v.id + ')" >' + v.name + '</li>'
                    $('#' + id + '').append(li);
                })
            }
        }
    })
}

function Href(id) {
    $.ajax({
        url: '/cateproducts/Href',
        type: 'get',
        data: {
            id
        },
        success: function (data) {
            if (data.code == 200) {
                $.each(data.a, function (k, v) {
                    window.location.href = "/danh-muc/" + v.meta + "/" + v.id
                })
            }
        }
    })
}
function Seach() {
    var seach = $('input[name="s"]').val().trim()
    $.ajax({
        url: '/products/Seachs',
        type: 'get',
        data: {
            seach
        },
        success: function (data) {
            if (data.code == 200) {
                window.location.href = "/tim-kiem/" + seach
            }
        }
    })
}

$('#seachlout').click(function () {
    var seach = $('input[name="s"]').val().trim()
    console.log(seach)
    Seachs(seach)
})
$('#seachlout1').click(function () {
    var seach = $('input[name="sss"]').val().trim()
    Seachs(seach)
})
function Seachs(seach) {
    $.ajax({
        url: '/products/Seachs',
        type: 'get',
        data: {
            seach
        },
        success: function (data) {
            if (data.code == 200) {
                window.location.href = "/tim-kiem/" + seach
            }
        }
    })
}
$('li[name="location"]').click(function () {
    $('.modal').modal('show')
})
