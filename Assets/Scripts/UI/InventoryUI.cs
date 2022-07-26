using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private SlotUI[] playerSlots;
        public Image dragItem;
        public ItemTooltip itemTooltip;

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }

        private void Start()
        {
            //Give every slot an index
            for(int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
        }

        private void OnUpdateInventoryUI(List<InventoryItem> list)
        {
            for(int i = 0; i < playerSlots.Length; i++)
            {
                //if this slot has an item
                if(list[i].itemAmount > 0)
                {
                    var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                    playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                }
                else
                {
                    playerSlots[i].UpdateEmptySlot();
                }
            }
        }

        /// <summary>
        /// Update hightlight on slot
        /// </summary>
        /// <param name="index">index of the selected slot</param>
        public void UpdateSlotHightLight(int index)
        {
            foreach(var slot in playerSlots)
            {
                if (slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHighLight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHighLight.gameObject.SetActive(false);
                }
            }
        }
    }
}
