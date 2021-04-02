using System;
using System.Runtime.InteropServices;

namespace Lab4_2
{
    class Lab4_2
    {
        static void Main(string[] args)
        {
            TriangleParameters();
        }

        static uint GetValidUint()
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

        static void TriangleParameters()
        {
            double sideA, sideB, sideC;
            double oppositeAngleToA, oppositeAngleToB, oppositeAngleToC;

            InitialiseTriangleBasicParameters(out sideA, out sideB, out sideC,
                                              out oppositeAngleToA, out oppositeAngleToB,
                                              out oppositeAngleToC);

            Console.WriteLine("\nSides of triangle:\tAngles of triangle:\n" +
                              "side A = {0}\t\t Alpha = {1}\n" +
                              "side B = {2}\t\t Beta = {3}\n" +
                              "side C = {4}\t\t Gamma = {5}\n",
                               sideA, oppositeAngleToA,
                               sideB, oppositeAngleToB,
                               sideC, oppositeAngleToC);

            Console.WriteLine("Perimeter of triangle = {0}",
                              CalcTrianglrPerimeter(sideA, sideB, sideC));
            Console.WriteLine("\nArea of triangle = {0}",
                              CalcTiangleArea(sideA, sideB, oppositeAngleToC));
            Console.WriteLine("\nRadius of the circumscribed circle = {0}",
                              CalcTriangleCircumscribedRadius(sideA, oppositeAngleToA));
            Console.WriteLine("\nRadius of the inscribed circle = {0}\n",
                              CalcTriangleInscribedRadius(sideA, sideB, sideC));
        }

        //Methods of initializing triangle parameters
        static void InitialiseTriangleBasicParameters(out double sideA, out double sideB, out double sideC,
                                                      out double oppositeAngleToA, out double oppositeAngleToB,
                                                      out double oppositeAngleToC)
        {
            sideA = 0; sideB = 0; sideC = 0;
            oppositeAngleToA = 0; oppositeAngleToB = 0; oppositeAngleToC = 0;
            Console.WriteLine("Select input parameters:\n" +
                              "1) Three sides\n" +
                              "2) Two sides and an angle between them\n" +
                              "3) Side and two adjacent angles");
            uint inputParametersType = GetValidUint();
            while (inputParametersType > 3)
            {
                Console.Clear();
                Console.WriteLine("Wrong input. There are only three types of input parameters.");
                inputParametersType = GetValidUint();
            }
            Console.Clear();
            switch (inputParametersType)
            {
                case 1:
                    InitializingUsingThreeSides(out sideA, out sideB, out sideC,
                                                out oppositeAngleToA, out oppositeAngleToB,
                                                out oppositeAngleToC);
                    break;
                case 2:
                    InitializingUsingTwoSidesAndAngle(out sideA, out sideB, out sideC,
                                                      out oppositeAngleToA, out oppositeAngleToB,
                                                      out oppositeAngleToC);
                    break;
                case 3:
                    InitializingUsingSideAndTwoAngle(out sideA, out sideB, out sideC,
                                                     out oppositeAngleToA, out oppositeAngleToB,
                                                     out oppositeAngleToC);
                    break;
            }
        }

        static void InitializingUsingThreeSides(out double sideA, out double sideB, out double sideC,
                                                out double oppositeAngleToA, out double oppositeAngleToB,
                                                out double oppositeAngleToC)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("For a triangle, the sum of the two sides must be greater than the third");
                Console.Write("Enter side A of the triangle: ");
                sideA = GetVlidDouble();
                Console.Write("\nEnter side B of the triangle: ");
                sideB = GetVlidDouble();
                Console.Write("\nEnter side C of the triangle: ");
                sideC = GetVlidDouble();
            }
            while (sideA + sideB < sideC ||
                   sideA + sideC < sideB ||
                   sideC + sideB < sideA);

