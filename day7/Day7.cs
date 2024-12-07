using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[GlobalClass]
public partial class Day7 : Node
{
    string filename = "day7/input.txt";

    public override void _Ready()
    {
        string[] file = File.ReadAllLines(filename);
        PartOne(file);
		PartTwo(file);
    }

    void PartOne(string[] lines)
    {
        long total = 0;
        foreach (string line in lines)
        {
            string[] bits = line.Trim().Split(new string[] { ": " }, StringSplitOptions.None);
            long answer = long.Parse(bits[0]);
            List<int> numbers = bits[1].Split().Select(int.Parse).ToList();
            bool result = Test(answer, numbers);
            if (result)
            {
                total += answer;
            }
        }
        GD.Print($"Part 1: {total}");
    }

    bool Test(long target, List<int> numbers)
    {
        var stack = new Stack<(long currentVal, int index)>();
        stack.Push((numbers[0], 1));

        while (stack.Count > 0)
        {
            var (currentVal, index) = stack.Pop();

            if (index == numbers.Count)
            {
                if (currentVal == target)
                    return true;
            }
            else
            {
                long multTest = currentVal * numbers[index];
                long addTest = currentVal + numbers[index];

                stack.Push((multTest, index + 1));
                stack.Push((addTest, index + 1));
            }
        }

        return false;
    }

	void PartTwo(string[] lines)
	{
		long total = 0;
        foreach (string line in lines)
        {
            string[] bits = line.Trim().Split(new string[] { ": " }, StringSplitOptions.None);
            long answer = long.Parse(bits[0]);
            List<int> numbers = bits[1].Split().Select(int.Parse).ToList();
            bool result = Test2(answer, numbers);
            if (result)
            {
                total += answer;
            }
        }
        GD.Print($"Part 2: {total}");
	
	}

	bool Test2(long target, List<int> numbers)
    {
        var stack = new Stack<(long currentVal, int index)>();
        stack.Push((numbers[0], 1));
		
        while (stack.Count > 0)
        {
            var (currentVal, index) = stack.Pop();

            if (index == numbers.Count)
            {
                if (currentVal == target)
                    return true;
            }
            else
            {
                long multTest = currentVal * numbers[index];
                long addTest = currentVal + numbers[index];
				long concatTest = long.Parse(currentVal.ToString() + numbers[index].ToString());

                stack.Push((multTest, index + 1));
                stack.Push((addTest, index + 1));
				stack.Push((concatTest, index + 1));
            }
        }

        return false;
    }
}
