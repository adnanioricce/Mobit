namespace Mobit.Services;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobit.Models;
using Mobit.Web.Data;
// vou me permitir ser menos rigoroso aqui e chamar isso de "Service" ao invés de "Repository"
public class StandardProductService : IProductService
{
	private readonly ApplicationDbContext _ctx;
	public StandardProductService(ApplicationDbContext ctx)
	{
		_ctx = ctx;		
	}
	
	public async Task<IEnumerable<ProductDto>> GetProductsAsync(Pagination pagination)
	{
		var products = await _ctx.Products
			.Skip((pagination.PageNumber - 1) * pagination.PageCount)
			.Take(pagination.PageCount)
			.ToListAsync();
		return products.Select(p => Product.To(p));
	}
	public async Task<ProductDto?> GetProductByIdAsync(int productId)
	{
		var product = await _ctx.Products.FindAsync(productId);
		
		if(product is null)
			return null;

		return Product.To(product);
	}

	public async Task<ProductDto?> CreateProductAsync(ProductDto dto)
	{
		_ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
		if(dto.Id != 0){
			throw new ArgumentException("Can't create a new product with a Id != 0");
		}
		var product = Product.From(dto);
		
		_ctx.Products.Add(product);
		await _ctx.SaveChangesAsync();
		return Product.To(product);
	}
	public async Task CreateOrUpdateProductsAsync(IEnumerable<Product> products)
    {		
		var updatedProducts = await GetExistingProducts(products);		
		_ctx.Products.UpdateRange(updatedProducts);
		await _ctx.SaveChangesAsync();
						
		var newProducts = products.Where(p => !updatedProducts.Any(up => up.Id == p.Id)).ToList();
        await _ctx.Products.AddRangeAsync(newProducts);
		await _ctx.SaveChangesAsync();
    }

    private async Task<IEnumerable<Product>> GetExistingProducts(IEnumerable<Product> products)
    {
		var ids = products.Select(p => p.Id).ToArray();
        var existingProducts = await _ctx.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
		var updatedProducts = products.Where(p => existingProducts.Any(ep => ep.Id == p.Id));
		return updatedProducts;
    }

    public async Task EditProductAsync(int productId,ProductDto dto)
	{
		var existingProduct = await _ctx.Products.FindAsync(productId);
		
		if(existingProduct is null)
			throw new KeyNotFoundException($"no product with Id = {dto.Id} was found");
		
		var updatedProduct = existingProduct with {
			Quantity = dto.Quantity
			,Price = dto.Price
			,Category = dto.Category
			,Description = dto.Description
			,Title = dto.Title
		};
		_ctx.Products.Update(updatedProduct);
		await _ctx.SaveChangesAsync();
	}

	public async Task DeleteProductAsync(int productId)
	{
		var product = await _ctx.Products.FindAsync(productId);
		
		if(product is null)
			return;

		_ctx.Products.Remove(product);
		
		await _ctx.SaveChangesAsync();
	}
}
