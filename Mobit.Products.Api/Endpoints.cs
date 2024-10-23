namespace Mobit.Products.Api;
public static class ProductEndpoints {
  
   //[HttpGet("{id:int}")]
  // public static async Task<IActionResult> Get(int id)
  // {
      /*var func = this.HandleEndpoint(async () => {
         var product = await _productService.GetProductByIdAsync(id);
         if(product is null){
            return NotFound();
         }
         return Ok(product);
      }
      ,_logger);
      return await func();
      */
  // }

  /*
   [HttpGet]
   public async Func<int,int,Task<IResult>> List(int page = 1,int pageCount = 100)
   {
      var func = this.HandleEndpoint(async () => {
         var product = await _productService.GetProductsAsync(new Pagination(page,pageCount));
         if(product is null){
            return Results.NotFound();
         }
         return Results.Ok(product);
      }
      ,_logger);
      return await func();
   }
   [HttpPost]
   public async Task<IActionResult> Post([FromBody]ProductDto product)
   {
      var func = this.HandleEndpoint(async () => {
         
         var createdProduct = await _productService.CreateProductAsync(product);
         
         _logger.LogInformation("new product inserted -> {product}!",createdProduct);

         return Ok(createdProduct);
      }
      ,_logger);
      return await func();
   }
   [HttpPut]
   public async Task<IActionResult> Put([FromBody]ProductDto product)
   {
      var func = this.HandleEndpoint(async () => {
         
         await _productService.EditProductAsync(product.Id,product);
         _logger.LogInformation("product with Id = {id} was updated successfully!",product.Id);
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
   */
/*   [HttpPost("upload")]
   public async Task<IActionResult> UploadCsv(IFormFile file)
   {
      var func = this.HandleEndpoint(async () => {
         if (file == null || file.Length == 0)
         {
            _logger.LogInformation("Received a empty form file on upload of product data");
            return BadRequest("File is empty or missing.");
         }
         using var rd = new StreamReader(file.OpenReadStream());
         
         var products = CsvReader.ReadProductsFromCsv(rd).ToList();
*/
}
