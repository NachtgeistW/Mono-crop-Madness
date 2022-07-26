using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Get Compoment")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] public Image slotHighLight;
        [SerializeField] private Button button;

        public bool isSelected;

        //item info
        public ItemDetails itemDetails;
        public int itemAmount;
        public int slotIndex;

        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        public void Start()
        {
            isSelected = false;
            if(itemDetails.itemID == 0)
                UpdateEmptySlot();
        }

        /// <summary>
        /// Update slot info
        /// </summary>
        /// <param name="item"></param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            slotImage.enabled = true;
            itemAmount = amount;
            amountText.text = amount.ToString();
            button.interactable = true;
        }

        /// <summary>
        /// Update slot to an empty slot
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
            { 
                isSelected = false;
            }
            amountText.text = string.Empty;
            slotImage.enabled = false;
            button.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;
            inventoryUI.UpdateSlotHightLight(slotIndex);
            EventHandler.CallItemSelectEvent(itemDetails, isSelected);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            inventoryUI.dragItem.enabled = true;
            inventoryUI.dragItem.sprite = slotImage.sprite;
            inventoryUI.dragItem.SetNativeSize();
            
            isSelected = true;
            inventoryUI.UpdateSlotHightLight(slotIndex);
        }

        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.enabled = false;

            //Drag the item to the world
/*            if (eventData.pointerCurrentRaycast.gameObject == null)
            {
                var pos = Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                EventHandler.CallInstantiateItemInScene(itemDetails.itemID, pos);
            }
*/        }
    }
}
