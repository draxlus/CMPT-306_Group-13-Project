using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Purpose: Takes the world, and maps it as a 2d grid for AI Pathfinding
public class WorldMapperManager : MonoBehaviour
{
    Node[][] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask isObstacleMask;

    int name = 0;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        //How many nodes can we fit onto out grid
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        createGrid();
    }

    void createGrid()
    {
        grid = new Node[gridSizeX][];


        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                name++;
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, isObstacleMask));
                Node n = new Node((int)worldPoint.x, (int)worldPoint.y, name, walkable);
                n.height = (int)worldPoint.z;
                grid[x][y]= n;

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                for(int y = 0; y < gridSizeY; y++)
                {
                    Gizmos.color = (grid[x][y].isObstacle) ? Color.white : Color.red;
                    Gizmos.DrawCube(new Vector3(grid[x][y].row, grid[x][y].col, grid[x][y].height), Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }



}
