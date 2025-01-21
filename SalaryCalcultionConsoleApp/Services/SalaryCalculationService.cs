using SalaryCalcultionConsoleApp.Models;
using System;

namespace SalaryCalcultionConsoleApp.Services
{
    /// <summary>
    /// This class is used to calculate every component of the salary including superannuation, taxable income, net income, etc. 
    /// </summary>
    public class SalaryCalculationService
    {
        #region  methods 
        public (double superannuation, double taxableIncome) CalculateSuperAndTaxableIncome(int salaryCrossPackage, double superRate)
        {
            double income = salaryCrossPackage / (1 + superRate);
            double super = RoundDouble(income * superRate, 2);
            return (super, RoundDouble(income, 2));

        }

        public (int medicareLevy, int budgetRepairLevy, int incomeTax) CalculateDeductions(int taxableIncome)
        {
            var service = new DeductionLookupFactoryService();
            int medicareLevy = CalculateDeduction(service.GetDeductionLookupService(DeductionTypeEnum.MedicareLevy)?.GetDeductionLookupData(taxableIncome), taxableIncome);
            int budgetRepairLevy = CalculateDeduction(service.GetDeductionLookupService(DeductionTypeEnum.BudgetRepairLevy)?.GetDeductionLookupData(taxableIncome), taxableIncome);
            int incomeTax = CalculateDeduction(service.GetDeductionLookupService(DeductionTypeEnum.IncomeTax)?.GetDeductionLookupData(taxableIncome), taxableIncome);
            return (medicareLevy, budgetRepairLevy, incomeTax);

        }

        public double CalculateNetIncome(int salaryCrossPackage, double superannuation, int deductions)
        {
            return RoundDouble(salaryCrossPackage - superannuation - deductions, 2);
        }

        public double CalculatePayPacket(double netIncome, PayFrequencyEnum payFrequency)
        {
            var payPacket = netIncome / (int)payFrequency;

            return RoundDouble(payPacket, 2);

        }

        #endregion

        #region Helper methods

        public double RoundDouble(double amount, int places)
        {
            return Math.Round(amount, places);

        }

        private int CalculateDeduction(DeductionLookupModel deductionData, int taxableIncome)
        {
            if (deductionData == null)
            {
                throw new ArgumentNullException("Deduction lookup data can not be null");
            }

            return Convert.ToInt32(Math.Round(deductionData.AdditionAmount + (taxableIncome - deductionData.ExcessDeductionAmount) * deductionData.DeductionRate, 0));
        }

        #endregion

    }
}
