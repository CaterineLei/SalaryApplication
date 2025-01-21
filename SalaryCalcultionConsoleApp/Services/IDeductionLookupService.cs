using SalaryCalcultionConsoleApp.Models;

namespace SalaryCalcultionConsoleApp.Services
{
    public interface IDeductionLookupService
    {
        DeductionLookupModel GetDeductionLookupData(int taxableIncome);
    }
}
