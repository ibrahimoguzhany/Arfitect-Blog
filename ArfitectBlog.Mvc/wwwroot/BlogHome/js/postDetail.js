$(document).ready(function () {
    $(function () {
        $(document).on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-comment-add');
                const action = form.attr('action');
                const dataToSend = form.serialize();
                $.post(action, dataToSend).done(function (data) {
                    const commentAddAjaxModel = jQuery.parseJSON(data);
                    console.log(commentAddAjaxModel);
                    const newFormBody = $('.form-card', commentAddAjaxModel.CommentAddPartial);
                    const cardBody = $('.form-card');
                    cardBody.replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        const newSingleComment = `
                            <div class="media mb-4">
                                <img class="d-flex mr-3 rounded-circle" src="https://randomuser.me/api/portraits/men/34.jpg" alt="">
                                 <div class="media-body">
                                        <h5 class="mt-0">${commentAddAjaxModel.CommentDto.Comment.CreatedByName}</h5>
                                        ${commentAddAjaxModel.CommentDto.Comment.Text}
                                </div>
                             </div>`;
                        const newSingleCommentObject = $(newSingleComment);
                        newSingleCommentObject.hide();
                        $('#comments').append(newSingleCommentObject);
                        newSingleCommentObject.fadeIn(2000);
                        toastr.success(
                            `${commentAddAjaxModel.CommentDto.Comment.CreatedByName
                            } yorumunuz başarıyla eklenmiştir. Bir örneği karşınıza gelecek. Fakat onaylandıktan sonra görünür olacaktır.`);
                        $('#btnSave').prop('disabled', true);
                        setTimeout(function() {
                                $('#btnSave').prop('disabled', false);
                            },
                            15000);
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText += `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                }).fail(function(err) {
                    console.error(err);
                });
            });
    });
});