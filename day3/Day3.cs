using Godot;
using System;
using System.Collections.Generic;
using System.IO;

[GlobalClass]
public partial class Day3 : Node
{

	string filename = "day3/input.txt";

    public override void _Ready()
    {
        string input = File.ReadAllText(filename);
		int part1 = PartOne(input);

		int part2 = PartTwo(input);

		GD.Print($"part 1: {part1}");
		GD.Print($"part 2: {part2}");
    }

    

	int PartOne(string input)
	{
		List<int> res = new List<int>();
		for(int i = 0; i < input.Length; i++)
		{
			if(input[i] == 'm')
			{
				if (i + 3 < input.Length && input[i + 1] == 'u' && input[i + 2] == 'l' && input[i + 3] == '(')
				{
					if (i + 4 < input.Length && !Char.IsDigit(input[i + 4])) continue;

					int k = 4;
					string num1 = "";
					string num2 = "";

					while (k < input.Length && Char.IsDigit(input[i + k]))
					{
						num1 += input[i + k];
						k++;
					}

					if (k < input.Length && input[i + k] != ',') continue;
					k++;

					while (k < input.Length && Char.IsDigit(input[i + k]))
					{
						num2 += input[i + k];
						k++;
					}

					if (k < input.Length && input[i + k] != ')') continue;

					res.Add(int.Parse(num1) * int.Parse(num2));
				}
			}
		}

		int sum = 0;
		foreach (var val in res)
        {
            sum += val;
        }
        return sum;
	}

	int PartTwo(string input)
    {
        List<int> res = new List<int>();
        bool performScan = true;

        for (int i = 0; i < input.Length; i++)
        {
            if (performScan && input[i] == 'm')
            {
                if (i + 3 < input.Length && input[i + 1] == 'u' && input[i + 2] == 'l' && input[i + 3] == '(')
                {
                    if (i + 4 < input.Length && !Char.IsDigit(input[i + 4])) continue;

                    int k = 4;
                    string num1 = "";
                    string num2 = "";

                    while (k < input.Length && Char.IsDigit(input[i + k]))
                    {
                        num1 += input[i + k];
                        k++;
                    }

                    if (k < input.Length && input[i + k] != ',') continue;

                    k++;

                    while (k < input.Length && Char.IsDigit(input[i + k]))
                    {
                        num2 += input[i + k];
                        k++;
                    }

                    if (k < input.Length && input[i + k] != ')') continue;

                    res.Add(int.Parse(num1) * int.Parse(num2));
                }
            }

            if (input[i] == 'd')
            {
                if (i + 1 < input.Length && input[i + 1] == 'o')
                {
                    if (i + 2 < input.Length && input[i + 2] == '(')
                    {
                        if (i + 3 < input.Length && input[i + 3] == ')')
                        {
                            performScan = true;
                        }
                    }
                    else
                    {
                        if (i + 2 < input.Length && input[i + 2] == 'n')
                        {
                            if (i + 3 < input.Length && input[i + 3] == '\'')
                            {
                                if (i + 4 < input.Length && input[i + 4] == 't')
                                {
                                    if (i + 5 < input.Length && input[i + 5] == '(')
                                    {
                                        if (i + 6 < input.Length && input[i + 6] == ')')
                                        {
                                            performScan = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        int sum = 0;
        foreach (var val in res)
        {
            sum += val;
        }
        return sum;
    }

}
