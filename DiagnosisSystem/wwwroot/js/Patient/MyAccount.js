<script>
    $(document).ready(function () {
        $(".nav-option").click(function () {
            var sectionId = $(this).attr("id") + "-container";
            var scrollToElement = $("#" + sectionId);

            // Smooth scroll to the target element
            $('html, body').animate({
                scrollTop: scrollToElement.offset().top
            }, 1000);

            // Update selected state and selector position
            $(".nav-option").removeClass("selected");
            $(this).addClass("selected");

            var index = $(this).index();
            var widthSum = 0;

            // Calculate the total width of all options before the clicked one
            for (var i = 0; i < index; i++) {
                widthSum += $(".nav-option").eq(i).outerWidth(true);
            }

            // Calculate the left position for the selector
            var leftPosition = widthSum + ($(this).outerWidth(true) - $(".selector").outerWidth()) / 2;
            $(".selector").css("left", leftPosition + "px");
        });

    // Initial positioning of selector
    var initialPosition = $(".nav-option.selected").position().left;
    $(".selector").css("left", initialPosition + "px");
    });
</script>
