using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class mapgenerate : MonoBehaviour
{

    public int width;
    public int height;

    public int corex, corey;

    public int coret;
    public int coreb;
    /* coreb less than 0 */
    public int corel;
    /* corel less than 0 */
    public int corer;

    public int roadwidth;

    public List<Vector3> objpoints;
    public List<Vector3> swppoints;
    public Vector3 corepoint;
    public GameObject[] outside;

    public GameObject swamppoint, core;

    [Range(0, 100)]
    public int fillpersent;

    // Start is called before the first frame update
    void Start()
    {
        int[,] map = new int[width, height];

        generateoutside();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void generateoutside()
    {


        objpoints = placeobj.objpoints(height/2, -(height/2), -(width/2), width/2, fillpersent, coret, coreb, corel, corer, roadwidth);

        foreach (Vector3 point in objpoints)
        {
            int rand = Random.Range(0, outside.Length);
            Instantiate(outside[rand], point, Quaternion.identity);
           
        }

        swppoints = placeswp.swppoints(width / 2, height / 2);
        foreach (Vector3 point in swppoints)
        {
            Instantiate(swamppoint, point, Quaternion.identity);

        }

        corepoint = placeswp.corepoint(corex, corey);
        Instantiate(core, corepoint, Quaternion.identity);


    }




}