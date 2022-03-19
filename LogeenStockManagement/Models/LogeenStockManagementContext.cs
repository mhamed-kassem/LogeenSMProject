using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class LogeenStockManagementContext : DbContext
    {
        public LogeenStockManagementContext()
        {
        }

        public LogeenStockManagementContext(DbContextOptions<LogeenStockManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DiscountForClient> DiscountForClients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<ExpiredProduct> ExpiredProducts { get; set; }
        public virtual DbSet<ExportPayment> ExportPayments { get; set; }
        public virtual DbSet<ImportPayment> ImportPayments { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTransfered> ProductTransfereds { get; set; }
        public virtual DbSet<PurchaseBill> PurchaseBills { get; set; }
        public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        public virtual DbSet<PurchaseReturnsBill> PurchaseReturnsBills { get; set; }
        public virtual DbSet<SaleBill> SaleBills { get; set; }
        public virtual DbSet<SaleBillProduct> SaleBillProducts { get; set; }
        public virtual DbSet<SalesReturnsBill> SalesReturnsBills { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockProduct> StockProducts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<TraderType> TraderTypes { get; set; }
        public virtual DbSet<TransferOperation> TransferOperations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=.; Database=LogeenStockManagement; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "UQ__Category__737584F6589B22FE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.HasIndex(e => e.Phone, "UQ__Client__5C7E359E2E55BE4E")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasColumnType("numeric(20, 0)");

                entity.Property(e => e.TradeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("ClientTypeFK");
            });

            modelBuilder.Entity<DiscountForClient>(entity =>
            {
                entity.ToTable("DiscountForClient");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.NationalId)
                    .HasName("EmployeePK");

                entity.ToTable("Employee");

                entity.HasIndex(e => e.Phone, "UQ__Employee__5C7E359E85ECC478")
                    .IsUnique();

                entity.Property(e => e.NationalId)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("NationalID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.HaveAccess).HasColumnName("Have_Access");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasColumnType("numeric(20, 0)");

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeJobFK");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeStockFK");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expense");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExpensePayMethodFK");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExpenseStockFK");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExpenseTypeFK");
            });

            modelBuilder.Entity<ExpenseType>(entity =>
            {
                entity.ToTable("ExpenseType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpiredProduct>(entity =>
            {
                entity.ToTable("ExpiredProduct");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ExpiredProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExpiredProductTypeFK");
            });

            modelBuilder.Entity<ExportPayment>(entity =>
            {
                entity.ToTable("ExportPayment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurchaseBillCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PurchaseBillCodeNavigation)
                    .WithMany(p => p.ExportPayments)
                    .HasForeignKey(d => d.PurchaseBillCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OutPayPurchaseBillFK");
            });

            modelBuilder.Entity<ImportPayment>(entity =>
            {
                entity.ToTable("ImportPayment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SaleBillCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SaleBillCodeNavigation)
                    .WithMany(p => p.ImportPayments)
                    .HasForeignKey(d => d.SaleBillCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OutPayFK");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.HasIndex(e => e.JobTitle, "UQ__Job__44C68B9F366C41C9")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.HasIndex(e => e.Name, "UQ__PaymentM__737584F6E001D9DF")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Name, "UQ__Product__737584F69A03EF09")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductCategoryFK");
            });

            modelBuilder.Entity<ProductTransfered>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProductTransfered");

                entity.Property(e => e.ProductionDate).HasColumnType("date");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductTransferedTypeFK");

                entity.HasOne(d => d.TransferOperation)
                    .WithMany()
                    .HasForeignKey(d => d.TransferOperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductTransferOPerationFK");
            });

            modelBuilder.Entity<PurchaseBill>(entity =>
            {
                entity.HasKey(e => e.BillCode)
                    .HasName("PurchasePK");

                entity.ToTable("PurchaseBill");

                entity.Property(e => e.BillCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Paidup).HasColumnName("paidup");

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.PurchaseBills)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchasePayMethodFK");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.PurchaseBills)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseStock");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseBills)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("PurchaseSupplierFK");

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.PurchaseBills)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("PurchaseTaxFK");
            });

            modelBuilder.Entity<PurchaseProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PurchaseProduct");

                entity.Property(e => e.PurchaseBillCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseProductFK");

                entity.HasOne(d => d.PurchaseBillCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PurchaseBillCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseProductBillFK");
            });

            modelBuilder.Entity<PurchaseReturnsBill>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PurchaseReturnPK");

                entity.ToTable("PurchaseReturnsBill");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PayMethodId).HasColumnName("payMethodId");

                entity.Property(e => e.PurchaseBillCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.PurchaseReturnsBills)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseReturnPayMethodFK");

                entity.HasOne(d => d.PurchaseBillCodeNavigation)
                    .WithMany(p => p.PurchaseReturnsBills)
                    .HasForeignKey(d => d.PurchaseBillCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseReturnPurchaseBillFK");

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.PurchaseReturnsBills)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("PurchaseReturnTaxFK");
            });

            modelBuilder.Entity<SaleBill>(entity =>
            {
                entity.HasKey(e => e.BillCode)
                    .HasName("SaleBillPK");

                entity.ToTable("SaleBill");

                entity.Property(e => e.BillCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Paidup).HasColumnName("paidup");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.SaleBills)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("SaleClientFK");

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.SaleBills)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SalePayMethodFK");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.SaleBills)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleStockFK");

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.SaleBills)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("SaleTaxFK");
            });

            modelBuilder.Entity<SaleBillProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SaleBillProduct");

                entity.Property(e => e.SaleBillCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleProductTypeFK");

                entity.HasOne(d => d.SaleBillCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SaleBillCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleProductBillFK");
            });

            modelBuilder.Entity<SalesReturnsBill>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("SaleReturnPK");

                entity.ToTable("SalesReturnsBill");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SaleBillCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.SalesReturnsBills)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleReturnPayMethodFK");

                entity.HasOne(d => d.SaleBillCodeNavigation)
                    .WithMany(p => p.SalesReturnsBills)
                    .HasForeignKey(d => d.SaleBillCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleReturnSaleBillFK");

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.SalesReturnsBills)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("SaleReturnTaxFK");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.HasIndex(e => e.Name, "UQ__Stock__737584F60F9BEEFB")
                    .IsUnique();

                entity.HasIndex(e => e.Address, "UQ__Stock__7D0C3F3247A39C4E")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StockProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StockProduct");

                entity.Property(e => e.ProductionDate).HasColumnType("date");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductTypeFK");

                entity.HasOne(d => d.Stock)
                    .WithMany()
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductStockFK");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.HasIndex(e => e.Phone, "UQ__Supplier__5C7E359ECA8875A0")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Supplier__737584F6B005F3F5")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasColumnType("numeric(20, 0)");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SupplierTypeFK");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.ToTable("Tax");

                entity.HasIndex(e => e.Name, "UQ__Tax__737584F6B9EC7EDD")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TraderType>(entity =>
            {
                entity.ToTable("TraderType");

                entity.HasIndex(e => e.TypeName, "UQ__TraderTy__D4E7DFA80953F4A3")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransferOperation>(entity =>
            {
                entity.ToTable("TransferOperation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.FromStock)
                    .WithMany(p => p.TransferOperationFromStocks)
                    .HasForeignKey(d => d.FromStockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TransferFromStockFK");

                entity.HasOne(d => d.ToStock)
                    .WithMany(p => p.TransferOperationToStocks)
                    .HasForeignKey(d => d.ToStockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TransferToStockFK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
