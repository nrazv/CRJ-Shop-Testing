namespace CRJ_Shop.Models.SeedModels;

public record SeedProduct(string title, string slug, Double price, string description, string[] images, SeedCategory category);
