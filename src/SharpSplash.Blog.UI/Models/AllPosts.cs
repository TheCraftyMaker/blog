namespace SharpSplash.Blog.UI.Models
{
    public class AllPosts
    {
        public int Limit { get; set; } = 0;
        public int Total { get; set; } = 0;
        public IEnumerable<Post> Objects { get; set; }

        public AllPosts()
        {
            Objects = new List<Post>();
        }
    }
}