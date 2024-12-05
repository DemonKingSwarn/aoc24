using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class Day5 : Node
{
    string filename = "day5/input.txt";

    public override void _Ready()
    {
        string input = GetInput(filename);
        GD.Print($"Part1: {PartOne(input)}");
        GD.Print($"Part2: {PartTwo(input)}");
    }

    string GetInput(string directory)
    {
        return System.IO.File.ReadAllText(directory);
    }

    (List<(int Left, int Right)> Rules, List<int[]> Updates) FormatInput(string input)
    {
        var parts = input.Trim().Split(new string[] { "\n\n" }, StringSplitOptions.None);

        var rules = parts[0].Split('\n').Select(line =>
        {
            var parts = line.Split('|').Select(int.Parse).ToArray();
            return (Left: parts[0], Right: parts[1]);
        }).ToList();

        var updates = parts[1].Split('\n').Select(line => line.Split(',').Select(int.Parse).ToArray()).ToList();

        return (rules, updates);
    }

    Dictionary<int, HashSet<int>> IndexRules(List<(int Left, int Right)> rules)
    {
        var result = new Dictionary<int, HashSet<int>>();

        foreach (var rule in rules)
        {
            if (!result.ContainsKey(rule.Left))
            {
                result[rule.Left] = new HashSet<int>();
            }
            result[rule.Left].Add(rule.Right);
        }

        return result;
    }

    bool ValidateUpdate(int[] update, Dictionary<int, HashSet<int>> rulesIndex)
    {
        for (int i = 0; i < update.Length; i++)
        {
            for (int j = i + 1; j < update.Length; j++)
            {
                int left = update[i];
                int right = update[j];

                if (!rulesIndex.ContainsKey(left) || !rulesIndex[left].Contains(right))
                {
                    return false;
                }
            }
        }

        return true;
    }

    int GetMiddle(int[] arr)
    {
        return arr[arr.Length / 2];
    }

    // PartOne solution
    int PartOne(string input)
    {
        var formattedInput = FormatInput(input);
        var rulesIndex = IndexRules(formattedInput.Rules);

        var validUpdates = formattedInput.Updates.Where(update => ValidateUpdate(update, rulesIndex)).ToList();
        var middles = validUpdates.Select(GetMiddle).ToList();

        return middles.Sum();
    }

    int[] FixUpdate(int[] update, Dictionary<int, HashSet<int>> rulesIndex)
    {
        return update.OrderBy(a => update.Count(b => rulesIndex.ContainsKey(a) && rulesIndex[a].Contains(b)))
                     .ToArray();
    }


    // PartTwo solution
    int PartTwo(string input)
    {
        var formattedInput = FormatInput(input);
        var rulesIndex = IndexRules(formattedInput.Rules);

        var invalidUpdates = formattedInput.Updates.Where(update => !ValidateUpdate(update, rulesIndex)).ToList();
        
        var fixedUpdates = invalidUpdates.Select(update => FixUpdate(update, rulesIndex)).ToList();
        
        var middles = fixedUpdates.Select(GetMiddle).ToList();

        return middles.Sum();
    }
}
