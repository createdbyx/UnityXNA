namespace Microsoft.Xna.Framework.Input.Touch
{
    using System;

    public struct TouchLocation : IEquatable<TouchLocation>
    {
        public TouchLocationState State { get; set; }

        public bool Equals(TouchLocation touchLocation)
        {
            return touchLocation.State == this.State;
        }
    }
}

