function handleUserBoxClick(element) {
    var userBoxes = document.querySelectorAll('.user-box');
    userBoxes.forEach(function (box) {
        box.classList.remove("clicked");
    });

    element.classList.add("clicked");

    // Check if any user box is clicked to toggle the "active" class on the Next button
    var nextButton = document.getElementById('nextButton');
    nextButton.classList.toggle("active", Array.from(userBoxes).some(box => box.classList.contains('clicked')));
}
function handleNextButtonClick() {
    var activeUserBox = document.querySelector('.user-box.clicked');

    // Check if the "Next" button is active and a user box is selected
    if (activeUserBox) {
        // Navigate to the corresponding route based on the selected option
        var userOption = activeUserBox.innerText.toLowerCase();
        switch (userOption) {
            case 'patient':
                // Redirect to the patient registration route
                window.location.href = "/Account/Register";
                break;
            case 'Medical Practitioner':
                // Redirect to the patient registration route
                window.location.href = "/Account/doctorRegister";
                break;

        }
    }
}