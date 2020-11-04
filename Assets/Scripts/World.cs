using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static int worldHeight, worldWidth;
    public static Node[,] world;
    public static int nodeName;
    public static LayerMask env;
    private static float tileRadius;

    public static GameObject cube;


    //Get the matrix reprenstation of the world
    public static Node[,] worldToMarix()
    {
        for (int row = 0; row < worldHeight; row++)
        {
            for (int col = 0; col < worldWidth; col++)
            {
                //populate the world array
                world[row, col] = new Node(row, col, nodeName, false);
                nodeName++;
                Vector3 tilePos = new Vector3(tileRadius + col, tileRadius + row);
               // cube.name = nodeName.ToString();
                //Instantiate(cube, tilePos, Quaternion.identity);

            }
        }
        return world;
    }

    //Returns the name of a node at position p
    public static int findNodeNameAtPosition(Vector3 p)
    {
        return world[(int)p.x, (int)p.y].nodeName;
    }

    //Return the index of the node in world where the mouse clicked
    private void getNodeAtPosition()
    {

    }

    private void Start()
    {
        worldHeight = 20;
        worldWidth = 20;
        world = new Node[worldHeight, worldWidth];
        
        Node[,] n = worldToMarix();

        //for(int row = 0; row < worldHeight; row++)
        //{
        //    for(int col = 0; col < worldHeight; col++)
        //    {
        //        print(world[row, col].nodeName);
        //    }
        //}

        
    }

   

}
