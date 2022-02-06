using System.Net.Http.Json;
using Microsoft.Toolkit.Diagnostics;
using SharpSplash.Blog.UI.Infrastructure.Configuration;
using SharpSplash.Blog.UI.Models;

namespace SharpSplash.Blog.UI.Services
{
    public class CosmicService : ICosmicService
    {
        private readonly HttpClient _httpClient;
        private readonly CosmicOptions _cosmicConfig;

        private const string BucketUrl = "/{COSMIC_BUCKET_SLUG}/objects";

        public CosmicService(IConfiguration configuration, HttpClient httpClient)
        {
            Guard.IsNotNull(configuration, nameof(configuration));
            Guard.IsNotNull(httpClient, nameof(httpClient));

            _httpClient = httpClient;

            _cosmicConfig = configuration
                .GetSection(CosmicOptions.Cosmic)
                .Get<CosmicOptions>();
        }

        public async Task<AllPosts> GetAllPosts()
        {
            try
            {
                var url = _cosmicConfig.Url;
                var bucketSlug = _cosmicConfig.BucketSlug;
                var readKey = _cosmicConfig.ReadKey;

                var resourceUrl = $"{url}{BucketUrl.Replace("{COSMIC_BUCKET_SLUG}", bucketSlug)}" +
                                  $"?type=posts&read_key={readKey}";

                return await _httpClient.GetFromJsonAsync<AllPosts>(resourceUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}