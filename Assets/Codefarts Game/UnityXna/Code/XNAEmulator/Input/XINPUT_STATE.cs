using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct XINPUT_STATE
    {
        public int PacketNumber;
        public XINPUT_GAMEPAD GamePad;
    }
}
