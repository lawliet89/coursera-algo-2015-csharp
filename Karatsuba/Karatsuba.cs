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
            // Base case -- can't split it further
            if (x < 10 || y < 10)
            {
                return x*y;
            }

            // We get the highest number of digits
            var n = Math.Max(GetBasePower(x), GetBasePower(y));
            // Half it, and flooring it in essence
            var halfN = n/2;

            // Split somewhere down the middle. So 12345 ==> 123, 45 because n = 5 and n/2 = 2
            var split = Split(x, halfN);
            var a = split[0];
            var b = split[1];

            split = Split(y, halfN);
            var c = split[0];
            var d = split[1];

            var ac = Multiply(a, c);
            var bd = Multiply(b, d);

            var middle = Multiply(a + b, c + d) - ac - bd;

            // This work even for odd n
            return BigInteger.Pow(10, halfN * 2)*ac + BigInteger.Pow(10, halfN)*middle + bd;
        }

        public static int GetBasePower(BigInteger x)
        {
            // Log10(100) = 2 but there are three digits. So Floor then add 1
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
