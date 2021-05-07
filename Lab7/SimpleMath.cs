using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    static class SimpleMath
    {
        // Use Euclid's algorithm
        public static long GreatestCommonDivisor(long number1, long number2)
        {
            number1 = Math.Abs(number1);
            number2 = Math.Abs(number2);

            for (; ; )
            {
                long remainder = number1 % number2;

                if (remainder == 0) return number2;

                number1 = number2;
                number2 = remainder;
            }
        }

        // Use connection betwen LCM and GCD
        public static long LeastCommonMultiple(long number1, long number2)
        {
            return number1 * number2 / GreatestCommonDivisor(number1, number2);
        }

    }
}
