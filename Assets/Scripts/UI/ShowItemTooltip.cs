using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(SlotUI))]
    public class ShowItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SlotUI slotUI;
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private Vector3 offset = Vector3.up * 60;

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            inventoryUI.itemTooltip.gameObject.SetActive(true);
            inventoryUI.itemTooltip.SetupTooltip(slotUI.itemDetails);

            //Force rebuild the layout of the tooltip
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

            //Set its position
            inventoryUI.itemTooltip.transform.position = transform.position + offset;
        }
/*        public void OnPointerMove(PointerEventData eventData)
        {
            inventoryUI.itemTooltip.transform.position = Input.mousePosition + offset;
        }

*/        public void OnPointerExit(PointerEventData eventData)
        {
            inventoryUI.itemTooltip.gameObject.SetActive(false);
        }

    }
}
