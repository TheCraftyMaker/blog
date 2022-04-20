using MudBlazor;

namespace SharpSplash.Blog.UI.Shared
{
    public class ThemeProvider
    {
        private bool _isDarkMode = true;
        
        private static MudTheme _mudTheme;
        
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                var changed = value != _isDarkMode;
                if (changed)
                {
                    DarkModeChanged?.Invoke(value);
                }
                _isDarkMode = value;
            }
        }
        
        public Func<bool, Task> DarkModeChanged { get; set; }
        
        public static MudTheme CurrentTheme
        {
            get
            {
                if (_mudTheme != null) 
                    return _mudTheme;
                
                _mudTheme = new MudTheme();
                return _mudTheme;
            }
        }
    }
}