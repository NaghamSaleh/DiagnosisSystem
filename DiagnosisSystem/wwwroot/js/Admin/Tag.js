$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault(); // Prevent the form from submitting normally

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                if (data.success) {
                    // Wait for 1 second before showing the success message
                    setTimeout(function () {
                        $('#successMessage').fadeIn('slow').delay(2000).fadeOut('slow');
                    });

                    $('form')[0].reset();
                } else {
                    // Show the error message
                    alert('Failed to save: ' + data.message);
                }
            },
            error: function () {
                // Show a generic error message
                alert('An error occurred while saving.');
            }
        });
    });
});