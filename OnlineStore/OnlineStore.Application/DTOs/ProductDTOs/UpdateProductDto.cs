namespace OnlineStore.Application.DTOs.ProductDTOs;

public class UpdateProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long CategoryId { get; set; }
}
