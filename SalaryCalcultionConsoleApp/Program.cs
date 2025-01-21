using SalaryCalcultionConsoleApp.Models;
using SalaryCalcultionConsoleApp.Services;
using System;
using System.Linq;

namespace SalaryCalcultionConsoleApp
{
    public class Program
    {
      
        static void Main(string[] args)
        {
            //Use a SalaryComponentModel object to hold salary details data
            var salaryData = new SalaryComponentModel();
            var (salaryPackage, payFrequency) = UserInput();
            salaryData.CrossPackage = salaryPackage;
            salaryData.PayFrequency = (PayFrequencyEnum)Enum.Parse(typeof(PayFrequencyEnum), payFrequency);
            Console.WriteLine("Calculating salary details...");
            Console.WriteLine();

            //Calculate salary details data and fill it in a SalaryComponentModel object
            CalculateAndFillSalaryData(salaryData);

            //Display salary details on console
            DisplaySalaryDetails(salaryData);
        }
        
        /// <summary>
        /// User enters salary package and pay frequency
        /// </summary>
        /// <returns>salary package and pay frequency</returns>
        private static (int salaryPackage, string payFrequency) UserInput()
        {
            //User enters the salary package
            int min = 0;
            int max = int.MaxValue;
            int packageValue;
            string packagePrompt = $"Please enter your salary package amount which is an integer ({min} - {max}):  ";
            Console.Write(packagePrompt);
            //validate the user input
            while (!int.TryParse(Console.ReadLine(), out packageValue) || packageValue < min || packageValue > max)
            {
                Console.WriteLine("Invalid input. You can only enter numbers (0 - 9). No any other characters are allowed. Try again ");
                Console.Write(packagePrompt);
            }

            //User enters the pay frequency
            string[] PayFrequencyTypes = { "W", "F", "M" };
            string payFrequencyPrompt = "Please enter your pay frequency (W for weekly, F for fortnightly, M for monthly):  ";
            Console.Write(payFrequencyPrompt);
            string payFrequency = Console.ReadLine().Trim().ToUpper();
            //validate the user input 
            while (!PayFrequencyTypes.Contains(payFrequency))
            {
                Console.WriteLine("Invalid input. You can only enter a single character W or F or M. No any other characters are allowed. Try again ");
                Console.Write(payFrequencyPrompt);
                payFrequency = Console.ReadLine().Trim().ToUpper();
            }
            
            return (packageValue, payFrequency);

        }

        /// <summary>
        /// Calculate salary details data and fill the data in an object
        /// </summary>
        /// <param name="salaryData"></param>
        private static void CalculateAndFillSalaryData(SalaryComponentModel salaryData)
        {
           
            var service = new SalaryCalculationService();
            var (super, income) = service.CalculateSuperAndTaxableIncome(salaryData.CrossPackage, salaryData.SuperannuationRate);
            salaryData.Superannuation = super;
            salaryData.TaxableIncome = income;
            var (medicareLevy, budgetRepairLevy, incomeTax) = service.CalculateDeductions(Convert.ToInt32(service.RoundDouble(salaryData.TaxableIncome, 0)));
            salaryData.MedicareLevy = medicareLevy;
            salaryData.BudgetRepairLevy = budgetRepairLevy;
            salaryData.IncomeTax = incomeTax;
            salaryData.NetIncome = service.CalculateNetIncome(salaryData.CrossPackage, salaryData.Superannuation, salaryData.MedicareLevy + salaryData.BudgetRepairLevy + salaryData.IncomeTax);
            salaryData.PayPacket = service.CalculatePayPacket(salaryData.NetIncome, salaryData.PayFrequency);

        }

        /// <summary>
        /// Display the salary details on console
        /// </summary>
        /// <param name="salaryData"></param>
        private static void DisplaySalaryDetails(SalaryComponentModel salaryData)
        {
            Console.WriteLine("Cross package: {0:C0}", salaryData.CrossPackage);
            Console.WriteLine($"Superanuation: {salaryData.Superannuation:C2}");
            Console.WriteLine();

            Console.WriteLine($"Taxable income: {salaryData.TaxableIncome:C2}");
            Console.WriteLine();

            Console.WriteLine("Deductions:");
            Console.WriteLine($"Medicare Levy: {salaryData.MedicareLevy:C0}");
            Console.WriteLine($"Budget Repair Levy: {salaryData.BudgetRepairLevy:C0}");
            Console.WriteLine($"Income Tax: {salaryData.IncomeTax:C0}");
            Console.WriteLine();

            Console.WriteLine($"Net income: {salaryData.NetIncome:C2}");
            string payFrequencyText=string.Empty;
            switch(salaryData.PayFrequency)
            {
                case PayFrequencyEnum.M:
                    payFrequencyText = "per month";
                    break;
                case PayFrequencyEnum.F:
                    payFrequencyText = "per fortnight";
                    break;
                case PayFrequencyEnum.W:
                    payFrequencyText = "per week";
                    break;

            }
            Console.WriteLine($"Pay packet: {salaryData.PayPacket:C2} {payFrequencyText}");
            Console.WriteLine("Press any key to end ...");
            Console.ReadLine();

        }

    }
}
