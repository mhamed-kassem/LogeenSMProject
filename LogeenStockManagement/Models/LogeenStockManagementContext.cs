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

                entity.HasIndex(e => e.Name, "UQ__Category__737584F6F0171D11")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasIndex(e => e.Phone, "UQ__Client__5C7E359EAD867081")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.Phone, "UQ__Employee__5C7E359EA91876DA")
                    .IsUnique();

                entity.HasIndex(e => e.NationalId, "UQ__Employee__E9AA321A59B85CC1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.HaveAccess).HasColumnName("Have_Access");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NationalId)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("NationalID");

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

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("ExpenseEmployeFK");

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

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.ExportPayments)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OutPayMethodFK");

                entity.HasOne(d => d.PurchaseBill)
                    .WithMany(p => p.ExportPayments)
                    .HasForeignKey(d => d.PurchaseBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OutPayPurchaseBillFK");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ExportPayments)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OutPaySupplierFK");
            });

            modelBuilder.Entity<ImportPayment>(entity =>
            {
                entity.ToTable("ImportPayment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ImportPayments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InPayClientFK");

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.ImportPayments)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InPayMethodFK");

                entity.HasOne(d => d.SaleBill)
                    .WithMany(p => p.ImportPayments)
                    .HasForeignKey(d => d.SaleBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InPayBillFK");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.HasIndex(e => e.JobTitle, "UQ__Job__44C68B9F7797CA43")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasIndex(e => e.Name, "UQ__PaymentM__737584F68060D9A9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasIndex(e => e.Name, "UQ__Product__737584F6BC80717D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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
                entity.ToTable("ProductTransfered");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductionDate).HasColumnType("date");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTransfereds)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductTransferedTypeFK");

                entity.HasOne(d => d.TransferOperation)
                    .WithMany(p => p.ProductTransfereds)
                    .HasForeignKey(d => d.TransferOperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductTransferOPerationFK");
            });

            modelBuilder.Entity<PurchaseBill>(entity =>
            {
                entity.ToTable("PurchaseBill");

                entity.HasIndex(e => e.BillCode, "UQ__Purchase__1CC9F83E29B160CD")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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
                entity.ToTable("PurchaseProduct");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseProductTypeFK");

                entity.HasOne(d => d.PurchaseBill)
                    .WithMany(p => p.PurchaseProducts)
                    .HasForeignKey(d => d.PurchaseBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseProductBillFK");
            });

            modelBuilder.Entity<PurchaseReturnsBill>(entity =>
            {
                entity.ToTable("PurchaseReturnsBill");

                entity.HasIndex(e => e.Code, "UQ__Purchase__A25C5AA7A7A73F46")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PayMethodId).HasColumnName("payMethodId");

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.PurchaseReturnsBills)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseReturnPayMethodFK");

                entity.HasOne(d => d.PurchaseBill)
                    .WithMany(p => p.PurchaseReturnsBills)
                    .HasForeignKey(d => d.PurchaseBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseReturnPurchaseBillFK");

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.PurchaseReturnsBills)
                    .HasForeignKey(d => d.TaxId)
                    .HasConstraintName("PurchaseReturnTaxFK");
            });

            modelBuilder.Entity<SaleBill>(entity =>
            {
                entity.ToTable("SaleBill");

                entity.HasIndex(e => e.BillCode, "UQ__SaleBill__1CC9F83E88BECC50")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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
                entity.ToTable("SaleBillProduct");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SaleBillProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleProductTypeFK");

                entity.HasOne(d => d.SaleBill)
                    .WithMany(p => p.SaleBillProducts)
                    .HasForeignKey(d => d.SaleBillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleBillProductBillFK");
            });

            modelBuilder.Entity<SalesReturnsBill>(entity =>
            {
                entity.ToTable("SalesReturnsBill");

                entity.HasIndex(e => e.Code, "UQ__SalesRet__A25C5AA70B5443F8")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.PayMethod)
                    .WithMany(p => p.SalesReturnsBills)
                    .HasForeignKey(d => d.PayMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SaleReturnPayMethodFK");

                entity.HasOne(d => d.SaleBill)
                    .WithMany(p => p.SalesReturnsBills)
                    .HasForeignKey(d => d.SaleBillId)
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

                entity.HasIndex(e => e.Name, "UQ__Stock__737584F6F6B94B0F")
                    .IsUnique();

                entity.HasIndex(e => e.Address, "UQ__Stock__7D0C3F322F23BC8A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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
                entity.ToTable("StockProduct");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductionDate).HasColumnType("date");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StockProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductTypeFK");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockProducts)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductStockFK");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.HasIndex(e => e.Phone, "UQ__Supplier__5C7E359E6642A318")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Supplier__737584F61E5160D6")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasIndex(e => e.Name, "UQ__Tax__737584F6CA5B32B0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasIndex(e => e.TypeName, "UQ__TraderTy__D4E7DFA8A2818C9A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TransferOperations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TransferEmployeeFK");

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
