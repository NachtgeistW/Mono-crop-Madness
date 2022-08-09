using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CropDetails
{
    public int seedItemID;
    [Header("不同阶段需要的天数")]
    public int[] growthDays;
    public int TotalGrowthDays
    {
        get
        {
            int amount = 0;
            foreach (var day in growthDays)
            {
                amount += day;
            }
            return amount;
        }
    }
    [Header("不同阶段的prefab")]
    public GameObject[] growthPrefabs;
    [Header("不同阶段的图片")]
    public Sprite[] growthSprites;

    [Header("Option")]
    public bool hasAnimation;
    public bool hasParticalEffect;
}
