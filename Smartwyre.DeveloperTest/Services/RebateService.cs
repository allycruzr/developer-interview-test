using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IIncentiveCalculatorFactory _calculatorFactory;

    public RebateService(
        IRebateDataStore rebateDataStore,
        IProductDataStore productDataStore,
        IIncentiveCalculatorFactory calculatorFactory)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _calculatorFactory = calculatorFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null)
        {
            return new CalculateRebateResult 
            { 
                Success = false,
                ErrorMessage = "Rebate not found"
            };
        }

        if (product == null)
        {
            return new CalculateRebateResult 
            { 
                Success = false,
                ErrorMessage = "Product not found"
            };
        }

        var calculator = _calculatorFactory.Create(rebate.Incentive);

        if (!calculator.CanCalculate(rebate, product, request))
        {
            return new CalculateRebateResult 
            { 
                Success = false,
                ErrorMessage = "Product does not support this incentive type or invalid data provided"
            };
        }

        var rebateAmount = calculator.Calculate(rebate, product, request);

        _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

        return new CalculateRebateResult 
        { 
            Success = true,
            RebateAmount = rebateAmount
        };
    }
}
