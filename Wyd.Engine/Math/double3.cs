using System.Numerics;
using System.Runtime.CompilerServices;

namespace Wyd.Engine.Math
{
    public class double3
    {
        #region DEFAULT STATIC MEMBERS

        public static double3 Zero => (double3) double4.Zero;
        public static double3 One => (double3) double4.One;
        public static double3 Forward => (double3) double4.Forward;
        public static double3 Back => (double3) double4.Back;
        public static double3 Right => (double3) double4.Right;
        public static double3 Left => (double3) double4.Left;
        public static double3 Up => (double3) double4.Up;
        public static double3 Down => (double3) double4.Down;

        #endregion

        private readonly Vector<double> _Values;

        public double X => _Values[0];
        public double Y => _Values[1];
        public double Z => _Values[2];

        public double3(Vector<double> values) => _Values = values;

        public double3(double a, double b, double c)
        {
            _Values = new Vector<double>(new[]
            {
                a,
                b,
                c,
                0,
                0,
                0,
                0,
                0
            });
        }


        public static implicit operator Vector<double>(double3 a) => a._Values;

        public static implicit operator double3(Vector<double> a) => new double3(a);

        public static explicit operator double(double3 a) => a._Values[0];

        public static explicit operator double2(double3 a) => a._Values;

        public static explicit operator double4(double3 a) => a._Values;


        #region OPERATOR OVERLOADS - double3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator +(double3 a, double3 b) => new double3(a._Values + b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator -(double3 a, double3 b) => new double3(a._Values - b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator *(double3 a, double3 b) => new double3(a._Values * b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator /(double3 a, double3 b) => new double3(a._Values / b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator ^(double3 a, double3 b) => new double3(a._Values ^ b._Values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator ++(double3 a)
        {
            a += One;
            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 operator --(double3 a)
        {
            a -= One;
            return a;
        }

        #endregion
    }
}
