using System;

namespace MicroRenameLogic.Extensions;

public static partial class MathExtensions
{
    public static int Clamp(this int value, int min, int max)
    {
        if (min > max)
            throw new ArgumentException("Minimum value is bigger than maximum value.");

        if (value < min)
            value = min;
        if (value > max)
            value = max;

        return value;
    }
}