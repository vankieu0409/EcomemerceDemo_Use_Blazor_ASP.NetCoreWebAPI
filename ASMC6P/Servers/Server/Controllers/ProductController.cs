using ASMC6P.Server.Services.ProductService;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;

namespace ASMC6P.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ProductEntity>>> GetAdminProducts(ODataQueryOptions<ProductEntity> queryOptions)
        {
            var result = await _productService.GetAdminProducts();
            var castedResult = queryOptions.ApplyTo(result.AsQueryable()).Cast<ProductEntity>();
            return Ok(castedResult);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductEntity>> CreateProduct(ProductEntity product)
        {
            var result = await _productService.CreateProduct(product);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductEntity>> UpdateProduct(ProductEntity product)
        {
            var result = await _productService.UpdateProduct(product);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetProducts(ODataQueryOptions<ProductEntity> queryOptions)
        {
            var result = await _productService.GetProductsAsync();
            var castedResult = queryOptions.ApplyTo(result.AsQueryable()).Cast<ProductEntity>();
            return Ok(castedResult);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductEntity>> GetProduct(Guid productId)
        {
            var result = await _productService.GetProductAsync(productId);
            return Ok(result);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<List<ProductEntity>>> GetProductsByCategory(string categoryUrl)
        {
            var result = await _productService.GetProductsByCategory(categoryUrl);
            return Ok(result);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ProductSearchResult>> SearchProducts(string searchText, int page = 1)
        {
            var result = await _productService.SearchProducts(searchText, page);
            return Ok(result);
        }

        [HttpGet("searchsuggestions/{searchText}")]

        public async Task<ActionResult<List<ProductEntity>>> GetProductSearchSuggestions([FromODataUri] string searchText, ODataQueryOptions<string> queryOptions)
        {
            var result = await _productService.GetProductSearchSuggestions(searchText);
            var castedResult = queryOptions.ApplyTo(result.AsQueryable()).Cast<string>();
            return Ok(castedResult);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<List<ProductEntity>>> GetFeaturedProducts(ODataQueryOptions<ProductEntity> queryOptions)
        {
            var result = await _productService.GetFeaturedProducts();
            var castedResult = queryOptions.ApplyTo(result.AsQueryable()).Cast<ProductEntity>();
            return Ok(castedResult);
        }
    }
}
