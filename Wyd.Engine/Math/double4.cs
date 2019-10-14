#region

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

#endregion

namespace Wyd.Engine.Math
{
    public struct double4
    {
        #region DEFAULT STATIC MEMBERS

        public static double4 Zero => new double4(Vector<double>.Zero);
        public static double4 One => new double4(Vector<double>.One);
        public static double4 Forward => new double4(0, 0, 1, 0);
        public static double4 Back => new double4(0, 0, -1, 0);
        public static double4 Left => new double4(-1, 0, 0, 0);
        public static double4 Right => new double4(1, 0, 0, 0);
        public static double4 Up => new double4(0, 1, 0, 0);
        public static double4 Down => new double4(0, -1, 0, 0);

        #endregion

        private readonly Vector<double> _Values;

        public double X => _Values[0];
        public double Y => _Values[1];
        public double Z => _Values[2];
        public double W => _Values[3];

        public double4(Vector<double> values) => _Values = values;

        public double4(params double[] args) => _Values = new Vector<double>(new Span<double>(args, 0, 8));


        #region CONVERSION OPERATORS

        public static implicit operator Vector<double>(double4 a) => a._Values;

        public static implicit operator double4(Vector<double> a) => new double4(a);

        public static explicit operator double(double4 a) => a._Values[0];

        public static explicit operator double2(double4 a) => a._Values;

        public static explicit operator double3(double4 a) => a._Values;

        #endregion

        #region ARITHMETIC OPERATORS

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator +(double4 a, double4 b) => new double4(a._Values + b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator -(double4 a, double4 b) => new double4(a._Values - b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator *(double4 a, double4 b) => new double4(a._Values * b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator /(double4 a, double4 b) => new double4(a._Values / b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator ^(double4 a, double4 b) => new double4(a._Values ^ b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator ++(double4 a)
        {
            a += One;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double4 operator --(double4 a)
        {
            a -= One;
            return a;
        }

        #endregion
    }
}
