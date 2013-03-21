$(document).ready(function () {
    $('.add').click(function () {
        var newTextBoxDiv = $(document.createElement('div'));
        newTextBoxDiv.html('<input type="text"/><input type="button" value="Remove" class="remove"/>');
        $(this).closest('input').before(newTextBoxDiv);
    });

    $('.remove').live('click', function () {
        $(this).closest('div').remove();
    });

    var rules = [];

    $('#form').submit(function () {
        $('.rule').each(function () {
            var ifAction = '';
            var thenActionData = [];
            var elseActionData = [];

            ifAction = $(this).children('.ifCondition').val();

            $(this).children('.thenActions').each(function () {
                $(this).children('div').each(function () {
                    $(this).children('input[type=text]').each(function () {
                        if ($(this).val() != "") {
                            thenActionData.push($(this).val());
                        }
                    });
                });
            });

            $(this).children('.elseActions').each(function () {
                $(this).children('div').each(function () {
                    $(this).children('input[type=text]').each(function () {
                        if ($(this).val() != "") {
                            elseActionData.push($(this).val());
                        }
                    });
                });
            });

            var rule = new Object();
            rule.Condition = ifAction;
            rule.ThenActions = thenActionData;
            rule.ElseActions = elseActionData;

            rules.push(rule);
        });

        $('#rulesObject').val(JSON.stringify(rules));
    });
});