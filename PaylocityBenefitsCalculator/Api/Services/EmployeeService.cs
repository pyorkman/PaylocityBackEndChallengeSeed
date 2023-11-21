using Api.Data;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Services;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Services
{

    public class EmployeeService
    {
        private readonly DataContext _context;

        public List<GetEmployeeDto> GetAll()
        {
            var employees = DataContext.Data;

            return employees;
        }

        public GetEmployeeDto Get(int id)
        {
            var employee = DataContext.Data.FirstOrDefault(e => e.Id == id);

            return employee;
        }

        public GetPaycheckDto GetPaycheck(int employeeId)
        {
            var employeePaycheck = new List<GetPaycheckDto>();
            try
            {
                var employees = DataContext.Data;
                foreach (var employee in employees.Where(x => x.Id == employeeId))
                {
                    var benefitsCalculator = new BenefitsCalculatorService(employee);

                    employeePaycheck.Add(new()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Salary = employee.Salary,
                        DateOfBirth = employee.DateOfBirth,
                        Dependents = employee.Dependents,
                        PayBeforeDeductions = benefitsCalculator.TotalBeforeDeductions(),
                        DeductionsCost = benefitsCalculator.GetDeductionPerPaycheck(),
                        TotalPay = benefitsCalculator.TotalPayPerPaycheck(),
                    });
                }

                var result = new ApiResponse<List<GetPaycheckDto>>
                {
                    Data = employeePaycheck,
                    Success = true
                };
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            return null;
            //TODO: Correct object type of employeePaycheck
            //return employeePaycheck;
        }

        private GetEmployeeDto MapToDto(Employee employee)
        {
            return new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents.Select(d => new GetDependentDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Relationship = d.Relationship,
                    DateOfBirth = d.DateOfBirth
                }).ToList()
            };
        }
    }
}