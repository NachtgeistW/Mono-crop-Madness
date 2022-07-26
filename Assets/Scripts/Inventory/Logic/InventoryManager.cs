using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("Item Data")]
        public ItemDataList_SO ItemDataList_SO;

        [Header("Bag Data")]
        public InventoryBag_SO PlayerBag;   // infomation of player's bag

        private void Start()
        {
            EventHandler.CallOnUpdateInventoryUI(PlayerBag.itemList);
        }

        /// <summary>
        /// Return item info via item ID
        /// </summary>
        /// <param name="ID">item ID</param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return ItemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// Add item into player's bag
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestory">If it needs to be destroy from map</param>
        public void AddItem(Item item, bool toDestory)
        {
            //If the player already has this item
            var index = GetItemIndexInBag(item.itemID);
            AddItemAtIndex(item.itemID, index, 1);

            Debug.Log(GetItemDetails(item.itemID).name);
            
            //Update UI by calling the action
            EventHandler.CallOnUpdateInventoryUI(PlayerBag.itemList);
        }

        /// <summary>
        /// Search item in player's bag by its ID
        /// </summary>
        /// <param name="ID">item ID</param>
        /// <returns>the index of item in bag, -1 if not found</returns>
        private int GetItemIndexInBag(int ID)
        {
            for(int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                if(PlayerBag.itemList[i].itemID == ID)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Add item in specificy slot index
        /// </summary>
        /// <param name="ID">item id</param>
        /// <param name="index">slot index</param>
        /// <param name="amount">amount to be added</param>
        private void AddItemAtIndex(int ID, int index, int amount)
        {
            //This item doesn't exist in player's bag
            if (index == -1)
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for(int i = 0; i < PlayerBag.itemList.Count; i++)
                {
                    //find an empty slot
                    if (PlayerBag.itemList[i].itemID == 0)
                    {
                        PlayerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else
            {
                int newAmount = PlayerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = newAmount };
                PlayerBag.itemList[index] = item;
            }
        }
    }
}
