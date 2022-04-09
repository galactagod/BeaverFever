using Godot;
using System;

public static class Calculations
{
    public static bool HitFlash(int timer, bool curVisibility)
    {
        if (timer % 7 == 0)
        {
            curVisibility = !curVisibility;
        }

        return curVisibility;
    }
}
