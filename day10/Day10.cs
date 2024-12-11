using Godot;
using System.Collections.Generic;
using System.IO;

[GlobalClass]
public partial class Day10 : Node
{
    string filename = "day10/input.txt";

	 (int, int)[] directions = { (-1, 0), (1, 0), (0, 1), (0, -1) };


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        string[] lines = File.ReadAllLines(filename);
		Dictionary<(int, int), char> area = new Dictionary<(int, int), char>();
		for (int y = 0; y < lines.Length; y++)
		{
			for(int x = 0; x < lines[y].Length; x++)
			{
				area[(x, y)] = lines[x][y];
			}
		}

		List<(int, int)> trailheads = new List<(int, int)>();
        foreach (var key in area.Keys)
        {
            if (area[key] == '0')
            {
                trailheads.Add(key);
            }
        }

        // Count unique last positions in trails for part 1
        int part1 = 0;
        foreach (var xy in trailheads)
        {
            var trails = FindTrails(xy, new List<(int, int)>(), area);
            var lastPositions = new HashSet<(int, int)>();
            foreach (var trail in trails)
            {
                lastPositions.Add(trail[trail.Count - 1]);
            }
            part1 += lastPositions.Count;
        }
        GD.Print($"1: {part1}");

        // Count all trails for part 2
        int part2 = 0;
        foreach (var xy in trailheads)
        {
            var trails = FindTrails(xy, new List<(int, int)>(), area);
            part2 += trails.Count;
        }
    	GD.Print($"2: {part2}");

    }

	
	List<List<(int, int)>> FindTrails((int, int) xy, List<(int, int)> trail, Dictionary<(int, int), char> area)
    {
        List<List<(int, int)>> trails = new List<List<(int, int)>>();

        if (area[xy] == '9')
        {
            trails.Add(new List<(int, int)>(trail));
        }
        else
        {
            foreach (var d in directions)
            {
                var n_xy = (xy.Item1 + d.Item1, xy.Item2 + d.Item2);
                if (area.ContainsKey(n_xy) && int.Parse(area[n_xy].ToString()) - int.Parse(area[xy].ToString()) == 1)
                {
                    var newTrail = new List<(int, int)>(trail) { n_xy };
                    trails.AddRange(FindTrails(n_xy, newTrail, area));
                }
            }
        }

        return trails;
    }


}
