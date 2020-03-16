using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.DtoParameters;
using Routine.Api.Entities;
using Routine.Api.EntitiesDto;
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
        private readonly IMapper _mapper;//对象映射器
        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ??
                throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        //public async Task<IActionResult> GetCompanies()
        //Task<IActionResult<CompanyDto>>
        //这样写更能明确【返回类区、属性】  

        //程序会自动的把 companyDtoParameters标记为FromBody 因为它是一个类。所以就不能用 ?name=小明 ；如果想用这种方法，
        //就用[FromQuery]标记为请求头 
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies([FromQuery]CompanyDtoParameters companyDtoParameters)
        {
            //Entity Model
            var companies = await _companyRepository.GetCompaniesAsync(companyDtoParameters);
            //404 NoFound();


            //返回viewModel 
            #region  //属性较少可以使用此方法。最好使用对象映射器   [AutoMapper]
            //nuget中安装 AutoMapper.Extensions.Microsoft.DependencyInjection而不是AutoMapper ，因为前者是后者的拓展。 和asp.netCore中di体系更好的结合
            //安装之后需要在 Startup.cs 的ConfigureServices中注册
            // var companiesDtos = new List<CompanyDto>();
            //foreach (var item in companies)
            //{
            //    companiesDtos.Add(new CompanyDto
            //    {
            //        Id = item.Id,
            //        CompanyName = item.Name
            //    }); ;
            //} 


            //2对应映射器
            var companiesDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            var companiesDtosTs = _mapper.Map<IEnumerable<Company>>(companiesDtos);
            #endregion
            return Ok(companiesDtos);
        }


        [HttpGet("{companyId}",Name =nameof(GetCompany))]//api/Companies/companyId=>api/Companies/123
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(Guid companyId) 
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

            try
            {
                var companiesDtos = _mapper.Map<CompanyDto>(company);
                //404 NoFound();
                return Ok(companiesDtos);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult<CompanyAddDto>> CreateCompany(CompanyAddDto company)
        {
            if (company==null)
            {
                return BadRequest(); //返回400错误
            }

            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAsync();
            var returndto = _mapper.Map<CompanyDto>(entity);

            return CreatedAtRoute(nameof(GetCompany),new { companyId=entity.Id}, returndto);

        }

        #region postman请求格式 CreateCompany
        //        {
        //	"name":"Facebook",
        //	"Introduction":"A Good Company",
        //	"employees":[
        //		{
        //			"employeeNo":"20200106",
        //			"firstName":"Lee1",
        //			"lastName":"LeiLie1",
        //			"gender":1,
        //			"dataOfBirth":"1989-12-31"

        //        },
        //			{
        //			"employeeNo":"20200107",
        //			"firstName":"Lee2",
        //			"lastName":"LeiLie2",
        //			"gender":1,
        //			"dataOfBirth":"1989-12-31"
        //		},
        //			{
        //			"employeeNo":"20200108",
        //			"firstName":"Lee3",
        //			"lastName":"LeiLie3",
        //			"gender":1,
        //			"dataOfBirth":"1989-12-31"
        //		}
        //		]


        //}
        #endregion


    }
}
