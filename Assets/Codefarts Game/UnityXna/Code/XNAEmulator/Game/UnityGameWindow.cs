namespace Microsoft.Xna.Framework
{
    using System;
    using System.Runtime.InteropServices;

    using UnityEngine;

    class UnityGameWindow : GameWindow
    {
#if UNITY_STANDALONE_WIN
        //Import the following.
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern System.IntPtr FindWindow(System.String className, System.String windowName);
#endif

        public override bool AllowUserResizing { get; set; }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            throw new NotImplementedException();
        }

        public override Rectangle ClientBounds
        {
            get { return new Rectangle(0, 0, Screen.width, Screen.height); }
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
            throw new NotImplementedException();
        }

        public override IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public override string ScreenDeviceName
        {
            get { throw new NotImplementedException(); }
        }
        
        protected override void SetTitle(string title)
        {
#if UNITY_STANDALONE_WIN
            ////Get the window handle.
            //var windowPtr = FindWindow(null, "Old Window Title");
            ////Set the title text using the window handle.
            //SetWindowText(windowPtr, "New Window Title - Yayyy");  
#endif
        }
    }
}
