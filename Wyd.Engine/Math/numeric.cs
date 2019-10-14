#region

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

#endregion

namespace Wyd.Engine.Math
{
    public class numeric<T> where T : unmanaged
    {
        #region DEFAULT STATIC MEMBERS

        public static numeric<T> Zero => new numeric<T>(Vector<T>.Zero);
        public static numeric<T> One => new numeric<T>(Vector<T>.One);

        #endregion

        protected readonly Vector<T> Values;

        public T this[int index] => Values[index];

        public numeric(Vector<T> values) => Values = values;
        public numeric(params T[] args) => Values = new Vector<T>(new Span<T>(args, 0, 8));

        #region IMPLICIT CAST OPERATORS

        public static implicit operator Vector<T>(numeric<T> a) => a.Values;
        public static implicit operator numeric<T>(Vector<T> a) => new numeric<T>(a);

        #endregion

        #region OPERATOR OVERLOADS

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator +(numeric<T> a, numeric<T> b) => new numeric<T>(a.Values + b.Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator -(numeric<T> a, numeric<T> b) => new numeric<T>(a.Values - b.Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator *(numeric<T> a, numeric<T> b) => new numeric<T>(a.Values * b.Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator /(numeric<T> a, numeric<T> b) => new numeric<T>(a.Values / b.Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator ^(numeric<T> a, numeric<T> b) => new numeric<T>(a.Values ^ b.Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator ++(numeric<T> a)
        {
            a += One;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static numeric<T> operator --(numeric<T> a)
        {
            a -= One;
            return a;
        }

        #endregion
    }
}
