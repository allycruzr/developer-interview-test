using System;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators;

public class IncentiveCalculatorFactory : IIncentiveCalculatorFactory
{
    private readonly Dictionary<IncentiveType, IIncentiveCalculator> _calculators;

    public IncentiveCalculatorFactory()
    {
        _calculators = new Dictionary<IncentiveType, IIncentiveCalculator>
        {
            { IncentiveType.FixedCashAmount, new FixedCashAmountCalculator() },
            { IncentiveType.FixedRateRebate, new FixedRateRebateCalculator() },
            { IncentiveType.AmountPerUom, new AmountPerUomCalculator() }
        };
    }

    public IIncentiveCalculator Create(IncentiveType incentiveType)
    {
        if (_calculators.TryGetValue(incentiveType, out var calculator))
        {
            return calculator;
        }

        throw new ArgumentException($"No calculator found for incentive type: {incentiveType}", nameof(incentiveType));
    }
}

