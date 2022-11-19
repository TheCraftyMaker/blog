using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using MudBlazor;

namespace SharpSplash.Blog.UI.Shared
{
    public partial class MainLayout
    {
        [Inject] public ThemeProvider ThemeProvider { get; set; }

        [Inject] public ILocalStorageService LocalStorageService { get; set; }

        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        private const string DarkModeStorageKey = "sharpsplash-darkmode";

        private string _darkModeStateIcon;
        private string _logoUri;
        private bool _hideAbout;
        private IJSObjectReference _module;

        protected override async Task OnInitializedAsync()
        {
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");

            ThemeProvider.DarkModeChanged = HandleDarkModeChange;
            NavigationManager.LocationChanged += HandlePageChange;

            var isDarkMode = ThemeProvider.IsDarkMode;
            if (!await LocalStorageService.ContainKeyAsync(DarkModeStorageKey))
            {
                await LocalStorageService.SetItemAsync(DarkModeStorageKey, isDarkMode);
            }
            else
            {
                isDarkMode = await LocalStorageService.GetItemAsync<bool>(DarkModeStorageKey);
                ThemeProvider.IsDarkMode = isDarkMode;
            }

            if (NavigationManager.Uri.EndsWith("/"))
            {
                _hideAbout = false;
            }

            _logoUri = GetLogoUri(isDarkMode);
            _darkModeStateIcon = GetDarkModeLogo(isDarkMode);

            await _module.InvokeVoidAsync("SetTheme");
        }

        private async Task HandleDarkModeChange(bool isDarkMode)
        {
            _logoUri = GetLogoUri(isDarkMode);
            _darkModeStateIcon = GetDarkModeLogo(isDarkMode);

            await LocalStorageService.SetItemAsync(DarkModeStorageKey, isDarkMode);

            await _module.InvokeVoidAsync("SetTheme");
        }

        private static string GetLogoUri(bool isDarkMode)
        {
            return isDarkMode
                ? "img/astronaut-dark.png"
                : "img/astronaut-light.png";
        }

        private static string GetDarkModeLogo(bool isDarkMode)
        {
            return isDarkMode
                ? Icons.Material.Filled.LightMode
                : Icons.Material.Filled.DarkMode;
        }
        
        private void HandlePageChange(object sender, LocationChangedEventArgs args)
        {
            _hideAbout = !args.Location.EndsWith("/");
            
            StateHasChanged();
        }
    }
}