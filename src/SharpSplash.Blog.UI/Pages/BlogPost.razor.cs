using Microsoft.AspNetCore.Components;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class BlogPost
    {
        [Parameter] public Models.Post Post { get; set; }
        
        [Parameter] public int PageLocation { get; set; }
    }
}