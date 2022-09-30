﻿var pagenum = $("#pagenum option:selected").val();
var page = 1;
var seach = "";
Cate(pagenum, page, seach);

//phan trang
$('#page').on('click', 'li', function (e) {
    e.preventDefault();
    page = $(this).attr('id');
    Cate(pagenum, page, seach);


});


function Cate(pagenum, page, seach) {
    var Stt = 1;
    $.ajax({
        url: '/cate/List',
        type: 'get',
        data: { pagenum, page, seach },
        success: function (data) {
            $('#tbd').empty();
            $('#kt_datatable_info').empty();
            if (data.code == 200) {
                $.each(data.c, function (k, v) {
                    let table = '<tr id="' + v.id + '" role="row" class="odd">';
                    table += '<td>' + (Stt++) + '</td>'
                    table += '<td><a href="/Hino/Product/Index/' + v.id + '">' + v.name + ' <img src="/Images/icons8-open-file-folder-18.png" /></a></td>'
                    table += '<td class="action" nowrap="nowrap">';
                    table += '<div class="dropdown dropdown-inline">';
                    table += '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">';
                    table += '<i class="la la-cog"></i></a>';
                    table += '<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">';
                    table += '<ul class="nav nav-hoverable flex-column">';
                    table += ' <li class="nav-item">';
                    table += '<a class="nav-link" href="/Hino/Cate/Edits/' + v.id + '">';
                    table += '<i class="nav-icon la la-edit"></i>';
                    table += '<span class="nav-text">Edit </span></a></li>';
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
    Cate(pagenum, page, seach)
})


//------------------------tim kiem------------------

$('#seach').on('keyup', function (e) {
    page = 1;
    seach = $('#seach').val();
    Cate(pagenum, page, seach);
});

//----------------Add::Unit---------------------
function Add() {
    var name = $('#name').val().trim();
    var title = $('#title').val().trim();
    var meta = $('#meta').val().trim();

    if (name.length <= 0) {
        alert("Nhập Tên !!!")
        return;
    }
    $.ajax({
        url: '/cate/Add',
        type: 'post',
        data: {
            name,title,meta
        },
        success: function (data) {
            if (data.code == 200) {
                Swal.fire({
                    title: "Tạo Loại Sản Phẩm Thành Công",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Confirm me!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                });
                window.location.href = "/Hino/Cate/Index";
            } else if (data.code == 300) {
                alert(data.msg)
            }
            else {
                alert("Tạo Loại Sản Phẩm Thất Bại")
            }
        }
    })
}

//----------------Edit::Unit---------------------
function Edit() {
    var id = $('#id').val().trim();
    var name = $('#name').val().trim();
    var title = $('#title').val().trim();
    var meta = $('#meta').val().trim();
    if (name.length <= 0) {
        alert("Nhập Tên !!!")
        return;
    } 
    $.ajax({
        url: '/cate/Edit',
        type: 'post',
        data: {
            id, name, title,meta
        },
        success: function (data) {
            if (data.code == 200) {
                Swal.fire({
                    title: "Tạo Loại Sản Phẩm Thành Công",
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Confirm me!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                });
                window.location.href = "/Hino/Cate/Index";
            } else if (data.code == 300) {
                alert(data.msg)
            }
            else {
                alert("Sửa Loại Sản Phẩm Thất Bại")
            }
        }
    })
}

//----------------Delete::Unit---------------------
$(document).on('click', "a[name='delete']", function () {
    var id = $(this).closest('tr').attr('id');
    if (confirm("Bạn Muốn Xóa Dữ Liệu Này ???")) {
        $.ajax({
            url: '/cate/Delete',
            type: 'post',
            data: {
                id
            },
            success: function (data) {
                if (data.code == 200) {
                    Swal.fire({
                        title: "Xóa Loại Sản Phẩm Thành Công",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Confirm me!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    });
                    window.location.href = "/Hino/Cate/Index";
                }
                else {
                    alert(data.msg)
                }
            }
        })
    }
})


//cat chuoi
$('#name').keyup(function () {
    $('#title').val($(this).val());
    $('#meta').val(GetChuoi($(this).val()));
});
