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

        public async Task<AllPosts> GetPosts(int limit, int skip)
        {
            try
            {
                var url = _cosmicConfig.Url;
                var bucketSlug = _cosmicConfig.BucketSlug;
                var readKey = _cosmicConfig.ReadKey;

                var resourceUrl = $"{url}/{bucketSlug}/objects?type=posts&limit={limit}&skip={skip * limit}" +
                                  $"&sort=created_at&read_key={readKey}";

                return await _httpClient.GetFromJsonAsync<AllPosts>(resourceUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<SinglePost> GetPost(string slug)
        {
            try
            {
                var url = _cosmicConfig.Url;
                var bucketSlug = _cosmicConfig.BucketSlug;
                var readKey = _cosmicConfig.ReadKey;

                var resourceUrl = $"{url}/{bucketSlug}/object/{slug}?read_key={readKey}";

                return await _httpClient.GetFromJsonAsync<SinglePost>(resourceUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}