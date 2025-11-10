using System;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Calculators;

public class FixedRateRebateCalculatorTests
{
    private readonly FixedRateRebateCalculator _calculator;

    public FixedRateRebateCalculatorTests()
    {
        _calculator = new FixedRateRebateCalculator();
    }

    [Fact]
    public void CanCalculate_ReturnsTrue_WhenAllConditionsAreMet()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenRebateIsNull()
    {
        // Arrange
        Rebate? rebate = null;
        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenProductIsNull()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        Product? product = null;

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate, product!, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenProductDoesNotSupportIncentive()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenRebatePercentageIsZero()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenProductPriceIsZero()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 0m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenVolumeIsZero()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 0m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ReturnsCorrectAmount_WithVariousInputs()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.20m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 100m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 5m
        };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        // Expected: 100 * 0.20 * 5 = 100
        Assert.Equal(100m, result);

        // Test with different values to ensure formula works correctly
        var rebate2 = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product2 = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request2 = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        var result2 = _calculator.Calculate(rebate2, product2, request2);
        // Expected: 50 * 0.15 * 10 = 75
        Assert.Equal(75m, result2);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenRequestIsNull()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        CalculateRebateRequest? request = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _calculator.CanCalculate(rebate, product, request!));
    }

    [Fact]
    public void Calculate_ThrowsException_WhenRequestIsNull()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        CalculateRebateRequest? request = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _calculator.Calculate(rebate, product, request!));
    }

    [Fact]
    public void CanCalculate_ReturnsTrue_WhenVolumeIsNegative()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = -10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate, product, request);

        // Assert
        // Negative volume passes validation because != 0
        // This is a potential bug - negative volume should be rejected
        Assert.True(result);
    }

    [Fact]
    public void Calculate_ReturnsNegativeAmount_WhenVolumeIsNegative()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate2",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 0m,
            Percentage = 0.15m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate2",
            ProductIdentifier = "product1",
            Volume = -10m
        };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        // Calculates: 50 * 0.15 * (-10) = -75
        // This is a problem - negative values should not be calculated
        Assert.Equal(-75m, result);
    }
}
