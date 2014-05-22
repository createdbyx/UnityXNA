using Microsoft.Xna.Framework;
using System;

namespace Microsoft.Xna.Framework.Input.Touch
{
    public struct GestureSample
    {
        internal GestureType _gestureType;

        internal TimeSpan _timestamp;

        internal Vector2 _position;

        internal Vector2 _position2;

        internal Vector2 _delta;

        internal Vector2 _delta2;

        public Vector2 Delta
        {
            get
            {
                return this._delta;
            }
        }

        public Vector2 Delta2
        {
            get
            {
                return this._delta2;
            }
        }

        public GestureType GestureType
        {
            get
            {
                return this._gestureType;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }
        }

        public Vector2 Position2
        {
            get
            {
                return this._position2;
            }
        }

        public TimeSpan Timestamp
        {
            get
            {
                return this._timestamp;
            }
        }

        public GestureSample(GestureType gestureType, TimeSpan timestamp, Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2)
        {
            this._gestureType = gestureType;
            this._timestamp = timestamp;
            this._position = position;
            this._position2 = position2;
            this._delta = delta;
            this._delta2 = delta2;
        }
    }
}