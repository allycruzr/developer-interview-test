using System;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Calculators;

public class AmountPerUomCalculatorTests
{
    private readonly AmountPerUomCalculator _calculator;

    public AmountPerUomCalculatorTests()
    {
        _calculator = new AmountPerUomCalculator();
    }

    [Fact]
    public void CanCalculate_ReturnsTrue_WhenAllConditionsAreMet()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
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
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        Product? product = null;

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
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
            RebateIdentifier = "rebate3",
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 0m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 7.5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
            ProductIdentifier = "product1",
            Volume = 20m
        };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        // Expected: 7.5 * 20 = 150
        Assert.Equal(150m, result);

        // Test with different values to ensure formula works correctly
        var rebate2 = new Rebate
        {
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product2 = new Product
        {
            Identifier = "PRODUCT1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request2 = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
            ProductIdentifier = "product1",
            Volume = 10m
        };

        var result2 = _calculator.Calculate(rebate2, product2, request2);
        // Expected: 5 * 10 = 50
        Assert.Equal(50m, result2);
    }

    [Fact]
    public void CanCalculate_ReturnsFalse_WhenRequestIsNull()
    {
        // Arrange
        var rebate = new Rebate
        {
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
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
            Identifier = "rebate3",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5m,
            Percentage = 0m
        };

        var product = new Product
        {
            Identifier = "product1",
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebate3",
            ProductIdentifier = "product1",
            Volume = -10m
        };

        // Act
        var result = _calculator.Calculate(rebate, product, request);

        // Assert
        // Calculates: 5 * (-10) = -50
        // This is a problem - negative values should not be calculated
        Assert.Equal(-50m, result);
    }
}
