
    var c = document.body.className;
    c = c.replace(/woocommerce-no-js/, 'woocommerce-js');
    document.body.className = c;
    $('a[data-open="#main-menu"]').click(function (e) {
        e.preventDefault()
            $('.mfp-wrap').css('display', 'block')
        })
$('.mfp-close').click(function () { $('.mfp-wrap').css('display', 'none') })

$('a[data-open="#cart-popup"]').click(function (e) {
    e.preventDefault()
    $('.mfp-wrap1').css('display', 'block')
})
$('.mfp-close1').click(function () { $('.mfp-wrap1').css('display', 'none') })