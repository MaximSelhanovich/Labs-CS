using System;
using System.Text;
using System.Globalization;

namespace Lab2
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool choise = selectTask();
            while (choise)
            {
                //Console.Clear();
                choise = selectTask();
            }
        }

        static bool selectTask()
        {
            Console.WriteLine("\nThree tasks are available:\n" +
                  "1) Change letters after vowels\n" +
                  "2) Parameters of triangle\n" +
                  "3) Months in differen languages\n" +
                  "4) Exit\n");
            Console.Write("Enter task number: ");

            uint taskNumber = getValidUint();
            while (taskNumber > 4 || taskNumber == 0)
            {
                Console.Clear();
                Console.WriteLine("Wrong input. There are only three tasks.");
                Console.Write("Enter task number: ");
                taskNumber = getValidUint();
            }
            Console.Clear();
            switch (taskNumber)
            {
                case 1:
                    ChangeLettersAfterVowels();
                    break;
                case 2:
                    triangleParameters();
                    break;
                case 3:
                    printMonthsInDifferentLanguages();
                    break;
                case 4:
                    return false;
                default:
                    return true;
            }
            Console.WriteLine("Task {0} is completed", taskNumber);
            return true;
        }

        static uint getValidUint()
        {
            uint tempUint;
            bool inputCheck = uint.TryParse(Console.ReadLine(), out tempUint);

            while (!inputCheck)
            {
                Console.Clear();
                Console.WriteLine("Oops, wrong input, try again");
                inputCheck = uint.TryParse(Console.ReadLine(), out tempUint);
            }
            return tempUint;
        }

        //--------------FIRST TASK STARTS-------------------------------

        static void ChangeLettersAfterVowels()
        {
            Console.WriteLine("Enter string for the task:");
            StringBuilder inputStringBuilder = new StringBuilder(Console.ReadLine());
            string helperString = inputStringBuilder.ToString();

            for (int i = 0; i < helperString.Length - 1; ++i)
            {
                //Such a weird check is needed because there are [\]^ _` between letters in unicode
                if (vowelCheck(helperString[i]) &&
                   ((helperString[i + 1] >= 'A' && helperString[i + 1] <= 'Z') ||
                   (helperString[i + 1] >= 'a' && helperString[i + 1] <= 'z')))
                {
                    if (helperString[i + 1] == 'Z')
                    {
                        inputStringBuilder[i + 1] = 'A';
                    }
                    else if (helperString[i + 1] == 'z')
                    {
                        inputStringBuilder[i + 1] = 'a';
                    }
                    else
                    {
                        inputStringBuilder[i + 1] += '\u0001';
                    }
                }
            }
            Console.WriteLine(inputStringBuilder);
        }

        static bool vowelCheck(char letter)
        {
            //IndexOf returns -1 if there are no matches
            //Letter "Y" is not always considered a vowel, so it is not here
            return "aeiouAEIOU".IndexOf(letter) >= 0;
        }
        //--------------FIRST TASK ENDS-------------------------------------------------

        //--------------SECOND TASK STARTS----------------------------------------------
        static void triangleParameters()
        {
            double sideA, sideB, sideC;
            double oppositeAngleToA, oppositeAngleToB, oppositeAngleToC;

            initialiseTriangleBasicParameters(out sideA, out sideB, out sideC,
                                              out oppositeAngleToA, out oppositeAngleToB,
                                              out oppositeAngleToC);

            Console.WriteLine("\nSides of triangle:\tAngles of triangle:\n" +
                              "side A = {0}\t\t Alpha = {1}\n" +
                              "side B = {2}\t\t Beta = {3}\n" +
                              "side C = {4}\t\t Gamma = {5}\n",
                               sideA, radiansToDegrees(oppositeAngleToA), 
                               sideB, radiansToDegrees(oppositeAngleToB),
                               sideC, radiansToDegrees(oppositeAngleToC));

            Console.WriteLine("Perimeter of triangle = {0}",
                              calcTrianglrPerimeter(sideA, sideB, sideC));
            Console.WriteLine("\nArea of triangle = {0}",
                              calctTiangleArea(sideA, sideB, oppositeAngleToC));
            Console.WriteLine("\nRadius of the circumscribed circle = {0}", 
                              calcTriangleCircumscribedRadius(sideA, oppositeAngleToA));
            Console.WriteLine("\nRadius of the inscribed circle = {0}\n",
                              calcTriangleInscribedRadius(sideA, sideB, sideC));
        }

        //Methods of initializing triangle parameters
        static void initialiseTriangleBasicParameters(out double sideA, out double sideB, out double sideC,
                                                      out double oppositeAngleToA, out double oppositeAngleToB,
                                                      out double oppositeAngleToC)
        {
            sideA = 0; sideB = 0; sideC = 0;
            oppositeAngleToA = 0; oppositeAngleToB = 0; oppositeAngleToC = 0;
            Console.WriteLine("Select input parameters:\n" +
                              "1) Three sides\n" +
                              "2) Two sides and an angle between them\n" +
                              "3) Side and two adjacent angles");
            uint inputParametersType = getValidUint();
            while (inputParametersType > 3)
            {
                Console.Clear();
                Console.WriteLine("Wrong input. There are only three types of input parameters.");
                inputParametersType = getValidUint();
            }
            Console.Clear();
            switch (inputParametersType)
            {
                case 1:
                    initializingUsingThreeSides(out sideA, out sideB, out sideC,
                                                out oppositeAngleToA, out oppositeAngleToB,
                                                out oppositeAngleToC);
                    break;
                case 2:
                    initializingUsingTwoSidesAndAngle(out sideA, out sideB, out sideC,
                                                      out oppositeAngleToA, out oppositeAngleToB,
                                                      out oppositeAngleToC);
                    break;
                case 3:
                    initializingUsingSideAndTwoAngle(out sideA, out sideB, out sideC,
                                                     out oppositeAngleToA, out oppositeAngleToB,
                                                     out oppositeAngleToC);
                    break;
            }
        }

        static void initializingUsingThreeSides(out double sideA, out double sideB, out double sideC,
                                                out double oppositeAngleToA, out double oppositeAngleToB,
                                                out double oppositeAngleToC)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("For a triangle, the sum of the two sides must be greater than the third");
                Console.Write("Enter side A of the triangle: ");
                sideA = getVlidDouble();
                Console.Write("\nEnter side B of the triangle: ");
                sideB = getVlidDouble();
                Console.Write("\nEnter side C of the triangle: ");
                sideC = getVlidDouble();
            }
            while (sideA + sideB < sideC ||
                   sideA + sideC < sideB ||
                   sideC + sideB < sideA);

            oppositeAngleToA = calcTriangleAngle(sideA, sideB, sideC);
            oppositeAngleToB = calcTriangleAngle(sideB, sideA, sideC);
            oppositeAngleToC = calcTriangleAngle(sideC, sideB, sideA);
        }

        static void initializingUsingTwoSidesAndAngle(out double sideA, out double sideB, out double sideC,
                                                      out double oppositeAngleToA, out double oppositeAngleToB,
                                                      out double oppositeAngleToC)
        {
            Console.Write("Enter side A of the triangle: ");
            sideA = getVlidDouble();
            Console.Write("\nEnter side B of the triangle: ");
            sideB = getVlidDouble();
            do
            {
                Console.Clear();
                Console.WriteLine("For a triangle, the sum of the angles must be equals to 180 degrees");
                Console.Write("\nEnter the angle between A and B: ");
                oppositeAngleToC = getVlidDouble();
            }
            while (oppositeAngleToC >= 180);

            oppositeAngleToC = degreesToRadians(oppositeAngleToC);
            sideC = calcTriangleSideCosine(sideA, sideB, oppositeAngleToC);
            
            oppositeAngleToA = calcTriangleAngle(sideA, sideB, sideC);
            oppositeAngleToB = calcTriangleAngle(sideB, sideA, sideC);
        }

        static void initializingUsingSideAndTwoAngle(out double sideA, out double sideB, out double sideC,
                                                     out double oppositeAngleToA, out double oppositeAngleToB,
                                                     out double oppositeAngleToC)
        {
            Console.Write("Enter side A of the triangle: ");
            sideA = getVlidDouble();
            do
            {
                Console.Clear();
                Console.WriteLine("For a triangle, the sum of the angles must be equals to 180 degrees");
                Console.Write("\nEnter the angle between A and B: ");
                oppositeAngleToC = getVlidDouble();
                Console.Write("\nEnter the angle between A and C: ");
                oppositeAngleToB = getVlidDouble();
            }
            while (oppositeAngleToC + oppositeAngleToB >= 180);

            oppositeAngleToA = 180 - oppositeAngleToB - oppositeAngleToC;

            oppositeAngleToA = degreesToRadians(oppositeAngleToA);
            oppositeAngleToB = degreesToRadians(oppositeAngleToB);
            oppositeAngleToC = degreesToRadians(oppositeAngleToC);

            sideB = calcTriangleSideSine(sideA, oppositeAngleToA, oppositeAngleToB);
            sideC = calcTriangleSideSine(sideA, oppositeAngleToA, oppositeAngleToC);
        }

        //Calculate the angle of a triangle using the cosine theorem
        //The side opposite to the required angle must be passed first 
        static double calcTriangleAngle(double sideA, double sideB, double sideC)
        {
            return Math.Acos((sideB * sideB + sideC * sideC - sideA * sideA) /
                             2 / sideB / sideC);
        }

        //Calculate  the side of a triangle using the sine theorem
        static double calcTriangleSideSine(double sideA, double oppositeAngleToA,
                                           double oppositeAngleToNecessarySide)
        {
            return sideA * Math.Sin(oppositeAngleToNecessarySide) /
                           Math.Sin(oppositeAngleToA);
        }

        //Calculate the side of a triangle using the cosine theorem
        static double calcTriangleSideCosine(double sideA, double sideB, double angleBetweenAandB)
        {
            return Math.Sqrt(sideA * sideA + sideB * sideB -
                             2 * sideA * sideB * Math.Cos(angleBetweenAandB));
        }

        static double calcTrianglrPerimeter(double sideA, double sideB, double sideC)
        {
            return sideA + sideB + sideC;
        }

        static double calctTiangleArea(double sideA, double sideB, double angleBetweenAB)
        {
            return 0.5 * sideA * sideB * Math.Sin(angleBetweenAB);
        }

        static double calcTriangleCircumscribedRadius(double sideA, double oppositeAngleToA)
        {
            return 0.5 * sideA / Math.Sin(oppositeAngleToA);
        }

        static double calcTriangleInscribedRadius(double sideA, double sideB, double sideC)
        {
            double semiPerimeter = 0.5 * calcTrianglrPerimeter(sideA, sideB, sideC);
            return Math.Sqrt((semiPerimeter - sideA) * (semiPerimeter - sideB) *
                             (semiPerimeter - sideC) / semiPerimeter);
        }

        static double degreesToRadians(double degrees)
        { 
            return Math.PI * degrees / 180.0;
        }

        static double  radiansToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        static double getVlidDouble()
        {
            double tempDouble;
            bool inputCheck = double.TryParse(Console.ReadLine(), out tempDouble);

            while (!inputCheck || tempDouble <= 0)
            {
                Console.WriteLine("Oops, wrong input, try again");
                inputCheck = double.TryParse(Console.ReadLine(), out tempDouble);
                Console.Clear();
            }
            return tempDouble;
        }
        //-----------------------SECOND TASK ENDS--------------------------------------------------

        //-----------------------THIRD TASK STARTS-------------------------------------------------
        static void printNeutralCultures()
        {
            Console.WriteLine("\t\tAvailable languages");
            foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
            {
                Console.Write("{0,-7}", cultureInfo.Name);
                Console.WriteLine(" {0,-40}", cultureInfo.EnglishName);
            }
        }

        static void printMonthsInDifferentLanguages()
        {
            printNeutralCultures();
            Console.Write("\nEnter the language: ");
            String userCulture = Console.ReadLine();
            CultureInfo setLangugage = new CultureInfo("");
            bool error;

            do
            {
                try
                {
                    setLangugage = new CultureInfo(userCulture);
                    error = false;
                }
                catch
                {
                    Console.Write("Wrong input. Try again:");
                    userCulture = Console.ReadLine();
                    error = true;
                }
            }
            while (error);

            for (int i = 1; i <= 12; ++i)
            {
                DateTime temp = new DateTime(2000, i, 23);
                Console.WriteLine(temp.ToString("MMMM", setLangugage));
            }
        }
        //-----------------------THIRD TASK ENDS-------------------------------------------------
    }
}
