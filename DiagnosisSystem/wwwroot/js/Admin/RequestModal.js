$(document).ready(function () {
    // Handle click event for approve link
    $('.approve-form').click(function (e) {
        e.preventDefault();

        var url = $(this).attr('href');
        $.ajax({
            url: url,
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    $('#ApproveModal').modal('show');
                }
            },
            error: function () {
                alert('An error occurred while processing your request.');
            }
        });
    });

    // Handle click event for reject link
    $('.reject-form').click(function (e) {
        e.preventDefault();

        var url = $(this).attr('href');
        $.ajax({
            url: url,
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    $('#RejectModal').modal('show');
                }
            },
            error: function () {
                alert('An error occurred while processing your request.');
            }
        });
    });

    // Redirect to Requests view when the Ok button is clicked in the modal
    $('#ApproveModal #continueButton, #RejectModal #continueButton').click(function () {
        window.location.href = '/Admin/Requests';
    });
});
