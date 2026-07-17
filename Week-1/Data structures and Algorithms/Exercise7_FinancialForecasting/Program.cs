using System;
using System.Collections.Generic;

namespace Exercise7_FinancialForecasting
{
    class Program
    {
        // Simple recursive approach
        static double PredictFutureValue(double currentValue, double growthRate, int years)
        {
            if (years == 0) return currentValue;
            
            return PredictFutureValue(currentValue * (1 + growthRate), growthRate, years - 1);
        }

        // Optimized recursive approach (Memoization)
        // Note: For this specific math problem, memoization is overkill compared to a direct formula 
        // (currentValue * (1 + growthRate)^years), but it's used here to demonstrate optimization.
        static Dictionary<int, double> memo = new Dictionary<int, double>();
        static double PredictFutureValueMemoized(double currentValue, double growthRate, int years, int currentYear = 0)
        {
            if (currentYear == years) return currentValue;

            if (memo.ContainsKey(currentYear))
                return memo[currentYear];

            double result = PredictFutureValueMemoized(currentValue * (1 + growthRate), growthRate, years, currentYear + 1);
            memo[currentYear] = result;
            
            return result;
        }

        static void Main(string[] args)
        {
            double initialValue = 1000.0; // $1000
            double annualGrowthRate = 0.05; // 5%
            int yearsToForecast = 10;

            Console.WriteLine("--- Recursive Financial Forecasting ---");
            double futureValue = PredictFutureValue(initialValue, annualGrowthRate, yearsToForecast);
            Console.WriteLine($"Predicted Value after {yearsToForecast} years: ${futureValue:F2}");

            Console.WriteLine("\n--- Optimized (Memoized) Financial Forecasting ---");
            double futureValueOpt = PredictFutureValueMemoized(initialValue, annualGrowthRate, yearsToForecast);
            Console.WriteLine($"Predicted Value after {yearsToForecast} years: ${futureValueOpt:F2}");
        }
    }
}
