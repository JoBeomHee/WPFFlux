using Client.Business.Domain.Action;
using Client.Domain.Theme;
using Fluxor;

namespace Client.Reduce;

public class ThemeReduce
{
    [ReducerMethod]
    public static ThemeState ReduceToggleThemeAction(ThemeState state, ToggleThemeAction action)
    {
        return new ThemeState(!state.IsDark);
    }
}
