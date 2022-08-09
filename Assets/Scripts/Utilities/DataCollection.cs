using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int itemID;
    public string name;
    public int amount;

    public ItemType itemType;
    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDescription;
    public int itemUseRadius;
    public int itemEffectRadius;
    public bool canBePickup;
}

[System.Serializable]
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}

[System.Serializable]
public class TileProperty
{
    public Vector2Int tileCoordinate;
    public GridType gridType;
    public bool boolTypeValue;
}

[System.Serializable]
public class TileDetails
{
    public Vector2Int coord;
    public bool canPlant;   //This equals to canDropItem
    public bool isNPCObstacle;
    public int daySincePlanted = -1;
    public int seedID = -1;
    public int grothDays = -1;
}