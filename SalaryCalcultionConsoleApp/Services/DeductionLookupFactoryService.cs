using SalaryCalcultionConsoleApp.Models;

namespace SalaryCalcultionConsoleApp.Services
{
    /// <summary>
    /// This is a factory class which returns the particular deduction 
    /// lookup service required
    /// 
    /// </summary>
    public class DeductionLookupFactoryService
    {

        public IDeductionLookupService GetDeductionLookupService(DeductionTypeEnum type)
        {
            switch (type)
            {
                case DeductionTypeEnum.MedicareLevy:
                    return new MedicareLevyLookupService();

                case DeductionTypeEnum.BudgetRepairLevy:
                    return new BudgetRepairLevyLookupService();

                case DeductionTypeEnum.IncomeTax:
                    return new IncomeTaxLookupService();

            }
            return null;

        }
    }
}
