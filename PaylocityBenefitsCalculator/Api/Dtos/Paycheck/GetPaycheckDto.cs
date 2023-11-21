using Api.Dtos.Employee;

public class GetPaycheckDto : GetEmployeeDto
{
    public decimal PayBeforeDeductions { get; set; }
    public decimal BaseBenefitsCosts { get; set; }
    public decimal DependentsBenefitsCosts { get; set; }
    public decimal SeniorBenefitsCosts { get; set; }
    public decimal HighEarningSalaryBenefitsCosts { get; set; }
    public decimal DeductionsCost { get; set; }
    public decimal TotalPay { get; set; }
}