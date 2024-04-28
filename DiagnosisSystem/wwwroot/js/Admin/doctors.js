    document.addEventListener('DOMContentLoaded', function () {
        var sections = document.querySelectorAll('.section');
    var subSections = document.querySelectorAll('.sub-sections');

    sections.forEach(function (section) {
        section.addEventListener('click', function () {
            var subSectionsList = this.nextElementSibling;
            subSectionsList.style.display = subSectionsList.style.display === 'none' ? 'block' : 'none';
        });
        });

    subSections.forEach(function (subSections) {
        subSections.addEventListener('click', function () {
            var subSubSectionsList = this.querySelector('.sub-sub-sections');
            subSubSectionsList.style.display = subSubSectionsList.style.display === 'none' ? 'block' : 'none';
        });
        });
    });




    // Function to show the popup dialog
        function showPopupDialog(data) {
            // Append the returned partial view to the body
            $('body').append(data);
        // Show the popup dialog
        $('#popupDialog').show();
        // Automatically close the popup after 3 seconds (adjust duration as needed)
        setTimeout(function() {
            $('#popupDialog').hide();
        }, 3000); // 3000 milliseconds = 3 seconds
    }

        // Function to handle the approval action
        function approveUser(userId) {
            $.get('@Url.Action("Approve", "YourController")?userId=' + userId, function (data) {
                showPopupDialog(data);
            });
    }

        // Function to handle the rejection action
        function rejectUser(userId) {
            $.get('@Url.Action("Reject", "YourController")?userId=' + userId, function (data) {
                showPopupDialog(data);
            });
    }





