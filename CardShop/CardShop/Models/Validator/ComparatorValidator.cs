using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardShop.Models.Validator
{
    public class Comparator : ValidationAttribute, IClientValidatable
    {
        public enum Operator
        {
            GreaterThan = 1, LessThan = -1, EqualTo = 0
        }
 
        // for implementation on 
        private const string jQueryValidatorName = "comparator";
        private const string defaultErrorMessage = "Value of {1} must be greater than value of {0}.";
        private readonly string otherProperty;
        private readonly string errorMessage;
        private readonly Operator op;

        public Comparator(string otherProperty,Operator op=Operator.EqualTo)
        {
            this.otherProperty = otherProperty;
            this.errorMessage = null;
            this.op = op;
        }
        public Comparator(string otherProperty, string errorMessage, Operator op = Operator.EqualTo)
        {
            this.errorMessage = errorMessage;
            this.otherProperty = otherProperty;
            this.op = op;
        }

        protected override ValidationResult IsValid(object value, ValidationContext ctx) {

            
            var startDateValue = ctx.ObjectInstance.GetType().GetProperty(otherProperty).GetValue(ctx.ObjectInstance, null);
            ValidationResult result;
            if (value!= null && startDateValue != null && ((IComparable)startDateValue).CompareTo((IComparable)value) == (int)this.op)
            {
                result = ValidationResult.Success;
            }else{
                result = new ValidationResult(GetErrorMessage(ctx.DisplayName));
            }

             return result;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = (GetErrorMessage(metadata.DisplayName)),
                ValidationType = jQueryValidatorName
            };
        }
        private string GetErrorMessage(string propertyName) {
            return errorMessage != null ? errorMessage : string.
                    Format(defaultErrorMessage, otherProperty, propertyName);
        }
    }
}