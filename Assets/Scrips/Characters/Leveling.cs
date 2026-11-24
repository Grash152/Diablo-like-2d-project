using Scrips.Characters;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public static class Leveling
{
    [SerializeField] public static int currentXP = 0;
    [SerializeField] public static int currentLvl = 0;
    public static bool _upgradeOpen=false;
    public static int levelUp = 50;
    public static int DiffFormLevel=10;

    private static void LevelUP()
    {
        Debug.Log("LEVEL UP GG");
        currentLvl++;
        levelUp += DiffFormLevel;
        DiffFormLevel += 20;
        _upgradeOpen = true;
    }
    public static void GainXP(int xp)
    {
        currentXP += xp;
        if (currentXP >= levelUp)
        {
            LevelUP();
        }
        Debug.Log(currentXP);
    }
    public static int GetLevelUp()
    {
        return levelUp;
    }
    public static int GetCurrentXp()
    {
        return currentXP;
    }
}



