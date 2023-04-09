use [SalesWebMvcAdo]


--Criar DataBase
CREATE DATABASE [SalesWebMvcAdo]

--Deletar DataBase
DROP DATABASE [SalesWebMvcAdo]



--Criar Tabelas
CREATE TABLE [tb_department](
Id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(50) NOT NULL,
);



CREATE TABLE [tb_seller](
Id INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(50) NOT NULL,
Email VARCHAR(50),
BirthDate DATE,
BaseSalary DECIMAL(6,2)NOT NULL,
DepartmentId INT NOT NULL,
CONSTRAINT FK_Seller_Department_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES [tb_department] (Id)
);



CREATE TABLE [tb_saleStatus](
Id INT PRIMARY KEY IDENTITY(0,1),
Name VARCHAR(50) NOT NULL
);


CREATE TABLE [tb_salesRecord](
Id INT PRIMARY KEY IDENTITY(1,1),
Date DATE,
Amount DECIMAL(6,2) NOT NULL,
Status INT NOT NULL,
CONSTRAINT FK_SalesRecord_SaleStatus_StatusId FOREIGN KEY (Status) REFERENCES [tb_saleStatus](Id),
SellerId INT NOT NULL,
CONSTRAINT FK_SalesRecord_Seller_SellerId FOREIGN KEY (SellerId) REFERENCES [tb_seller] (Id)
);




-- inserir -- dentro -- tabela -> campos 
insert into tb_department(Name) Values('Computers');
insert into tb_department(Name) Values('Eletronics');


insert into tb_seller(Name, Email, BirthDate, BaseSalary, DepartmentId) Values ('Eddy', 'Eddy@gmail.com', '1988-12-24', 5000, 1);
insert into tb_seller(Name, Email, BirthDate, BaseSalary, DepartmentId) Values ('João', 'João@gmail.com', '1999-10-29', 4500, 2);


insert into tb_saleStatus(Name) Values ('Pending');
insert into tb_saleStatus(Name) Values ('Billed');
insert into tb_saleStatus(Name) Values ('Canceled');


insert into tb_salesRecord(Date, Amount, Status, SellerId) Values ('2018-09-25', 1100.00, 0, 2);
insert into tb_salesRecord(Date, Amount, Status, SellerId) Values ('2018-09-26', 5100.00, 0, 1);
insert into tb_salesRecord(Date, Amount, Status, SellerId) Values ('2018-09-27', 4100.00, 0, 2);
insert into tb_salesRecord(Date, Amount, Status, SellerId) Values ('2018-09-28', 5500.00, 1, 1);
insert into tb_salesRecord(Date, Amount, Status, SellerId) Values ('2018-09-29', 7500.00, 1, 2);
insert into tb_salesRecord(Date, Amount, Status, SellerId) Values ('2018-09-30', 3400.00, 2, 1);




--exemplos

    CONSTRAINT fk_PesCarro FOREIGN KEY (DepartmentId) REFERENCES Pessoa (ID_Pessoa)
	
	select * from tb_department

	select * from tb_seller

	select * from tb_salesRecord

	select * from tb_saleStatus

	select Name, Email, BaseSalary from tb_seller


	select  tb_seller.Name,
			tb_seller.Email,
			tb_department.Name,
			tb_department.DepartmentId
	from tb_seller
	inner join tb_department
	on tb_seller.DepartmentId = tb_department.Id

		select d.Id,
		       s.Name,
			   d.Name,
			   s.Email 
			   from tb_seller s
	inner join tb_department d
	on s.DepartmentId = d.Id

-- join = juntar Seller com departmento    
		   
    select s.Id[IdVendedor],
	       s.Name,
	       d.Id[IdVenda],
		   d.Date,
		   d.Amount,
		   d.Status,
		   t.Name
		   
	       from tb_seller s
		   inner join tb_salesRecord d
		   
		   on s.DepartmentId = d.SellerId
		   inner join tb_saleStatus t
		   on  t.Id = d.Status