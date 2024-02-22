using Fluxor;

namespace Client.Domain.Theme;

[FeatureState]
public class ThemeState
{
    public bool IsDark { get; }

    private ThemeState() { }

    public ThemeState(bool isDark)
    {
        IsDark = isDark;
    }
}