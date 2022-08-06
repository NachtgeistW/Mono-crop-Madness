using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemShadow : MonoBehaviour
    {
        public SpriteRenderer itemSpriteRenderer;
        private SpriteRenderer shadowSpriteRenderer;

        private void Awake() 
        {
            shadowSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start() 
        {
            shadowSpriteRenderer.sprite = itemSpriteRenderer.sprite;
            shadowSpriteRenderer.color = new Color(0, 0, 0, 0.3f);
        }
    }
}