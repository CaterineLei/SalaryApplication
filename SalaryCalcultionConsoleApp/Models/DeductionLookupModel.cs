
namespace SalaryCalcultionConsoleApp.Models
{
    /// <summary>
    /// This class is to hold the data for deduction calculation
    /// </summary>
    public class DeductionLookupModel
    {
        public double DeductionRate { get; set; }
        public int ExcessDeductionAmount { get; set; }
        //The fixed tax amount
        public int AdditionAmount { get; set; }

        public DeductionLookupModel( double deductionRate, int excessDeductionAmount, int additionAmount=0)
        {
            DeductionRate = deductionRate;
            ExcessDeductionAmount = excessDeductionAmount;
            AdditionAmount = additionAmount;
        }
    }
}
