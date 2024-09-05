namespace Mobit.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Mobit.Models;

public static class CsvReader
{
    public static IEnumerable<T> ReadDataFromCsv<T>(StreamReader reader
        ,Func<string,T> parseLine)
    {            
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            
            if (string.IsNullOrWhiteSpace(line))
                continue;
            
            var data = parseLine(line);
            if (data != null)
            {
                yield return data;
            }
            
        }        
    }
    public static IEnumerable<T> ReadDataFromCsv<T>(string filePath
        ,Func<string,T> parseLine)
    {
        using var reader = new StreamReader(filePath);
        return ReadDataFromCsv(reader,parseLine);
    }
    public static IEnumerable<Product> ReadProductsFromCsv(StreamReader csvReader)
    {
        return ReadDataFromCsv(csvReader,MapLineToProduct);
    }
    public static IEnumerable<Product> ReadProductsFromCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        return ReadDataFromCsv(filePath,MapLineToProduct);
    }

    private static Product MapLineToProduct(string line)
    {                
        try
        {
            var Id = int.Parse(line.Substring(0, 14).Trim());
            var Category = line.Substring(14, 20).Trim();
            var Title = line.Substring(34, 35).Trim();
            var Description = line.Substring(69, 100).Trim();
            var Price = decimal.Parse(line.Substring(169, 9).Trim(), CultureInfo.InvariantCulture);
            var StockCode = line.Substring(178).Trim();
            var product = new Product(
                Id,
                0,
                Price,
                Title,
                Category,
                Description
            )
            ;

            return product;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing line: {ex.Message}");
            return null;
        }
    }
}
