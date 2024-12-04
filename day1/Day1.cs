using Godot;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

public partial class Day1 : Node
{
	string filename = "day1/input.txt";
	string pattern = @"(\d+)[\s]*(\d+)";
	
	Regex regex;

    public override void _Ready()
    {
		regex = new Regex(pattern);
		var lines = File.ReadLines(filename)
			.Select(line => {
				var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
				return new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1]));
			}).ToList();

		var first = lines.Select(x => x.Item1).OrderBy(x => x).ToList();
		var second = lines.Select(x => x.Item2).OrderBy(x => x).ToList();

		int nol = lines.Count();

		int part1 = 0;
		int part2 = 0;

		for(int i = 0; i < nol; i++)
		{
			int x = first[i];
			int y = second[i];
			int n = second.Count(s => s == x);

			if(x != y)
			{
				int diff = x > y ? x - y : y - x;
				part1 += diff;
			}

			part2 += x * n;
		}

		GD.Print($"Part 1: {part1}");
		GD.Print($"Part 2: {part2}");
    }
}
