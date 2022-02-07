using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SharpSplash.Blog.UI.Shared
{
    public partial class MainLayout
    {
        [Inject] public ThemeProvider ThemeProvider { get; set; }

        private string _darkModeStateIcon;
        private string _logoUri;

        protected override void OnInitialized()
        {
            ThemeProvider.DarkModeChanged = HandleDarkModeChange;

            var isDarkMode = ThemeProvider.IsDarkMode;

            _logoUri = GetLogoUri(isDarkMode);
            _darkModeStateIcon = GetDarkModeLogo(isDarkMode);
        }

        private void HandleDarkModeChange(bool isDarkMode)
        {
            _logoUri = GetLogoUri(isDarkMode);
            _darkModeStateIcon = GetDarkModeLogo(isDarkMode);
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
    }
}