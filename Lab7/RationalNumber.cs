using System;
using System.Globalization;

namespace Lab_7
{
    class RationalNumber : IComparable<RationalNumber>, IEquatable<RationalNumber>, IFormattable
    {
        public long Numerator { get; private set; }

        private long _denominator;

        public long Denominator
        {
            get
            {
                return _denominator;
            }
            private set
            {
                if (value == 0) throw new DivideByZeroException("Division by zero.");

                if (value < 0)
                {
                    Numerator *= -1;
                    _denominator = -1 * value;
                    return;
                }

                _denominator = value;
            }
        }

        public void ReduceFraction()
        {
            //GCD - Greatest Common Divisor
            long tempGCD = SimpleMath.GreatestCommonDivisor(Numerator, Denominator);

            Numerator /= tempGCD;
            Denominator /= tempGCD;
        }

        public static void ToCommonDenominator(RationalNumber rational1, RationalNumber rational2)
        {
            long temp = SimpleMath.LeastCommonMultiple(rational1.Denominator,
                                                       rational2.Denominator);

            rational1.Numerator *= (temp / rational1.Denominator);
            rational2.Numerator *= (temp / rational2.Denominator);

            rational1.Denominator = rational2.Denominator = temp;
        }

        public RationalNumber() : this(1, 1)
        {}

