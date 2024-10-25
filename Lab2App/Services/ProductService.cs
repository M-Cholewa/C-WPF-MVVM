using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Lab2App.Cache;
using Lab2App.Model;

namespace Lab2App.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly CacheService<Guid, Product> _productCache;
        private readonly CacheService<string, List<ProductSimplified>> _productListCache;

        // Singleton
        private static ProductService? _instance;
        public static ProductService Instance => _instance ??= new ProductService();

        private ProductService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Config.ProductUrl)
            };

            // Ustawiamy domyślny czas cache'owania na 5 minut
            _productCache = new CacheService<Guid, Product>(Config.ProductCacheDurationMinutes);
            _productListCache = new CacheService<string, List<ProductSimplified>>(Config.ProductListCacheDurationMinutes);
        }

        public async Task<List<ProductSimplified>> GetAllProductsAsync()
        {
            return await _productListCache.GetOrAddAsync(Config.ProductListCacheKey, async () =>
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProductSimplified>>(string.Empty);
                if (response == null)
                {
                    throw new HttpRequestException("Failed to retrieve product list from server.");
                }
                return response;
            });
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productCache.GetOrAddAsync(id, async () =>
            {
                var response = await _httpClient.GetAsync($"Product/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to retrieve product with ID {id}. Status code: {response.StatusCode}");
                }

                var product = await response.Content.ReadFromJsonAsync<Product>();
                if (product == null)
                {
                    throw new HttpRequestException($"Product with ID {id} not found on server.");
                }

                return product;
            });
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync(string.Empty, product);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to add product to server.");
            }

            var addedProduct = await response.Content.ReadFromJsonAsync<Product>();
            if (addedProduct == null)
            {
                throw new HttpRequestException("Server returned null after adding product.");
            }

            // Dodaj produkt do cache i wyczyść cache listy produktów
            _productCache.Update(addedProduct.Id, addedProduct);
            _productListCache.Clear();

            return addedProduct;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"Product/{product.Id}", product);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to update product with ID {product.Id}.");
            }

            var updatedProduct = await response.Content.ReadFromJsonAsync<Product>();
            if (updatedProduct == null)
            {
                throw new HttpRequestException("Server returned null after updating product.");
            }

            // Aktualizuj produkt w cache i wyczyść cache listy produktów
            _productCache.Update(updatedProduct.Id, updatedProduct);
            _productListCache.Clear();

            return updatedProduct;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Product/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to delete product with ID {id}. Status code: {response.StatusCode}");
            }

            // Usuń produkt z cache i wyczyść cache listy produktów
            _productCache.Remove(id);
            _productListCache.Clear();
        }
    }
}
