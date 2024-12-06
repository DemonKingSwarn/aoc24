using System.IO;
using System.Collections.Generic;
using Godot;
using System.Linq;
using System;

public partial class Day6 : Node2D
{
    string filename = "day6/input.txt";

    string[] lines;
    string uwu;

    public override void _Ready()
    {
        string[] lines = File.ReadAllLines(filename);
        uwu = string.Join("\n", lines);

        
        int p1 = PartOne();
        int p2 = PartTwo();

        GD.Print($"Part 1: {p1}");
        GD.Print($"Part 2: {p2}");
    }

    bool IsExit(int row, int col, List<List<char>> graph)
    {
        int numRows = graph.Count;
        int numCols = graph[0].Count;
        return row == -1 || row == numRows || col == -1 || col == numCols;
    }

    bool IsValid(int row, int col, List<List<char>> graph)
    {
        return graph[row][col] != '#';
    }

    int PartOne()
    {

		var up = Tuple.Create(-1, 0);
        var right = Tuple.Create(0, 1);
        var down = Tuple.Create(1, 0);
        var left = Tuple.Create(0, -1);

        List<Tuple<int, int>> dirs = new List<Tuple<int, int>> { up, right, down, left };



        List<List<char>> graph = uwu.Split("\n")
                                    .Select(line => line.ToList())
                                    .ToList();

        int numRows = graph.Count;
        int numCols = graph.Count > 0 ? graph[0].Count : 0;

        int startRow = 0;
        int startCol = 0;
        foreach (int row in GD.Range(numRows))
        {
            foreach (int col in GD.Range(numCols))
            {
                if (graph[row][col] == '^')
                {
                    startRow = row;
                    startCol = col;
                }
            }
        }

        HashSet<int> seen = new HashSet<int>();
        int stepCount = 0;
        int currDirIndex = 0;
        List<Tuple<int, int>> stack = new List<Tuple<int, int>>() { Tuple.Create(startRow, startCol) };

        while (stack.Count > 0)
        {
            var position = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            int curRow = position.Item1;
            int curCol = position.Item2;

            int positionKey = curRow * 1000 + curCol;

            if (!seen.Contains(positionKey))
            {
                stepCount++;
            }
            seen.Add(positionKey);
            graph[curRow][curCol] = (char)stepCount;
            var curDir = dirs[currDirIndex];
            int newRow = curRow + curDir.Item1;
            int newCol = curCol + curDir.Item2;
            if (IsExit(newRow, newCol, graph))
            {
                break;
            }
            if (IsValid(newRow, newCol, graph))
            {
                stack.Add(Tuple.Create(newRow, newCol));
            }
            else
            {
                while (!IsValid(newRow, newCol, graph))
                {
                    currDirIndex = (currDirIndex + 1) % 4;
                    curDir = dirs[currDirIndex];
                    newRow = curRow + curDir.Item1;
                    newCol = curCol + curDir.Item2;
                }
                stack.Add(Tuple.Create(newRow, newCol));
            }
        }

        return stepCount;
    }

    int PartTwo()
    {

		var up = Tuple.Create(-1, 0);
        var right = Tuple.Create(0, 1);
        var down = Tuple.Create(1, 0);
        var left = Tuple.Create(0, -1);

        List<Tuple<int, int>> dirs = new List<Tuple<int, int>> { up, right, down, left };



        List<List<char>> graph = uwu.Split("\n")
                                    .Select(line => line.ToList())
                                    .ToList();

        int numRows = graph.Count;
        int numCols = graph.Count > 0 ? graph[0].Count : 0;

        int startRow = 0;
        int startCol = 0;
        List<Tuple<int, int>> skipPoints = new List<Tuple<int, int>>();
        foreach (int row in GD.Range(numRows))
        {
            foreach (int col in GD.Range(numCols))
            {
                if (graph[row][col] == '^')
                {
                    startRow = row;
                    startCol = col;
                }
                if (graph[row][col] == '.')
                {
                    skipPoints.Add(Tuple.Create(row, col));
                }
            }
        }
        int loopCount = 0;
        foreach (var points in skipPoints)
        {
			int skipRow = points.Item1;
			int skipCol = points.Item2;
            if (IsLoop(startRow, startCol, skipRow, skipCol, graph, dirs))
            {
                loopCount += 1;
            }
        }
        return loopCount;
    }

    bool IsLoop(int startRow, int startCol, int skipRow, int skipCol, List<List<char>> graph, List<Tuple<int, int>> dirs)
    {
        HashSet<Tuple<int, int>> seen = new HashSet<Tuple<int, int>>();
        int currDirIndex = 0;
        graph[skipRow][skipCol] = '#';
        List<Tuple<int, int>> stack = new List<Tuple<int, int>>() { Tuple.Create(startRow, startCol) };
        int retrackingPrevStepCount = 0;

        while (stack.Count > 0)
        {
            var position = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            int curRow = position.Item1;
            int curCol = position.Item2;

            var positionKey = Tuple.Create(curRow, curCol);

            if (seen.Contains(positionKey))
            {
                retrackingPrevStepCount++;
            }
            else
            {
                retrackingPrevStepCount = 0;
            }
            if (retrackingPrevStepCount > 1000)
            {
                graph[skipRow][skipCol] = '.';
                return true;
            }
            seen.Add(Tuple.Create(curRow, curCol));
            var curDir = dirs[currDirIndex];
            int newRow = curRow + curDir.Item1;
            int newCol = curCol + curDir.Item2;
            if (IsExit(newRow, newCol, graph))
            {
                graph[skipRow][skipCol] = '.';
                return false;
            }
            if (IsValid(newRow, newCol, graph))
            {
                stack.Add(Tuple.Create(newRow, newCol));
            }
            else
            {
                while (!IsValid(newRow, newCol, graph))
                {
                    currDirIndex = (currDirIndex + 1) % 4;
                    curDir = dirs[currDirIndex];
                    newRow = curRow + curDir.Item1;
                    newCol = curCol + curDir.Item2;
                }
                stack.Add(Tuple.Create(newRow, newCol));
            }
        }
        graph[skipRow][skipCol] = '.';
        return false;

    }


}
