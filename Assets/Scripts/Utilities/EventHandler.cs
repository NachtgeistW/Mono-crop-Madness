using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    //Register event action
    public static event Action<List<InventoryItem>> UpdateInventoryUI;
    //Call the action
    public static void CallUpdateInventoryUI(List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(list);
    }

    public static event Action<int, Vector3> InstantiateItemInScene;
    public static void CallInstantiateItemInScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }

    public static event Action<int, Vector3, ItemType> DropItemEvent;
    public static void CallDropItemEvent(int ID, Vector3 pos, ItemType itemType) => DropItemEvent?.Invoke(ID, pos, itemType);

    public static event Action<int> GameHourEvent;
    public static void CallGameHourEvent(int hour)
    {
        GameHourEvent?.Invoke(hour);
    }

    public static event Action<int, int, int, int> GameDateEvent;
    public static void CallGameDateEvent(int hour, int day, int month, int year)
    {
        GameDateEvent?.Invoke(hour, day, month, year);
    }

    public static event Action<int> GameDayEvent;
    public static void CallGameDayEvent(int day) => GameDayEvent?.Invoke(day);

    public static event Action<ItemDetails, bool> ItemSelectEvent;
    public static void CallItemSelectEvent(ItemDetails itemDetail, bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDetail, isSelected);
    }

    public static event Action<string, Vector3> TransitionEvent;
    public static void CallTransitionEvent(string sceneName, Vector3 pos)
    {
        TransitionEvent?.Invoke(sceneName, pos);
    }

    public static event Action BeforeSceneUnloadedEvent;
    public static void CallBeforeSceneUnloadedEvent()
    {
        BeforeSceneUnloadedEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<Vector3> MoveToPositionEvent;
    public static void CallMoveToPositionEvent(Vector3 pos) => MoveToPositionEvent?.Invoke(pos);

    public static event Action<Vector3, ItemDetails> MouseClickEvent;
    public static void CallMouseClickEvent(Vector3 pos, ItemDetails itemDetails)
        => MouseClickEvent?.Invoke(pos, itemDetails);
    public static event Action<Vector3, ItemDetails> ExecuteActionAfterAnimation;
    public static void CallExecuteActionAfterAnimation(Vector3 pos, ItemDetails itemDetails)
        => ExecuteActionAfterAnimation?.Invoke(pos, itemDetails);

    public static event Action<int, TileDetails> PlantSeedEvent;
    public static void CallPlantSeedEvent(int seedID, TileDetails tile) => PlantSeedEvent?.Invoke(seedID, tile);

    public static event Action<int, TileDetails> CropFullyGrowthEvent;
    public static void CallCropFullyGrowthEvent(int seedID, TileDetails tile) => CropFullyGrowthEvent?.Invoke(seedID, tile);
}
