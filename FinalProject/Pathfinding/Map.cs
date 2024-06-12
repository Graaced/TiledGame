using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FinalProject
{
    class Map
    {
        private Dictionary<Node, Node> cameFrom;                    
        private Dictionary<Node, int> costSoFar;                    
        private PriorityQueue frontier;                             

        private int width;
        private int height;
        private int[] cells;
        private Sprite sprite;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public Node[] Nodes { get; }

        public Map(int w, int h, int[] cells)
        {
            width = w;
            height = h;
            this.cells = cells;
            sprite = new Sprite(1, 1);

            Nodes = new Node[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                int x = i % width;
                int y = i / width;

                if (cells[i] <= 0)
                {
                    Nodes[i] = new Node(x, y, 0, i);
                }
                else
                    Nodes[i] = new Node(x, y, int.MaxValue, i);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;

                    if (Nodes[index] == null)
                    {
                        continue;
                    }

                    AddNeighbours(Nodes[index], x, y);
                }
            }
        }

        private void AddNeighbours(Node n, int x, int y)        
        {
            CheckNeighbour(n, x, y - 1);                       
            CheckNeighbour(n, x, y + 1);                       
            CheckNeighbour(n, x - 1, y);                       
            CheckNeighbour(n, x + 1, y);                       
        }

        private void CheckNeighbour(Node n, int x, int y)
        {
            if (x < 0 || x >= width)
            {
                return;
            }

            if (y < 0 || y >= height)
            {
                return;
            }

            

            int index = y * width + x;
            Node neighbour = Nodes[index];

            if (neighbour != null)
            {
                n.AddNeighbour(neighbour);                     
            }
        }
        

        private void RemoveNode(int x, int y)
        {
            int index = y * width + x;
            Node node = GetNode(x, y);

            foreach (Node adjNode in node.Neighbours)
            {
                adjNode.RemoveNeighbour(node);
            }

            Nodes[index] = null;
            cells[index] = 0;
        }
        

        public Node GetNode(int x, int y)
        {
            if ((x < 0 || x >= width) || (y < 0 || y >= height))
            {
                return null;
            }

            return Nodes[y * width + x];
        }

        public Node GetRandomNode()
        {
            Node randomNode = null;

            do
            {
                randomNode = Nodes[RandomGenerator.GetRandomInt(0, Nodes.Count())];

            } while (randomNode.Cost == int.MaxValue);

            return randomNode;
        }

        public void AStar(Node start, Node end)
        {
            cameFrom = new Dictionary<Node, Node>();                                                
            costSoFar = new Dictionary<Node, int>();                                                
            frontier = new PriorityQueue();                                                         

            cameFrom[start] = start;                                                                
            costSoFar[start] = 0;
            frontier.Enqueue(start, Heuristic(start, end));

            while (!frontier.IsEmpty)
            {
                Node currentNode = frontier.Dequeue();

                if (currentNode == end)
                {
                    return;
                }

                foreach (Node nextNode in currentNode.Neighbours)
                {
                    int newCost = costSoFar[currentNode] + nextNode.Cost;

                    if (nextNode.Cost == 0)
                    {
                        if (!costSoFar.ContainsKey(nextNode) || costSoFar[nextNode] > newCost)
                        {
                            cameFrom[nextNode] = currentNode;
                            costSoFar[nextNode] = newCost;
                            int priority = newCost + Heuristic(nextNode, end);
                            frontier.Enqueue(nextNode, priority);
                        } 
                    }
                }
            }
        }

        private int Heuristic(Node start, Node end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }

        public List<Node> GetPath(int startX, int startY, int endX, int endY)
        {
            List<Node> path = new List<Node>();

            Node start = GetNode(startX, startY);
            Node end = GetNode(endX, endY);

            if (start == null || end == null)
            {
                return path;
            }

            AStar(start, end);

            if (!cameFrom.ContainsKey(end))             
            {
                return path;
            }

            Node currentNode = end;

            while (currentNode != cameFrom[currentNode])        
            {
                path.Add(currentNode);
                currentNode = cameFrom[currentNode];            
            }

            path.Reverse();

            return path;
        }

        public void Draw()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sprite.position = new OpenTK.Vector2(x, y);

                    if (GetNode(x, y) == null)
                    {
                        sprite.DrawColor(new OpenTK.Vector4(0, 0, 0, 1));
                    }
                    else
                    {
                        sprite.DrawColor(new OpenTK.Vector4(1, 0, 0, 1));
                    }
                }
            }
        }
    }
}
