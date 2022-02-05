namespace SharpSplash.Blog.UI.Models;

public class Post
{
    public string _id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Published_at { get; set; }
    public PostMetadata Metadata { get; set; }
}

public class PostMetadata
{
    public Hero hero { get; set; }
}

public class Hero
{
    public string url { get; set; }
    public string imgix_url { get; set; }
}

