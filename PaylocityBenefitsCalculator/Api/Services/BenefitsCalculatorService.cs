using Api.Dtos.Employee;
using Api.Dtos.Dependent;


namespace Api.Services
{
    public class BenefitsCalculatorService
    {
        private GetEmployeeDto EmployeeDto;

        int checksPerYear = 26;
        int baseBenefitsCost = 1000;
        int monthlyDependentCost = 600;
        int montlySeniorDependentCost = 200;
        int additionalBenefitsCostsSalaryQualifier = 80000;

        public BenefitsCalculatorService(GetEmployeeDto EmployeeDto)
        {
            this.EmployeeDto = EmployeeDto;
        }

        public decimal TotalBeforeDeductions()
        {
            //26 paychecks per year with deductions spread as evenly as possible on each paycheck
            return EmployeeDto.Salary / checksPerYear;
        }

        public int getAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            return age;
        }

        public decimal GetDeductionPerPaycheck()
        {
            DateTime today = DateTime.Today;
            decimal totalBeforeDeductions = this.TotalBeforeDeductions();

            // employees have a base cost of $1,000 per month (for benefits)
            decimal total = (baseBenefitsCost * 12) / checksPerYear;

            if (EmployeeDto.Dependents.Any())
            {
                //each dependent represents an additional $600 cost per month (for benefits)
                decimal dependentBenefitCost = (monthlyDependentCost * 12) / checksPerYear;
                total += (EmployeeDto.Dependents.Count() * dependentBenefitCost);

                // dependents that are over 50 years old will incur an additional $200 per month
                var dependentsOver50 = EmployeeDto.Dependents.Where(d => this.getAge(d.DateOfBirth) > 50);
                if (dependentsOver50.Any())
                {
                    total += ((montlySeniorDependentCost * 12) / checksPerYear);
                }
            }

            // employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
            if (EmployeeDto.Salary > additionalBenefitsCostsSalaryQualifier)
            {
                decimal additionalQualifierDeduction = (EmployeeDto.Salary * 2) / 100;
                total += (additionalQualifierDeduction / checksPerYear);
            }
            return total;
        }

        public decimal TotalPayPerPaycheck()
        {
            decimal totalbeforeDeductions = TotalBeforeDeductions();
            decimal deductionsPerPaycheck = GetDeductionPerPaycheck();

            return totalbeforeDeductions - deductionsPerPaycheck;
        }
    }
}