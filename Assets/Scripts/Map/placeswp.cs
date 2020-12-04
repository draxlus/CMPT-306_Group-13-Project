using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class placeswp 
{
    public static List<Vector3> swppoints (int x, int y )
    {
        List<Vector3> points = new List<Vector3>();

        points.Add(new Vector3((float)x, 0.0f, 0.0f));
        points.Add(new Vector3((float)-x, 0.0f, 0.0f));
        points.Add(new Vector3(0.0f, (float)y, 0.0f));
        points.Add(new Vector3(0.0f, (float)-y, 0.0f));

        return points;
    }

    public static Vector3 corepoint(int x, int y)
    {
        Vector3 point = new Vector3((float)x, (float)y, 0.0f);

        return point;

    }

}
