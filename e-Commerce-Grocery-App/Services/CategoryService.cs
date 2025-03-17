using System;
using System.Collections.Generic;
using System.Linq; // Added for Enumerable.Empty<T>()
using System.Net.Http;
using System.Text.Json; // Added for JsonSerializer
using System.Threading.Tasks; // Added for ValueTask
using Constants;
using Models;

namespace Services
{
    public class CategoryService
    {
        private IEnumerable<Category>? _categories;
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor to inject IHttpClientFactory
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async ValueTask<IEnumerable<Category>> GetCategoriesAsync()
        {
            // If categories are null, fetch from API
            if (_categories is null)
            {
                var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);
                var response = await httpClient.GetAsync("/masters/categories");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        // Deserialize JSON to IEnumerable<Category>
                        _categories = JsonSerializer.Deserialize<IEnumerable<Category>>(content);
                    }
                }
                else
                {
                    // Return an empty collection if the response is not successful
                    return Enumerable.Empty<Category>();
                }
            }

            // Return the cached categories if not null
            return _categories ?? Enumerable.Empty<Category>();
        }
    }
}
