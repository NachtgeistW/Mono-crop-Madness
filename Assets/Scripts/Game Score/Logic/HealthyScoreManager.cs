using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthyScoreManager : Singleton<HealthyScoreManager>
{
    public int HealthyScore;

    private void OnEnable()
    {
        EventHandler.GameDayEvent += OnGameDayEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameDayEvent -= OnGameDayEvent;
    }

    private void Start()
    {
        HealthyScore = 20;
    }

    private void Update()
    {
        if (HealthyScore >= 100)
        {
            EventHandler.CallGameEndEvent(true);
            Debug.Log("Succeed");
        }
        if (HealthyScore <= 0)
        {
            EventHandler.CallGameEndEvent(false);
            Debug.Log("Failed");
        }
    }

    /// <summary>
    /// Update healty score everyday
    /// </summary>
    /// <param name="day"></param>
    private void OnGameDayEvent(int day)
    {
        HealthyScore -= Settings.DailyReduceScore;
        var fullyGrowthPlantAmounts = Map.GridMapManager.Instance.GetFullyGrowthPlantAmountOnScene(SceneManager.GetActiveScene().name);
        HealthyScore += fullyGrowthPlantAmounts[ItemType.Grass] * Settings.GrassRecoveryScore +
            fullyGrowthPlantAmounts[ItemType.Bush] * Settings.BushRecoveryScore +
            fullyGrowthPlantAmounts[ItemType.Tree] * Settings.TreeRecoveryScore;
    }
}
