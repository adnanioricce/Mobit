namespace Mobit.Services;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobit.Models;
using Mobit.Web.Data;

public record Pagination(int PageNumber,int PageCount);
public record ProductDto(int Id,int Quantity,decimal Price,string Title,string Category,string Description);
public interface IProductService 
{	
	Task<IEnumerable<ProductDto>> GetProductsAsync(Pagination pagination);
	Task<ProductDto?> GetProductByIdAsync(int productId);
	Task<ProductDto?> CreateProductAsync(ProductDto product);
	Task CreateProductsAsync(IEnumerable<Product> products);
	Task EditProductAsync(int productId,ProductDto product);
	Task DeleteProductAsync(int productId);
	
}
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
		var product = Product.From(dto);
		_ctx.Products.Add(product);
		await _ctx.SaveChangesAsync();
		return Product.To(product);
	}
	public async Task CreateProductsAsync(IEnumerable<Product> products)
    {		
		var ids = products.Select(p => p.Id).ToArray();
		var existingProducts = await _ctx.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
		var updatedProducts = products.Where(p => existingProducts.Any(ep => ep.Id == p.Id));
		var newProducts = products.Where(p => !existingProducts.Any(p => p.Id == p.Id)).ToList();
        await _ctx.Products.AddRangeAsync(newProducts);
		_ctx.Products.UpdateRange(updatedProducts);
        await _ctx.SaveChangesAsync();
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
