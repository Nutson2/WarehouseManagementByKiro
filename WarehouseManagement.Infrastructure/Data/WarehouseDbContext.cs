using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Infrastructure.Data;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Resource> Resources { get; set; }
    public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Balance> Balances { get; set; }
    public DbSet<ReceiptDocument> ReceiptDocuments { get; set; }
    public DbSet<ReceiptResource> ReceiptResources { get; set; }
    public DbSet<ShipmentDocument> ShipmentDocuments { get; set; }
    public DbSet<ShipmentResource> ShipmentResources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        ConfigureBaseEntity(modelBuilder);
        ConfigureResource(modelBuilder);
        ConfigureUnitOfMeasure(modelBuilder);
        ConfigureClient(modelBuilder);
        ConfigureBalance(modelBuilder);
        ConfigureReceiptDocument(modelBuilder);
        ConfigureReceiptResource(modelBuilder);
        ConfigureShipmentDocument(modelBuilder);
        ConfigureShipmentResource(modelBuilder);
    }

    private static void ConfigureBaseEntity(ModelBuilder modelBuilder)
    {
        // Configure EntityStatus enum conversion for all entities
        modelBuilder.Entity<Resource>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<UnitOfMeasure>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<Client>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<Balance>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<ReceiptDocument>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<ReceiptResource>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<ShipmentDocument>()
            .Property(e => e.Status)
            .HasConversion<int>();

        modelBuilder.Entity<ShipmentResource>()
            .Property(e => e.Status)
            .HasConversion<int>();
    }

    private static void ConfigureResource(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            // Unique constraint on Name
            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_Resources_Name_Unique");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);
        });
    }

    private static void ConfigureUnitOfMeasure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UnitOfMeasure>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            // Unique constraint on Name
            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_UnitsOfMeasure_Name_Unique");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);
        });
    }

    private static void ConfigureClient(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500);

            // Unique constraint on Name
            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_Clients_Name_Unique");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);
        });
    }

    private static void ConfigureBalance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);

            // Foreign key relationships
            entity.HasOne(e => e.Resource)
                .WithMany()
                .HasForeignKey(e => e.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.UnitOfMeasure)
                .WithMany()
                .HasForeignKey(e => e.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint on ResourceId + UnitOfMeasureId combination
            entity.HasIndex(e => new { e.ResourceId, e.UnitOfMeasureId })
                .IsUnique()
                .HasDatabaseName("IX_Balances_Resource_Unit_Unique");
        });
    }

    private static void ConfigureReceiptDocument(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReceiptDocument>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Date)
                .IsRequired();

            // Unique constraint on Number
            entity.HasIndex(e => e.Number)
                .IsUnique()
                .HasDatabaseName("IX_ReceiptDocuments_Number_Unique");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);

            // One-to-many relationship with ReceiptResources
            entity.HasMany(e => e.Resources)
                .WithOne(r => r.ReceiptDocument)
                .HasForeignKey(r => r.ReceiptDocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureReceiptResource(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReceiptResource>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);

            // Foreign key relationships
            entity.HasOne(e => e.ReceiptDocument)
                .WithMany(d => d.Resources)
                .HasForeignKey(e => e.ReceiptDocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Resource)
                .WithMany()
                .HasForeignKey(e => e.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.UnitOfMeasure)
                .WithMany()
                .HasForeignKey(e => e.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureShipmentDocument(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShipmentDocument>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Date)
                .IsRequired();

            entity.Property(e => e.DocumentStatus)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(DocumentStatus.Draft);

            // Unique constraint on Number
            entity.HasIndex(e => e.Number)
                .IsUnique()
                .HasDatabaseName("IX_ShipmentDocuments_Number_Unique");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);

            // Foreign key relationship with Client
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many relationship with ShipmentResources
            entity.HasMany(e => e.Resources)
                .WithOne(r => r.ShipmentDocument)
                .HasForeignKey(r => r.ShipmentDocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureShipmentResource(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShipmentResource>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(EntityStatus.Active);

            // Foreign key relationships
            entity.HasOne(e => e.ShipmentDocument)
                .WithMany(d => d.Resources)
                .HasForeignKey(e => e.ShipmentDocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Resource)
                .WithMany()
                .HasForeignKey(e => e.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.UnitOfMeasure)
                .WithMany()
                .HasForeignKey(e => e.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}