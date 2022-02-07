using Microsoft.AspNetCore.Components;
using SharpSplash.Blog.UI.Models;
using SharpSplash.Blog.UI.Services;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class Index
    {
        [Inject] public ICosmicService CosmicService { get; set; }
        
        private AllPosts _allPosts = new();
        
        private bool _loading;

        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            
            _allPosts = await CosmicService.GetAllPosts();

            _loading = false;
        }
    }
}