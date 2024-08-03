using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.Types;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace MicroRenameLogic.ViewContexts.RenameLogViewContext;

public sealed partial class RenameLogEntry : ObservableObject
{
    // Session

    [ObservableProperty]
    private int _session = 0;

    [JsonIgnore]
    [ObservableProperty]
    [property: JsonIgnore]
    private string _color = "#888";

    // Time

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MajorTime))]
    [NotifyPropertyChangedFor(nameof(MinorTime))]
    [NotifyPropertyChangedFor(nameof(TimeOfDay))]
    [NotifyPropertyChangedFor(nameof(Date))]
    private DateTime _timeStamp = DateTime.Now;

    public string MajorTime => TimeStamp.ToString("HH:mm");
    public string MinorTime => TimeStamp.ToString("ss.fff");
    public string TimeOfDay => TimeStamp.TimeOfDay.ToString();
    public string Date => TimeStamp.Date.ToString();

    // Filenames

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OldFileName))]
    private string _oldLocation = "X:\\Path\\To\\Old.file";

    public string OldFileName => Path.GetFileName(OldFileName);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NewFileName))]
    private string _newLocation = "X:\\Path\\To\\New.file";

    public string NewFileName => Path.GetFileName(NewFileName);

    // Status

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StatusIndex))]
    private RenameStatus _status = RenameStatus.Unknown;

    public byte StatusIndex => (byte)Status;
    public bool IsEnabled => Status != RenameStatus.Expired;

    // Error

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _errorSource = string.Empty;

    [ObservableProperty]
    public string _errorType = string.Empty;

    [ObservableProperty]
    public string _errorData = string.Empty;
}