
use master
go

Create Database LogeenStockManagement
on
 (
	NAME='LogeenStocks',
	FILENAME=N'D:\ITI dot net\Graduation Project\DBFiles\LogeenStocks.mdf',
	SIZE=100MB,
	FILEGROWTH=100MB
 )
LOG ON
 (
	NAME='Stock_log',
	FILENAME=N'D:\ITI dot net\Graduation Project\DBFiles\LogeenStocks_log.ldf',
	SIZE=100MB,
	FILEGROWTH=50MB
 );