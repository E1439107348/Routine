using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Routine.Api.Entities;
using Routine.Api.EntitiesDto;
using Routine.Api.Helpers;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companyCollections")]
    public class CompanyCollectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyCollectionController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        /// <summary>
        /// 集合类参数无法从路由里面直接获取， FromRoute 声明
        /// </summary>
        /// <param name="ids"></param>
        /// 此处ids用括号括起来是为了方便
        /// <returns></returns>
        [HttpGet("({ids})", Name =nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection(
            [FromRoute]
            [ModelBinder(BinderType =typeof(ArrayModelBinder))] //将string转化成guid
        IEnumerable<Guid> ids)
        {
            if(ids == null){
                return BadRequest();
            }
            var entities = await _companyRepository.GetCompaniesAsync(ids);
            if (entities.Count()!=ids.Count())
            {
                return NotFound();
            }
            var dtosReturn = _mapper.Map<IEnumerable<CompanyDto>>(entities);
            return Ok(dtosReturn);
        }
        #region 请求方式
        //http://localhost:5000/api/companyCollections/(50124a77-11aa-4f1d-8d91-9b182078b449,2ab98d6a-6d78-4baa-80f3-1d25a99d143b,7307ef7e-5380-467b-bba7-c9cdfbfe1f48)
        #endregion

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(IEnumerable<CompanyAddDto> companyCollection)
        {
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var item in companyEntities)
            {
                _companyRepository.AddCompany(item);
            }
            await _companyRepository.SaveAsync();
            var dtos = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var idsString = string.Join(",", dtos.Select(x => x.Id));
            return CreatedAtRoute(nameof(GetCompanyCollection), new { ids= idsString }, dtos);
        }
        #region  CreateCompanyCollection postman请求方式
        //http://localhost:5000/api/companyCollections

        //        [
        //{
        //"name":"HuaWei",
        //"introduction":"A good Company"
        //},
        //{ 
        //"name":"XIAOMI",
        //"introduction":"A good Company"
        //},
        //{
        //"name":"OPPO",
        //"introduction":"A good Company"
        //}
        //]
        #endregion


    }
}
