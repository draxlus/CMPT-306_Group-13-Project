using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class respawnresoures 
{
    public static List<Vector3> respawn (int areat, int areab, int areal, int arear, List<Vector3> points,  int newtree, int newstone, int maxdiscard, int vareat, int vareab, int vareal, int varear, int roadwidth)
    {
        int width = arear - areal;
        int height = areat - areab;
        List<Vector3> newpoints = new List<Vector3>();
        int treeresp = newtree;
        int stoneresp = newstone;

        int discardit = 0;

        while (newtree > 0 || discardit < maxdiscard ) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            if (placeobj.isvalid((areal + x), (areab + y), vareat, vareab, vareal, varear))
            {
                if (placeobj.notinroad((areal + x), (areab + y), roadwidth))
                {
                    foreach (Vector3 point in points) {
                        if (!(point.x == x && point.y == y)) {
                            foreach (Vector3 newpoint in newpoints)
                            {
                                if (!(newpoint.x == x && newpoint.y == y))
                                {
                                    newpoints.Add(new Vector3((float)(areal + x), (float)(areab + y), 0.0f));
                                    treeresp--;
                                }
                            }
                        
                        }else { discardit++; }
                    }
                    
                }
                
            }
        }

        discardit = 0;

        newpoints.Add(new Vector3(0.0f, 0.0f, 0.0f));

        while (newstone > 0 || discardit < maxdiscard)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            if (placeobj.isvalid((areal + x), (areab + y), vareat, vareab, vareal, varear))
            {
                if (placeobj.notinroad((areal + x), (areab + y), roadwidth))
                {
                    foreach (Vector3 point in points) {
                        if (!(point.x == x && point.y == y))
                        {
                            foreach (Vector3 newpoint in newpoints)
                            {
                                if (!(newpoint.x == x && newpoint.y == y))
                                {
                                    newpoints.Add(new Vector3((float)(areal + x), (float)(areab + y), 0.0f));
                                    stoneresp--;
                                }
                            }
                        
                        }else { discardit++; }
                    }
                
                }
                 
            }
        }
        discardit = 0;

        return newpoints;
    }

}
