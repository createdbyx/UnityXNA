#if UNITY3D
namespace Microsoft.Xna.Framework
{
    using UnityEngine;

    public class XnaGameBehavior : MonoBehaviour
    {
        public bool IsPaused;
        public bool IsFocused ;

        private void OnApplicationPause(bool pauseStatus)
        {
            this.IsPaused = pauseStatus;
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            this.IsFocused = focusStatus;
        }
    }
} 
#endif