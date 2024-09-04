using Mobit.Models;
using Bogus;
using System.Collections.Generic;

public static class ProductGenerator
{
    public static List<Product> GenerateFakeProducts(int count)
    {
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.Quantity, f => f.Random.Int(0, 100))
            .RuleFor(p => p.Price, f => f.Finance.Amount(1m, 1000m))
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

        return productFaker.Generate(count);
    }
}