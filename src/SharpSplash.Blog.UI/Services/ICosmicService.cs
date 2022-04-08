using SharpSplash.Blog.UI.Models;

namespace SharpSplash.Blog.UI.Services
{
    public interface ICosmicService
    {
        Task<AllPosts> GetPosts(int limit, int skip);
    }
}