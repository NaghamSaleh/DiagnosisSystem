$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: $(this).serialize(),
            dataType: 'json', // Expect JSON response
            success: function (data) {
                if (data.success) {
                    if ($(e.target).hasClass("approve-form")) {
                        $('#ApproveModal').modal('show');
                    } else if ($(e.target).hasClass("reject-form")) {
                        $('#RejectModal').modal('show');
                    }
                    location.reload();

                }
            },
            error: function () {
                alert('An error occurred while processing your request.');
            }
        });
    });
});
