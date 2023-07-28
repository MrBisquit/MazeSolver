using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    public static class Types
    {
        public class ImageResult
        {
            public bool Solved { get; }
            public int Seed { get; }
            public Bitmap Bitmap { get; }
            public int Moves { get; }
            public ImageResult(bool Solved, int Seed, Bitmap bm, int Moves)
            {
                this.Solved = Solved;
                this.Seed = Seed;
                this.Bitmap = bm;
                this.Moves = Moves;
            }
        }
        public class Solver
        {
            private bool solved = false;
            private int moves = 0;
            private int seed = 0;

            private int x = 0;
            private int y = 0;

            private Color white = Color.White;
            private Color black = Color.Black;
            private Color red   = Color.Red;
            public ImageResult Solve(Bitmap bm)
            {
                Random r = new Random();
                seed = r.Next();
                r = new Random(seed);
                while(!solved)
                {
                    if(x == 100 && y == 100)
                    {
                        solved = true;
                        break;
                    }
                    bool forward = false;
                    bool left = false;
                    bool right = false;

                    int possible_moves = 0;

                    if(bm.GetPixel(x, y + 1) == white)
                    {
                        forward = true;
                        possible_moves++;
                    }

                    if(bm.GetPixel(x + 1, y) == white)
                    {
                        left = true;
                        possible_moves++;
                    }

                    if(bm.GetPixel(x - 1, y) == white)
                    {
                        right = true;
                        possible_moves++;
                    }

                    if(bm.GetPixel(x, y + 1) == black && bm.GetPixel(x + 1, y) == black && bm.GetPixel(x - 1, y) == black)
                    {
                        solved = false;
                        return new ImageResult(solved, seed, bm, moves);
                    }

                    List<string> pmoves = new List<string>();
                    if (forward && y != 100) pmoves.Add("forward");
                    if (left && x != 0)    pmoves.Add("left");
                    if (right && x != 100)   pmoves.Add("right");

                    string move = pmoves[r.Next(pmoves.Count - 1)];
                    if(move == "forward")
                    {
                        y += 1;
                    } else if(move == "left")
                    {
                        x -= 1;
                    } else if(move == "right")
                    {
                        x += 1;
                    }

                    bm.SetPixel(x, y, red);
                }

                return new ImageResult(solved, seed, bm, moves);
            }
        }
    }
}