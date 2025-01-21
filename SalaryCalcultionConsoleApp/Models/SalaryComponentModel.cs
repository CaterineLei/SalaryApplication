
namespace SalaryCalcultionConsoleApp.Models
{
    public class SalaryComponentModel
    {
        public int CrossPackage { get; set; }
        public double TaxableIncome { get; set; }
        public double Superannuation { get; set; }
        public double NetIncome { get; set; }
        //deductions
        public int MedicareLevy { get; set; }
        public int BudgetRepairLevy { get; set; }
        public int IncomeTax { get; set; }

        public double PayPacket { get; set; }
        public double SuperannuationRate { get; set; } = 0.095;
        public PayFrequencyEnum PayFrequency { get; set; }

        




    }
}
