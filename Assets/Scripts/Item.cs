using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour{
    public enum InteractionType{ NONE, PickUp, Examine}
    public InteractionType interactionType;
    public List<GameObject> items = new List<GameObject>();
    public string descriptionText;
    public Sprite image;
    private void Reset(){
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 9; // Layer 9 is Item layer
    }
    
    public void Interact(){
        switch(interactionType){
            case InteractionType.PickUp:
                //FindObjectOfType<InventorySystem>().PickUpItem(gameObject);
                FindObjectOfType<Interactions>().PickUpItem(gameObject);
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                FindObjectOfType<Interactions>().ExamineItem(this);
                break;
            default:
                break;
        }
    }
}