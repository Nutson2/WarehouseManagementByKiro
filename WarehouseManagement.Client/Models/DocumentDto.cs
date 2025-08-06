namespace WarehouseManagement.Client.Models;

public class ReceiptDocumentDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<ReceiptResourceDto> Resources { get; set; } = new();
}

public class ReceiptResourceDto
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public int UnitOfMeasureId { get; set; }
    public string UnitOfMeasureName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}

public class CreateReceiptDocumentDto
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<CreateReceiptResourceDto> Resources { get; set; } = new();
}

public class CreateReceiptResourceDto
{
    public int ResourceId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public decimal Quantity { get; set; }
}

public class UpdateReceiptDocumentDto
{
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<UpdateReceiptResourceDto> Resources { get; set; } = new();
}

public class UpdateReceiptResourceDto
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public decimal Quantity { get; set; }
}

public class ShipmentDocumentDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int Status { get; set; }
    public List<ShipmentResourceDto> Resources { get; set; } = new();
}

public class ShipmentResourceDto
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public int UnitOfMeasureId { get; set; }
    public string UnitOfMeasureName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}

public class CreateShipmentDocumentDto
{
    public string Number { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public List<CreateShipmentResourceDto> Resources { get; set; } = new();
}

public class CreateShipmentResourceDto
{
    public int ResourceId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public decimal Quantity { get; set; }
}

public class UpdateShipmentDocumentDto
{
    public string Number { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public List<UpdateShipmentResourceDto> Resources { get; set; } = new();
}

public class UpdateShipmentResourceDto
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public decimal Quantity { get; set; }
}