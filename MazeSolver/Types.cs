using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

            private int x = 100;
            private int y = 97;

            private int last_x = 1;
            private int last_y = 1;

            private Color white = Color.FromArgb(255, 255, 255, 255);
            private Color black = Color.FromArgb(255, 0, 0, 0);
            private Color red   = Color.Red;
            private Bitmap bm;
            public ImageResult Solve(Bitmap Maze, int seed)
            {
                bm = Maze;
                Random r = new Random(seed);
                r = new Random(r.Next());

                //x = r.Next(100);
                //y = r.Next(100);
                last_x = x;
                last_y = y;

                try
                {
                    bm.SetPixel(x, y, red);
                }
                catch { }

                while(!solved)
                {
                    if(moves % 40 == 0)
                    {
                        //MessageBox.Show($"X: {x} Y: {y} {bm.GetPixel(x, y)} {bm.GetPixel(x, y + 1)} {bm.GetPixel(x + 1, y)} {moves} {seed} {solved}");
                    }
                    //MessageBox.Show($"X: {x} Y: {y} {bm.GetPixel(x, y)} {bm.GetPixel(x, y + 1)} {bm.GetPixel(x + 1, y)} {moves} {seed} {solved}");
                    moves += 1;
                    if(x == 100 && y == 100)
                    {
                        solved = true;
                        break;
                    }
                    bool forward = false;
                    bool left = false;
                    bool right = false;

                    int possible_moves = 0;

                    try
                    {
                        if (bm.GetPixel(x, y + 1) == white)
                        {
                            forward = true;
                            possible_moves++;
                        }
                    } catch { }

                    try
                    {
                        if (bm.GetPixel(x + 1, y) == white)
                        {
                            left = true;
                            possible_moves++;
                        }
                    } catch { }

                    try
                    {
                        if (bm.GetPixel(x - 1, y) == white)
                        {
                            right = true;
                            possible_moves++;
                        }
                    } catch { }

                    try
                    {
                        if (bm.GetPixel(x, y + 1) == black && bm.GetPixel(x + 1, y) == black && bm.GetPixel(x - 1, y) == black)
                        {
                            solved = false;
                            return new ImageResult(solved, seed, bm, moves);
                        }
                    } catch { }

                    try
                    {

                        List<string> pmoves = new List<string>();
                        if (forward && y != 100) pmoves.Add("forward");
                        if (left && x != 0) pmoves.Add("left");
                        if (right && x != 100) pmoves.Add("right");

                        string move = pmoves[r.Next(pmoves.Count)];
                        if (move == "forward")
                        {
                            y += 1;
                        }
                        else if (move == "left")
                        {
                            x -= 1;
                        }
                        else if (move == "right")
                        {
                            x += 1;
                        }
                    }
                    catch { }

                    if(x == last_x &&  y == last_y)
                    {
                        break;
                    }

                    last_x = x;
                    last_y = y;

                    try
                    {
                        bm.SetPixel(x, y, red);
                    } catch { }
                }
                //MessageBox.Show("Finished");
                return new ImageResult(solved, seed, bm, moves);
            }
        }
    }
}