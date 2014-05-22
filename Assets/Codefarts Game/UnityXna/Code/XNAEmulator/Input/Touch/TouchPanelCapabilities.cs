namespace Microsoft.Xna.Framework.Input.Touch
{
    public struct TouchPanelCapabilities
    {
        public bool IsConnected
        {
            get;
            private set;
        }

        public int MaximumTouchCount
        {
            get;
            private set;
        }

        internal static TouchPanelCapabilities GetCaps()
        {
            return new TouchPanelCapabilities();
        }
    }
}