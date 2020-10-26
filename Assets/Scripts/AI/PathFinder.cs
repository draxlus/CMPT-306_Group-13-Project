using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinder : MonoBehaviour
{

    private static bool debugModeOn = false;

    //a* implementation
    public List<Node> findPath(Node[][] maze, Node startNode, Node endNode)
    {
        //Create an open and a closed list
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        openList.Add(startNode);

        int distanceBetweenChildAndCurrentNode;
        //find the h cost of startnode
       
        startNode.hCost = Mathf.RoundToInt(Mathf.Pow(endNode.col - startNode.col, 2) + Mathf.Pow(endNode.row - startNode.row, 2));


        //Keep searching until the open list is empty
        while (openList.Count > 0)
        {
            openList.Sort();//sort the list in terms of increasing f cost
            Node currentNode = openList[0];
            if (debugModeOn)
            {
                print("Entering");
                print("printing the openlist after it has been sorted");
                foreach (Node n in openList)
                {
                    print(n.nodeName + " has fCost " + n.getfCost());
                }
                print("New current node is node " + currentNode.nodeName);
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);



            //Found the end node
            if (currentNode.hCost == 0) {
                List<Node> returnList = new List<Node>();
                if (debugModeOn)
                {
                    print("Found the end node");
                    print("Is parent null " + (currentNode.parent == null));
                    print("Endnode: " + currentNode.nodeName);

                }
                returnList.Add(currentNode);
                while(currentNode.parent != null)
                {
                    returnList.Add(currentNode.parent);
                    if (debugModeOn)
                    {
                        print("Parent node: " + currentNode.parent);

                    }
                    currentNode = currentNode.parent;


                }
                returnList.Reverse();
                return returnList;
                //break;

            }

            //Search through the adjacent nodes of the current node
            for (int i = currentNode.row - 1; i <= currentNode.row + 1; i++)
            {
                for (int j = currentNode.col - 1; j <= currentNode.col + 1; j++)
                {
                    //Check if the adjacent nodes are valid array indicies
                    if (isValidIndexinArray(maze, i, j))
                    {
                        //Check the other nodes, if the childnode is the current node
                        if(!maze[i][j].Equals(currentNode))
                        {
                            if(debugModeOn)
                            {
                                print("Row " + i + " Col " + j + " is valid");
                            }
                            //set the parent of the adjacent node to be the current node

                            Node childNode = maze[i][j];
                            childNode.parent = currentNode;
                            
                            if (debugModeOn)
                            {
                                print("----Printing the child node details--------");
                                print("nodename " + childNode.nodeName);
                                print("row " + childNode.row);
                                print("col " + childNode.col);
                                print("parent node " + childNode.parent);
                            }

                            //if the child has been closed, move on
                            if (closeList.Contains(childNode))
                            {
                                if (debugModeOn)
                                {
                                    print("Breaking out because the child node is already in closeList");

                                }
                                continue;
                            }

                           
                            //Calculate the g-cost for the childnode
                            distanceBetweenChildAndCurrentNode = 10;
                            childNode.gCost = currentNode.gCost + distanceBetweenChildAndCurrentNode;

                            //Calculate the h cost for the childNode
                            int hCostSquared = Mathf.RoundToInt(Mathf.Pow(endNode.col - childNode.col, 2) + Mathf.Pow(endNode.row - childNode.row, 2));
                            childNode.hCost = hCostSquared;

                            if (debugModeOn)
                            {
                                print("The g cost of node " + childNode.nodeName + " is " + childNode.gCost);
                                print("The h cost of node " + childNode.nodeName + " is " + childNode.hCost);
                            }

                            //Child is already on the openlist, ie, we have already encountered this childNode
                            if (openList.Contains(childNode))
                            {
                                if (debugModeOn)
                                {
                                    print("Child node is already in the openlist");

                                }
                                foreach (Node n in openList)
                                {
                                    if (n.nodeName.Equals(childNode.nodeName))
                                    {
                                        if (debugModeOn)
                                        {
                                            print("Node " + n.nodeName + " old gCost " + n.gCost + " new gCost " + childNode.gCost);
                                            print("old g cost " + n.gCost + " old h cost " + n.hCost + "old fcost "+n.getfCost());
                                        }
                                        if (childNode.gCost > n.gCost)
                                        {
                                            //go to the top of the for loop, because the current g cost is higher than what is already in the open list
                                            if (debugModeOn)
                                            {
                                                print("Childnode " + childNode.nodeName + " was found in the open list with a high g cost");

                                            }
                                            continue;

                                        }
                                        
                                        
                                    }
                                }
                            }

                            //Seeing the childnode for the first time
                            else if (!closeList.Contains(childNode))
                            {
                                Node temp = new Node();
                                temp.parent = childNode.parent;
                                temp.gCost = childNode.gCost;
                                temp.hCost = childNode.hCost;
                                temp.row = childNode.row;
                                temp.col = childNode.col;
                                temp.nodeName = childNode.nodeName;
                                temp.isObstacle = childNode.isObstacle;

                                openList.Add(temp);
                                if (debugModeOn)
                                {
                                    print("Childnode " + temp.nodeName + " was added to the open list with a gcost of "+temp.gCost);

                                }
                            }

                        }
                        else
                        {
                            if (debugModeOn)
                            {
                                print("Childnode" + maze[i][j].nodeName + " was current node and we broke out");

                            }
                            continue;
                        }
                    }
                    else
                    {
                        if (debugModeOn)
                        {
                            print("Row " + i + " Col " + j + " is not valid or is an obstacle");

                        }
                        continue;

                    }

                }
                
            }

        }
        return null;
    }

    //Purpose: Checks if rowIndex and colIndex are valid for the array a
    public bool isValidIndexinArray(Node[][] a, int rowIndex, int colIndex)
    {
        bool validRow = (0 <= rowIndex && rowIndex <= a.Length - 1);
        bool validCol = (0 <= colIndex && colIndex <= a.Length - 1);
        if (validRow && validCol)
        {
            if (!a[rowIndex][colIndex].isObstacle)
            {
                return true;

            }
        }
        return false;
    }

    private void Start()
    {
        //Tester for findPath()

        //No obstacles
        Node[][] worldConfigOne = new Node[][]
        {
            new Node[] {new Node(0,0,0, false), new Node(0,1,1, false), new Node(0,2,2, false), new Node(0,3,3, false)},
            new Node[] {new Node(1,0,4, false), new Node(1,1,5,false), new Node(1,2,6, false), new Node(1,3,7, false)},
            new Node[] {new Node(2,0,8,false), new Node(2,1,9,false), new Node(2,2,10,false),new Node(2,3,11,false)},
            new Node[] {new Node(3,0,12,false),new Node(3,1,13,false),new Node(3,2,14,false),new Node(3,3,15,false)},


        };

        //Obstacles
        Node[][] worldConfigTwo = new Node[][]
        {
            new Node[] {new Node(0,0,0, false), new Node(0,1,1, false), new Node(0,2,2, false), new Node(0,3,3, true)},
            new Node[] {new Node(1,0,4, true), new Node(1,1,5,true), new Node(1,2,6, true), new Node(1,3,7, false)},
            new Node[] {new Node(2,0,8,false), new Node(2,1,9,false), new Node(2,2,10,false),new Node(2,3,11,false)},
            new Node[] {new Node(3,0,12,true),new Node(3,1,13,true),new Node(3,2,14,false),new Node(3,3,15,false)},


        };

        //Obstacles
        Node[][] worldConfigThree = new Node[][]
        {
            new Node[] {new Node(0,0,0, false), new Node(0,1,1, false), new Node(0,2,2, false), new Node(0,3,3, true)},
            new Node[] {new Node(1,0,4, true), new Node(1,1,5,true), new Node(1,2,6, true), new Node(1,3,7, false)},
            new Node[] {new Node(2,0,8,false), new Node(2,1,9,false), new Node(2,2,10,false),new Node(2,3,11,false)},
            new Node[] {new Node(3,0,12,true),new Node(3,1,13,true),new Node(3,2,14,false),new Node(3,3,15,false)},


        };



        Node start = new Node(0, 0, 0, false);
        Node end = new Node(2, 0, 8, false);
        List<Node> l = new List<Node>();

        l = findPath(worldConfigThree, start, end); //stores the list of paths returned from the start node to the end node
        string path = "";
        foreach(Node n in l)
        {
            if (!n.nodeName.Equals(end.nodeName))
            {
                path += n.nodeName + "-> ";

            }
            else
            {
                path += n.nodeName;
            }
        }
        print(path);

        //worldConfigOne output
        //0 -> 5 -> 10 -> 5

        //worldConfigTwo output
        //0 -> 1 -> 2 -> 7 -> 11 -> 15

        //worldConfigThree output
        //0 -> 1 -> 2 -> 7 -> 10 -> 9 -> 8
    }



}
