<script>
    document.getElementById('loginForm').addEventListener('submit', function(event) {
        var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    var emailError = document.getElementById('emailError');
    var passwordError = document.getElementById('passwordError');

    // Reset error messages
    emailError.classList.add('d-none');
    passwordError.classList.add('d-none');

    // Validate email
    if (email.trim() === '') {
        emailError.classList.remove('d-none');
    event.preventDefault(); // Prevent form submission
        }

    // Validate password
    if (password.trim() === '') {
        passwordError.classList.remove('d-none');
    event.preventDefault(); // Prevent form submission
        }
    });
</script>