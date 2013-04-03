$(document).ready(function () {

    $('#button').click(function () {
        if ($('#form').find('#file').val() == '') {
            $('#error').html('Please select a file.');
            return false;
        }

        var files = document.getElementById('file');
        var file = files.files[0];
        var fd = new FormData();
        fd.append("file", file);

        $.ajax({
            url: 'Upload',
            type: 'POST',
            data: fd,
            dataType: 'text',
            processData: false,
            contentType: false,
            success: function (data, status, xhr) {
                if (data == '') {
                    $('#error').html('Incorrectly formatted file.');
                } else {
                    $('#error').html('');

                    var rules = JSON.parse(data);
                    var count = rules.length;

                    $('#savedRules').html('<h4>Successfully uploaded ' + count + ' new rulesets.</h4>');
                    for (var i = 0; i < data.length; i++) {
                        $('#savedRules').append('<div><a href="Details/' + rules[i].RuleSetId + '">' + rules[i].Name + '</a></div>');
                    }
                }
            },
            error: function (xhr, status, error) {
                $('#error').html(error);
            }
        })
    });
});