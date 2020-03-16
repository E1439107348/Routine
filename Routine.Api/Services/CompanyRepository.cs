using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.DtoParameters;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        public CompanyRepository(RoutineDbContext context)
        {
            //如果未null就抛出异常
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }





        #region 实现类 
        #region Company
        public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyDtoParameters companyDtoParameters)
        {
            if (companyDtoParameters == null)
            { throw new ArgumentNullException(nameof(companyDtoParameters)); }
            if (string.IsNullOrWhiteSpace(companyDtoParameters.CompanyName) && string.IsNullOrWhiteSpace(companyDtoParameters.SearchTerm))
            { return await _context.Companyies.ToListAsync(); }

            var queryExpression = _context.Companyies.Where(c => true);

            if (!string.IsNullOrWhiteSpace(companyDtoParameters.CompanyName))
            {
                companyDtoParameters.CompanyName = companyDtoParameters.CompanyName.Trim();
                queryExpression = queryExpression.Where(c => c.Name.Contains(companyDtoParameters.CompanyName)); 
            }
            if (!string.IsNullOrWhiteSpace(companyDtoParameters.SearchTerm))
            {
                companyDtoParameters.SearchTerm = companyDtoParameters.SearchTerm.Trim();
                queryExpression = queryExpression.Where(c => c.Name.Contains(companyDtoParameters.SearchTerm)|| c.Introduction.Contains(companyDtoParameters.SearchTerm));
            }
            return await queryExpression.ToListAsync();

        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companyies
                .FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await _context.Companyies
                .Where(x => companyIds.Contains(x.Id))
                .ToListAsync();
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            company.Id = Guid.NewGuid();
            if (company.Employees != null)
            {
                foreach (var item in company.Employees)
                {
                    item.Id = Guid.NewGuid();
                }
            }

            _context.Companyies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companyies.Remove(company);

        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companyies.AnyAsync(x => x.Id == companyId);
        }

        #endregion

        #region Employee
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (string.IsNullOrWhiteSpace(genderDisplay))
            {
                return await _context.Employees
                  .Where(x => x.CompanyId == companyId)
                  .OrderBy(x => x.EmployeeNo)
                  .ToListAsync();
            }
            var genderStr = genderDisplay.Trim();
            var gender = Enum.Parse<Gender>(genderStr);//通过枚举获取对应的值；
            return await _context.Employees
              .Where(x => x.CompanyId == companyId && x.Gender == gender)
              .OrderBy(x => x.EmployeeNo)
              .ToListAsync();
        }
        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }



            return await _context.Employees
              .Where(x => x.CompanyId == companyId && x.Id == employeeId)
              .FirstOrDefaultAsync();

        }
        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }


            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }
        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
        #endregion

        #endregion

    }
}