            oppositeAngleToA = CalcTriangleAngle(sideA, sideB, sideC);
            oppositeAngleToB = CalcTriangleAngle(sideB, sideA, sideC);
            oppositeAngleToC = CalcTriangleAngle(sideC, sideB, sideA);
        }

        static void InitializingUsingTwoSidesAndAngle(out double sideA, out double sideB, out double sideC,
                                                      out double oppositeAngleToA, out double oppositeAngleToB,
                                                      out double oppositeAngleToC)
        {
            Console.Write("Enter side A of the triangle: ");
            sideA = GetVlidDouble();
            Console.Write("\nEnter side B of the triangle: ");
            sideB = GetVlidDouble();
            do
            {
                Console.Clear();
                Console.WriteLine("For a triangle, the sum of the angles must be equals to 180 degrees");
                Console.Write("\nEnter the angle between A and B: ");
                oppositeAngleToC = GetVlidDouble();
            }
            while (oppositeAngleToC >= 180);

            DegreesToRadians(out oppositeAngleToC);
            sideC = СalcTriangleSideCosine(sideA, sideB, oppositeAngleToC);

            oppositeAngleToA = CalcTriangleAngle(sideA, sideB, sideC);
            oppositeAngleToB = CalcTriangleAngle(sideB, sideA, sideC);
        }

        static void InitializingUsingSideAndTwoAngle(out double sideA, out double sideB, out double sideC,
                                                     out double oppositeAngleToA, out double oppositeAngleToB,
                                                     out double oppositeAngleToC)
        {
            Console.Write("Enter side A of the triangle: ");
            sideA = GetVlidDouble();
            do
            {
                Console.Clear();
                Console.WriteLine("For a triangle, the sum of the angles must be equals to 180 degrees");
                Console.Write("\nEnter the angle between A and B: ");
                oppositeAngleToC = GetVlidDouble();
                Console.Write("\nEnter the angle between A and C: ");
                oppositeAngleToB = GetVlidDouble();
            }
            while (oppositeAngleToC + oppositeAngleToB >= 180);

            oppositeAngleToA = 180 - oppositeAngleToB - oppositeAngleToC;

            DegreesToRadians(out oppositeAngleToA);
            Console.WriteLine("\n\ntyiuwdhfj \n{0}\n\n", oppositeAngleToA);

            DegreesToRadians(out oppositeAngleToB);

            Console.WriteLine("\n\ntyiuwdhfj \n{0}\n\n", oppositeAngleToB);
            DegreesToRadians(out oppositeAngleToC);
            Console.WriteLine("\n\ntyiuwdhfj \n{0}\n\n", oppositeAngleToC);

            sideB = CalcTriangleSideSine(sideA, oppositeAngleToA, oppositeAngleToB);
            sideC = CalcTriangleSideSine(sideA, oppositeAngleToA, oppositeAngleToC);
        }

        public const string pathToDLL =
            "D:\\First Grade\\2sem\\LabsC#\\TriangleWork\\Debug\\TriangleWork.dll";

        //Calculate the angle of a triangle using the cosine theorem
        //The side opposite to the required angle must be passed first
        [DllImport(pathToDLL, EntryPoint = "calcTriangleAngle",
                              CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double CalcTriangleAngle(double sideA, double sideB,
                                                      double sideC);


        //Calculate  the side of a triangle using the sine theorem
        [DllImport(pathToDLL, EntryPoint = "calcTriangleSideSine",
                              CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double CalcTriangleSideSine(double sideA, double oppositeAngleToA,
                                                         double oppositeAngleToNecessarySide);


        //Calculate the side of a triangle using the cosine theorem
        [DllImport(pathToDLL, EntryPoint = "calcTriangleSideCosine",
                              CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double СalcTriangleSideCosine(double sideA, double sideB,
                                                           double angleBetweenAandB);


        [DllImport(pathToDLL, EntryPoint = "calcTrianglrPerimeter",
                              CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double CalcTrianglrPerimeter(double sideA, double sideB,
                                                          double sideC);


        [DllImport(pathToDLL, EntryPoint = "calcTiangleArea",
                              CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double CalcTiangleArea(double sideA, double sideB,
                                                    double angleBetweenAB);


        [DllImport(pathToDLL, EntryPoint = "calcTriangleCircumscribedRadius",
                              CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double CalcTriangleCircumscribedRadius(double sideA,
                                                                    double oppositeAngleToA);

        [DllImport(pathToDLL, EntryPoint = "calcTriangleInscribedRadius",
                              CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        public static extern double CalcTriangleInscribedRadius(double sideA, double sideB,
                                                                double sideC);


        [DllImport(pathToDLL, EntryPoint = "degreesToRadians",
                              CallingConvention = CallingConvention.StdCall)]
        public static extern void DegreesToRadians(out double degrees);


        [DllImport(pathToDLL, EntryPoint = "radiansToDegrees",
                              CallingConvention = CallingConvention.StdCall)]
        public static extern void RadiansToDegrees(out double radians);

        static double GetVlidDouble()
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
    }
}
