using ASMC6P.Server.Repositories.ImageRepositories;
using ASMC6P.Server.Repositories.ProductRepositories;
using ASMC6P.Server.Repositories.ProductVariantRepositories;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace ASMC6P.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IImageRepository _ImageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository, IProductVariantRepository productVariantRepository, IImageRepository imageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _productVariantRepository = productVariantRepository;
            _ImageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<ProductEntity>> CreateProduct(ProductEntity product)
        {
            foreach (var variant in product.Variants)
            {
                variant.ProductType = null;
            }
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return new ServiceResponse<ProductEntity> { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(Guid productId)
        {
            var dbProduct = _productRepository.AsQueryable().FirstOrDefault(c => c.Id == productId, null);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found."
                };
            }

            await _productRepository.RemoveAsync(dbProduct);

            await _productRepository.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<ProductEntity>>> GetAdminProducts()
        {
            var response = new ServiceResponse<List<ProductEntity>>
            {
                Data = await _productRepository.AsQueryable()
                    .Where(p => !p.IsDeleted)
                    .Include(p => p.Variants.Where(v => !v.IsDeleted))
                    .ThenInclude(v => v.ProductType)
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<ProductEntity>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<ProductEntity>>
            {
                Data = await _productRepository.AsQueryable()
                    .Where(p => p.Featured && p.Visible && !p.IsDeleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.IsDeleted))
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<ProductEntity>> GetProductAsync(Guid productId)
        {
            var response = new ServiceResponse<ProductEntity>();
            ProductEntity product = null;

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _productRepository.AsQueryable()
                    .Include(p => p.Variants.Where(v => !v.IsDeleted))
                    .ThenInclude(v => v.ProductType)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            }
            else
            {
                product = await _productRepository.AsQueryable()
                    .Include(p => p.Variants.Where(v => v.Visible && !v.IsDeleted))
                    .ThenInclude(v => v.ProductType)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted && p.Visible);
            }

            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exist.";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<ProductEntity>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<ProductEntity>>
            {
                Data = await _productRepository.AsQueryable()
                    .Where(p => p.Visible && !p.IsDeleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.IsDeleted))
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<ProductEntity>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<ProductEntity>>
            {
                Data = await _productRepository.AsQueryable()
                    .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) &&
                        p.Visible && !p.IsDeleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.IsDeleted))
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);
            var products = await _productRepository.AsQueryable()
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.Visible && !p.IsDeleted)
                                .Include(p => p.Variants)
                                .Include(p => p.Images)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<ProductEntity>> UpdateProduct(ProductEntity product)
        {
            var dbProduct = await _productRepository.AsQueryable()
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                return new ServiceResponse<ProductEntity>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.Visible = product.Visible;
            dbProduct.Featured = product.Featured;

            var productImages = dbProduct.Images;
            await _ImageRepository.RemoveRangeAsync(productImages);

            dbProduct.Images = product.Images;

            foreach (var variant in product.Variants)
            {
                var dbVariant = await _productVariantRepository.AsQueryable()
                    .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
                        v.ProductTypeId == variant.ProductTypeId);
                if (dbVariant == null)
                {
                    variant.ProductType = null;
                    await _productVariantRepository.AddAsync(variant);
                }
                else
                {
                    dbVariant.ProductTypeId = variant.ProductTypeId;
                    dbVariant.Price = variant.Price;
                    dbVariant.OriginalPrice = variant.OriginalPrice;
                    dbVariant.Visible = variant.Visible;
                    dbVariant.IsDeleted = variant.IsDeleted;
                }
            }

            await _productRepository.SaveChangesAsync();
            return new ServiceResponse<ProductEntity> { Data = product };
        }

        private async Task<List<ProductEntity>> FindProductsBySearchText(string searchText)
        {
            return await _productRepository.AsQueryable()
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.Visible && !p.IsDeleted)
                                .Include(p => p.Variants)
                                .ToListAsync();
        }
    }
}
