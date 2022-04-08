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
        private bool _noMorePosts;
        private const int AmountOfPostPerPage = 5;
        private int _page;

        protected override async Task OnInitializedAsync()
        {
            _loading = true;

            _allPosts = await CosmicService.GetPosts(AmountOfPostPerPage, _page);

            _loading = false;
            
            StateHasChanged();
        }

        private async Task OlderClick()
        {
            _loading = true;

            _page += 1;

            _allPosts = await CosmicService.GetPosts(AmountOfPostPerPage, _page);

            _noMorePosts = (_page + 1) * AmountOfPostPerPage >= _allPosts.Total;

            _loading = false;

            StateHasChanged();
        }
        
        private async Task RecentClick()
        {
            if(_page == 0)
                return;
            
            _loading = true;

            _page -= 1;

            _allPosts = await CosmicService.GetPosts(AmountOfPostPerPage, _page);

            _noMorePosts = (_page + 1) * AmountOfPostPerPage >= _allPosts.Total;

            _loading = false;

            StateHasChanged();
        }
    }
}