using System;

namespace Exo.Rp.Models.Enums
{
    [Flags]
    public enum AnimationFlags : short
    {
        Loop = 1 << 0,
        StopOnLastFrame = 1 << 1,
        OnlyAnimateUpperBody = 1 << 4,
        AllowPlayerControl = 1 << 5,
        Cancellable = 1 << 7
    }
}