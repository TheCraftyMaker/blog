using Microsoft.AspNetCore.Components;
using SharpSplash.Blog.UI.Models;
using SharpSplash.Blog.UI.Services;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class BlogPostDetail
    {
        [Inject] public ICosmicService CosmicService { get; set; }
        
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Parameter] public string Slug { get; set; }
        
        [Parameter] public int PageLocation { get; set; }

        private SinglePost _post;
        private bool _loading;

        protected override async Task OnParametersSetAsync()
        {
            if(_loading)
                return;
            
            _loading = true;
            
            StateHasChanged();

            _post = await CosmicService.GetPost(Slug);

            _loading = false;
            
            StateHasChanged();
        }
        
        private void BackClick()
        {
            NavigationManager.NavigateTo($"/{PageLocation}");
        }
    }
}