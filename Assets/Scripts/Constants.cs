using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniverseConstants
{
    //operating at 1:1000 scale, 1m = 1km
    //public const float gravity = 6.67e-8f;
    public const float gravity = 0.0001f;

    //units: meters
    public const double lightSpeed = 3e8;

    public const double plancksConstant = 6.626e-24;

    //units: meters
    public const double AU = 1.496e11;

    //units: meters
    public const double parsec = 3.08e16;

    //units: meters
    public const double lightYear = 9.46e15;

    public const float timeStep = 1f;
}
