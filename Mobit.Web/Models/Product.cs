using System.ComponentModel.DataAnnotations;
using Mobit.Services;

namespace Mobit.Models;

public record Product(
   int Id
   ,int Quantity
   ,decimal Price
   ,string Title
   ,string Category
   ,string Description
)
{
   public static Product From(ProductDto dto){
      var product = new Product(dto.Id,dto.Quantity,dto.Price,dto.Title,dto.Category,dto.Description);
      var validations = ProductValidator.Validate(product);
      if(validations.Any()){
         throw new ValidationException(
            string.Join("\n",validations.Select(v => $"[{string.Join(",",v.MemberNames)}] = {v.ErrorMessage}")));
      }      
      return product;
   }
   public static ProductDto To(Product product){
      return new (
         product.Id
         ,product.Quantity
         ,product.Price
         ,product.Title
         ,product.Category
         ,product.Description);
   }
}

public static class ProductValidator
{
   public static IEnumerable<ValidationResult> Validate(this Product product){
      if(product.Quantity < 0){
         yield return new ValidationResult("it's not possible to define a product with a negative quantity",
         new [] {
            nameof(product.Quantity)
         });
      }
      if(product.Price < 0.0m){
         yield return new ValidationResult("it's not possible to define a product with a negative price",
         new [] {
            nameof(product.Price)
         });
      }
      if(product.Category.Length > 64){
         yield return new ValidationResult("it's not possible to define a product with a Category content bigger than 64 characters",
         new [] {
            nameof(product.Category)
         });
      }
      if(product.Description.Length > 64){
         yield return new ValidationResult("it's not possible to define a product with a Description content bigger than 64 characters",
         new [] {
            nameof(product.Description)
         });
      }
      if(product.Title.Length > 256){
         yield return new ValidationResult("it's not possible to define a product with a Title content bigger than 64 characters",
         new [] {
            nameof(product.Title)
         });
      }
   }
}