using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public static class placeobj
{

    public static List<Vector3> objpoints(int areat,int areab, int areal, int arear , int fillpersent, int vareat, int vareab, int vareal, int varear, int roadwidth)
    {
        int width = arear - areal;
        int height = areat - areab;
        List<Vector3>  positions = new List<Vector3>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (isvalid((areal + x), (areab + y), vareat , vareab , vareal, varear)) 
                { 
                    if (notinroad((areal + x), (areab + y), roadwidth))
                    {
                        int rand = Random.Range(0, 100);
                        if (rand <= fillpersent)
                        {
                            positions.Add(new Vector3((float)(areal + x), (float)(areab + y), 0.0f));
                        }
                    }
                }
                
            }
        }

        return positions;
    }


    public static bool isvalid(int x, int y, int areat, int areab, int areal, int arear)
    {
        if (((x<areal) || (x > arear)) || ((y<areab) || (y > areat)))
        { 
            return true;
        }
        return false;
    }

    public static bool notinroad(int x, int y, int roadwidth)
    {
        roadwidth = roadwidth / 2;
        if (x>roadwidth || x<(-roadwidth))
        {
            if (y>roadwidth || y < (-roadwidth))
            {
                return true;
            }

        }
        return false;
    }
}
