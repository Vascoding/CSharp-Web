using System;

namespace CameraStore.Data.Enumerations
{
    [Flags]
    public enum LightMatering
    {
        Spot = 1,
        CenterWeighted = 2,
        Evaluative = 4
    }
}
