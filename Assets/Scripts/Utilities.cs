using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{

    public static Utilities instance = new Utilities();
    
    public bool IsTransformWithinObject(Vector3 toCompare, Vector3 against)
    {
        if (toCompare.x + 0.5 >= against.x && toCompare.x + 0.5 <= against.x + 1 && toCompare.y >= against.y && toCompare.y <= against.y + 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetRandomIntWithinRange(int min, int max)
    {
        return new System.Random().Next(min, max);
    }

}
