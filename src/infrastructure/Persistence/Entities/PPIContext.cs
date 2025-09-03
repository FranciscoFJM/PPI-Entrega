namespace Persistence.Entities;

public partial class PPIContext : DbContext
{
    public PPIContext(DbContextOptions<PPIContext> options)
        : base(options) { }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<OrderState> OrderStates { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<OperationType> OperationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.ToTable("Assets", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.Ticker).HasMaxLength(100);
            entity.Property(e => e.AssetTypeId);

            entity.HasOne<AssetType>()
                .WithMany()
                .HasForeignKey(e => e.AssetTypeId)
                .HasConstraintName("FK_Assets_AssetTypes");
        });

        modelBuilder.Entity<OrderState>(entity =>
        {
            entity.ToTable("OrderStates", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders", "dbo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AccountId);
            entity.Property(e => e.Quantity);
            entity.Property(e => e.Price).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.OperationTypeId).HasMaxLength(1);
            entity.Property(e => e.StatusId);
            entity.Property(e => e.AssetId);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(16, 4)");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.ToTable("AssetTypes", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<OperationType>(entity =>
        {
            entity.ToTable("OperationTypes", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasMaxLength(1);
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
