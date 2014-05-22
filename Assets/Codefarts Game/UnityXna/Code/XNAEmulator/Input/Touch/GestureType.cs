namespace Microsoft.Xna.Framework.Input.Touch
{
    using System;

    [Flags]
    public enum GestureType
    {
        None = 0,
        Tap = 1,
        DoubleTap = 2,
        Hold = 4,
        HorizontalDrag = 8,
        VerticalDrag = 16,
        FreeDrag = 32,
        Pinch = 64,
        Flick = 128,
        DragComplete = 256,
        PinchComplete = 512
    }
}