using UnityEditor;
using UnityEngine;

namespace Transition
{
    public class Teleport : MonoBehaviour
    {
        [SceneName]
        public string sceneToGo;
        public Vector3 posToGO;

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                EventHandler.CallTransitionEvent(sceneToGo, posToGO);
            }
        }
    }
}