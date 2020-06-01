use QLBH;
exec searchByCode 1
select * from Users
alter proc getProductByCode
(
@code int 
)

as
begin
select Product.id as "Id sản phẩm", Product.name as "Tên sản phẩm" , Product.price as "Giá sản phẩm",Product.code as "Mã sản phẩm" , Category.Name as "Tên danh mục" ,Product.image as "Ảnh sản phẩm" from Product
join Category on Product.cat_id = Category.id  where Product.code = @code
END
exec getAttrByProductId 31
drop table Bill;
select * from Product
drop table BillDetail;
drop table Bill
create table Bill
(
id int identity primary key,
total float ,
maHoaDon nvarchar(255),
userPhone varchar(255),
create_at TIMESTAMP,
adminPhone varchar(255) 
)
go 
create table BillDetail
(
id int identity primary key,
product_id int FOREIGN KEY(product_id) REFERENCES Product(id),
bill_id int FOREIGN KEY(bill_id) REFERENCES Bill(id),
quantity int not null,

created_at TIMESTAMP
)
select * from Bill;
select * from BillDetail
delete from Bill
delete from BillDetail
alter proc getBillDetailbyBill1(
@BillId int
)
as
begin
select Product.code ,Product.name ,Category.Name as "Mame cate", Product.price, BillDetail.quantity from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
where BillDetail.bill_id = @BillId
END

exec getBillDetailbyBill1 1



alter proc getPrice1(
@code int

)
as
declare @price int
begin
select Product.price , BillDetail.quantity  from Product join BillDetail on Product.id = BillDetail.product_id 
where Billdetail.bill_id = @code

end

exec getPrice1  1 