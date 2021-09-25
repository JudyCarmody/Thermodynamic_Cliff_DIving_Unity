using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour{
    private GameObject currentPlatform;
    SpriteRenderer platform;
    [SerializeField] private Color newCyan = new Color(50, 0, 100, 80);
    [SerializeField] private BoxCollider2D playerCollider;

    void Update(){ ChangePlatformColor(); }

    // Player lands on platform
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("OneWayPlatform")){
            currentPlatform = collision.gameObject;
            ChangePlatformColor(); 
        }
    }

    // Player leaves platform
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("OneWayPlatform") || collision.gameObject.CompareTag("Respawn")){
            currentPlatform = null;
        }
    }

    // Change color of platform when player lands
    private void ChangePlatformColor(){
        // Uncomment the line below to make the platform color solid.
        //BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();
        currentPlatform.GetComponent<Renderer>().material.color = newCyan;
    }
}