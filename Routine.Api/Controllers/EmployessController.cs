using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.EntitiesDto;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/employess")]
    public class EmployessController : ControllerBase
    {
        public readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;//对象映射器
        public EmployessController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ??
                throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("GetOfId")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId)
        {
            //没有找到
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return Ok("222");
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);

        }


        [HttpGet]
        [Route("GetOfId/{employeesId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeForCompany(Guid companyId, Guid employeesId)
        {
            //没有找到
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);

        }

    }







}
