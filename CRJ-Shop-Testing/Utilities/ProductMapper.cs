using CRJ_Shop.Models;
using CRJ_Shop.Models.SeedModels;

namespace CRJ_Shop.Utilities;

public class ProductMapper
{
    public static Func<SeedProduct, Product> mapSeedToProduct = seedProduct => new Product
    {
        Name = seedProduct.title,
        Title = seedProduct.slug,
        Price = seedProduct.price,
        Description = seedProduct.description,
        Image = seedProduct.images[0],
        ProductCategories = new List<ProductCategory>(),
        ProductOrders = new List<ProductOrder>()
    };

    public static AvailableCategories GetCategory(string categoryName) => categoryName switch
    {
        "Shoes" => AvailableCategories.Shoes,
        "Electronics" => AvailableCategories.Electronics,
        "Clothes" => AvailableCategories.Clothes,
        "Toys" => AvailableCategories.Toys,
        "Sports" => AvailableCategories.Sports,
        "Miscellaneous" => AvailableCategories.Miscellaneous,
        _ => AvailableCategories.Miscellaneous,
    };
}
