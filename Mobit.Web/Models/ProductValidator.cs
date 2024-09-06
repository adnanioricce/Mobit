using System.ComponentModel.DataAnnotations;

namespace Mobit.Models;

public static class ProductValidator
{
   public static IEnumerable<ValidationResult> Validate(this Product product){
      if(product is null){
         throw new ArgumentNullException(nameof(product),"validation method received a null product as argument");
      }
      if(product.Quantity <= 0){
         yield return new ValidationResult("it's not possible to define a product with less than 1 unit in stock ",
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
      if(string.IsNullOrWhiteSpace(product.Category)){
         yield return new ValidationResult("it's not possible to define a product with a empty Category",
         new [] {
            nameof(product.Category)
         });
      }
      if(string.IsNullOrWhiteSpace(product.Description)){
         yield return new ValidationResult("it's not possible to define a product with a empty Description",
         new [] {
            nameof(product.Description)
         });
      }
      if(string.IsNullOrWhiteSpace(product.Title)){
         yield return new ValidationResult("it's not possible to define a product with a empty Title",
         new [] {
            nameof(product.Title)
         });
      }
      
   }
}