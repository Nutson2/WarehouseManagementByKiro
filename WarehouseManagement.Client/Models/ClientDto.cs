namespace WarehouseManagement.Client.Models;

public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Status { get; set; }
}

public class CreateClientDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class UpdateClientDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}