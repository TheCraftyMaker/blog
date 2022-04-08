using Microsoft.AspNetCore.Components;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class BlogPostDetail
    {
        [Parameter] public string Slug { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            return;
        }
    }
}
