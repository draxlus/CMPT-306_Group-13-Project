using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Author: Sheroze Ajmal (lambda)

//Dependencies: Node.cs

//Usage instructions:
/*
 * 1) Set the worldDimension equal to the number of columns in world(represented as a matrix)
 * 2) Define the start node
 * 3) Define the end node
 * 4) Call findPath()
 * ^ Returns the list of nodes contained in the shortest path from the start node to the end node
 */

public class EnemyAI : Player
{
    //Important Note: The world must be a square matrix
    
    public static bool debugModeOn = false;

    //Number of columns in the world
    public int worldDimension;

    public Player player, tower;
    public enum intelligence
    {
        Low,
        Medium,
        High
    };

    //A* implementation
    //Explaination:
    /*
     * g-Cost: how far a given node is from the start node
     * h-Cost: how far a given node is from the end node
     * openList: list of probable nodes that will lead to the shortest path (we haven't searched through these nodes, yet)
     * closeList: list of nodes that we have already looked at
     */
    public List<Node> findPath(Node[,] maze, Node startNode, Node endNode)
    {
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        openList.Add(startNode);

        //Used as a weight to specify how much movement in each direction costs
        int distanceBetweenChildAndCurrentNode;

        //find the h-Cost of startnode
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
                    print("Node: "+n.nodeName + " has fCost " + n.getfCost());
                }
                print("Current node is node " + currentNode.nodeName);
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);


            //Found the end node (goal)
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
                    //-------------
                    if (isValidIndexinArray(maze, i, j))
                    {
                        //Check the other nodes, if the childnode is the current node
                        if(!maze[i,j].Equals(currentNode))
                        {
                            if(debugModeOn)
                            {
                                print("Row " + i + " Col " + j + " is valid");
                            }
                            //set the parent of the adjacent node to be the current node

                            Node childNode = maze[i,j];
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

                            //g-Cost = 14 if childnode is diagonally away from the current node
                            if((childNode.row == currentNode.row -1 || childNode.row == currentNode.row + 1) && (childNode.col == currentNode.col -1 || childNode.col == currentNode.col + 1))
                            {
                                //Calculate the g-cost for the childnode
                                distanceBetweenChildAndCurrentNode = 14;
                                childNode.gCost = currentNode.gCost + distanceBetweenChildAndCurrentNode;
                            }
                            else
                            {
                                //Calculate the g-cost for the childnode
                                distanceBetweenChildAndCurrentNode = 10;
                                childNode.gCost = currentNode.gCost + distanceBetweenChildAndCurrentNode;
                            }

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
                                print("Childnode" + maze[i,j].nodeName + " was current node and we broke out");

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
    public bool isValidIndexinArray(Node[,] a, int rowIndex, int colIndex)
    {
        bool validRow = (0 <= rowIndex && rowIndex <= worldDimension -1);
        bool validCol = (0 <= colIndex && colIndex <= worldDimension -1);
        if (validRow && validCol)
        {
            if (!a[rowIndex, colIndex].isObstacle)
            {
                return true;

            }
        }
        return false;
    }

    //Purpose: Attacks either the player or the tower based on a heuristic
    private void attackTarget()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 towerPos = new Vector2(tower.transform.position.x, tower.transform.position.y);
        Node enemyNode = new Node((int)transform.position.x, (int)transform.position.y, World.findNodeNameAtPosition(transform.position), false);

        if (player.health > tower.health)
        {
            //Attack the tower
            Node tower = new Node((int)towerPos.x, (int)towerPos.y, World.findNodeNameAtPosition(towerPos), false);
            findPath(World.worldToMarix(), enemyNode, tower);
            print("Attacking tower");
        }
        else
        {
            Node tower = new Node((int)playerPos.x, (int)playerPos.y, World.findNodeNameAtPosition(playerPos), false);
            print("Attacking player");

        }
    }

    private void Start()
    {
        worldDimension = World.worldHeight;
        Invoke("attackTarget", 1);
    }
    



}
