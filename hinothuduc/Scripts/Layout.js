product_cat()
function product_cat() {
    $.ajax({
        url: '/cateproducts/CateProduct',
        type: 'get',
        success: function (data) {
            $('#menu-item').empty();
            $('#menu-mobile').empty();
            if (data.code == 200) {
                let b = '<a href="/" class="nav-top-link">Trang chủ</a>'
                let c ='<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/">Trang chủ</a></li>'
                $.each(data.a, function (k, v) {
                    c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/danh-muc/' + v.meta + '/' + v.id + '" >' + v.name + '</a><li>'
                })
                $.each(data.c, function (k, v) {
                    b += '<a class="dropdown nav-top-link">' + v.name + '<div class="dd"><ul id="' + v.id + '"></ul></div> </a>'
                    IDCATECAR(v.id)
                })
                b += '<a href="/dich-vu/" class="nav-top-link">Dịch Vụ</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/dich-vu/" class="nav-top-link">Dịch Vụ</a></li>'
                b += '<a href="/ho-tro-tra-gop/" class="nav-top-link">Hỗ Trợ Trả Góp</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/ho-tro-tra-gop/" class="nav-top-link">Hỗ Trợ Trả Góp</a></li>'
                b += '<a href="/tin-tuc/" class="nav-top-link">Tin Tức</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/tin-tuc/" class="nav-top-link">Tin Tức</a></li>'
                b += '<a href="/lien-he/" class="nav-top-link">Liên Hệ</a>'
                c += '<li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home menu-item-85" id="menu-mobile"><a href="/lien-he/" class="nav-top-link">Liên Hệ</a></li>'
                
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
function Seachs() {
    var seach = $('input[name="ss"]').val().trim()
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

