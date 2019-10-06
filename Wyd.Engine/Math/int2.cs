#region

using System.Numerics;

#endregion

namespace Wyd.Engine.Math
{
    public struct int2
    {
        #region DEFAULT STATIC MEMBERS

        public static int2 Zero => new int2(0, 0);
        public static int2 One => new int2(1, 1);
        public static int2 Left => new int2(-1, 0);
        public static int2 Right => new int2(1, 0);
        public static int2 Up => new int2(0, 1);
        public static int2 Down => new int2(0, -1);

        #endregion

        private readonly Vector<int> _Values;

        public int X => _Values[0];
        public int Y => _Values[1];

        public int2(Vector<int> values) => _Values = values;

        public int2(int a, int b)
        {
            _Values = new Vector<int>(new[]
            {
                a,
                b,
                0,
                0,
                0,
                0,
                0,
                0
            });
        }


        #region OPERATOR OVERLOADS - int2

        public static int2 operator +(int2 a, int2 b) => new int2(a._Values + b._Values);

        public static int2 operator -(int2 a, int2 b) => new int2(a._Values - b._Values);

        public static int2 operator *(int2 a, int2 b) => new int2(a._Values * b._Values);

        public static int2 operator /(int2 a, int2 b) => new int2(a._Values / b._Values);

        public static int2 operator ^(int2 a, int2 b) => new int2(a._Values ^ b._Values);

        public static int2 operator ++(int2 a)
        {
            a += One;
            return a;
        }

        public static int2 operator --(int2 a)
        {
            a -= One;
            return a;
        }

        #endregion
    }
}
