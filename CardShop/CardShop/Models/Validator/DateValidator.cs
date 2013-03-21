using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardShop.Models.Validator
{
    public class DateValidator : ValidationAttribute, IClientValidatable
    {

        private const string jQueryValidatorName = "datevalidator";
        private const string defaultErrorMessage = "Date of {1} must be greater than date of {0}.";
        private readonly string startDate;
        private readonly string errorMessage;
        public DateValidator(string startDate)
        {
            this.startDate = startDate;
            this.errorMessage = null;
        }
        public DateValidator(string startDate, string errorMessage) {
            this.errorMessage = errorMessage;
            this.startDate = startDate;
        }

        protected override ValidationResult IsValid(object endDate, ValidationContext ctx) {

            
            var startDateValue = ctx.ObjectInstance.GetType().GetProperty(startDate).GetValue(ctx.ObjectInstance, null);
            ValidationResult result;
            if (endDate!= null && startDateValue != null && ((DateTime)startDateValue) < ((DateTime)endDate))
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
        private string GetErrorMessage(string endDate) {
            return errorMessage != null ? errorMessage : string.
                    Format(defaultErrorMessage, startDate, endDate);
        }
    }
}