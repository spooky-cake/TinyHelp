using MudBlazor;

namespace TinyHelp.Themes;

public class MainTheme
{
    public static MudTheme Get()
    {
        return new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#ff97b8",
                Secondary = "#9bbfd1",
                AppbarBackground = "#f6fdff",
            }
        };
    }
}