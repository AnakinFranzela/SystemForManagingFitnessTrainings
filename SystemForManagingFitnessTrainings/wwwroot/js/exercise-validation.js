$(document).ready(function () {
    $('#submit-btn').click(function (e) {
        var isChecked = $('#checkboxes-group input[type="checkbox"]:checked').length > 0;

        if (!isChecked) {
            e.preventDefault();
            $('#error-msg').show();
        } else {
            $('#error-msg').hide();
        }
    });
});