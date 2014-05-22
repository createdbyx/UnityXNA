namespace Microsoft.Xna.Framework.Input
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    using Vector2 = Microsoft.Xna.Framework.Vector2;

    public static class GamePad
    {
        internal static GamePadState GetState(PlayerIndex playerIndex)
        {
            // TODO: Improve
            var buttons = new List<Buttons>();
            foreach (var name in Enum.GetNames(typeof(Buttons)))
            {
                try
                {
                    var value = (Buttons)Enum.Parse(typeof(Buttons), name, true);
                    if (!Enum.IsDefined(typeof(Buttons), value) | value.ToString().Contains(","))
                    {
                        Debug.LogError(string.Format("{0} is not an underlying value of the Buttons enumeration.", name));
                        continue;
                    }

                    if (Input.GetButton(name))
                    {
                        buttons.Add(value);
                    }
                }
                catch (Exception)
                {
                    // Debug.LogException(ae);
                }
            }

            float leftTrigger = 0;
            float rightTrigger = 0;
            var leftThumbStick = new Vector2();
            var rightThumbStick = new Vector2();

            try
            {
                leftTrigger = Input.GetAxis("LeftTrigger");
                rightTrigger = Input.GetAxis("RightTrigger");
                leftThumbStick = new Vector2(Input.GetAxis("LeftThumbStickX"), Input.GetAxis("LeftThumbStickY"));
                rightThumbStick = new Vector2(Input.GetAxis("RightThumbStickX"), Input.GetAxis("RightThumbStickY"));
            }
            catch (Exception)
            {
                // Debug.LogException(ae);
            }

            return new GamePadState(leftThumbStick, rightThumbStick, leftTrigger, rightTrigger, buttons.ToArray());
        }
    }
}