using System;
using System.Numerics;

namespace Karatsuba
{
    // Let's work in Base 10 for now
    public static class Karatsuba
    {
        public static int Base = 10;

        // x = 10^n/2 a + b
        // y = 10^n/2 c + d
        // x.y = 10^n ac + 10^(n/2)(ad + bc) + bd
        public static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            if (x < 10 || y < 10)
            {
                return x*y;
            }
            var n = Math.Max(GetBasePower(x), GetBasePower(y));
            var halfN = n/2;

            var split = Split(x, halfN);
            var a = split[0];
            var b = split[1];

            split = Split(y, halfN);
            var c = split[0];
            var d = split[1];

            var ac = Multiply(a, c);
            var bd = Multiply(b, d);

            var middle = Multiply(a + b, c + d) - ac - bd;

            return BigInteger.Pow(10, halfN * 2)*ac + BigInteger.Pow(10, halfN)*middle + bd;
        }

        public static int GetBasePower(BigInteger x)
        {
            return Convert.ToInt32(Math.Floor(BigInteger.Log10(x)) + 1);
        }

        // x = 10^n/2 a + b
        // Returns [a, b]
        public static BigInteger[] Split(BigInteger x, int halfN)
        {
            BigInteger remainder;
            var quotient = BigInteger.DivRem(x, BigInteger.Pow(Base, halfN), out remainder);
            return new[] { quotient, remainder };
        }
    }
}
