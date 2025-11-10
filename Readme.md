# Smartwyre Developer Test Instructions

You have been selected to complete our candidate coding exercise. Please follow the directions in this readme.

Clone, **DO NOT FORK**, this repository to your account on the online Git resource of your choosing (GitHub, BitBucket, GitLab, etc.). Your solution should retain previous commit history and you should utilize best practices for committing your changes to the repository.

You are welcome to use whatever tools you normally would when coding — including documentation, libraries, frameworks, or AI tools (such as ChatGPT or Copilot).

However, it is important that you fully understand your solution. As part of the interview process, we will review your code with you in detail. You should be able to:

- Explain the design choices you made.
- Walk us through how your solution works.
- Make modifications or extensions to your code during the review.

Please note: if your submission appears to have been generated entirely by an AI agent or another third party, without your own understanding or contribution, it will not meet our evaluation criteria.

# The Exercise

In the 'RebateService.cs' file you will find a method for calculating a rebate. At a high level the steps for calculating a rebate are:

 1. Lookup the rebate that the request is being made against.
 2. Lookup the product that the request is being made against.
 2. Check that the rebate and request are valid to calculate the incentive type rebate.
 3. Store the rebate calculation.

What we'd like you to do is refactor the code with the following things in mind:

 - Adherence to SOLID principles
 - Testability
 - Readability
 - Currently there are 3 known incentive types. In the future the business will want to add many more incentive types. Your solution should make it easy for developers to add new incentive types in the future.

We’d also like you to 
 - Add some unit tests to the Smartwyre.DeveloperTest.Tests project to show how you would test the code that you’ve produced 
 - Run the RebateService from the Smartwyre.DeveloperTest.Runner console application accepting inputs (either via command line arguments or via prompts is fine)

The only specific "rules" are:

- The solution must build
- All tests must pass

You are free to use any frameworks/NuGet packages that you see fit. You should plan to spend around 1 hour completing the exercise.

Feel free to use code comments to describe your changes. You are also welcome to update this readme with any important details for us to consider.

## Running the Application

The `Smartwyre.DeveloperTest.Runner` console application demonstrates the refactored `RebateService` in action.

### How to Run

```bash
dotnet run --project Smartwyre.DeveloperTest.Runner
```

### Demo Mode

**Note:** The Data Stores (`RebateDataStore` and `ProductDataStore`) are stubs that simulate database access using in-memory test data. In a production environment, these would connect to an actual database.

### Usage

When prompted, enter:

1. **Rebate Identifier** - Use one of the demo identifiers:
   - `rebate1` or `fixed-cash` → FixedCashAmount (fixed amount: $100)
   - `rebate2` or `fixed-rate` → FixedRateRebate (15% of price × volume)
   - `rebate3` or `amount-per-uom` → AmountPerUom ($5 per unit × volume)

2. **Product Identifier** - Any value (e.g., `product1`)

3. **Volume** - A decimal number (e.g., `10`)

### Example

```
Enter Rebate Identifier: rebate1
Enter Product Identifier: product1
Enter Volume: 10

✓ Rebate calculation successful!
  Calculated Rebate Amount: $100.00
```

### Testing Different Scenarios

- **FixedCashAmount**: Use `rebate1` with any product and volume
- **FixedRateRebate**: Use `rebate2` with volume > 0 (calculates: price × percentage × volume)
- **AmountPerUom**: Use `rebate3` with volume > 0 (calculates: amount × volume)

Entering an invalid rebate identifier will result in a "not found" error.

Once you have completed the exercise either ensure your repository is available publicly or contact the hiring manager to set up a private share.
