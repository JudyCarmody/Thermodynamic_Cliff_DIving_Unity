//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    // Detection Point
    public Transform detectionPoint;
    
    // Detection Radius
    private const float detectionRadius = 0.2f;

    // Detection Layer
    public LayerMask detectionLayer;

    // Update is called once per frame
    void Update(){
        if(DetectObject()){
            if(InteractInput()){
                Debug.Log("Interact");
            }
        }
        
    }

    bool InteractInput(){ return Input.GetKeyDown(KeyCode.E); }

    bool DetectObject(){ return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer); }
}
