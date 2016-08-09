var activeLink;

$(document).ready(function () {
    var $listItems = $('.pagination li');

    $listItems.click(function () {
        $listItems.removeClass('active');
        $(this).addClass('active');
        var activeLink = $(this);////  store in variable and do work on pev next btn
    });
});