using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PathFinding
{
    class Path
    {
        public List<Node> ReconstructPath(Node currentNode)
        {
            List<Node> totalPath = new List<Node>();
            totalPath.Add(currentNode);
            Node parent = null;

            while (currentNode.parents.Count > 0)
            {
                parent = currentNode.parents.OrderBy(node => node.F).ToList()[0];

                currentNode = parent;
                totalPath.Add(currentNode);
            }

            return totalPath;
        }
        public List<Node> GeneratePath(Vector2 startPos, Vector2 endPos, Grid grid)
        {
            Node startNode = new Node((int)startPos.X, (int)startPos.Y, false);
            Node endNode = new Node((int)endPos.X, (int)endPos.Y, false);
            List<Node> totalPath = new List<Node>();

            //nodes that have been discovered but not evaluated
            List<Node> openSet = new List<Node>();
            openSet.Add(startNode);
            totalPath.Add(startNode);

            //nodes that have been evaluated
            List<Node> closedSet = new List<Node>();
            Node cameFrom = startNode;

            startNode.G = 0;
            startNode.H = startNode.CalcDist(
                new Vector2(startNode.X, startNode.Y),
                new Vector2(endNode.X, endNode.Y));

            while (openSet.Count > 0)
            {
                //Node currentNode = FindLowest(openSet);
                Node currentNode = openSet.OrderBy(node => node.F).ToList()[0];

                openSet.Remove(currentNode);
                currentNode.OpenSet = false;
                closedSet.Add(currentNode);
                currentNode.ClosedSet = true;

                if (currentNode.X == endNode.X && currentNode.Y == endNode.Y)
                {
                    return ReconstructPath(currentNode);
                }

                List<Node> adjacencies = new List<Node>();
                
                int currX = currentNode.X;
                int currY = currentNode.Y;


                //Create the neighbors of the CurrentNode
                if ((currY + 1) < grid.MapHeight)
                {
                    adjacencies.Add(grid.grid[currX, currY + 1]);
                }
                if ((currY - 1) >= 0)
                {
                    adjacencies.Add(grid.grid[currX, currY - 1]);
                }
                if ((currX + 1) < grid.MapWidth)
                {
                    adjacencies.Add(grid.grid[currX + 1, currY]);
                }
                if ((currX - 1) >= 0)
                {
                    adjacencies.Add(grid.grid[currX - 1, currY]);
                }
                if ((currX + 1) < grid.MapWidth && (currY + 1) < grid.MapHeight)
                {
                    adjacencies.Add(grid.grid[currX + 1, currY + 1]);
                }
                if ((currX + 1) < grid.MapWidth && (currY - 1) >= 0)
                {
                    adjacencies.Add(grid.grid[currX + 1, currY - 1]);
                }
                if ((currX - 1) >= 0 && (currY + 1) < grid.MapHeight)
                {
                    adjacencies.Add(grid.grid[currX - 1, currY + 1]);
                }
                if ((currX - 1) >= 0 && (currY - 1) >= 0)
                {
                    adjacencies.Add(grid.grid[currX - 1, currY - 1]);
                }


                for (int i = 0; i < adjacencies.Count; i++)
                {
                    if (adjacencies[i].ClosedSet || !adjacencies[i].Walkable)
                    {
                        continue;
                    }

                    double neighborDist = currentNode.G + adjacencies[i].CalcDist(new Vector2(adjacencies[i].X, adjacencies[i].Y),
                        new Vector2(currentNode.X, currentNode.Y));

                    if (!adjacencies[i].OpenSet)
                    {
                        adjacencies[i].OpenSet = true;
                        openSet.Add(adjacencies[i]);
                    }
                    else if (neighborDist >= adjacencies[i].G)
                    {
                        continue;
                    }
                    adjacencies[i].parents.Add(currentNode);
                    adjacencies[i].G = neighborDist;
                    adjacencies[i].H = adjacencies[i].CalcDist(new Vector2(adjacencies[i].X, adjacencies[i].Y),
                        new Vector2(endNode.X, endNode.Y));
                }
            }
            return null;
        }

        public Node FindLowest(List<Node> nodes)
        {
            Node min = null;
            if (nodes.Count > 0)
            {
                min = nodes[0];
            }
            else
            {
                return null;
            }
            for (int i = 0; i < nodes.Count; i++ )
            {
                if (nodes[i].F < min.F)
                {
                    min = nodes[i];
                }
            }
            return min;
        }
    }
}
