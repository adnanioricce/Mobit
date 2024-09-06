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
