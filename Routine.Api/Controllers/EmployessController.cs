using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
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
       // [Route("GetOfId")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId,[FromQuery]string genderDisplay)
        {
            //没有找到
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId, genderDisplay);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);

        }


        [HttpGet]
        //[Route("GetOfId/{employeesId}",Name =nameof(GetEmployeeForCompany))]
        [Route("{employeesId}", Name = nameof(GetEmployeeForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeForCompany(Guid companyId, Guid employeesId, [FromQuery]string genderDisplay)
        {
            //没有找到
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId, genderDisplay);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);

        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employee)
        {
            try
            {
                if (!await _companyRepository.CompanyExistsAsync(companyId))
                {
                    return NotFound();
                }
                var entity = _mapper.Map<Employee>(employee);
                _companyRepository.AddEmployee(companyId, entity);
                await _companyRepository.SaveAsync();

                var dtoReturn = _mapper.Map<EmployeeDto>(entity);  
                return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeesId = dtoReturn.Id }, dtoReturn);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }







}
