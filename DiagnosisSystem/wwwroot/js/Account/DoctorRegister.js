$(document).ready(function () {
    var homeUrl = $('#homeUrl').data('url'); 

    $('form').submit(function (e) {
        e.preventDefault(); 
        $('#pleaseWaitModal').modal('show');
        $('#continueButton').click(function () {
            window.location.href = "/Account/Login"; 
            window.reload();

        });
        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: $(this).serialize(),
            error: function () {
                alert('An error occurred while processing your request.');
            },
            complete: function () {
                $('#pleaseWaitModal').modal('hide');
            }
        });
    });
});
