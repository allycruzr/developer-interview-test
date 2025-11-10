namespace Smartwyre.DeveloperTest.Types;

public class Product
{
    // Note: Id is not used in business logic (could be database primary key).
    // Identifier is used for lookups and represents the business identifier (e.g., SKU, barcode).
    public int Id { get; set; }
    public required string Identifier { get; set; }
    
    public decimal Price { get; set; }
    public required string Uom { get; set; }
    public SupportedIncentiveType SupportedIncentives { get; set; }
}
