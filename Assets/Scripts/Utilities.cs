using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{

    public static Utilities instance = new Utilities();
    
    // Currently assumes that against is a 16x16 px square
    public bool IsTransformWithinObject(Vector3 toCompare, Vector3 against)
    {
        // 4 pixels
        double buffer = 0.125;

        return (toCompare.x >= against.x - (0.25 + buffer) && toCompare.x <= against.x + (0.25 + buffer) && toCompare.y >= against.y - (0.25 + buffer) && toCompare.y <= against.y + (0.25 + buffer));
    }

    // Currently assumes that against is a 16x16 px square
    public bool IsTransformWithinObjectNoBuffer(Vector3 toCompare, Vector3 against)
    {
        return (toCompare.x >= against.x - 0.25 && toCompare.x <= against.x + 0.25 && toCompare.y >= against.y - 0.25 && toCompare.y <= against.y + 0.25);
    }

    public int GetRandomIntWithinRange(int min, int max)
    {
        return new System.Random().Next(min, max);
    }

}
