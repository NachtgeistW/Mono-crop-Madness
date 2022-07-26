using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchBounds : MonoBehaviour
{
    //TODO: ?????????
    // Start is called before the first frame update
    void Start()
    {
        SwitchConfinerShape();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchConfinerShape()
    {
        var confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        var confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerShape;

        //Call this if the bounding shape's points change at runtime
        confiner.InvalidatePathCache();
    }
}
