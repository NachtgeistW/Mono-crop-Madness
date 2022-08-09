using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CropDetails
{
    public int seedItemID;
    [Header("��ͬ�׶���Ҫ������")]
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
    [Header("��ͬ�׶ε�prefab")]
    public GameObject[] growthPrefabs;
    [Header("��ͬ�׶ε�ͼƬ")]
    public Sprite[] growthSprites;

    [Header("Option")]
    public bool hasAnimation;
    public bool hasParticalEffect;
}
