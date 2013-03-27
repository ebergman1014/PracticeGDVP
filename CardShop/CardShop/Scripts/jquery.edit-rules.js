$(document).ready(function () {
    $('.addThen').live('click', function () {
        var newTextBoxDiv = $(document.createElement('div')).attr('class', 'thenAction');
        newTextBoxDiv.html('Field: <input type="text" class="field" /><br />Value: <input type="text" class="value" /><br />' +
            '<input type="button" value="Remove" class="remove" /></div>');
        $(this).closest('input').before(newTextBoxDiv);
    });

    $('.addElse').live('click', function () {
        var newTextBoxDiv = $(document.createElement('div')).attr('class', 'elseAction');
        newTextBoxDiv.html('Field: <input type="text" class="field" /><br />Value: <input type="text" class="value" /><br />' +
            '<input type="button" value="Remove" class="remove" /></div>');
        $(this).closest('input').before(newTextBoxDiv);
    });

    $('.remove').live('click', function () {
        $(this).closest('div').remove();
    });

    $('#newRule').click(function () {
        var newRuleDiv = $(document.createElement('div')).attr('class', 'rule');
        newRuleDiv.html('Name: <input type="text" class="name" />');
        newRuleDiv.append('<p>If Condition</p><div class="ifCondition">Field: <input type="text" class="field" /><br />' +
            'Value: <input type="text" class="value" /></div>');
        newRuleDiv.append('<p>Then Actions</p><div class="thenActions"><div class="thenAction">Field: <input type="text" class="field" /><br />' +
            'Value: <input type="text" class="value" /><br /><input type="button" value="Remove" class="remove" /></div>' +
            '<input type="button" value="Add Action" class="addThen"></div>');
        newRuleDiv.append('<p>Else Actions</p><div class="elseActions"><div class="elseAction">Field: <input type="text" class="field" /><br />' +
            'Value: <input type="text" class="value" /><br /><input type="button" value="Remove" class="remove" /></div>' +
            '<input type="button" value="Add Action" class="addElse"></div>');
        $(this).closest('input').before(newRuleDiv);
    });

    var rules = [];

    $('#form').submit(function () {
        $('.rule').each(function () {
            var name = "";
            var ifCondition = new Object();
            var thenActionData = [];
            var elseActionData = [];

            name = $(this).children('.name').val();

            var ifConditionDiv = $(this).children('.ifCondition');
            ifCondition.field = ifConditionDiv.children('.field').val();
            ifCondition.value = ifConditionDiv.children('.value').val();

            $(this).find('.thenAction').each(function () {
                var thenAction = new Object();
                $(this).find('.field').each(function () {
                    thenAction.field = $(this).val();
                });
                $(this).find('.value').each(function () {
                    thenAction.value = $(this).val();
                });
                if (thenAction.field && thenAction.value) {
                    thenActionData.push(thenAction);
                }
            });

            $(this).find('.elseAction').each(function() {
                var elseAction = new Object();
                $(this).find('.field').each(function () {
                    elseAction.field = $(this).val();
                });
                $(this).find('.value').each(function () {
                    elseAction.value = $(this).val();
                });
                if (elseAction.field && elseAction.value) {
                    elseActionData.push(elseAction);
                }
            });

            var rule = new Object();
            rule.Name = name;
            rule.Condition = ifCondition;
            rule.ThenActions = thenActionData;
            rule.ElseActions = elseActionData;

            if (rule.Name && rule.Condition && rule.ThenActions.length !== 0) {
                rules.push(rule);
            }
        });

        if (rules.length === 0) {
            return false;
        } else {
            $('#rulesetWrapper_RuleSet1').val(JSON.stringify(rules));
        }        
    });
});