﻿using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
    internal class DrawOrderComparer : IComparer<IDrawable>
    {
        public readonly static DrawOrderComparer Default;

        static DrawOrderComparer()
        {
            DrawOrderComparer.Default = new DrawOrderComparer();
        }

        public DrawOrderComparer()
        {
        }

        public int Compare(IDrawable x, IDrawable y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            if (x.Equals(y))
            {
                return 0;
            }
            if (x.DrawOrder < y.DrawOrder)
            {
                return -1;
            }
            return 1;
        }
    }
}