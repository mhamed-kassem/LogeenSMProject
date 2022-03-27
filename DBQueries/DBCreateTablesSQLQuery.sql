
use LogeenStockManagement
go

CREATE TABLE Stock
(
	ID INT IDENTITY(1,1),
	[Name] VARCHAR(50) NOT NULL UNIQUE,
	[Address] VARCHAR(200) NOT NULL UNIQUE,
	Constraint StockPK PRIMARY KEY (ID)
);

CREATE TABLE Job
(
  ID INT IDENTITY(1,1),
  JobTitle VARCHAR(50) NOT NULL UNIQUE,
  JobDescription VARCHAR(200),
  Constraint JobPK PRIMARY KEY (ID)
);

CREATE TABLE Category
(
  ID INT IDENTITY(1,1),
  [Name] VARCHAR(50) NOT NULL UNIQUE,
  [Description] VARCHAR(200) NOT NULL,
  Constraint CategoryPK PRIMARY KEY (ID)
);

CREATE TABLE TraderType
(
  ID INT IDENTITY(1,1),
  TypeName VARCHAR(50) NOT NULL UNIQUE,
  [Description] VARCHAR(200),
  Constraint TraderTypePK PRIMARY KEY (ID)
);

CREATE TABLE Client
(
  ID INT IDENTITY(1,1),
  [Name] VARCHAR(50) NOT NULL,
  BalanceOutstand FLOAT,
  Phone NUMERIC(20) NOT NULL UNIQUE,
  [Address] VARCHAR(200),
  TradeName VARCHAR(100),
  TypeId INT,
  Constraint ClientPK PRIMARY KEY (ID),
  Constraint ClientTypeFK FOREIGN KEY (TypeId) REFERENCES TraderType(ID)
);

CREATE TABLE DiscountForClient
(
  ID INT IDENTITY(1,1),
  DiscountValue FLOAT NOT NULL,
  Notes VARCHAR(200),
  StartDate DATE NOT NULL,
  EndDate DATE NOT NULL,
  Constraint DiscountPK PRIMARY KEY (ID)
);

CREATE TABLE PaymentMethod
(
  ID INT IDENTITY(1,1),
  Balance FLOAT,
  [Name] VARCHAR(50) NOT NULL UNIQUE,
  [Type] VARCHAR(10) NOT NULL, --TODO Check in(Bank ,MoneySafe)
  Constraint PaymentMethodPK PRIMARY KEY (ID),
  Constraint TypeCheck Check ([Type] in ('Bank','MoneySafe') )
);

CREATE TABLE Tax
(
  ID INT IDENTITY(1,1),
  [Name] VARCHAR(50) NOT NULL UNIQUE,
  [Percentage] INT NOT NULL,
  [Description] VARCHAR(200) NOT NULL,
  Constraint TaxPK PRIMARY KEY (ID)
);

CREATE TABLE ExpenseType
(
  ID INT IDENTITY(1,1),
  [Name] VARCHAR(50) NOT NULL,
  Details VARCHAR(150) NOT NULL,
  Constraint ExpenseTypePK PRIMARY KEY (ID)
);

CREATE TABLE Employee
(
  ID int IDENTITY(1,1),
  [Name] VARCHAR(100) NOT NULL,
  [Address] VARCHAR(200) NOT NULL,
  NationalID NUMERIC(20) UNIQUE,
  Phone NUMERIC(20) NOT NULL UNIQUE,
  Salary FLOAT NOT NULL,
  Photo VARCHAR(100),
  Have_Access INT,
  StockId INT NOT NULL,
  JobId INT NOT NULL,
  CONSTRAINT EmployeePK PRIMARY KEY (ID),
  CONSTRAINT EmployeeStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
  CONSTRAINT EmployeeJobFK FOREIGN KEY (JobId) REFERENCES Job(ID)
);


CREATE TABLE TransferOperation
(
  ID INT IDENTITY(1,1),
  [Date] DATE NOT NULL  Default GETDATE(), --TODO  defalt value today
  Notes VARCHAR(200),
  EmployeeId int not null,
  FromStockId INT NOT NULL,
  ToStockId INT NOT NULL,
  Constraint TransferPK PRIMARY KEY (ID),
  Constraint TransferEmployeeFK FOREIGN KEY (EmployeeId) REFERENCES Employee(ID), 
  Constraint TransferFromStockFK FOREIGN KEY (FromStockId) REFERENCES Stock(ID),
  Constraint TransferToStockFK FOREIGN KEY (ToStockId) REFERENCES Stock(ID) 
);


