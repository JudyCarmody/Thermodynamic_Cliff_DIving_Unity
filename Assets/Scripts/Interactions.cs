//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour{
    private const float detectionRadius = 0.2f;

    public Transform detectionPoint;
    public LayerMask detectionLayer;
    public GameObject detectedObject;

    // Update is called once per frame
    void Update(){
        if(DetectObject()){
            if(InteractInput()){
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteractInput(){ return Input.GetKeyDown(KeyCode.E); }

    bool DetectObject(){
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if(obj == null){
            detectedObject = null;
            return false;
        }

        else{
            detectedObject = obj.gameObject;
            return true;
        }
    }
}
