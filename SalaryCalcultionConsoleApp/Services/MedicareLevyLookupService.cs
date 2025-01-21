using SalaryCalcultionConsoleApp.Models;

namespace SalaryCalcultionConsoleApp.Services
{
    public class MedicareLevyLookupService : IDeductionLookupService
    {
        //Medicare Levy lookup data
        private readonly int[,] incomeRange = { { 0, 21335 }, { 21336, 26668 }, { 26669, int.MaxValue } };
        private readonly double[] levyRate = { 0, 0.1, 0.02 };
        private readonly int[] excessDeduction = { 0, 21335, 0 };

        public DeductionLookupModel GetDeductionLookupData(int taxableIncome)
        {
            for (int i = 0; i < incomeRange.GetLength(0); i++)
            {
                if (taxableIncome >= incomeRange[i, 0] && taxableIncome <= incomeRange[i, 1])
                {
                    return new DeductionLookupModel(levyRate[i], excessDeduction[i]);

                }
            }

            return null;
        }
    }
}