CREATE TABLE Supplier
(
  ID INT IDENTITY(1,1),
  [Name] VARCHAR(50) NOT NULL UNIQUE,
  Phone NUMERIC(20) NOT NULL UNIQUE,
  [Address] VARCHAR(100) NOT NULL,
  BalanceDebit FLOAT,
  TypeId INT NOT NULL,
  CONSTRAINT SupplierPK PRIMARY KEY (ID),
  CONSTRAINT SupplierTypeFK FOREIGN KEY (TypeId) REFERENCES TraderType(ID)
);

CREATE TABLE Product
(
  ID INT IDENTITY(1,1),
  [Name] VARCHAR(50) NOT NULL UNIQUE,
  [Description] VARCHAR(200),
  MiniAmount INT NOT NULL,
  Barcode VARCHAR(100),
  SellingPrice FLOAT NOT NULL,
  PurchasingPrice FLOAT NOT NULL,
  ExpiryPeriod INT NOT NULL,
  CategoryId INT NOT NULL,
  CONSTRAINT ProductPK PRIMARY KEY (ID),
  CONSTRAINT ProductCategoryFK FOREIGN KEY (CategoryId) REFERENCES Category(ID)
  --UNIQUE (Barcode) --TODO
);


CREATE TABLE SaleBill
(
  ID INT IDENTITY(1,1),
  BillCode VARCHAR(50) UNIQUE,
  [Date] DATE NOT NULL Default GETDATE(),
  BillType VARCHAR(10) NOT NULL, --TODO check in cash or Debit
  Discount FLOAT NOT NULL,
  CheckNumber VARCHAR(100) NOT NULL,
  paidup FLOAT,
  Remaining FLOAT ,
  BillTotalPrice FLOAT,
  StockId INT NOT NULL,
  PayMethodId INT NOT NULL,
  ClientId INT,
  TaxId INT,
  CONSTRAINT SaleBillPK PRIMARY KEY (ID),
  CONSTRAINT SaleStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
  CONSTRAINT SalePayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID),
  CONSTRAINT SaleClientFK FOREIGN KEY (ClientId) REFERENCES Client(ID),
  CONSTRAINT SaleTaxFK FOREIGN KEY (TaxId) REFERENCES Tax(ID)
);

CREATE TABLE SaleBillProduct
(
  ID INT IDENTITY(1,1),
  AmountToSell INT NOT NULL,
  Discount FLOAT,
  TotalPrice FLOAT,
  SaleBillId int NOT NULL,
  ProductId INT NOT NULL,
  CONSTRAINT SaleBillProductPK PRIMARY KEY (ID),
  CONSTRAINT SaleBillProductBillFK FOREIGN KEY (SaleBillId) REFERENCES SaleBill(ID),
  CONSTRAINT SaleProductTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID)
);


CREATE TABLE SalesReturnsBill
(
  ID INT IDENTITY(1,1),
  Code VARCHAR(50) UNIQUE,
  [Date] DATE NOT NULL Default GETDATE(),
  Discount FLOAT,
  NetMoney FLOAT,
  SaleBillId int NOT NULL,
  TaxId INT,
  PayMethodId INT NOT NULL,
  CONSTRAINT SaleReturnPK PRIMARY KEY (ID),
  CONSTRAINT SaleReturnSaleBillFK FOREIGN KEY (SaleBillId) REFERENCES SaleBill(ID),
  CONSTRAINT SaleReturnTaxFK FOREIGN KEY (TaxId) REFERENCES Tax(ID),
  CONSTRAINT SaleReturnPayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
);

CREATE TABLE ImportPayment
(
  ID INT IDENTITY(1,1),
  [Date] DATE NOT NULL Default GETDATE(),
  PayedBalance FLOAT NOT NULL,
  CheckNumber VARCHAR(50) NOT NULL,
  SaleBillId int NOT NULL,
  ClientId int not null,
  PayMethodId int not null,
  CONSTRAINT InPayPK PRIMARY KEY (ID),
  CONSTRAINT InPayBillFK FOREIGN KEY (SaleBillId) REFERENCES SaleBill(ID),
  CONSTRAINT InPayClientFK FOREIGN KEY (ClientId) REFERENCES Client(ID),
  CONSTRAINT InPayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
);

CREATE TABLE PurchaseBill
(
  ID INT IDENTITY(1,1),
  BillCode VARCHAR(50) UNIQUE,
  [Date] DATE NOT NULL Default GETDATE(),
  BillType VARCHAR(10) NOT NULL, --TODO check in cash or debit
  Discount FLOAT,
  paidup FLOAT NOT NULL,
  CheckNumber VARCHAR(50) NOT NULL,
  BillTotal FLOAT,
  Remaining FLOAT,
  StockId INT NOT NULL,
  TaxId INT,
  PayMethodId INT NOT NULL,
  SupplierId INT,
  CONSTRAINT PurchasePK PRIMARY KEY (ID),
  CONSTRAINT PurchaseStock FOREIGN KEY (StockId) REFERENCES Stock(ID),
  CONSTRAINT PurchaseTaxFK FOREIGN KEY (TaxId) REFERENCES Tax(ID),
  CONSTRAINT PurchasePayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID),
  CONSTRAINT PurchaseSupplierFK FOREIGN KEY (SupplierId) REFERENCES Supplier(ID),
  Constraint BillTypeCheck check(BillType in ('Cash','Debit') )
);

