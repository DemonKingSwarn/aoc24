using Godot;
using System;
using System.IO;
using System.Linq;

[GlobalClass]
public partial class Day4 : Node
{
    string filename = "day4/input.txt";


    public override void _Ready()
    {
        string[] lines = File.ReadAllLines(filename);

        int p1 = 0;
        var directions = new (int, int)[]
        {
            (-1, -1), (-1, 0), (-1, 1), (0, 1),
            (1, 1), (1, 0), (1, -1), (0, -1)
        };

        bool ValidIndex(int i, int j) => i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length;

        string GetC(int i, int j, string d = "")
        {
            return ValidIndex(i, j) ? lines[i][j].ToString() : d;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'X')
                {
                    foreach (var (di, dj) in directions)
                    {
                        if (string.Concat(Enumerable.Range(0, 4).Select(n => GetC(i + n * di, j + n * dj))) == "XMAS")
                        {
                            p1++;
                        }
                    }
                }
            }
        }

        GD.Print($"Part 1: {p1}");

        int p2 = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'A')
                {
                    for (int k = 0; k < directions.Length / 2; k++)
                    {
                        var (di, dj) = directions[k];
                        var (di2, dj2) = directions[(k + directions.Length / 2) % directions.Length];

                        bool condition1 = GetC(i - di, j - dj) == "M" && GetC(i + di, j + dj) == "S";
                        bool condition2 = GetC(i - di2, j - dj2) == "M" && GetC(i + di2, j + dj2) == "S";

                        if (condition1 && condition2)
                        {
                            GD.Print($"Pattern found at ({i}, {j})");
                            p2++;
                        }
                    }
                }
            }
        }

        GD.Print($"Part 2: {p2}");
    }


}
