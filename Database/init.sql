CREATE TABLE Products(
	"Id" SERIAL PRIMARY KEY
	,"Quantity" INTEGER
	,"Price" DECIMAL(10,2)
	,"Title" VARCHAR(64)
	,"Category" VARCHAR(64)
	,"Description" VARCHAR(256)
);
