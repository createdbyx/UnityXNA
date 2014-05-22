namespace Microsoft.Xna.Framework.Input.Touch
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public struct TouchCollection : IList<TouchLocation>, ICollection<TouchLocation>, IEnumerable<TouchLocation>, IEnumerable
    {
        public TouchLocation this[int index] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public int Count
        {
            get { throw new NotImplementedException(); }
        }
        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
        public int IndexOf(TouchLocation item)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, TouchLocation item)
        {
            throw new NotImplementedException();
        }
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(TouchLocation item)
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool Contains(TouchLocation item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(TouchLocation[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(TouchLocation item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TouchLocation> GetEnumerator()
        {
            return (IEnumerator<TouchLocation>)new TouchCollectionEnum(new TouchLocation[0]);
            // TODO:    
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal void Update(ref XNAINPUT_TOUCH_LOCATION_STATE prevState, ref XNAINPUT_TOUCH_LOCATION_STATE newState, bool connected)
        {
            throw new NotImplementedException();
        }
    }

    public class TouchCollectionEnum : IEnumerator<TouchLocation>
    {
        int position = -1;
        public TouchLocation[] _touches;


        public TouchCollectionEnum(TouchLocation[] list)
        {
            _touches = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _touches.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public TouchLocation Current
        {
            get
            {
                try
                {
                    return _touches[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {}
    }
}
