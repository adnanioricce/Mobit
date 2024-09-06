using System.ComponentModel.DataAnnotations;

namespace Mobit.Models;

public static class ProductValidator
{
   public static IEnumerable<ValidationResult> Validate(this Product product){
      if(product is null){
         throw new ArgumentNullException(nameof(product),"validation method received a null product as argument");
      }
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