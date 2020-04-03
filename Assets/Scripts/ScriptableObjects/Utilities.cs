using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities: ScriptableObject
{   
    // Currently assumes that against is a 16x16 px square
    // TODO(tomi): Generalize this function to query grid size
    public bool IsTransformWithinObject(Vector3 toCompare, Vector3 against, float buffer = 0)
    {
        return (toCompare.x >= against.x - (0.25 + buffer) && toCompare.x <= against.x + (0.25 + buffer) && toCompare.y >= against.y - (0.25 + buffer) && toCompare.y <= against.y + (0.25 + buffer));
    }

    public int GetRandomIntWithinRange(int min, int max)
    {
        return new System.Random().Next(min, max);
    }

}
