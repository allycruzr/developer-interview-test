using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators;

public class FixedRateRebateCalculator : IIncentiveCalculator
{
    public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        if (rebate == null || product == null)
        {
            return false;
        }

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        {
            return false;
        }

        if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        {
            return false;
        }

        return true;
    }

    public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return product.Price * rebate.Percentage * request.Volume;
    }
}

