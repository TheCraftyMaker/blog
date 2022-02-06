using System.Text.Json.Serialization;
using MudBlazor;

namespace SharpSplash.Blog.UI.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonPropertyName("published_at")] public DateTime PublishedAt { get; set; }
        public PostMetadata Metadata { get; set; }
    }

    public class PostMetadata
    {
        public IEnumerable<Category> Categories { get; set; }
        public string Teaser { get; set; }
        public Hero Hero { get; set; }

        public PostMetadata()
        {
            Categories = new List<Category>();
        }
    }

    public class Hero
    {
        public string Url { get; set; }
        public string ImgIxUrl { get; set; }
    }

    public class Category
    {
        public string Title { get; set; } 

        public Color Color
        {
            get
            {
                return Title.ToLowerInvariant() switch
                {
                    "make it funky" => Color.Secondary,
                    "let's get technical" => Color.Info,
                    "deep thoughts" => Color.Success,
                    "random" => Color.Warning,
                    _ => Color.Default
                };
            }
        }
    }
}