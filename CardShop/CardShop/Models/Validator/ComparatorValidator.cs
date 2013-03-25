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
 
        // This is the calling "method" for jQuery validator.
        private const string jQueryValidatorName = "comparator";
        private const string defaultErrorMessage = "Value of {1} must be greater than value of {0}.";
        // The property you want to compare
        private readonly string otherProperty;
        // If errorMessage is custom,  created in one of the constructors
        private readonly string errorMessage;
        private readonly Operator op;
        /// <summary>
        /// Will compare propery you signify in the annotation.
        /// By default, the operator is equal to.
        /// You are compoaring against the field. so greater than is 
        /// field > otherProperty
        /// </summary>
        /// <param name="otherProperty"> the property you want to compare against</param>
        /// <param name="op"> the operator enum you want to call in</param>
        public Comparator(string otherProperty,Operator op=Operator.EqualTo)
        {
            this.otherProperty = otherProperty;
            this.errorMessage = null;
            this.op = op;
        }
        /// <summary>
        /// Will compare propery you signify in the annotation.
        /// By default, the operator is equal to.
        /// You are compoaring against the field. so greater than is 
        /// field > otherProperty
        /// </summary>
        /// <param name="otherProperty"> the property you want to compare against</param>
        /// <param name="errorMessage"> Custom message you want to display.</param>
        /// <param name="op"> the operator enum you want to call in</param>
        public Comparator(string otherProperty, string errorMessage, Operator op = Operator.EqualTo)
        {
            this.errorMessage = errorMessage;
            this.otherProperty = otherProperty;
            this.op = op;
        }

        protected override ValidationResult IsValid(object value, ValidationContext ctx) {

            // get the otherProperty value.  
            var otherPropertyValue = ctx.ObjectInstance.GetType().GetProperty(otherProperty).GetValue(ctx.ObjectInstance, null);
            ValidationResult result;
            // check for not null values, and then compare the properties. The (int) cast is required by
            // Microsoft enum logic.
            if (value!= null && otherPropertyValue != null && ((IComparable)value).CompareTo((IComparable)otherPropertyValue) == (int)this.op)
            {
                result = ValidationResult.Success;
            }else{
                // call ErrorMessage generator
                result = new ValidationResult(GetErrorMessage(ctx.DisplayName));
            }

             return result;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            // yields states object is an iterator (if another
            // yield, would do second yield
            yield return new ModelClientValidationRule
            {

                ErrorMessage = (GetErrorMessage(metadata.DisplayName)),
                ValidationType = jQueryValidatorName
            };
        }
        private string GetErrorMessage(string propertyName) {
            // return either default error message or custom message
            // might want to string.Format() custom error message
            return errorMessage != null ? errorMessage : string.
                    Format(defaultErrorMessage, otherProperty, propertyName);
        }
    }
}