using ASMC6P.Server.Repositories.ImageRepositories;
using ASMC6P.Server.Repositories.ProductRepositories;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace ASMC6P.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _ImageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository, IImageRepository imageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;

            _ImageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductEntity> CreateProduct(ProductEntity product)
        {
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var dbProduct = _productRepository.AsQueryable().FirstOrDefault(c => c.Id == productId, null);
            if (dbProduct == null)
            {
                return false;
            }

            await _productRepository.RemoveAsync(dbProduct);

            await _productRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductEntity>> GetAdminProducts()
        {
            var response = await _productRepository.AsQueryable()
                .Where(p => !p.IsDeleted)
                .ToListAsync();


            return response;
        }

        public async Task<List<ProductEntity>> GetFeaturedProducts()
        {
            var response = new List<ProductEntity>();

            response = await _productRepository.AsQueryable()
                    .Where(p => !p.IsDeleted).ToListAsync();


            return response;
        }

        public async Task<ProductEntity> GetProductAsync(Guid productId)
        {
            var response = new ProductEntity();
            ProductEntity product = null;

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                response = await _productRepository.AsQueryable().FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            }
            else
            {
                response = await _productRepository.AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            }

            if (product == null)
            {
                return product;
            }
            else
            {
                return response;
            }


        }

        public async Task<List<ProductEntity>> GetProductsAsync()
        {
            var response = new List<ProductEntity>();

            response = await _productRepository.AsQueryable().Where(p => !p.IsDeleted)
                .ToListAsync();


            return response;
        }

        public async Task<List<ProductEntity>> GetProductsByCategory(string categoryUrl)
        {
            var response = new List<ProductEntity>();

            response = await _productRepository.AsQueryable()
                    .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) && !p.IsDeleted)
                    .ToListAsync();


            return response;
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Name);
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

            return result;
        }

        public async Task<ProductSearchResult> SearchProducts(string searchText, int page)
        {
            var pageResults = 4f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);
            var products = await _productRepository.AsQueryable()
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) && !p.IsDeleted)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            var response = new ProductSearchResult
            {
                Products = products,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return response;
        }

        public async Task<ProductEntity> UpdateProduct(ProductEntity product)
        {
            var dbProduct = await _productRepository.AsQueryable()
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                return new ProductEntity();
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.CategoryId = product.CategoryId;


            //foreach (var variant in product.Variants)
            //{
            //    var dbVariant = await _productVariantRepository.AsQueryable()
            //        .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
            //            v.ProductTypeId == variant.ProductTypeId);
            //    if (dbVariant == null)
            //    {
            //        variant.ProductType = null;
            //        await _productVariantRepository.AddAsync(variant);
            //    }
            //    else
            //    {
            //        dbVariant.ProductTypeId = variant.ProductTypeId;
            //        dbVariant.Price = variant.Price;
            //        dbVariant.OriginalPrice = variant.OriginalPrice;
            //        dbVariant.Visible = variant.Visible;
            //        dbVariant.IsDeleted = variant.IsDeleted;
            //    }
            //}

            await _productRepository.SaveChangesAsync();
            return product;
        }

        private async Task<List<ProductEntity>> FindProductsBySearchText(string searchText)
        {
            return await _productRepository.AsQueryable()
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) && !p.IsDeleted)
                                .ToListAsync();
        }
    }
}
