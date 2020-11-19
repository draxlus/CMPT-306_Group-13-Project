using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Sheroze Ajmal (lambda)

//Purpose: Representation of the game world

public class World : MonoBehaviour
{
    public GameObject gridCube;
    public Node[,] world;
    public Node start;
    public Node end;
    public int dim; //worldDimensions
    private int n;


    public void drawGrid()
    {
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                world[i, j] = new Node(i, j, n, false);
                GameObject c = Instantiate(gridCube, new Vector3(i, j, 0), Quaternion.identity);
                c.name = n.ToString();
                n++;

            }
        }
    }

    public void InputHandling()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            if (Input.GetKeyDown(KeyCode.S))
            {
                start.row = world[(int)selection.position.x, (int)selection.position.y].row;
                start.col = world[(int)selection.position.x, (int)selection.position.y].col;
                start.nodeName = world[(int)selection.position.x, (int)selection.position.y].nodeName;
                start.isObstacle = false;
                selection.GetComponent<Renderer>().material.color = Color.green;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                end.row = world[(int)selection.position.x, (int)selection.position.y].row;
                end.col = world[(int)selection.position.x, (int)selection.position.y].col;
                end.nodeName = world[(int)selection.position.x, (int)selection.position.y].nodeName;
                end.isObstacle = false;
                selection.GetComponent<Renderer>().material.color = Color.blue;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                world[(int)selection.position.x, (int)selection.position.y].isObstacle = true;
                selection.GetComponent<Renderer>().material.color = Color.red;

            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                EnemyAI path = new EnemyAI(dim);
                List<Node> l = path.findPath(world, start, end);
                foreach(Node n in l)
                {
                    GameObject.Find(n.nodeName.ToString()).GetComponent<Renderer>().material.color = Color.cyan;
                }
            }
        }
    }

    private void Start()
    {
        //dim = 5;
        start = new Node();
        end = new Node();
        world = new Node[dim, dim];
        n = 0;
        drawGrid();
    }

    private void Update()
    {
        InputHandling();
    }

}
