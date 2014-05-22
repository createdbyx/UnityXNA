#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Authors: Rob Loach (http://www.robloach.net)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

/*
using System;

namespace Microsoft.Xna.Framework.Input
{
    public struct GamePadThumbSticks
    {
        #region Private Fields

        internal Vector2 left;
        internal Vector2 right;

        #endregion Private Fields

        #region Public Properties

        public Vector2 Left
        {
            get { return this.left; }
        }


        public Vector2 Right
        {
            get { return this.right; }
        }

        #endregion


        #region Public methods

        public static bool operator !=(GamePadThumbSticks left, GamePadThumbSticks right)
        {
            return !(left == right);
        }

        public static bool operator ==(GamePadThumbSticks left, GamePadThumbSticks right)
        {
            return ((left.left == right.left) && (left.right == right.right));
        }

        public override bool Equals(object obj)
        {
            return (obj is GamePadThumbSticks) ? ((GamePadThumbSticks)obj) == this : false;
        }

        public override int GetHashCode()
        {
            return this.left.GetHashCode() ^ this.right.GetHashCode();
        }

        public GamePadThumbSticks(Microsoft.Xna.Framework.Vector2 leftThumbstick, Microsoft.Xna.Framework.Vector2 rightThumbstick)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("{{Left:{0} Right:{1}}}", left, right);
        }

        #endregion Public Methods
    }
}
*/

using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GamePadThumbSticks
    {
        public bool Equals(GamePadThumbSticks other)
        {
            return this._left.Equals(other._left) && this._right.Equals(other._right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this._left.GetHashCode() * 397) ^ this._right.GetHashCode();
            }
        }

        internal Vector2 _left;
        internal Vector2 _right;
        public GamePadThumbSticks(Vector2 leftThumbstick, Vector2 rightThumbstick)
        {
            this._left = leftThumbstick;
            this._right = rightThumbstick;
            this._left = Vector2.Min(this._left, Vector2.One);
            this._left = Vector2.Max(this._left, -Vector2.One);
            this._right = Vector2.Min(this._right, Vector2.One);
            this._right = Vector2.Max(this._right, -Vector2.One);
        }

        public Vector2 Left
        {
            get
            {
                return this._left;
            }
        }
        public Vector2 Right
        {
            get
            {
                return this._right;
            }
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is GamePadThumbSticks && Equals((GamePadThumbSticks)obj);
        }
               

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Left:{0} Right:{1}}}", new object[] { this._left, this._right });
        }

        public static bool operator ==(GamePadThumbSticks left, GamePadThumbSticks right)
        {
            return ((left._left == right._left) && (left._right == right._right));
        }

        public static bool operator !=(GamePadThumbSticks left, GamePadThumbSticks right)
        {
            if (!(left._left != right._left))
            {
                return (left._right != right._right);
            }
            return true;
        }
    }
}
