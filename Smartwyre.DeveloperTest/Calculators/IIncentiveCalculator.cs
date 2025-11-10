using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators;

public interface IIncentiveCalculator
{
    bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request);
    decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
}
