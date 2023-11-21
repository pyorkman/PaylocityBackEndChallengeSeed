
using Api.Dtos.Dependent;
using Api.Models;
using Api.Data;
using System.ComponentModel.DataAnnotations;

namespace Api.Services
{
    public class DependentService 
    {
        private readonly DataContext _context;

        public DependentService(DataContext context)
        {
            _context = context;
        }

        //TODO: Create dependent based on the rules given in the instructions
        //public GetDependentDto CreateDependent(GetDependentDto dependent)
        //{
        //    var employee = _context.Include(employee => employee.Dependents).FirstOrDefault(e => e.Id == dependent.EmployeeId);


        //    var newDependent = new Dependent
        //    {
        //        Id = _context.Dependents.Max(d => d.Id) + 1,
        //        FirstName = dependent.FirstName,
        //        LastName = dependent.LastName,
        //        Relationship = dependent.Relationship,
        //        DateOfBirth = dependent.DateOfBirth,
        //        EmployeeId = dependent.EmployeeId
        //    };

        //    employee.Dependents.Add(newDependent);
        //    _context.Employees.Update(employee);
        //    _context.Dependents.Add(newDependent);
        //    _context.SaveChanges();

        //    return Get(newDependent.Id);
        //}

        private GetDependentDto MapToDto(Dependent dependent)
        {
            return new GetDependentDto
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                DateOfBirth = dependent.DateOfBirth,
            };
        }
    }
}