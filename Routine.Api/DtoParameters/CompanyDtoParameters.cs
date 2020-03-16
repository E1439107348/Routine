using System;

namespace Routine.Api.DtoParameters
{
    /// <summary>
    /// 搜索条件 
    /// </summary>
    public class CompanyDtoParameters
    {
       
        public string CompanyName { get; set; }
        public string SearchTerm { get; set; }
    }
}
