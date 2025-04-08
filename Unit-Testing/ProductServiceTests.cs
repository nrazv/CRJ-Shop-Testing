using CRJ_Shop.Models;
using CRJ_Shop.Repositories.Products;
using CRJ_Shop.Services.Products;
using Moq;
using System.Linq.Expressions;


namespace Unit_Testing;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> productsRepositoryMock;
    private readonly ProductService productService;

    public ProductServiceTests()
    {
        productsRepositoryMock = new Mock<IProductRepository>();
        productService = new ProductService(productsRepositoryMock.Object);
    }


    [Theory]
    [MemberData(nameof(ProductTestData.ProductTestInstance), MemberType = typeof(ProductTestData))]
    public async Task Delete_GivenProductReturnsTrueWhenDeleted(Product product)
    {
        productsRepositoryMock.Setup(r => r.SaveChanges()).ReturnsAsync(1);

        var deleted = await productService.Delete(product);
        productsRepositoryMock.Verify(r => r.Delete(product), Times.Once());

        Assert.True(deleted);
    }

    [Theory]
    [MemberData(nameof(ProductTestData.ProductTestInstance), MemberType = typeof(ProductTestData))]
    public async Task Delete_GivenProductReturnsFalseWhenNotDeleted(Product product)
    {
        productsRepositoryMock.Setup(r => r.SaveChanges()).ReturnsAsync(0);

        var deleted = await productService.Delete(product);
        productsRepositoryMock.Verify(r => r.Delete(product), Times.Once());

        Assert.False(deleted);
    }


    [Theory]
    [MemberData(nameof(ProductTestData.ProductListTestInstance), MemberType = typeof(ProductTestData))]
    public async Task DeleteAll_GivenProductsListReturnsTrueWhenDeleted(IEnumerable<Product> products)
    {
        productsRepositoryMock.Setup(r => r.SaveChanges()).ReturnsAsync(3);

        var deleted = await productService.DeleteAll(products);
        productsRepositoryMock.Verify(r => r.DeleteRange(products), Times.Once());

        Assert.True(deleted);
    }



    [Theory]
    [MemberData(nameof(ProductTestData.ProductListTestInstance), MemberType = typeof(ProductTestData))]
    public async Task DeleteAll_GivenProductsListReturnsFalseWhenNotDeleted(IEnumerable<Product> products)
    {
        productsRepositoryMock.Setup(r => r.SaveChanges()).ReturnsAsync(0);

        var deleted = await productService.DeleteAll(products);
        productsRepositoryMock.Verify(r => r.DeleteRange(products), Times.Once());

        Assert.False(deleted);
    }

    [Theory]
    [MemberData(nameof(ProductTestData.ProductListTestInstance), MemberType = typeof(ProductTestData))]
    public async Task GetAll_ReturnsAllProducts(IEnumerable<Product> products)
    {
        productsRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(products);

        var result = await productService.GetAll();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Name == "Product One");
        productsRepositoryMock.Verify(r => r.GetAll(), Times.Once());
    }

    [Theory]
    [MemberData(nameof(ProductTestData.ProductTestInstance), MemberType = typeof(ProductTestData))]

    public async Task GetById_ReturnsProductWhitIdOne(Product product)
    {
        productsRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);
        var result = await productService.GetById(1);

        productsRepositoryMock.Verify(r => r.Get(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once());
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Same(result, product);
    }



}
