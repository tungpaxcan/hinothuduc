var pagenum = $("#pagenum option:selected").val();
var page = 1;
var seach = "";
Image(pagenum, page, seach);

//phan trang
$('#page').on('click', 'li', function (e) {
    e.preventDefault();
    page = $(this).attr('id');
    Image(pagenum, page, seach);
});


function Image(pagenum, page, seach) {
    var id = $('#id').val().trim();
    var Stt = 1;
    $.ajax({
        url: '/product/ImageAdd',
        type: 'get',
        data: { pagenum, page, seach, id },
        success: function (data) {
            $('#tbd').empty();
            $('#kt_datatable_info').empty();
            if (data.code == 200) {
                $.each(data.c, function (k, v) {
                    let table = '<tr id="' + v.id + '" role="row" class="odd">';
                    table += '<td>' + (Stt++) + '</td>'
                    table += '<td ><img style="width:100px" src="' + v.image + '" /></td>'
                    table += '<td class="action" nowrap="nowrap">';
                    table += '<div class="dropdown dropdown-inline">';
                    table += '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">';
                    table += '<i class="la la-cog"></i></a>';
                    table += '<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">';
                    table += '<ul class="nav nav-hoverable flex-column">';
                    table += '<li class="nav-item"><a class="nav-link" onclick="printDiv(\'' + v.id + '\')">';
                    table += '<i class="nav-icon la la-print"></i><span class="nav-text">Print</span></a></li>';
                    table += '<li class="nav-item"><a class="nav-link" name="delete">';
                    table += '<i class="nav-icon la la-trash"></i><span class="nav-text">Delete</span></a></li>';
                    table += '</ul></div></div>';
                    table += '</td>';
                    table += '</tr>';
                    $('#tbd').append(table);
                });

                //--------------------------------
                let kt_datatable_info = 'Showing 1 to ' + pagenum + ' of ' + data.count + ' entries'
                $('#kt_datatable_info').append(kt_datatable_info);
                //-----------------------------page---------------------------
                $('#page').empty();
                if (parseInt(page) >= 2) {
                    let pagemin = '<li id="' + 1 + '"><a class="a_1 a_2" >' + 1 + '...</a></li>';
                    $('#page').append(pagemin);
                    let pre = ' <li id="' + (parseInt(page) - 1) + '" class="paginate_button page-item previous disabled" >';
                    pre += '<a  aria-controls="kt_datatable" data-dt-idx="0" tabindex="0" class="page-link">';
                    pre += '<i class="ki ki-arrow-back"></i></a></li>';
                    $('#page').append(pre);
                }
                for (let i = parseInt(page); i <= (parseInt(page) + 4); i++) {
                    if (i == data.pages + 1) {
                        return;
                    }
                    let li = '<li id="' + i + '" class="paginate_button page-item ">';
                    li += '<a aria-controls="kt_datatable" data-dt-idx="1" tabindex="0" class="page-link">' + i + '</a></li>';
                    $('#page').append(li);
                }
                let next = '<li  id="' + (parseInt(page) + 1) + '" class="paginate_button page-item next" id="kt_datatable_next">';
                next += '<a href="#" aria-controls="kt_datatable" data-dt-idx="6" tabindex="0" class="page-link"><i class="ki ki-arrow-next"></i></a></li>';
                $('#page').append(next);

                let pagemax = '<li id="' + data.pages + '"><a class="a_1 a_2" >...' + data.pages + '</a></li>';
                $('#page').append(pagemax);

            } else (
                alert(data.msg)
            )
        }
    })
}

$('#pagenum').on('change', function () {
    var pagenum = $("#pagenum option:selected").val();
    var page = 1;
    var seach = "";
    Image(pagenum, page, seach)
})


//------------------------tim kiem------------------

$('#seach').on('keyup', function (e) {
    page = 1;
    seach = $('#seach').val();
    Image(pagenum, page, seach);
});

//----------------Add::Unit---------------------
function Add() {
    var id = $('#id').val().trim();
    var image = $('#picturefile').val().trim();
    if (image.length <= 0) {
        alert("Chọn Hình Ảnh !!!")
        return;
    }
    $.ajax({
        url: '/product/AddImage',
        type: 'post',
        data: {
            id,image
        },
        success: function (data) {
            if (data.code == 200) {
                Swal.fire({
                    title: "Tạo Hình Ảnh Thành Công",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Confirm me!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                });
                window.location.href = "/Hino/Product/Image/" + id;
            } else if (data.code == 300) {
                alert(data.msg)
            }
            else {
                alert("Tạo Hình Ảnh Thất Bại")
            }
        }
    })
}

//----------------Delete::Unit---------------------
$(document).on('click', "a[name='delete']", function () {
    var id = $(this).closest('tr').attr('id');
    var ids = $('#id').val().trim();
    if (confirm("Bạn Muốn Xóa Dữ Liệu Này ???")) {
        $.ajax({
            url: '/product/DeleteImage',
            type: 'post',
            data: {
                id
            },
            success: function (data) {
                if (data.code == 200) {
                    Swal.fire({
                        title: "Xóa Hình Ảnh Thành Công",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Confirm me!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    });
                    window.location.href = "/Hino/Product/Image/" + ids;
                }
                else {
                    alert(data.msg)
                }
            }
        })
    }
})

//--------------------hien ten hinh--------------------------
$('#btnUpload').click(function () {
    $('#fileUpload').trigger('click');
});
//----------------------chon file-------------------------
$('#fileUpload').change(function () {
    if (window.FormData !== undefined) {
        let fileUpload = $('#fileUpload').get(0);
        let files = fileUpload.files;
        let formdata = new FormData();
        formdata.append('file', files[0]);
        $.ajax({
            type: 'post',
            url: '/hino/product/UploadImage',
            contentType: false,
            processData: false,
            data: formdata,
            success: function (urlImage) {
                $('#picture').attr('src', urlImage);
                $('#picturefile').val(urlImage);
            }
        })
    }

});
