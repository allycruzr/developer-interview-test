using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Rebate? GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve rebate, code removed for brevity
        // For demo purposes, simulate database lookup with in-memory data
        
        if (string.IsNullOrWhiteSpace(rebateIdentifier))
        {
            return null;
        }

        // Note: Rebates are specific promotions/campaigns that must exist in the system.
        // Each rebate has a unique identifier and specific configuration (type, amount, percentage).
        // Simulate different rebate types based on identifier
        return rebateIdentifier.ToLower() switch
        {
            "rebate1" or "fixed-cash" => new Rebate
            {
                Identifier = rebateIdentifier,
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100m,
                Percentage = 0m
            },
            "rebate2" or "fixed-rate" => new Rebate
            {
                Identifier = rebateIdentifier,
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 0m,
                Percentage = 0.15m
            },
            "rebate3" or "amount-per-uom" => new Rebate
            {
                Identifier = rebateIdentifier,
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5m,
                Percentage = 0m
            },
            _ => null // Not found
        };
    }

    public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
    {
        // Store rebate calculation in database, code removed for brevity
    }
}
