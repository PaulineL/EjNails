/*!
* Start Bootstrap - Landing Page v6.0.4 (https://startbootstrap.com/theme/landing-page)
* Copyright 2013-2021 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-landing-page/blob/master/LICENSE)
*/
// This file is intentionally blank
// Use this file to add JavaScript to your project
$(window).scroll(function () {
    var scrolledFromTop = $(window).scrollTop() + $(window).height();
    $(".appear").each(function () {
        var distanceFromTop = $(this).offset().top;
        if (scrolledFromTop >= distanceFromTop + 100) {
            console.log("hello");
            var delaiAnim = $(this).data("delai");
            $(this).delay(delaiAnim).animate({
                top: 0,
                opacity: 1
            });
        }
    });
});