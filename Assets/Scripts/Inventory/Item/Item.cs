using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{ 
    //场景中的物体
    public class Item : MonoBehaviour
    {
        public int itemID;
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D coll;
        public ItemDetails itemDetails;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (itemID != 0)
                Init(itemID);
        }

        public void Init(int ID)
        {
            itemID = ID;

            //Let the Inventory get current data
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            if (itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;

                //change collider size
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.size.x);
            }
        }
    }

}

