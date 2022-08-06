using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ItemBounce : MonoBehaviour
    {
        private Transform spriteTransform;
        private BoxCollider2D boxCollider;

        public float gravity = -3.5f;
        private bool isGrounded = false;
        private float distance;
        private Vector2 direction;
        private Vector3 targetPos;

        private void Awake() 
        {
            spriteTransform = transform.GetChild(0);
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }

        private void Update() 
        {
            OnBounce();
        }

        public void InitBounceItem(Vector3 target, Vector2 dir)
        {
            boxCollider.enabled = false;
            direction = dir;
            targetPos = target;
            distance = Vector3.Distance(targetPos, transform.position);

            //Generate items at 1.5 from the player
            spriteTransform.position += Vector3.up * 1.5f;
        }

        private void OnBounce()
        {
            isGrounded = spriteTransform.position.y <= transform.position.y;
            if(Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position += (Vector3)direction * Time.deltaTime * distance * -gravity;
            }

            //If still in the air
            if (!isGrounded)
            {
                spriteTransform.position += Vector3.up * Time.deltaTime * gravity;
            }
            else
            {
                spriteTransform.position = transform.position;
                boxCollider.enabled = true;
            }
        }
    }
}
