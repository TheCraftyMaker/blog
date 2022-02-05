namespace SharpSplash.Blog.UI.Infrastructure.Configuration
{
    public class CosmicOptions
    {
        public const string Cosmic = "Cosmic";
        
        public string Url { get; set; }
        public string BucketSlug { get; set; }
        
        public string ReadKey { get; set; }
    }
}