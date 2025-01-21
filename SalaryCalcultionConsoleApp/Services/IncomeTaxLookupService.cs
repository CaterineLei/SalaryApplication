using SalaryCalcultionConsoleApp.Models;

namespace SalaryCalcultionConsoleApp.Services
{
    public class IncomeTaxLookupService : IDeductionLookupService
    {
        //Income Tax lookup data
        private readonly int[,] incomeRange = { { 0, 18200 }, { 18201, 37000 }, { 37001, 87000 }, { 87001, 180000 }, { 180001, int.MaxValue } };
        private readonly double[] taxRate = { 0, 0.19, 0.325, 0.37, 0.47 };
        private readonly int[] excessDeduction = { 0, 18200, 37000, 87000, 180000 };
        private readonly int[] additionTax = { 0, 0, 3572, 19822, 54232 };

        public DeductionLookupModel GetDeductionLookupData(int taxableIncome)
        {
            for (int i = 0; i < incomeRange.GetLength(0); i++)
            {
                if (taxableIncome >= incomeRange[i, 0] && taxableIncome <= incomeRange[i, 1])
                {
                    return new DeductionLookupModel(taxRate[i], excessDeduction[i], additionTax[i]);

                }
            }

            return null;
        }

    }
}
