using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharpSplash.Blog.UI.Models;
using SharpSplash.Blog.UI.Services;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class BlogPostDetail
    {
        [Inject] public ICosmicService CosmicService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Parameter] public string Slug { get; set; }

        [Parameter] public int PageLocation { get; set; }

        private SinglePost _post;
        private bool _loading;
        private IJSObjectReference _module;

        protected override async Task OnInitializedAsync()
        {
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");
        }

        protected override async Task OnParametersSetAsync()
        {
            if (_loading)
                return;

            _loading = true;

            StateHasChanged();

            _post = await CosmicService.GetPost(Slug);

            await StyleCodeBlocks();

            StateHasChanged();
        }

        private void BackClick()
        {
            NavigationManager.NavigateTo($"/{PageLocation}");
        }

        private async Task StyleCodeBlocks()
        {
            if (_post?.Object == null || string.IsNullOrEmpty(_post.Object.Content))
                return;

            try
            {
                var content = _post.Object.Content;
                if (content.Contains("[CODE LANG="))
                {
                    const string pattern = @"\[CODE LANG=(.*?)\](.*?)\[\/CODE\]";
                    var matches = Regex.Matches(content, pattern, RegexOptions.Multiline | RegexOptions.Singleline);

                    foreach (Match match in matches)
                    {
                        var block = match.Value;
                        
                        if (string.IsNullOrEmpty(block))
                            continue;

                        var language = match.Groups[1].Captures[0].Value.Replace("&quot;", "");
                        if (string.IsNullOrEmpty(language))
                            continue;

                        var code = match.Groups[2].Captures[0].Value;
                        if (string.IsNullOrEmpty(code))
                            continue;

                        var styled = await _module.InvokeAsync<string>("HighLight", code, language);
                        if (string.IsNullOrEmpty(styled))
                            continue;

                        styled = styled
                            .Replace("BR", "<p></p>")
                            .Replace("TB", "&nbsp;&nbsp;")
                            .Replace("&amp;gt;", ">")
                            .Replace("&amp;lt;", "<")
                            .Replace(@"&amp;nbsp<span class=""token punctuation"">;</span>", " ")
                            .Replace(@"<span class=""token operator"">&amp;</span>quot<span class=""token punctuation"">;</span>", "\"");

                        var styledBlock = $@"<pre class=""language-{language}""><code class=""language-{language}"">{styled}</code></pre>";
                        content = content.Replace(block, styledBlock);
                    }

                    _post.Object.Content = content;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _loading = false;
                    
                StateHasChanged();
            }
        }
    }
}