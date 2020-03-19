using Routine.Api.EntitiesDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Validation
{
    public class EmployeeNoMust:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = (EmployeeAddDto)validationContext.ObjectInstance;
            if (addDto.EmployeeNo==addDto.FirstName)
            {
                return new ValidationResult("id不能等于名", new[] { nameof(EmployeeAddDto) });
            }
            return ValidationResult.Success;
        }
    }
}
