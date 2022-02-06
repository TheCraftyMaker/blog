using Microsoft.AspNetCore.Components;
using SharpSplash.Blog.UI.Models;
using SharpSplash.Blog.UI.Services;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class Index
    {
        [Inject] public ICosmicService CosmicService { get; set; }

        private AllPosts _allPosts = new();

        protected override async Task OnInitializedAsync()
        {
            _allPosts = await CosmicService.GetAllPosts();
        }
    }
}