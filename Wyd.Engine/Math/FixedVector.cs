#region

using System.Numerics;

#endregion

namespace Wyd.Engine.Math
{
    public class FixedVector<T> where T : unmanaged
    {
        private readonly Vector<T> _Values;

        public int Length { get; private set; }

        public FixedVector(params T[] values)
        {
            _Values = new Vector<T>(values);
            Length = Vector<T>.Count;
        }

        public T this[int index] => _Values[index];
    }
}
