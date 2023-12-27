using System;
using UnityEngine;

public enum Direction
{
    Top,
    Left,
    Bottom,
    Right
}

public static class Extensions
{
    public static Direction Next(this Direction currentDirection)
    {
        Enum.TryParse($"{((int)currentDirection + 1) % 4}", out Direction res);
        return res;
    }
}
