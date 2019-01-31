using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(100, 100);

            Path path = new Path();

            List<Node> totalPath = path.GeneratePath(new Vector2(0, 0), new Vector2(0, 40), grid);

            for (int i = 0; i < totalPath.Count; i++)
            {
                grid.grid[totalPath[i].X, totalPath[i].Y] = 'O';
            }

            for (int i = 0; i < 100; i++)
            {
                for (int ii = 0; ii < 100; ii++)
                {
                    Console.Write(grid.grid[i, ii]);
                }
                Console.WriteLine();
            }
        }
    }
}
