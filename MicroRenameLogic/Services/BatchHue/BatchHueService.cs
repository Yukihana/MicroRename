using MicroRenameLogic.Services.Config;
using System;
using System.Collections.Generic;

namespace MicroRenameLogic.Services.BatchHue;

/// <summary>
/// This is not a lifetime service. Create an instance locally.
/// </summary>
public sealed partial class BatchHueService
{
    private readonly Random _rnd = new();
    private readonly List<float> _hues = [];

    // Primary

    public BatchInfo CreateBatchInfo()
    {
        int count = _hues.Count;
        float hue = GetNextHue(count > 0 ? _hues[^1] : 0f);

        // On max reached, reset
        if (count == int.MaxValue)
        {
            _hues.Clear();
            count = 0;
        }

        // Add hue and return optimized: count +1(Add new) -1(index) cancels out.
        _hues.Add(hue);
        return Build(count, hue);
    }

    // Secondary

    public BatchInfo Get(int index)
        => Build(index, _hues[index]);

    // Internal

    private BatchInfo Build(int index, float hue)
    {
        string color = hue
            .HslToRgb(
            saturation: ServicesRelay.Configuration.BatchColorSaturation,
            lightness: ServicesRelay.Configuration.BatchColorLightness)
            .ToHtmlArgb();
        return new(index, color);
    }

    private float GetNextHue(float current = 0f)
    {
        float nextModifier = (float)(180 * _rnd.NextDouble());
        float nextValue = (current + 90f + nextModifier) % 360;
        return nextValue;
    }
}