﻿namespace SharpSplash.Blog.UI.Models
{
    public class AllPosts
    {
        public int Total { get; set; }
        public IEnumerable<Post> Objects { get; set; }

        public AllPosts()
        {
            Objects = new List<Post>();
        }
    }
}