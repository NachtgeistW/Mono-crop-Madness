using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private Image dawnImage;
    [SerializeField] private Image dayImage;
    [SerializeField] private Image duskImage;
    [SerializeField] private Image nightImage;
    [SerializeField] private RectTransform clockParent;
    [SerializeField] private TextMeshProUGUI dateText;

    private List<GameObject> clockBlocks = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < clockParent.childCount; i++)
        {
            var block = clockParent.GetChild(i).gameObject;
            clockBlocks.Add(block);
            block.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnEnable()
    {
        EventHandler.GameDateEvent += OnGameDayEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameDateEvent -= OnGameDayEvent;
    }

    private void OnGameDayEvent(int hour, int day, int month, int year)
    {
        var culture = new System.Globalization.CultureInfo("en-GB");
        var dateTime = new System.DateTime(year, month, day);
        dateText.text = string.Format(culture, "{0:d}", dateTime);

        SwitchHourImage(hour);
        SwitchDayAndNightImage(hour);
    }

    /// <summary>
    /// Switch clock blocks based on the game hour
    /// </summary>
    /// <param name="hour">current game hour</param>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;

        if (index == 0)
        {
            foreach(var block in clockBlocks)
            {
                block.SetActive(false);
            }
        }
        else
        {
            for(int i = 0; i < clockBlocks.Count; i++)
            {
                if (i < index + 1)
                    clockBlocks[i].SetActive(true);
                else
                    clockBlocks[i].SetActive(false);
            }
        }
    }

    private void SwitchDayAndNightImage(int hour)
    {
        const float fadeSpeed = 1f;
        //Debug.Log(hour);
        if (hour == 6)
        {
            nightImage.DOFade(0, fadeSpeed);
            dawnImage.DOFade(1, fadeSpeed);
        }
        if (hour == 9)
        {
            dawnImage.DOFade(0, fadeSpeed);
            dayImage.DOFade(1, fadeSpeed);
        }
        if (hour == 16)
        {
            dayImage.DOFade(0, fadeSpeed);
            duskImage.DOFade(1, fadeSpeed);
        }
        if (hour == 19 || hour < 6)
        {
            duskImage.DOFade(0, fadeSpeed);
            nightImage.DOFade(1, fadeSpeed);
        }
    }
}
