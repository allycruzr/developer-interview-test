using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators;

public class FixedCashAmountCalculator : IIncentiveCalculator
{
    public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        if (rebate == null || product == null)
        {
            return false;
        }

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            return false;
        }

        if (rebate.Amount == 0)
        {
            return false;
        }

        return true;
    }

    public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount;
    }
}

