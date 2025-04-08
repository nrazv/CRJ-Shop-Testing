using CRJ_Shop.Models;


namespace Unit_Testing;

public static class ProductTestData
{
    public static IEnumerable<object[]> ProductTestInstance()
    {

        yield return new object[]
        {
            new Product(){
            Id = 1,
            Description = "Product Description",
            Image = "http://image.com",
            Name = "Test Product",
            Price = 100,
            AvailableAmount = 200,
            ProductCategories = new List<ProductCategory>(),
            ProductOrders = new List<ProductOrder>(),
            Title = "Best Test Products"
         } };
    }


    public static IEnumerable<object[]> ProductListTestInstance()
    {
        yield return new object[]
        {
        new List<Product>
        {
            new Product
            {
                Id = 1,
                Description = "Product 1",
                Image = "http://image1.com",
                Name = "Product One",
                Price = 100,
                AvailableAmount = 200,
                ProductCategories = new List<ProductCategory>(),
                ProductOrders = new List<ProductOrder>(),
                Title = "Awesome Product One"
            },
            new Product
            {
                Id = 2,
                Description = "Product 2",
                Image = "http://image2.com",
                Name = "Product Two",
                Price = 150,
                AvailableAmount = 300,
                ProductCategories = new List<ProductCategory>(),
                ProductOrders = new List<ProductOrder>(),
                Title = "Awesome Product Two"
            }
        }
        };
    }




}

