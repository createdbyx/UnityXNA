namespace Microsoft.Xna.Framework.Input
{
    public class KeyboardState
    {
        private bool[] keyStates;

        public KeyboardState(bool[] keyStates)
        {
            // TODO: Complete member initialization
            this.keyStates = keyStates;
        }

        internal bool IsKeyDown(Keys keys)
        {
            return keyStates[(int)keys];
        }

        public bool IsKeyUp(Keys keys)
        {
            return !keyStates[(int)keys];
        }
    }
}
