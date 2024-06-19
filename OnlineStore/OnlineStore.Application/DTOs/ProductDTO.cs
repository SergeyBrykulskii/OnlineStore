namespace OnlineStore.Application.DTOs;

//Dto for obtaining information about all products/products by specific category
public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}

//Dto for obtaining detailed information about a product by ID
public class ProductDetailDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long CategoryId { get; set; }
}

//Dto for creating a product
public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long CategoryId { get; set; }
}

//Dto for updating a product
public class UpdateProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long CategoryId { get; set; }
}