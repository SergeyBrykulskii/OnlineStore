namespace OnlineStore.Application.DTOs.ProductDTOs;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long CategoryId { get; set; }
}