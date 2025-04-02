using CRJ_Shop.Data;
using CRJ_Shop.Models;
using CRJ_Shop.Models.SeedModels;
using CRJ_Shop.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Services;

public class SeedService
{
    private readonly string API_URL = "https://api.escuelajs.co/api/v1/products";
    private HttpClient client;
    private readonly AppDbContext _dbContext;

    public SeedService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        client = new HttpClient();
    }

    public async Task SeedData()
    {
        try
        {

            List<SeedProduct>? seedProducts = await client.GetFromJsonAsync<List<SeedProduct>>(API_URL);

            foreach (var seedProduct in seedProducts)
            {
                Product product = ProductMapper.mapSeedToProduct(seedProduct);
                setProductCategory(product, seedProduct.category.name);
                await addIfValid(product);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    private void setProductCategory(Product product, string categoryname)
    {
        var categoryEnum = ProductMapper.GetCategory(categoryname);
        var category = _dbContext.Categories.First(p => p.ProductCategory == categoryEnum);
        var productCategory = new ProductCategory { Product = product, Category = category, CategoryId = category.Id };
        product.ProductCategories.Add(productCategory);
    }

    public async Task addIfValid(Product product)
    {
        bool exists = await _dbContext.Products.AnyAsync(p => p.Name == product.Name);
        if (exists is false)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task SeedCategories()
    {
        List<Category> productCategories = new List<Category> {
                        new Category { ProductCategory = AvailableCategories.Toys },
                        new Category { ProductCategory = AvailableCategories.Sports },
                        new Category { ProductCategory = AvailableCategories.Electronics },
                        new Category { ProductCategory = AvailableCategories.Miscellaneous },
                        new Category { ProductCategory = AvailableCategories.Shoes },
                        new Category { ProductCategory = AvailableCategories.Clothes } };


        foreach (var category in productCategories)
        {
            await addCategoryIfNotExists(category);
        }

    }


    public async Task addCategoryIfNotExists(Category productCategory)
    {
        bool exists = await _dbContext.Categories.AnyAsync(p => p.ProductCategory == productCategory.ProductCategory);
        if (exists is false)
        {
            _dbContext.Categories.Add(productCategory);
            await _dbContext.SaveChangesAsync();
        }
    }

}
