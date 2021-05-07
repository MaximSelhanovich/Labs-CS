using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    class Lab_7
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter fractions in such formats (E - to exit)");
            Console.WriteLine("1) a/b\n2) (a/b)\n3) a + b/c");

            List<RationalNumber> rationalNumbers = new List<RationalNumber>();

            RationalNumber rational;

            string fraction = Console.ReadLine();
            while (fraction != "E")
            {
                if (!RationalNumber.TryParse(fraction, out rational))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                }
                else
                {
                    rationalNumbers.Add(rational);
                }

                fraction = Console.ReadLine();
            }

            Console.WriteLine("Input: ");
            foreach (RationalNumber temp in rationalNumbers)
            {
                Console.WriteLine(temp.ToString("C"));
            }

            rationalNumbers.Sort();

            Console.WriteLine("Sorted variant: ");
            foreach (RationalNumber temp in rationalNumbers)
            {
                Console.WriteLine(temp.ToString("P"));
            }

            RationalNumber sumThemAll = new RationalNumber(0, 1);

            foreach (RationalNumber temp in rationalNumbers)
            {
                Console.WriteLine(temp.ToString("B"));
                sumThemAll += temp;
            }

            Console.WriteLine($"Total sum(double): {sumThemAll} = {(double)sumThemAll}");

            Console.WriteLine($"Total sum(long): {sumThemAll} = {(long)sumThemAll}");
        }
    }
}
