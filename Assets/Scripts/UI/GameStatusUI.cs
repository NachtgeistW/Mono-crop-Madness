using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusUI : MonoBehaviour
{
    public Image healthyBar;

    private void OnEnable()
    {
        EventHandler.GameDayEvent += OnGameDayEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameDayEvent -= OnGameDayEvent;
    }

    private void OnGameDayEvent(int day)
    {
        float percent = HealthyScoreManager.Instance.HealthyScore / 100f;
        if (percent > 1) percent = 1;
        if (percent < 0) percent = 0;
        healthyBar.rectTransform.localScale = new Vector3(percent, 1, 1);
    }
}
