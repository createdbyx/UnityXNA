namespace Microsoft.Xna.Framework.Input.Touch
{
    using Codefarts.Localization;

    using Microsoft.Xna.Framework;
    using System;

    public static class TouchPanel
        {
            private const GestureType AllGestureTypes = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.FreeDrag | GestureType.Pinch | GestureType.Flick | GestureType.DragComplete | GestureType.PinchComplete;

            private static bool nointerop;

            private static XNAINPUT_TOUCH_LOCATION_STATE prevState;

            private static TouchCollection touchState;

            private static DisplayOrientation displayOrientation;

            private static int displayWidth;

            private static int displayHeight;

            private static bool displaySettingsChanged;

            private static GestureType _enabledGestures;

            private static bool _haveGestureBeenEnabled;

            public static int DisplayHeight
            {
                get
                {
                    return TouchPanel.displayHeight;
                }
                set
                {
                    TouchPanel.displayHeight = value;
                    TouchPanel.displaySettingsChanged = true;
                }
            }

            private static void ValidateOrientation(DisplayOrientation orientation)
            {
                if (orientation != DisplayOrientation.Default && orientation != DisplayOrientation.LandscapeLeft && orientation != DisplayOrientation.LandscapeRight && orientation != DisplayOrientation.Portrait)
                {
                    throw new ArgumentException(LocalizationManager.Instance.Get("XNA_InvalidDisplayOrientation"));
                }
            }

            public static DisplayOrientation DisplayOrientation
            {
                get
                {
                    return TouchPanel.displayOrientation;
                }
                set
                {
                    ValidateOrientation(value);
                    TouchPanel.displayOrientation = value;
                    TouchPanel.displaySettingsChanged = true;
                }
            }

            public static int DisplayWidth
            {
                get
                {
                    return TouchPanel.displayWidth;
                }
                set
                {
                    TouchPanel.displayWidth = value;
                    TouchPanel.displaySettingsChanged = true;
                }
            }

            public static GestureType EnabledGestures
            {
                get
                {
                    return TouchPanel._enabledGestures;
                }
                set
                {
                    if (((int)value & -1024) != (int)GestureType.None)
                    {
                        throw new ArgumentException("EnabledGestures");
                    }

                    TouchPanel._enabledGestures = value;
                    TouchPanel._haveGestureBeenEnabled = true;
                }
            }

            public static bool IsGestureAvailable
            {
                get
                {
                    if (!TouchPanel._haveGestureBeenEnabled)
                    {
                        throw new InvalidOperationException(LocalizationManager.Instance.Get("XNA_GesturesNotEnabled"));
                    }
                    return false;
                }
            }

            public static IntPtr WindowHandle
            {
                get
                {
                    return IntPtr.Zero; // Framework.Input.Touch.WindowHandle;
                }

                set
                {
                   // Framework.Input.Touch.WindowHandle = value;
                }
            }

            static TouchPanel()
            {
                TouchPanel.nointerop = false;
                TouchPanel.touchState = new TouchCollection();
            }

            public static TouchPanelCapabilities GetCapabilities()
            {
                return TouchPanelCapabilities.GetCaps();
            }

            public static TouchCollection GetState()
            {
                XNAINPUT_TOUCH_LOCATION_STATE xNAINPUTTOUCHLOCATIONSTATE = new XNAINPUT_TOUCH_LOCATION_STATE();
                if (!TouchPanel.nointerop)
                {
                    try
                    {
                        if (TouchPanel.displaySettingsChanged)
                        {
                            TouchPanel.OnDisplaySettingsChanged();
                        }
                        TouchPanel.touchState.Update(ref TouchPanel.prevState, ref xNAINPUTTOUCHLOCATIONSTATE, true);
                        TouchPanel.prevState = xNAINPUTTOUCHLOCATIONSTATE;
                    }
                    catch
                    {
                        TouchPanel.nointerop = true;
                        TouchPanel.touchState.Update(ref TouchPanel.prevState, ref xNAINPUTTOUCHLOCATIONSTATE, false);
                    }
                }

                return TouchPanel.touchState;
            }

            private static void OnDisplaySettingsChanged()
            {
                TouchPanel.prevState = new XNAINPUT_TOUCH_LOCATION_STATE();
                TouchPanel.touchState = new TouchCollection();
                TouchPanel.displaySettingsChanged = false;
            }

            public static GestureSample ReadGesture()
            {
                if (TouchPanel._haveGestureBeenEnabled)
                {
                    throw new InvalidOperationException(LocalizationManager.Instance.Get("XNA_GesturesNotAvailable"));
                }

                throw new InvalidOperationException(LocalizationManager.Instance.Get("XNA_GesturesNotEnabled"));
            }
        }
    }

    //public class TouchPanel
    //{
    //    internal static TouchCollection GetState()
    //    {
    //        // TODO:
    //        return new TouchCollection();
    //    }   
    //}
