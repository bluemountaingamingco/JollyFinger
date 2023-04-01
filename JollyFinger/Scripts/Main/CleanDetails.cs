using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDetails 
{
    public static int GetStarWon(int levelIndex, float time)
    {
        int star = 0;

        if (levelIndex == 1)
        {
            if (time >= 7)
                star = 1;

            else if (time > 5.50)
                star = 2;

            else
                star = 3;
        }

        else if (levelIndex == 2)
        {
            if (time >= 9)
                star = 1;

            else if (time >= 8)
                star = 2;

            else
                star = 3;
        }

        return star;
    }

    public static float GetTargetAmount(int levelIndex)
    {
        float targetAmount = 0;

        if (levelIndex == 1)
            targetAmount = 13694.82f;
        else
            targetAmount = 1;

        return targetAmount;
    }
}
