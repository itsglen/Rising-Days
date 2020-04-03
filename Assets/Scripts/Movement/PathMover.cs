using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Any GameObject which contains this script can be moved along some path
 * Depends on parent GameObject containing: Rigidbody2D
*/
public class PathMover : MonoBehaviour
{

    public FloatVariable movementSpeed;
    private Rigidbody2D rigidbody2D;
    private List<Vector3> path;

    void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.rigidbody2D.freezeRotation = true;
    }

    public void SetPath(List<Vector3> path)
    {
        this.path = path;
    }

    public void BeginPathing()
    {
        // TODO(tomi): 
        // Write async operation that marches to path item and begins to marching to next path item only once reached previous path item
    }

    // Prevent excessive speed in diagonal directions by pythagoras
    private float GetCalibratedSpeed(float x, float y)
    {
        if (Math.Abs(x) > 0 && Math.Abs(y) > 0)
            return movementSpeed.Value / (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        return movementSpeed.Value;
    }

}
