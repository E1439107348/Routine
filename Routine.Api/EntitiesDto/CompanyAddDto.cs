using System.Collections.Generic;

namespace Routine.Api.EntitiesDto
{
    public class CompanyAddDto
    {
        public string Name { get; set; }
        public string Introduction { get; set; }

        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();//防止空指针异常

    }
}
