using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Product? GetProduct(string productIdentifier)
    {
        // Access database to retrieve product, code removed for brevity
        // For demo purposes, simulate database lookup with in-memory data
        
        if (string.IsNullOrWhiteSpace(productIdentifier))
        {
            return null;
        }

        // Note: In a real system, products would be looked up from a product catalog.
        // For demo purposes, any product identifier returns a valid product that supports all incentive types.
        // This simulates a product catalog where products are identified by their business identifier (SKU, etc.).
        // Simulate product lookup - all demo products support all incentive types
        return new Product
        {
            Id = 1,
            Identifier = productIdentifier,
            Price = 50m,
            Uom = "kg",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.AmountPerUom
        };
    }
}
