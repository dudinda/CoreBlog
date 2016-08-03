$(documnet).ready(function () {
    var maxTags = 5; //maximum input boxes allowed
    var $wrapper = $(".wrapper"); //Fields wrapper
    var $addButton = $("#add_field_button"); //Add button ID
    var $Button = $(".addButton");

    var tagCounter = 1; //initlal text box count
    $(Button).on("click", function (e) { //on add input button click
        if (tagCounter < maxTags) { //max input box allowed
            ++tagCounter; //text box increment
            $(wrapper).append('<div><input type="text" name="mytext[]"/><a href="#" class="remove_field">Remove</a></div>'); //add input box
        }
    });
});