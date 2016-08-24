function timer(seconds, url) {

    $('#timer').text(seconds);

    $('#timer').show();

    var interval = setInterval(function () {

        --seconds;

        $('#timer').text(seconds);

        if (seconds == 0) {

            clearInterval(interval);
            window.location = url;
            $('timer').hide();

        }

    }, 1000);

};
