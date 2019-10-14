#region

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

#endregion

namespace Wyd.Engine.Math
{
    public struct int4
    {
        #region DEFAULT STATIC MEMBERS

        public static int4 Zero => new int4(Vector<int>.Zero);
        public static int4 One => new int4(Vector<int>.One);
        public static int4 Forward => new int4(0, 0, 1, 0);
        public static int4 Back => new int4(0, 0, -1, 0);
        public static int4 Left => new int4(-1, 0, 0, 0);
        public static int4 Right => new int4(1, 0, 0, 0);
        public static int4 Up => new int4(0, 1, 0, 0);
        public static int4 Down => new int4(0, -1, 0, 0);

        #endregion

        private readonly Vector<int> _Values;

        public int X => _Values[0];
        public int Y => _Values[1];
        public int Z => _Values[2];
        public int W => _Values[3];

        public int4(Vector<int> values) => _Values = values;

        public int4(params int[] args) => _Values = new Vector<int>(new Span<int>(args, 0, 8));


        #region CONVERSION OPERATORS

        public static implicit operator Vector<int>(int4 a) => a._Values;

        public static implicit operator int4(Vector<int> a) => new int4(a);

        public static explicit operator int(int4 a) => a._Values[0];

        public static explicit operator int2(int4 a) => a._Values;

        public static explicit operator int3(int4 a) => a._Values;

        #endregion


        #region ARITHMETIC OPERATORS

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator +(int4 a, int4 b) => a._Values + b._Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator -(int4 a, int4 b) => a._Values - b._Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator *(int4 a, int4 b) => a._Values * b._Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator /(int4 a, int4 b) => a._Values / b._Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator ^(int4 a, int4 b) => a._Values ^ b._Values;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator ++(int4 a)
        {
            a += One;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4 operator --(int4 a)
        {
            a -= One;
            return a;
        }

        #endregion
    }
}
