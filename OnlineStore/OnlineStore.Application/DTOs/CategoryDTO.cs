namespace OnlineStore.Application.DTOs;

//Dto for obtaining information about category/all categories
public class CategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; }
}

public class CreateCategoryDto
{
    public string Name { get; set; }
}

public class UpdateCategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; }
}