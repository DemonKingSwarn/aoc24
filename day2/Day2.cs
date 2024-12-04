using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

[GlobalClass]
public partial class Day2 : Node
{
	string filename = "day2/input.txt";

	public override void _Ready()
	{
		int countPart1 = 0;
		int countPart2 = 0;

		foreach(var line in File.ReadLines(filename))
		{
			var list = line.Split(' ').Select(int.Parse).ToList();
			if(IsSafe(list))
			{
				countPart1++;
				countPart2++;
				continue;
			}

			for(int i = 0; i < list.Count; i++)
			{
				List<int> tempList = new List<int>(list);
				tempList.RemoveAt(i);
				if(IsSafe(tempList))
				{
					countPart2++;
					break;
				}
			}
		}

		GD.Print($"Part1: {countPart1}");
		GD.Print($"Part2: {countPart2}");
	}

	bool IsSafe(List<int> list)
	{
		int nol = list.Count;
		bool isIncreasing = true;
		bool isDecreasing = true;

		for(int i = 1; i < nol; i++)
		{
			int x = list[i-1];
			int y = list[i];
			int diff = y - x;
			int diffAbs = Math.Abs(diff);

			if(diffAbs < 1 || diffAbs > 3)
			{
				isIncreasing = false;
				isDecreasing = false;
				break;
			} 

			if(diff < 0) isIncreasing = false;
			if(diff > 0) isDecreasing = false;
		}

		return isIncreasing || isDecreasing;
	}

}
