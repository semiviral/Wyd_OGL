#region

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

#endregion

namespace Wyd.Engine.Math
{
    public class double2
    {
        #region DEFAULT STATIC MEMBERS

        public static double2 Zero => (double2)double4.Zero;
        public static double2 One => (double2)double4.One;
        public static double2 Forward => (double2)double4.Forward;
        public static double2 Back => (double2)double4.Back;
        public static double2 Right => (double2)double4.Right;
        public static double2 Left => (double2)double4.Left;
        public static double2 Up => (double2)double4.Up;
        public static double2 Down => (double2)double4.Down;

        #endregion

        private readonly Vector<double> _Values;

        public double X => _Values[0];
        public double Y => _Values[1];
        public double Z => _Values[2];
        public double W => _Values[3];

        public double2(Vector<double> values) => _Values = values;

        public double2(params double[] args)
        {
            double[] temp = new double[8];
            Array.Copy(args, temp, System.Math.Min(args.Length, 8));
            _Values = new Vector<double>(temp);
        }

        #region CONVERSION OPERATORS

        public static implicit operator Vector<double>(double2 a) => a._Values;

        public static implicit operator double2(Vector<double> a) => new double2(a);

        public static explicit operator double(double2 a) => a._Values[0];

        public static explicit operator double3(double2 a) => a._Values;

        public static explicit operator double4(double2 a) => a._Values;

        #endregion

        #region ARITHMETIC OPERATORS

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator +(double2 a, double2 b) => new double2(a._Values + b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator -(double2 a, double2 b) => new double2(a._Values - b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator *(double2 a, double2 b) => new double2(a._Values * b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator /(double2 a, double2 b) => new double2(a._Values / b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator ^(double2 a, double2 b) => new double2(a._Values ^ b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator ++(double2 a)
        {
            a += One;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator --(double2 a)
        {
            a -= One;
            return a;
        }

        #endregion
    }
}
