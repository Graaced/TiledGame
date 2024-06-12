using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FinalProject
{
    class Node
    {
        public Vector2 Position => new Vector2(X, Y);

        public int X { get; }
        public int Y { get; }
        public int Cost { get; private set; }
        public int Index { get; }
        public List<Node> Neighbours { get; }

        public Node(int x, int y, int cost, int index)
        {
            X = x;
            Y = y;
            Cost = cost;
            Index = index;
            Neighbours = new List<Node>();
        }

        public void AddNeighbour(Node node)
        {
            Neighbours.Add(node);
        }

        public void RemoveNeighbour(Node node)
        {
            Neighbours.Remove(node);
        }

        public void SetCost(int cost)
        {
            Cost = cost;
        }
    }
}
