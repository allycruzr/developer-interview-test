using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators;

public interface IIncentiveCalculatorFactory
{
    IIncentiveCalculator Create(IncentiveType incentiveType);
}

