using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    //Register event action
    public static event Action<List<InventoryItem>> UpdateInventoryUI;
    //Call the action
    public static void CallOnUpdateInventoryUI(List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(list);
    }

    public static event Action<int, Vector3> InstantiateItemInScene;
    public static void CallInstantiateItemInScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }

    public static event Action<int> GameHourEvent;
    public static void CallGameHourEvent(int hour)
    {
        GameHourEvent?.Invoke(hour);
    }

    public static event Action<int, int, int, int> GameDayEvent;
    public static void CallGameDayEvent(int hour, int day, int month, int year)
    {
        GameDayEvent?.Invoke(hour, day, month, year);
    }

    public static event Action<ItemDetails, bool> ItemSelectEvent;
    public static void CallItemSelectEvent(ItemDetails itemDetail, bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDetail, isSelected);
    }

    public static event Action BeforeSceneLoadedEvent;
    public static void CallBeforeSceneLoadedEvent()
    {
        BeforeSceneLoadedEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }
}