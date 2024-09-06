using System.Text;
using Mobit.Services;
using Newtonsoft.Json;
using NUnit;
namespace Mobit.Tests;

public class CsvReaderTests
{
    [TestCase(296910,10,"SMARTPHONE","SAMSUNG GALAXY S22","00000000000001 SMARTPHONE        	SAMSUNG GALAXY S22                  Smartphone Samsung Galaxy S22 5G SM-S901 128GB Câmera Tripla          				296910         ESTOQUE10")]
    [TestCase(469900,07,"SMARTPHONE","APPLE IPHONE 15","00000000000002 SMARTPHONE        	APPLE IPHONE 15                     Smartphone Apple iPhone 15 128GB Câmera Dupla                         				469900         ESTOQUE07")]
    [TestCase(419900,02,"NOTEBOOK","DELL INSPIRION I7",@"00000000000003 NOTEBOOK          	DELL INSPIRION I7                   Dell Laptop Inspiron 15 3501 15,6"" FHD i7 - Intel Core i7-1165G7, RAM DDR4 16GB, SSD 512GB          419900         ESTOQUE02")]
    [TestCase(419900,02,"NOTEBOOK","DELL INSPIRION I7",@"00000000000003 NOTEBOOK          	DELL INSPIRION I7                   Dell Laptop Inspiron 15 3501 15,6"" FHD i7 - Intel Core i7-1165G7, RAM DDR4 16GB, SSD 512GB          419900         ESTOQUE02")]
    [TestCase(001945,30,"MATERIAL ESCRITORIO","CADERNO TILIBRA",@"00000000000004 MATERIAL ESCRITORIO      CADERNO TILIBRA                     Tilibra 305421 Universitário 10 Matérias Zip - Caderno Espiral, Capa Dura, 160 Folhas, Preto        001945         ESTOQUE30")]    
    public void ShouldParseTextToProductInstance(
        int expectedPrice
        ,int expectedQuantity
        ,string category
        ,string title 
        ,string line){
        using var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(line)));
        var products = CsvReader.ReadProductsFromCsv(reader).ToList();
        Assert.That(products.Count,Is.EqualTo(1));    
        var first = products[0];
        Assert.That(first.Price,Is.EqualTo(expectedPrice / 100.0m));
        Assert.That(first.Quantity,Is.EqualTo(expectedQuantity));
        Assert.That(first.Title,Is.EqualTo(title));
        Assert.That(first.Category,Is.EqualTo(category));
    }
    [Test]
    public void TestMultipleLines()
    {
        var str = @"
00000000000001 SMARTPHONE        	SAMSUNG GALAXY S22                  Smartphone Samsung Galaxy S22 5G SM-S901 128GB Câmera Tripla          				296910         ESTOQUE10
00000000000002 SMARTPHONE        	APPLE IPHONE 15                     Smartphone Apple iPhone 15 128GB Câmera Dupla                         				469900         ESTOQUE07
00000000000003 NOTEBOOK          	DELL INSPIRION I7                   Dell Laptop Inspiron 15 3501 15,6"" FHD i7 - Intel Core i7-1165G7, RAM DDR4 16GB, SSD 512GB          419900         ESTOQUE02
00000000000004 MATERIAL ESCRITORIO      CADERNO TILIBRA                     Tilibra 305421 Universitário 10 Matérias Zip - Caderno Espiral, Capa Dura, 160 Folhas, Preto        001945         ESTOQUE30";
        using var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(str)));
        var products = CsvReader.ReadProductsFromCsv(reader).ToList();        
        Assert.That(products.Count,Is.EqualTo(4));
    }
}
