namespace SharpSplash.Blog.UI.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public PostMetadata Metadata { get; set; }
    }

    public class PostMetadata
    {
        public Hero Hero { get; set; }
    }

    public class Hero
    {
        public string Url { get; set; }
        public string ImgIxUrl { get; set; }
    }
}