﻿using Routine.Api.DtoParameters;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(CompanyDtoParameters companyDtoParameters);
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);

        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistsAsync(Guid companyId);


        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<bool> SaveAsync();




    }
}
