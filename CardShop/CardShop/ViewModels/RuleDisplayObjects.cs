using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Workflow.Activities.Rules;

namespace CardShop.ViewModels
{
    public class RuleObject
    {
        public string Name { get; set; }
        public ConditionObject Condition { get; set; }
        public List<ActionObject> ThenActions { get; set; }
        public List<ActionObject> ElseActions { get; set; }

        public RuleObject(System.Workflow.Activities.Rules.Rule rule)
        {
            this.Name = rule.Name;
            this.Condition = RuleObjectCondition((RuleExpressionCondition)rule.Condition);
            this.ThenActions = SetUpActions(rule.ThenActions);
            this.ElseActions = SetUpActions(rule.ElseActions);
        }

        public RuleObject()
        {
        }

        private ConditionObject RuleObjectCondition(RuleExpressionCondition condition)
        {
            ConditionObject conditionObj = new ConditionObject();

            CodeBinaryOperatorExpression operatorExpression = (CodeBinaryOperatorExpression)condition.Expression;

            CodeFieldReferenceExpression leftExpression = (CodeFieldReferenceExpression)operatorExpression.Left;
            conditionObj.field = leftExpression.FieldName;

            CodePrimitiveExpression rightExpression = (CodePrimitiveExpression)operatorExpression.Right;
            conditionObj.value = rightExpression.Value.ToString();

            return conditionObj;
        }

        private ActionObject RuleObjectAction(RuleStatementAction action)
        {
            ActionObject actionObj = new ActionObject();

            CodeAssignStatement domDataValue = (CodeAssignStatement)action.CodeDomStatement;

            CodeFieldReferenceExpression expressionField = (CodeFieldReferenceExpression)domDataValue.Left;
            actionObj.field = expressionField.FieldName;

            CodePrimitiveExpression expressionValue = (CodePrimitiveExpression)domDataValue.Right;
            actionObj.value = expressionValue.Value.ToString();

            return actionObj;
        }

        private List<ActionObject> SetUpActions(IList<RuleAction> actions)
        {
            List<ActionObject> actionObjects = new List<ActionObject>();
            foreach (RuleStatementAction action in actions)
            {
                actionObjects.Add(RuleObjectAction(action));
            }

            return actionObjects;
        }
    }

    public class ConditionObject
    {
        public string field { get; set; }
        public string value { get; set; }
        public System.CodeDom.CodeBinaryOperatorType operatorType { get; set; }
    }

    public class ActionObject
    {
        public string field { get; set; }
        public string value { get; set; }
    }
}