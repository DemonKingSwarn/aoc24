#!/usr/bin/env python3

with open("input.txt", 'r') as f:
    nums = f.read().split(" ")

def main(nums):
    nums = [int(num) for num in nums]
    
    i = 0
    j = 0
    p1 = 0
    p2 = 0

    while i < 25:
        nums = calc(nums)
        p1 = nums
        i += 1

    print(f"Part 1: {len(p1)}")
    
    while j < 75:
        nums = calc(nums)
        p2 = nums
        j += 1

    print(f"Part 2: {len(p2)}")


def split_nums(x):
    num_str = str(x)
    mid = len(num_str) // 2
    left = num_str[:mid]
    right = num_str[mid:]

    return left, right


def calc(nums):
    new = []
    for num in nums:
        if num == 0:
            new.append(1)
        elif len(str(num)) % 2 == 0:
            left, right = split_nums(num)
            new.append(int(left))
            new.append(int(right))
        else:
            new.append(num * 2024)

    return new

main(nums)
