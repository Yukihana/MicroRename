using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;

namespace MicroRenameLogic.Types;

public partial class MRTask : ObservableObject, IEquatable<MRTask>
{
    public MRTask(string location)
    {
        Filename = Path.GetFileName(location);
        Directory = Path.GetDirectoryName(location);
    }

    // Main

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Location))]
    private string _directory = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Location))]
    [NotifyPropertyChangedFor(nameof(FilenameWithoutExtension))]
    [NotifyPropertyChangedFor(nameof(Extension))]
    private string _filename = string.Empty;

    // Derived

    public string Location
        => Path.Combine(Directory, Filename);

    public string FilenameWithoutExtension
        => Path.GetFileNameWithoutExtension(Filename);

    public string Extension
        => Path.GetExtension(Filename);

    // Equality

    public bool Equals(MRTask other)
    {
        if (other is null)
            return false;
        return Location == other.Location;
    }
}