using Godot;
using System;

public static class Calculations
{
    public static bool HitFlash(LevelControl sndPlayer, AudioStreamSample snd, int timer, bool curVisibility)
    {

        if (timer % 7 == 0)
        {
            curVisibility = !curVisibility;       
        }

        if (timer % 16 == 0)
        {
            sndPlayer.SfxPlayerManager(-1, snd, -20, 0.8f);
        }

        return curVisibility;
    }

    // simply inserts commas based on number length and returns a string
    public static string IntStringCommas(int value)
    {
        string numStr = value.ToString();
        int divisor = 0;
        int strLen = numStr.Length;

        for (int i = 0; i < Math.Floor((double)(strLen-1) / 3); i++)
        {
            numStr = numStr.Insert(strLen - 3 - divisor, ",");
            divisor += 3;
        }

        return numStr;
        
    }
}
