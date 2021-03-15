$(document).ready(function () {

    /* DataTables start here. */

   const dataTable = $('#deletedPostsTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Post/GetAllDeletedPosts/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#deletedPostsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const postResult = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(postResult);
                            if (postResult.Data.ResultStatus === 0) {
                                let categoriesArray = [];
                                $.each(postResult.Data.Posts.$values,
                                    function (index, post) {
                                        const newPost = getJsonNetObject(post, postResult.Data.Posts.$values);
                                        let newCategory = getJsonNetObject(newPost.Category, newPost);
                                        if (newCategory !== null) {
                                            categoriesArray.push(newCategory);
                                        }
                                        if (newCategory === null) {
                                            newCategory = categoriesArray.find((category) => {
                                                return category.$id === newPost.Category.$ref;
                                            });
                                        }
                                        const newTableRow = dataTable.row.add([
                                            newPost.Id,
                                            newCategory.Name,
                                            newPost.Title,
                                            `<img src="/img/${newPost.Thumbnail}" alt="${newPost.Title}" class="my-image-table" />`,
                                            `${convertToShortDate(newPost.Date)}`,
                                            newPost.ViewCount,
                                            newPost.CommentCount,
                                            `${newPost.IsActive ? "Evet" : "Hayır"}`,
                                            `${newPost.IsDeleted ? "Evet" : "Hayır"}`,
                                            `${convertToShortDate(newPost.CreatedDate)}`,
                                            newPost.CreatedByName,
                                            `${convertToShortDate(newPost.ModifiedDate)}`,
                                            newPost.ModifiedByName,
                                            `
                                <button class="btn btn-primary btn-sm btn-undo" data-id="${newPost.Id}"><span class="fas fa-undo"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${newPost.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `${newPost.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#deletedPostsTable').fadeIn(1400);
                            } else {
                                toastr.error(`${postResult.Data.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#deletedPostsTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

    /* DataTables end here */

    /* Ajax POST / HardDeleting a Post  starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            const postTitle = tableRow.find('td:eq(2)').text();
            Swal.fire({
                title: 'Kalıcı olarak silmek istediğinize emin misiniz?',
                text: `${postTitle} başlıklı makale kalıcı olarak silinicektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, kalıcı olarak silmek istiyorum.',
                cancelButtonText: 'Hayır, istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { postId: id },
                        url: '/Admin/Post/HardDelete/',
                        success: function (data) {
                            const postResult = jQuery.parseJSON(data);
                            if (postResult.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `${postResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${postResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!");
                        }
                    });
                }
            });
        });

    /* Ajax POST / HardDeleting a Post ends here */

    /* Ajax POST / UndoDeleting a Post starts from here */

    $(document).on('click',
        '.btn-undo',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            let postTitle = tableRow.find('td:eq(2)').text();
            Swal.fire({
                title: 'Arşivden geri getirmek istediğinize emin misiniz?',
                text: `${postTitle} başlıklı makale arşivden geri getirilecektir!`,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, arşivden geri getirmek istiyorum.',
                cancelButtonText: 'Hayır, istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { postId: id },
                        url: '/Admin/Post/UndoDelete/',
                        success: function (data) {
                            const postUndoDeleteResult = jQuery.parseJSON(data);
                            console.log(postUndoDeleteResult);
                            if (postUndoDeleteResult.ResultStatus === 0) {
                                Swal.fire(
                                    'Arşivden Geri Getirildi!',
                                    `${postUndoDeleteResult.Message}`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${postUndoDeleteResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!");
                        }
                    });
                }
            });
        });
/* Ajax POST / UndoDeleting a Post ends here */

});