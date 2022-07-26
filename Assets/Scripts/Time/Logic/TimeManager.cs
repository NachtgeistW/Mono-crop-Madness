using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int gameHour, gameDay, gameMonth, gameYear;

    public bool gameClockPause;
    private float tikTime;

    void Awake()
    {
        InitGameTime();
    }

    private void Start()
    {
        EventHandler.CallGameDayEvent(gameHour, gameDay, gameMonth, gameYear);
    }

    void Update()
    {
        if (!gameClockPause)
        {
            tikTime += Time.deltaTime;
            if (tikTime >= Settings.hourThreshold)
            {
                tikTime -= Settings.hourThreshold;
                UpdateGameTime();
            }

        }
    }

    private void InitGameTime()
    {
        gameHour = 0;
        gameDay = 1;
        gameMonth = 1;
        gameYear = 2022;
    }
    
    private void UpdateGameTime()
    {
        gameHour++;
        if (gameHour > Settings.hourHold)
        {
            gameHour = 0;
            gameDay++;
            if (gameDay > Settings.dayHold)
            {
                gameDay = 1;
                gameMonth++;
                if (gameMonth > Settings.monthHold)
                {
                    gameMonth = 1;
                    gameYear++;
                }
            }
        }
        EventHandler.CallGameDayEvent(gameHour, gameDay, gameMonth, gameYear);
    }
}

