using System.Net.Http.Json;
using Microsoft.Toolkit.Diagnostics;
using SharpSplash.Blog.UI.Infrastructure.Configuration;
using SharpSplash.Blog.UI.Models;

namespace SharpSplash.Blog.UI.Services
{
    public class CosmicService : ICosmicService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public CosmicService(IConfiguration configuration, HttpClient httpClient)
        {
            Guard.IsNotNull(configuration, nameof(configuration));
            Guard.IsNotNull(httpClient, nameof(httpClient));

            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<AllPosts> GetAllPosts()
        {
            var cosmicConfig = _configuration
                .GetSection(CosmicOptions.Cosmic)
                .Get<CosmicOptions>();

            var url = cosmicConfig.Url;
            var bucketSlug = cosmicConfig.BucketSlug;
            var readKey = cosmicConfig.ReadKey;

            var resourceUrl = $"{url.Replace("{COSMIC_BUCKET_SLUG}", bucketSlug)}?pretty=true" +
                              @"&query={{""type"":""posts""}}" +
                              $"&read_key={readKey}&limit=20" +
                              "&props=slug,title,content";
            
            var allPosts = await _httpClient.GetFromJsonAsync<AllPosts>(resourceUrl);

            return allPosts ?? new AllPosts();
        }
    }
}