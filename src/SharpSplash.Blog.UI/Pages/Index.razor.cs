using System.Globalization;
using Microsoft.AspNetCore.Components;
using SharpSplash.Blog.UI.Models;
using SharpSplash.Blog.UI.Services;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class Index
    {
        [Inject] public ICosmicService CosmicService { get; set; }

        [Parameter] public int Page { get; set; }

        private AllPosts _allPosts = new();

        private bool _loading;
        private bool _noMorePosts;
        private const int AmountOfPostPerPage = 5;

        protected override async Task OnInitializedAsync()
        {
            await GetPosts();
        }

        private async Task OlderClick()
        {
            Page += 1;

            await GetPosts();
            
            StateHasChanged();
        }
        
        private async Task RecentClick()
        {
            if(Page == 0)
                return;

            Page -= 1;

            await GetPosts();

            StateHasChanged();
        }

        private async Task GetPosts()
        {
            _loading = true;

            _allPosts = await CosmicService.GetPosts(AmountOfPostPerPage, Page);

            CheckPosts();

            _loading = false;

            StateHasChanged();
        }

        private void CheckPosts()
        {
            _noMorePosts = (Page + 1) * AmountOfPostPerPage >= _allPosts.Total;

            if (!_allPosts.Objects.Any()) 
                return;

            _allPosts.Objects = _allPosts.Objects.OrderByDescending(x => x.Metadata.DatePublishedDateTime);
        }
    }
}