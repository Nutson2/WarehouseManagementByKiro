namespace WarehouseManagement.Client.Models;

public class ResourceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Status { get; set; }
}

public class CreateResourceDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateResourceDto
{
    public string Name { get; set; } = string.Empty;
}