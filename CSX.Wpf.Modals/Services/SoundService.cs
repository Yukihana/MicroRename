using CSX.Internals.Shared.Modals;
using System.Media;

namespace CSX.Wpf.Modals.Services;

public static partial class SoundService
{
    public static void PlayAlert(AlertTypes alertType)
    {
        switch (alertType)
        {
            case AlertTypes.None:
                return;

            case AlertTypes.Information:
            case AlertTypes.Exclamation:
            case AlertTypes.Success:
                SystemSounds.Asterisk.Play();
                break;

            case AlertTypes.Error:
                SystemSounds.Hand.Play();
                break;

            case AlertTypes.Warning:
            case AlertTypes.Question:
                SystemSounds.Exclamation.Play();
                break;

            default:
                break;
        }
    }
}