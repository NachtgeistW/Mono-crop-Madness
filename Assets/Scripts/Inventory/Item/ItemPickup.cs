using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ItemPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D coll)
        {
            Item item = coll.GetComponent<Item>();
            if (item != null)
            {
                if (item.itemDetails.canBePickup)
                {
                    InventoryManager.Instance.AddItem(item, true);
                }
            }
        }
    }
}