CREATE TABLE PurchaseProduct
(
  ID INT IDENTITY(1,1),
  Amount INT NOT NULL,
  Discount FLOAT,
  TotalPrice FLOAT NOT NULL,
  ProductId INT NOT NULL,
  PurchaseBillId int NOT NULL,
  CONSTRAINT PurchaseProductPK PRIMARY KEY (ID),
  CONSTRAINT PurchaseProductTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID),
  CONSTRAINT PurchaseProductBillFK FOREIGN KEY (PurchaseBillId) REFERENCES PurchaseBill(ID)
);

CREATE TABLE Expense
(
  ID INT IDENTITY(1,1),
  [Date] DATE NOT NULL Default GETDATE(), --TODO default today
  Notes VARCHAR(200),
  [Value] FLOAT NOT NULL,
  CheckNumber VARCHAR(50) NOT NULL,
  StockId INT NOT NULL,
  EmployeeId int,
  TypeId INT NOT NULL,
  PayMethodId INT NOT NULL,
  CONSTRAINT ExpensePK PRIMARY KEY (ID),
  CONSTRAINT ExpenseStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
  CONSTRAINT ExpenseEmployeFK FOREIGN KEY (EmployeeId) REFERENCES Employee(ID),
  CONSTRAINT ExpenseTypeFK FOREIGN KEY (TypeId) REFERENCES ExpenseType(ID),
  CONSTRAINT ExpensePayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
);

CREATE TABLE PurchaseReturnsBill
(
  ID INT IDENTITY(1,1),
  Code VARCHAR(50) UNIQUE,
  [Date] DATE NOT NULL Default GETDATE(),
  NetMoney FLOAT,
  Discount FLOAT,
  PurchaseBillId int NOT NULL,
  TaxId INT,
  payMethodId INT NOT NULL,
  CONSTRAINT PurchaseReturnPK PRIMARY KEY (ID),
  CONSTRAINT PurchaseReturnPurchaseBillFK FOREIGN KEY (PurchaseBillId) REFERENCES PurchaseBill(ID),
  CONSTRAINT PurchaseReturnTaxFK FOREIGN KEY (TaxId) REFERENCES Tax(ID),
  CONSTRAINT PurchaseReturnPayMethodFK FOREIGN KEY (payMethodId) REFERENCES PaymentMethod(ID)
);

CREATE TABLE ExportPayment
(
  ID INT IDENTITY(1,1),
  [Date] DATE NOT NULL Default GETDATE(),
  PayedBalance FLOAT NOT NULL,
  CheckNumber VARCHAR(50) NOT NULL,
  PurchaseBillId int NOT NULL,
  SupplierId int not null,
  PayMethodId int not null,
  CONSTRAINT OutPayPK PRIMARY KEY (ID),
  CONSTRAINT OutPayPurchaseBillFK FOREIGN KEY (PurchaseBillId) REFERENCES PurchaseBill(ID),
  CONSTRAINT OutPaySupplierFK FOREIGN KEY (SupplierId) REFERENCES Supplier(ID),
  CONSTRAINT OutPayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
);

CREATE TABLE ProductTransfered
(
  ID INT IDENTITY(1,1),
  Amount INT NOT NULL,
  ProductionDate DATE NOT NULL, --logic bring it from store products table or manully
  ProductId INT NOT NULL,
  TransferOperationId INT NOT NULL,
  CONSTRAINT ProductTransferedPK PRIMARY KEY (ID),
  CONSTRAINT ProductTransferedTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID),
  CONSTRAINT ProductTransferOPerationFK FOREIGN KEY (TransferOperationId) REFERENCES TransferOperation(ID),
);

CREATE TABLE StockProduct
(
  ID INT IDENTITY(1,1),
  Amount INT NOT NULL,
  ProductionDate DATE NOT NULL,
  StockId INT NOT NULL,
  ProductId INT NOT NULL,
  CONSTRAINT StockProductPK PRIMARY KEY (ID),
  CONSTRAINT ProductStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
  CONSTRAINT ProductTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID)
);

CREATE TABLE ExpiredProduct
(
  ID INT IDENTITY(1,1),
  Amount INT NOT NULL,
  DateAdded DATE Default GETDATE(),
  Notes VARCHAR(200),
  ProductId INT NOT NULL,
  CONSTRAINT ExpiredProductPK PRIMARY KEY (ID),
  CONSTRAINT ExpiredProductTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID)
);