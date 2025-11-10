using System;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();
        var calculatorFactory = new IncentiveCalculatorFactory();
        var rebateService = new RebateService(rebateDataStore, productDataStore, calculatorFactory);

        Console.WriteLine("=== Smartwyre Rebate Calculator ===");
        Console.WriteLine();

        Console.Write("Enter Rebate Identifier: ");
        var rebateIdentifier = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter Product Identifier: ");
        var productIdentifier = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter Volume: ");
        decimal volume;
        string? volumeInput;
        while (true)
        {
            volumeInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(volumeInput))
            {
                Console.Write("Volume cannot be empty. Please enter a valid decimal number (>= 0): ");
                continue;
            }

            if (!decimal.TryParse(volumeInput, out volume))
            {
                Console.Write("Invalid format. Please enter a valid decimal number (>= 0): ");
                continue;
            }

            if (volume < 0)
            {
                Console.Write("Volume cannot be negative. Please enter a number >= 0: ");
                continue;
            }

            break;
        }

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        Console.WriteLine();
        Console.WriteLine("Calculating rebate...");

        CalculateRebateResult result;
        try
        {
            result = rebateService.Calculate(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"✗ Error: {ex.Message}");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine();
        if (result.Success)
        {
            Console.WriteLine("✓ Rebate calculation successful!");
            if (result.RebateAmount.HasValue)
            {
                Console.WriteLine($"Calculated Amount: {result.RebateAmount.Value:C}");
            }
            Console.WriteLine("Calculation stored successfully.");
        }
        else
        {
            Console.WriteLine("✗ Rebate calculation failed.");
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                Console.WriteLine($"Reason: {result.ErrorMessage}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
