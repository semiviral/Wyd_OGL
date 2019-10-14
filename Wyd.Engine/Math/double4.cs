#region

using System.Numerics;
using System.Runtime.CompilerServices;

#endregion

namespace Wyd.Engine.Math
{
    public class double4 : numeric<double>
    {
        #region DEFAULT STATIC MEMBERS

        public static double4 Forward => new double4(0, 0, 1, 0);
        public static double4 Back => new double4(0, 0, -1, 0);
        public static double4 Left => new double4(-1, 0, 0, 0);
        public static double4 Right => new double4(1, 0, 0, 0);
        public static double4 Up => new double4(0, 1, 0, 0);
        public static double4 Down => new double4(0, -1, 0, 0);

        #endregion

        public double X => Values[0];
        public double Y => Values[1];
        public double Z => Values[2];
        public double W => Values[3];

        public double4(double a, double b, double c, double d) : base(a, b, c, d)
        {
            double4 e = default;
            double4 f = default;

            e += f;
        }

        public static implicit operator Vector<double>(double4 a) => a.Values;

        public static implicit operator numeric<double>(double4 a)
        {
            
        }
        
        public static explicit operator double(double4 a) => a.Values[0];

        public static explicit operator double2(double4 a) => a.Values;

        public static explicit operator double3(double4 a) => a.Values;
    }
}
