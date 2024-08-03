using System;
using CSX.Internals.Shared.Graphics;

namespace MicroRenameLogic.Services.BatchHue;

public static partial class BatchHueExtensions
{
    public static Color HslToRgb(this float hue, float saturation, float lightness)
    {
        float chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
        float x = chroma * (1 - Math.Abs((hue / 60) % 2 - 1));
        float m = lightness - chroma / 2;
        float red = 0, green = 0, blue = 0;

        if (0 <= hue && hue < 60)
        {
            red = chroma;
            green = x;
        }
        else if (60 <= hue && hue < 120)
        {
            red = x;
            green = chroma;
        }
        else if (120 <= hue && hue < 180)
        {
            green = chroma;
            blue = x;
        }
        else if (180 <= hue && hue < 240)
        {
            green = x;
            blue = chroma;
        }
        else if (240 <= hue && hue < 300)
        {
            red = x;
            blue = chroma;
        }
        else if (300 <= hue && hue < 360)
        {
            red = chroma;
            blue = x;
        }

        byte r = (byte)((red + m) * 255);
        byte g = (byte)((green + m) * 255);
        byte b = (byte)((blue + m) * 255);

        return new(r, g, b);
    }
}