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
        var values = line.Substring(14).Trim().Split("  ",StringSplitOptions.RemoveEmptyEntries);
        try
        {
            var Id = int.Parse(line.Substring(0, 14).Trim());
            var Category = values[0].Trim();
            var Title = values[1].Trim();
            var Description = values[2].Trim();
            var Price = int.Parse(values[3]) / 100.0m;
            var quantity = int.Parse(string.Join("", values[4].Where(ch => char.IsDigit(ch)).ToArray()));
            var product = new Product(
                Id,
                quantity,
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
            Console.WriteLine($"Error parsing line: {ex}");
            // var values = line.Substring(14).Trim().Split("\t",StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("values = {0}",string.Join(",", values));
            return null;
        }
    }
}
