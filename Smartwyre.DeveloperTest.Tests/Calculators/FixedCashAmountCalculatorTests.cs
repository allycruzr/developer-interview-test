using System;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Calculators;

public class FixedCashAmountCalculatorTests
{
    private readonly FixedCashAmountCalculator _calculator;

    public FixedCashAmountCalculatorTests()
    {
        _calculator = new FixedCashAmountCalculator();
    }

    [Fact]
    public void CanCalculate_ReturnsTrue_WhenAllConditionsAreMet()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
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
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
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
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100m,
            Percentage = 0m
        };

        Product? product = null;

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
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
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenRebateAmountIsZero()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 0m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        // Act
        var result = _calculator.CanCalculate(rebate!, product, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ReturnsRebateAmount_RegardlessOfVolume()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 150m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var request1 = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1",
            Volume = 5m
        };

        var request2 = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate1",
            ProductIdentifier = "product1",
            Volume = 100m
        };

        // Act
        var result1 = _calculator.Calculate(rebate, product, request1);
        var result2 = _calculator.Calculate(rebate, product, request2);

        // Assert
        // FixedCashAmount does not depend on volume, always returns the same value
        Assert.Equal(150m, result1);
        Assert.Equal(150m, result2);
        Assert.Equal(result1, result2);
    }

}