        public RationalNumber(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public RationalNumber(RationalNumber rational)
        {
            Numerator = rational.Numerator;
            Denominator = rational.Denominator;
        }


        public static RationalNumber operator +(RationalNumber rational)
        {
            return rational;
        }

        public static RationalNumber operator -(RationalNumber rational)
        {
            return new RationalNumber(-rational.Numerator, rational.Denominator);
        }


        public static RationalNumber operator +(RationalNumber rational1, RationalNumber rational2)
        {

            RationalNumber sum = new RationalNumber();

            sum.Denominator = SimpleMath.LeastCommonMultiple(rational1.Denominator,
                                                              rational2.Denominator);

            sum.Numerator = rational1.Numerator * sum.Denominator / rational1.Denominator +
                            rational2.Numerator * sum.Denominator / rational2.Denominator;

            return sum;
        }

        public static RationalNumber operator +(RationalNumber rational, int integer32)
        {
            return new RationalNumber(rational.Numerator + rational.Denominator * integer32,
                                      rational.Denominator); ;
        }


        public static RationalNumber operator -(RationalNumber rational1, RationalNumber rational2)
        {
            return rational1 + (-rational2);
        }

        public static RationalNumber operator -(RationalNumber rational, int integer32)
        {
            return rational + (-integer32);
        }


        public static RationalNumber operator *(RationalNumber rational1, RationalNumber rational2)
        {
            return new RationalNumber(rational1.Numerator * rational2.Numerator,
                                      rational1.Denominator * rational2.Denominator);
        }

        public static RationalNumber operator *(RationalNumber rational, int integer32)
        {
            return new RationalNumber(rational.Numerator * integer32, rational.Denominator);
        }


        public static RationalNumber operator /(RationalNumber rational1, RationalNumber rational2)
        {
            if (rational2.Numerator == 0) throw new DivideByZeroException("Division by zero.");

            return new RationalNumber(rational1.Numerator * rational2.Denominator,
                                      rational1.Denominator * rational2.Numerator);
        }

        public static RationalNumber operator /(RationalNumber rational, int integer32)
        {
            if (integer32 == 0) throw new DivideByZeroException("Division by zero.");

            return new RationalNumber(rational.Numerator, rational.Denominator * integer32);
        }


        public override int GetHashCode()
        {
            return Numerator.GetHashCode();
        }

        public bool Equals(RationalNumber rational)
        {
            if (rational == null) return false;

            return this == rational;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            RationalNumber temp = obj as RationalNumber;

            return this == temp;
        }


        public static bool operator ==(RationalNumber rational1, RationalNumber rational2)
        {
            RationalNumber temp1 = rational1;
            RationalNumber temp2 = rational2;

            temp1.ReduceFraction();
            temp2.ReduceFraction();

            return temp1.Numerator == temp2.Numerator && temp1.Denominator == temp2.Denominator;
        }

        public static bool operator ==(RationalNumber rational, int integer32)
        {
            return rational.Numerator == rational.Denominator * integer32;
        }


        public static bool operator !=(RationalNumber rational1, RationalNumber rational2)
        {
            return !(rational1 == rational2);
        }

        public static bool operator !=(RationalNumber rational, int integer32)
        {
            return !(rational == integer32);
        }


        public static bool operator <(RationalNumber rational1, RationalNumber rational2)
        {
            if (rational1.Numerator < 0 && rational2.Numerator >= 0) return true;
            if (rational1.Numerator >= 0 && rational2.Numerator < 0) return false;

            RationalNumber temp1 = rational1;
            RationalNumber temp2 = rational2;

            ToCommonDenominator(temp1, temp2);

            if (temp1.Numerator < temp2.Numerator) return true;

            return false;
        }

        public static bool operator <(RationalNumber rational, int integer32)
        {
            if (rational.Numerator < 0 && integer32 >= 0) return true;
            if (rational.Numerator >= 0 && integer32 < 0) return false;

            if (rational.Numerator < rational.Denominator * integer32) return true;

            return false;
        }


        public static bool operator >(RationalNumber rational1, RationalNumber rational2)
        {
            return rational2 < rational1 && rational1 != rational2;
        }

        public static bool operator >(RationalNumber rational, int integer32)
        {
            return integer32 < rational && rational != integer32;
        }


        public static bool operator <=(RationalNumber rational1, RationalNumber rational2)
        {
            return rational1 < rational2 || rational1 == rational2;
        }

        public static bool operator <=(RationalNumber rational, int integer32)
        {
            return rational < integer32 || rational == integer32;
        }


        public static bool operator >=(RationalNumber rational1, RationalNumber rational2)
        {
            return rational1 > rational2 || rational1 == rational2;
        }

        public static bool operator >=(RationalNumber rational, int integer32)
        {
            return rational > integer32 || rational == integer32;
        }


        public static explicit operator float(RationalNumber rational)
        {
            return (float)rational.Numerator / rational.Denominator;
        }

        public static explicit operator double(RationalNumber rational)
        {
            return (double)rational.Numerator / rational.Denominator;
        }

        public static explicit operator decimal(RationalNumber rational)
        {
            return (decimal)rational.Numerator / rational.Denominator;
        }

        public static explicit operator int(RationalNumber rational)
        {
            return (int)rational.Numerator / (int)rational.Denominator;
        }

        public static explicit operator long(RationalNumber rational)
        {
            return rational.Numerator / rational.Denominator;
        }

        public static implicit operator RationalNumber(int int32)
        {
            return new RationalNumber(int32, 1);
        }

        public static implicit operator RationalNumber(long int64)
        {
            return new RationalNumber(int64, 1);
        }

        public int CompareTo(RationalNumber toCompare)
        {
            if (toCompare == null) return 1;

            if (toCompare < this) return -1;
            if (toCompare == this) return 0;
            return 1;
        }

        public static bool TryParse(string toParse, out RationalNumber rational)
        {
            rational = new RationalNumber();
            string format;

            if (String.IsNullOrEmpty(toParse))
            {
                return false;
            }

            string[] spletedWords = toParse.Split();

            if (spletedWords.Length > 3)
            {
                return false;
            }

            if (spletedWords[0][0] == '(')
            {
                format = "B";
            }
            else if (Array.IndexOf(spletedWords, "+") >= 0 && spletedWords.Length == 3)
            {
                format = "P";
            }
            else if (Char.IsDigit(spletedWords[0][0]))
            {
                format = "C";
            }
            else 
            {
                format = "";
            }

            if (format == "")
            {
                return false;
            }

            if (format == "C" || format == "B")
            {
                string[] numbers = spletedWords[0].Split('/');

                if (numbers.Length != 2)
                {
                    return false;
                }

                long numerator;
                long denominator;

                if (format == "B")
                {
                    if (numbers[0][0] != '(' || numbers[1][numbers[1].Length - 1] != ')')
                    {
                        return false;
                    }

                    numbers[0] = numbers[0].Remove(0, 1);
                    numbers[1] = numbers[1].Remove(numbers[1].Length - 1, 1);
                }

                if (!long.TryParse(numbers[0], out numerator))
                {
                    return false;
                }

                if (!long.TryParse(numbers[0], out denominator))
                {
                    return false;
                }

                rational.Numerator = numerator;
                rational.Denominator = denominator;
                return true;
            }

            if (format == "P")
            {
                if (spletedWords[1] != "+" )
                {
                    return false;
                }

                long integerPart;

                if (!long.TryParse(spletedWords[0], out integerPart))
                {
                    return false;
                }

                string[] numbers = spletedWords[2].Split('/');

                if (numbers.Length != 2)
                {
                    return false;
                }

                long numerator;
                long denominator;

                if (!long.TryParse(numbers[0], out numerator))
                {
                    return false;
                }

                if (!long.TryParse(numbers[0], out denominator))
                {
                    return false;
                }

                rational.Numerator = numerator;
                rational.Denominator = denominator;

                rational += integerPart;
                return true;
            }

            return false;
        }

        public override string ToString() { return $"{Numerator}/{Denominator}"; }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format))
            {
                format = "C";
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                //Current fraction
                case "C":
                    {
                        return ToString();
                    }
                //Reduced fraction
                case "R":
                    {
                        RationalNumber temp = this;
                        temp.ReduceFraction();

                        return temp.ToString();
                    }
                //Proper fraction
                case "P":
                    {
                        return $"{Numerator / Denominator} + {Numerator % Denominator}/{Denominator}";
                    }
                //Current fraction with brackets
                case "B":
                    {
                        return $"({this})";
                    }
                default:
                    {
                        throw new FormatException(String.Format($"The {format} format string is not supported."));
                    }
            }
        }
    }
}