namespace SharpSplash.Blog.UI.Models
{
    public class Post
    {
        public string Id { get; set; } = "";
        public string Slug { get; set; } = "";
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime PublishedAt { get; set; } = DateTime.MinValue;
        public PostMetadata Metadata { get; set; }

        public Post()
        {
            Metadata = new PostMetadata();
        }
    }

    public class PostMetadata
    {
        public Hero Hero { get; set; }

        public PostMetadata()
        {
            Hero = new Hero();
        }
    }

    public class Hero
    {
        public string Url { get; set; } = "";
        public string ImgIxUrl { get; set; } = "";
    }
}