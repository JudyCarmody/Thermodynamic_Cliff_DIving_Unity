//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour{
    public enum InteractionType{ NONE, PickUp}
    public InteractionType type;

    private void Reset(){
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 9; // Layer 9 is Item layer
    }
    
    public void Interact(){
        switch(type){
            case InteractionType.PickUp:
                Debug.Log("PickUp");
                break;
            default:
                break;
        }
    }

}
