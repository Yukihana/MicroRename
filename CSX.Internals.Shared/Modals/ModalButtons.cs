using System.Collections.Generic;

namespace CSX.Internals.Shared.Modals;

public partial class ModalButtons<T>
{
    public Dictionary<T, string> Buttons { get; } = [];

    public T? DefaultButtonKey { get; set; } = default;

    public T? CancelButtonKey { get; set; } = default;
}