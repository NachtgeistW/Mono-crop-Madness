using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject menuPanel;

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
    }
}
