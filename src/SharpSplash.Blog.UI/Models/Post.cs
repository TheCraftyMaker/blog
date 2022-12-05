using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using MudBlazor;

namespace SharpSplash.Blog.UI.Models
{
    public class Post
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonPropertyName("published_at")] public DateTime PublishedAt { get; set; }
        public PostMetadata Metadata { get; set; }
        public string Thumbnail { get; set; }
    }

    public class PostMetadata
    {
        public IEnumerable<Category> Categories { get; set; }
        public string Teaser { get; set; }
        public Hero Hero { get; set; }

        [JsonPropertyName("date_published")] public string DatePublished { get; set; }

        public DateTime? DatePublishedDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(DatePublished))
                    return null;

                if (!DateTime.TryParseExact(DatePublished, "yyyy-MM-dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
                {
                    return null;
                }

                return parsed;
            }
        }

        public PostMetadata()
        {
            Categories = new List<Category>();
        }
    }

    public class Hero
    {
        public string Url { get; set; }
        [JsonPropertyName("imgix_url")] public string ImgIxUrl { get; set; }
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
                    "home automation" => Color.Secondary,
                    "tinkering" => Color.Info,
                    "3d printing" => Color.Success,
                    /*"random" => Color.Warning,*/
                    _ => Color.Default
                };
            }
        }
    }
}