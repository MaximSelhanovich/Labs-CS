#include "pch.h"
#include <cmath>

#define DllExport   __declspec( dllexport )
const double PI = 3.14159265358979323846;

extern "C" {

    //Calculate the angle of a triangle using the cosine theorem
    //The side opposite to the required angle must be passed first 
    DllExport double __cdecl calcTriangleAngle(double sideA, double sideB, double sideC) {
        return acos((sideB * sideB + sideC * sideC - sideA * sideA) /
                     2 / sideB / sideC);
    }

    //Calculate  the side of a triangle using the sine theorem
    DllExport double __cdecl calcTriangleSideSine(double sideA, double oppositeAngleToA,
                                                  double oppositeAngleToNecessarySide) {
        return sideA * sin(oppositeAngleToNecessarySide) /
               sin(oppositeAngleToA);
    }

    //Calculate the side of a triangle using the cosine theorem
    DllExport double __fastcall calcTriangleSideCosine(double sideA, double sideB,
                                                    double angleBetweenAandB) {
        return sqrt(sideA * sideA + sideB * sideB -
               2 * sideA * sideB * cos(angleBetweenAandB));
    }

    DllExport double __cdecl calcTrianglrPerimeter(double sideA, double sideB,
                                                   double sideC) {
        return sideA + sideB + sideC;
    }

    DllExport double __stdcall calcTiangleArea(double sideA, double sideB,
                                               double angleBetweenAB) {
        return 0.5 * sideA * sideB * sin(angleBetweenAB);
    }

    DllExport double __stdcall calcTriangleCircumscribedRadius(double sideA,
                                                               double oppositeAngleToA) {
        return 0.5 * sideA / sin(oppositeAngleToA);
    }

    DllExport double __stdcall calcTriangleInscribedRadius(double sideA, double sideB,
                                                           double sideC) {
        double semiPerimeter = 0.5 * calcTrianglrPerimeter(sideA, sideB, sideC);
        return sqrt((semiPerimeter - sideA) * (semiPerimeter - sideB) *
               (semiPerimeter - sideC) / semiPerimeter);
    }

    DllExport void __stdcall degreesToRadians(double *degrees) {
        *degrees = PI * *degrees / 180.0;
    }

    DllExport void __stdcall radiansToDegrees(double *radians) {
        *radians = 180 * *radians / PI;
    }
}