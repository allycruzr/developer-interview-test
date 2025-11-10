using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve rebate, code removed for brevity 
        return new Rebate();
    }

    public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
    {
        // Store rebate calculation in database, code removed for brevity
    }
}
