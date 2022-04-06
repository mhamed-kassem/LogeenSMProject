using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LogeenStockManagement.Migrations
{
    public partial class createusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Category",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Category", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Discount",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        DiscountValue = table.Column<double>(type: "float", nullable: false),
            //        Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        UnitCount = table.Column<int>(type: "int", nullable: true),
            //        StartDate = table.Column<DateTime>(type: "date", nullable: false),
            //        EndDate = table.Column<DateTime>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Discount", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpenseType",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Details = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpenseType", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Job",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        JobDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Job", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PaymentMethod",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Balance = table.Column<double>(type: "float", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValueSql: "('MoneySafe')")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PaymentMethod", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Stock",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Stock", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tax",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Percentage = table.Column<int>(type: "int", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tax", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TraderType",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TraderType", x => x.ID);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        MiniAmount = table.Column<int>(type: "int", nullable: false),
            //        Barcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        SellingPrice = table.Column<double>(type: "float", nullable: false),
            //        PurchasingPrice = table.Column<double>(type: "float", nullable: false),
            //        ExpiryPeriod = table.Column<int>(type: "int", nullable: false),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        DiscountId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.ID);
            //        table.ForeignKey(
            //            name: "ProductCategoryFK",
            //            column: x => x.CategoryId,
            //            principalTable: "Category",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ProductDiscountFK",
            //            column: x => x.DiscountId,
            //            principalTable: "Discount",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Employee",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        NationalID = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
            //        Phone = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
            //        Salary = table.Column<double>(type: "float", nullable: false),
            //        Photo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Have_Access = table.Column<bool>(type: "bit", nullable: false),
            //        StockId = table.Column<int>(type: "int", nullable: false),
            //        JobId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Employee", x => x.ID);
            //        table.ForeignKey(
            //            name: "EmployeeJobFK",
            //            column: x => x.JobId,
            //            principalTable: "Job",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "EmployeeStockFK",
            //            column: x => x.StockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Client",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        BalanceOutstand = table.Column<double>(type: "float", nullable: true),
            //        Phone = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        TradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        TypeId = table.Column<int>(type: "int", nullable: true),
            //        DiscountId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Client", x => x.ID);
            //        table.ForeignKey(
            //            name: "ClientDiscountFK",
            //            column: x => x.DiscountId,
            //            principalTable: "Discount",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ClientTypeFK",
            //            column: x => x.TypeId,
            //            principalTable: "TraderType",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Supplier",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        BalanceDebit = table.Column<double>(type: "float", nullable: true),
            //        TypeId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Supplier", x => x.ID);
            //        table.ForeignKey(
            //            name: "SupplierTypeFK",
            //            column: x => x.TypeId,
            //            principalTable: "TraderType",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpiredProduct",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        DateAdded = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
            //        Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        ProductId = table.Column<int>(type: "int", nullable: true),
            //        ProductionDate = table.Column<DateTime>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpiredProduct", x => x.ID);
            //        table.ForeignKey(
            //            name: "ExpiredProductTypeFK",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "StockProduct",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        ProductionDate = table.Column<DateTime>(type: "date", nullable: false),
            //        StockId = table.Column<int>(type: "int", nullable: true),
            //        ProductId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StockProduct", x => x.ID);
            //        table.ForeignKey(
            //            name: "ProductStockFK",
            //            column: x => x.StockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ProductTypeFK",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Expense",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
            //        Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        Value = table.Column<double>(type: "float", nullable: false),
            //        CheckNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        StockId = table.Column<int>(type: "int", nullable: true),
            //        EmployeeId = table.Column<int>(type: "int", nullable: true),
            //        TypeId = table.Column<int>(type: "int", nullable: true),
            //        PayMethodId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Expense", x => x.ID);
            //        table.ForeignKey(
            //            name: "ExpenseEmployeFK",
            //            column: x => x.EmployeeId,
            //            principalTable: "Employee",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ExpensePayMethodFK",
            //            column: x => x.PayMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ExpenseStockFK",
            //            column: x => x.StockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ExpenseTypeFK",
            //            column: x => x.TypeId,
            //            principalTable: "ExpenseType",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TransferOperation",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        EmployeeId = table.Column<int>(type: "int", nullable: false),
            //        FromStockId = table.Column<int>(type: "int", nullable: false),
            //        ToStockId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TransferOperation", x => x.ID);
            //        table.ForeignKey(
            //            name: "TransferEmployeeFK",
            //            column: x => x.EmployeeId,
            //            principalTable: "Employee",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "TransferFromStockFK",
            //            column: x => x.FromStockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "TransferToStockFK",
            //            column: x => x.ToStockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SaleBill",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BillCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        BillType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValueSql: "('Cash')"),
            //        Discount = table.Column<double>(type: "float", nullable: false),
            //        CheckNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        paidup = table.Column<double>(type: "float", nullable: true),
            //        Remaining = table.Column<double>(type: "float", nullable: true),
            //        BillTotalPrice = table.Column<double>(type: "float", nullable: true),
            //        StockId = table.Column<int>(type: "int", nullable: false),
            //        PayMethodId = table.Column<int>(type: "int", nullable: false),
            //        ClientId = table.Column<int>(type: "int", nullable: true),
            //        TaxId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SaleBill", x => x.ID);
            //        table.ForeignKey(
            //            name: "SaleClientFK",
            //            column: x => x.ClientId,
            //            principalTable: "Client",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SalePayMethodFK",
            //            column: x => x.PayMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SaleStockFK",
            //            column: x => x.StockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SaleTaxFK",
            //            column: x => x.TaxId,
            //            principalTable: "Tax",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PurchaseBill",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BillCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        BillType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValueSql: "('Cash')"),
            //        Discount = table.Column<double>(type: "float", nullable: true),
            //        paidup = table.Column<double>(type: "float", nullable: false),
            //        CheckNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        BillTotal = table.Column<double>(type: "float", nullable: true),
            //        Remaining = table.Column<double>(type: "float", nullable: true),
            //        StockId = table.Column<int>(type: "int", nullable: false),
            //        TaxId = table.Column<int>(type: "int", nullable: true),
            //        PayMethodId = table.Column<int>(type: "int", nullable: false),
            //        SupplierId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PurchaseBill", x => x.ID);
            //        table.ForeignKey(
            //            name: "PurchasePayMethodFK",
            //            column: x => x.PayMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "PurchaseStock",
            //            column: x => x.StockId,
            //            principalTable: "Stock",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "PurchaseSupplierFK",
            //            column: x => x.SupplierId,
            //            principalTable: "Supplier",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "PurchaseTaxFK",
            //            column: x => x.TaxId,
            //            principalTable: "Tax",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "ProductTransfered",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        ProductionDate = table.Column<DateTime>(type: "date", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        TransferOperationId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductTransfered", x => x.ID);
            //        table.ForeignKey(
            //            name: "ProductTransferedTypeFK",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "ProductTransferOPerationFK",
            //            column: x => x.TransferOperationId,
            //            principalTable: "TransferOperation",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ImportPayment",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        PayedBalance = table.Column<double>(type: "float", nullable: false),
            //        CheckNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        SaleBillId = table.Column<int>(type: "int", nullable: false),
            //        ClientId = table.Column<int>(type: "int", nullable: false),
            //        PayMethodId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ImportPayment", x => x.ID);
            //        table.ForeignKey(
            //            name: "InPayBillFK",
            //            column: x => x.SaleBillId,
            //            principalTable: "SaleBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "InPayClientFK",
            //            column: x => x.ClientId,
            //            principalTable: "Client",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "InPayMethodFK",
            //            column: x => x.PayMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SaleBillProduct",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AmountToSell = table.Column<int>(type: "int", nullable: false),
            //        Discount = table.Column<double>(type: "float", nullable: true),
            //        ProductionDate = table.Column<DateTime>(type: "date", nullable: false),
            //        TotalPrice = table.Column<double>(type: "float", nullable: true),
            //        SaleBillId = table.Column<int>(type: "int", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SaleBillProduct", x => x.ID);
            //        table.ForeignKey(
            //            name: "SaleBillProductBillFK",
            //            column: x => x.SaleBillId,
            //            principalTable: "SaleBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SaleProductTypeFK",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SalesReturnsBill",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
            //        NetMoney = table.Column<double>(type: "float", nullable: true),
            //        SaleBillId = table.Column<int>(type: "int", nullable: false),
            //        TaxId = table.Column<int>(type: "int", nullable: true),
            //        PayMethodId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SalesReturnsBill", x => x.ID);
            //        table.ForeignKey(
            //            name: "SaleReturnPayMethodFK",
            //            column: x => x.PayMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SaleReturnSaleBillFK",
            //            column: x => x.SaleBillId,
            //            principalTable: "SaleBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SaleReturnTaxFK",
            //            column: x => x.TaxId,
            //            principalTable: "Tax",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExportPayment",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        PayedBalance = table.Column<double>(type: "float", nullable: false),
            //        CheckNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        PurchaseBillId = table.Column<int>(type: "int", nullable: false),
            //        SupplierId = table.Column<int>(type: "int", nullable: false),
            //        PayMethodId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExportPayment", x => x.ID);
            //        table.ForeignKey(
            //            name: "OutPayMethodFK",
            //            column: x => x.PayMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "OutPayPurchaseBillFK",
            //            column: x => x.PurchaseBillId,
            //            principalTable: "PurchaseBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "OutPaySupplierFK",
            //            column: x => x.SupplierId,
            //            principalTable: "Supplier",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PurchaseProduct",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        Discount = table.Column<double>(type: "float", nullable: true),
            //        price = table.Column<double>(type: "float", nullable: true),
            //        TotalPrice = table.Column<double>(type: "float", nullable: false),
            //        ProductionDate = table.Column<DateTime>(type: "date", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        PurchaseBillId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PurchaseProduct", x => x.ID);
            //        table.ForeignKey(
            //            name: "PurchaseProductBillFK",
            //            column: x => x.PurchaseBillId,
            //            principalTable: "PurchaseBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "PurchaseProductTypeFK",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PurchaseReturnsBill",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        NetMoney = table.Column<double>(type: "float", nullable: true),
            //        PurchaseBillId = table.Column<int>(type: "int", nullable: false),
            //        TaxId = table.Column<int>(type: "int", nullable: true),
            //        payMethodId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PurchaseReturnsBill", x => x.ID);
            //        table.ForeignKey(
            //            name: "PurchaseReturnPayMethodFK",
            //            column: x => x.payMethodId,
            //            principalTable: "PaymentMethod",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "PurchaseReturnPurchaseBillFK",
            //            column: x => x.PurchaseBillId,
            //            principalTable: "PurchaseBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "PurchaseReturnTaxFK",
            //            column: x => x.TaxId,
            //            principalTable: "Tax",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReturnSaleProduct",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AmountReturned = table.Column<int>(type: "int", nullable: false),
            //        SaleProductId = table.Column<int>(type: "int", nullable: true),
            //        ReturnSaleBillId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReturnSaleProduct", x => x.ID);
            //        table.ForeignKey(
            //            name: "ReturnSaleBillFK",
            //            column: x => x.ReturnSaleBillId,
            //            principalTable: "SalesReturnsBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "SaleProductFK",
            //            column: x => x.SaleProductId,
            //            principalTable: "SaleBillProduct",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReturnPurchaseProduct",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AmountReturned = table.Column<int>(type: "int", nullable: false),
            //        PurchaseProductId = table.Column<int>(type: "int", nullable: true),
            //        ReturnPurchaseBillId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReturnPurchaseProduct", x => x.ID);
            //        table.ForeignKey(
            //            name: "PurchaseProductToReturnFK",
            //            column: x => x.PurchaseProductId,
            //            principalTable: "PurchaseProduct",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "ReturnPurchaseBillFK",
            //            column: x => x.ReturnPurchaseBillId,
            //            principalTable: "PurchaseReturnsBill",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Category__737584F6080FF870",
            //    table: "Category",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Client_DiscountId",
            //    table: "Client",
            //    column: "DiscountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Client_TypeId",
            //    table: "Client",
            //    column: "TypeId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Client__5C7E359ECC686610",
            //    table: "Client",
            //    column: "Phone",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employee_JobId",
            //    table: "Employee",
            //    column: "JobId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employee_StockId",
            //    table: "Employee",
            //    column: "StockId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Employee__5C7E359E8C71BBFF",
            //    table: "Employee",
            //    column: "Phone",
            //    unique: true,
            //    filter: "[Phone] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Employee__E9AA321ADDC4258D",
            //    table: "Employee",
            //    column: "NationalID",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Expense_EmployeeId",
            //    table: "Expense",
            //    column: "EmployeeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Expense_PayMethodId",
            //    table: "Expense",
            //    column: "PayMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Expense_StockId",
            //    table: "Expense",
            //    column: "StockId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Expense_TypeId",
            //    table: "Expense",
            //    column: "TypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExpiredProduct_ProductId",
            //    table: "ExpiredProduct",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExportPayment_PayMethodId",
            //    table: "ExportPayment",
            //    column: "PayMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExportPayment_PurchaseBillId",
            //    table: "ExportPayment",
            //    column: "PurchaseBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExportPayment_SupplierId",
            //    table: "ExportPayment",
            //    column: "SupplierId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ImportPayment_ClientId",
            //    table: "ImportPayment",
            //    column: "ClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ImportPayment_PayMethodId",
            //    table: "ImportPayment",
            //    column: "PayMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ImportPayment_SaleBillId",
            //    table: "ImportPayment",
            //    column: "SaleBillId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Job__44C68B9F7F6246B3",
            //    table: "Job",
            //    column: "JobTitle",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__PaymentM__737584F6D15096D9",
            //    table: "PaymentMethod",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_CategoryId",
            //    table: "Product",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_DiscountId",
            //    table: "Product",
            //    column: "DiscountId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Product__177800D366E65269",
            //    table: "Product",
            //    column: "Barcode",
            //    unique: true,
            //    filter: "[Barcode] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Product__737584F6B538FB8E",
            //    table: "Product",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductTransfered_ProductId",
            //    table: "ProductTransfered",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductTransfered_TransferOperationId",
            //    table: "ProductTransfered",
            //    column: "TransferOperationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseBill_PayMethodId",
            //    table: "PurchaseBill",
            //    column: "PayMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseBill_StockId",
            //    table: "PurchaseBill",
            //    column: "StockId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseBill_SupplierId",
            //    table: "PurchaseBill",
            //    column: "SupplierId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseBill_TaxId",
            //    table: "PurchaseBill",
            //    column: "TaxId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Purchase__1CC9F83EA0F070C4",
            //    table: "PurchaseBill",
            //    column: "BillCode",
            //    unique: true,
            //    filter: "[BillCode] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseProduct_ProductId",
            //    table: "PurchaseProduct",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseProduct_PurchaseBillId",
            //    table: "PurchaseProduct",
            //    column: "PurchaseBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseReturnsBill_payMethodId",
            //    table: "PurchaseReturnsBill",
            //    column: "payMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseReturnsBill_PurchaseBillId",
            //    table: "PurchaseReturnsBill",
            //    column: "PurchaseBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseReturnsBill_TaxId",
            //    table: "PurchaseReturnsBill",
            //    column: "TaxId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Purchase__A25C5AA7BFF1D1B6",
            //    table: "PurchaseReturnsBill",
            //    column: "Code",
            //    unique: true,
            //    filter: "[Code] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReturnPurchaseProduct_PurchaseProductId",
            //    table: "ReturnPurchaseProduct",
            //    column: "PurchaseProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReturnPurchaseProduct_ReturnPurchaseBillId",
            //    table: "ReturnPurchaseProduct",
            //    column: "ReturnPurchaseBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReturnSaleProduct_ReturnSaleBillId",
            //    table: "ReturnSaleProduct",
            //    column: "ReturnSaleBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReturnSaleProduct_SaleProductId",
            //    table: "ReturnSaleProduct",
            //    column: "SaleProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleBill_ClientId",
            //    table: "SaleBill",
            //    column: "ClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleBill_PayMethodId",
            //    table: "SaleBill",
            //    column: "PayMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleBill_StockId",
            //    table: "SaleBill",
            //    column: "StockId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleBill_TaxId",
            //    table: "SaleBill",
            //    column: "TaxId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__SaleBill__1CC9F83E0ACB23B2",
            //    table: "SaleBill",
            //    column: "BillCode",
            //    unique: true,
            //    filter: "[BillCode] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleBillProduct_ProductId",
            //    table: "SaleBillProduct",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SaleBillProduct_SaleBillId",
            //    table: "SaleBillProduct",
            //    column: "SaleBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SalesReturnsBill_PayMethodId",
            //    table: "SalesReturnsBill",
            //    column: "PayMethodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SalesReturnsBill_SaleBillId",
            //    table: "SalesReturnsBill",
            //    column: "SaleBillId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SalesReturnsBill_TaxId",
            //    table: "SalesReturnsBill",
            //    column: "TaxId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__SalesRet__A25C5AA71E2BDB20",
            //    table: "SalesReturnsBill",
            //    column: "Code",
            //    unique: true,
            //    filter: "[Code] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Stock__737584F658277066",
            //    table: "Stock",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Stock__7D0C3F3299E766D4",
            //    table: "Stock",
            //    column: "Address",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_StockProduct_ProductId",
            //    table: "StockProduct",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "UniqueProduct",
            //    table: "StockProduct",
            //    columns: new[] { "StockId", "ProductId", "ProductionDate" },
            //    unique: true,
            //    filter: "[StockId] IS NOT NULL AND [ProductId] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Supplier_TypeId",
            //    table: "Supplier",
            //    column: "TypeId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Supplier__5C7E359EACB37D5D",
            //    table: "Supplier",
            //    column: "Phone",
            //    unique: true,
            //    filter: "[Phone] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Supplier__737584F65B86B2E0",
            //    table: "Supplier",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Tax__737584F6044960F3",
            //    table: "Tax",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__TraderTy__D4E7DFA8847B954C",
            //    table: "TraderType",
            //    column: "TypeName",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransferOperation_EmployeeId",
            //    table: "TransferOperation",
            //    column: "EmployeeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransferOperation_FromStockId",
            //    table: "TransferOperation",
            //    column: "FromStockId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransferOperation_ToStockId",
            //    table: "TransferOperation",
            //    column: "ToStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "ExpiredProduct");

            migrationBuilder.DropTable(
                name: "ExportPayment");

            migrationBuilder.DropTable(
                name: "ImportPayment");

            migrationBuilder.DropTable(
                name: "ProductTransfered");

            migrationBuilder.DropTable(
                name: "ReturnPurchaseProduct");

            migrationBuilder.DropTable(
                name: "ReturnSaleProduct");

            migrationBuilder.DropTable(
                name: "StockProduct");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExpenseType");

            migrationBuilder.DropTable(
                name: "TransferOperation");

            migrationBuilder.DropTable(
                name: "PurchaseProduct");

            migrationBuilder.DropTable(
                name: "PurchaseReturnsBill");

            migrationBuilder.DropTable(
                name: "SalesReturnsBill");

            migrationBuilder.DropTable(
                name: "SaleBillProduct");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "PurchaseBill");

            migrationBuilder.DropTable(
                name: "SaleBill");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "TraderType");
        }
    }
}
