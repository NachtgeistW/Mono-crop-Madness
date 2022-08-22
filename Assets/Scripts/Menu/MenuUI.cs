using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject menuPanel;
    public GameObject endPanel;
    public GameObject endSucceedPanel;
    public GameObject endFailedPanel;

    private void OnEnable()
    {
        EventHandler.GameEndEvent += OnGameEndEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameEndEvent -= OnGameEndEvent;
    }

    private void Start() 
    {
        EventHandler.CallGamePauseEvent();
        endPanel.SetActive(false);
    }

    public void SwitchPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index) 
            {
                //put the panel at index i to the front
                panels[i].transform.SetAsLastSibling();
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game");
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        EventHandler.CallGameResumeEvent();
    }

    public void OnGameEndEvent(bool isGameClear)
    {
        endPanel.SetActive(true);
        if (isGameClear)
        {
            endSucceedPanel.SetActive(true);
            endFailedPanel.SetActive(false);
        }
        else
        {
            endFailedPanel.SetActive(true);
            endSucceedPanel.SetActive(false);
        }
    }
}
