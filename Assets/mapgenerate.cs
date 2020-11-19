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

    public int newstone;
    public int newtree;
    public int maxdiscard;

    public GameObject tree;
    public GameObject stone;

    public int count;
    public GameObject[] clones;
    [SerializeField] DayNight dayNight;

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


    void generateoutside()
    {
        clones = new GameObject[1000];

        objpoints = placeobj.objpoints(height / 2, -(height / 2), -(width / 2), width / 2, fillpersent, coret, coreb, corel, corer, roadwidth);

        count = 0;
        foreach (Vector3 point in objpoints)
        {
            int rand = Random.Range(0, outside.Length);
            clones[count] = Instantiate(outside[rand], point, Quaternion.identity);
            count++;
        }

        swppoints = placeswp.swppoints(width / 2, height / 2);
        foreach (Vector3 point in swppoints)
        {
            // int rand = Random.Range(0, outside.Length);
            Instantiate(swamppoint, point, Quaternion.identity);
            //Instantiate(outside[rand], point, Quaternion.identity);

        }

        corepoint = placeswp.corepoint(corex, corey);
        Instantiate(core, corepoint, Quaternion.identity);


    }


    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < clones.Length; i++)
         {
             if (Input.GetKeyDown(KeyCode.E) && clones[i].GetComponent<ItemChest>().isInRange)
             {
                 clones[i].gameObject.SetActive(false);
             }
         }


        if (dayNight.sun.intensity == 1f)
        {
            for (int i = 0; i < 20; i++)
            {
                if (count < 200)
                {
                    int rand = Random.Range(0, outside.Length);
                    clones[count] = Instantiate(outside[rand], new Vector3(Random.Range(-width + 1, width - 1), Random.Range(-height, height), -0.1f), Quaternion.identity);
                    count++;
                }
            }



        }
    }

}
