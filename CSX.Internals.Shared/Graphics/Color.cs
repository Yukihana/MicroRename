using System;

namespace CSX.Internals.Shared.Graphics;

public readonly struct Color(byte r, byte g, byte b, byte a)
{
    public readonly byte R { get; } = r;
    public readonly byte G { get; } = g;
    public readonly byte B { get; } = b;
    public readonly byte A { get; } = a;

    public Color(byte r, byte g, byte b) : this(r, g, b, 255)
    { }

    public Color((byte r, byte g, byte b) bytes) : this(bytes.r, bytes.g, bytes.b)
    { }

    public Color((byte r, byte g, byte b, byte a) bytes) : this(bytes.r, bytes.g, bytes.b, bytes.a)
    { }

    // From

    public static Color FromRgb(byte r, byte g, byte b)
        => new(r, g, b);

    public static Color FromRgba(byte r, byte g, byte b, byte a)
        => new(r, g, b, a);

    public static Color FromBytes(byte[] bytes)
    {
        if (bytes.Length == 3)
            return new(bytes[0], bytes[1], bytes[2]);
        else if (bytes.Length == 4)
            return new(bytes[0], bytes[1], bytes[2], bytes[3]);
        else if (bytes.Length < 3)
            throw new ArgumentException($"At least three values required. Provided: {bytes.Length}.", nameof(bytes));
        else
            throw new ArgumentException($"Too many values provided: {bytes.Length}.", nameof(bytes));
    }

    // To
    public override string ToString() => $"[R:{R},G:{G},B{B},A{A}]";

    public byte[] ToRgbBytes() => [R, G, B];

    public byte[] ToRgbaBytes() => [R, G, B, A];

    public string ToHtmlRgb() => $"#{R:X2}{G:X2}{B:X2}";

    public string ToHtmlArgb() => $"#{A:X2}{R:X2}{G:X2}{B:X2}";
}