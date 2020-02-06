using Microsoft.AspNetCore.Mvc;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{

    /// <summary>
    ///  [ApiController]需要在startup.cs中添加属性路由
    /// </summary>
    [ApiController]
    [Route("api/companies")] //或者  [Route("api/controller")]
    public class CompaniesController : ControllerBase
    {

        public readonly ICompanyRepository _companyRepository;
        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ??
                throw new ArgumentNullException(nameof(companyRepository));
        }


        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            //404 NoFound();
            return Ok(companies);
        }


        [HttpGet("{companyId}")]//api/Companies/companyId=>api/Companies/123
        public async Task<IActionResult> GetCompanY(Guid companyId)
        {
            //var exist= await _companyRepository.CompanyExistsAsync(companyId);
            //if (!exist)
            //{
            //    return NotFound();
            //}
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            //404 NoFound();
            return Ok(company);
        }

    }
}
