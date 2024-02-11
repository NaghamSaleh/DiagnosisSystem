<script>
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
</script>