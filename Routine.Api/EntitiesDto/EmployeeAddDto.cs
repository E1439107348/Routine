using Routine.Api.Entities;
using Routine.Api.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.EntitiesDto
{
    [EmployeeNoMust]
    public class EmployeeAddDto:IValidatableObject
    {

        [Display(Name = "id")]
        [Required(ErrorMessage = "{0}是必须的")]
        [MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string EmployeeNo { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能一样", new[] { nameof(FirstName), 
                    nameof(LastName) });

            }
        }
    }
}
