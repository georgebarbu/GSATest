using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercise.Web.Controllers
{
    public class CumulativePnlResultRequest : IValidatableObject
    {
        public DateTime StartDate { get; set; }
        public string Region { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Region))
                yield return new ValidationResult("Region is invalid", new[] {"Region"});

            if (StartDate == DateTime.MinValue)
                yield return new ValidationResult("StartDate is invalid", new[] {"Region"});
        }
    }
}