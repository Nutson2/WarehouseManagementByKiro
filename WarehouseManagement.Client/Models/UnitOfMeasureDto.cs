namespace WarehouseManagement.Client.Models;

public class UnitOfMeasureDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Status { get; set; }
}

public class CreateUnitOfMeasureDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateUnitOfMeasureDto
{
    public string Name { get; set; } = string.Empty;
}