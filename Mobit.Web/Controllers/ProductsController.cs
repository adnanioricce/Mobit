namespace Mobit.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobit.Extensions;
using Mobit.Services;
using Mobit.Web;
using System.Threading.Tasks;
[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase 
{
   private readonly IProductService _productService;
   private readonly ILogger<ProductsController> _logger;
   public ProductsController(
      IProductService productService
      ,ILogger<ProductsController> logger)
   {
      _productService = productService;
      _logger = logger;
   }
   [HttpGet("{id:int}")]
   public async Task<IActionResult> Get(int id)
   {
      var func = this.HandleEndpoint(async () => {
         var product = await _productService.GetProductByIdAsync(id);
         if(product is null){
            return NotFound();
         }
         return Ok(product);
      }
      ,_logger);
      return await func();
   }
   [HttpGet("")]
   public async Task<IActionResult> List(int page = 1,int pageCount = 100)
   {
      var func = this.HandleEndpoint(async () => {
         var product = await _productService.GetProductsAsync(new Pagination(page,pageCount));
         if(product is null){
            return NotFound();
         }
         return Ok(product);
      }
      ,_logger);
      return await func();
   }
   [HttpPost("")]
   public async Task<IActionResult> Create([FromBody]ProductDto product)
   {
      var func = this.HandleEndpoint(async () => {
         
         var createdProduct = await _productService.CreateProductAsync(product);

         return Ok(createdProduct);
      }
      ,_logger);
      return await func();
   }
   [HttpPut("")]
   public async Task<IActionResult> Edit([FromBody]ProductDto product)
   {
      var func = this.HandleEndpoint(async () => {
         
         await _productService.EditProductAsync(product.Id,product);

         return Ok();
      }
      ,_logger);
      return await func();
   }
   [HttpDelete("{id:int}")]
   public async Task<IActionResult> Delete(int id)
   {
      var func = this.HandleEndpoint(async () => {
         
         await _productService.DeleteProductAsync(id);

         return Ok();
      }
      ,_logger);
      return await func();
   }
}
