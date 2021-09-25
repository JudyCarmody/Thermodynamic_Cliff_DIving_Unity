using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour{
    [SerializeField]private GameObject currentPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private float wait = 0.5f;

// When player presses DOWN, collision is disabled. This allows the player to fall to a platform below
void Update(){
    if(Input.GetKeyDown(KeyCode.DownArrow)){ if(currentPlatform != null){ StartCoroutine(DisableCollision()); } } }

    // Player lands on platform
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("OneWayPlatform") || collision.gameObject.CompareTag("Respawn")){
            currentPlatform = collision.gameObject;
        }
    }

    // Player leaves platform
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("OneWayPlatform") || collision.gameObject.CompareTag("Respawn")){
            currentPlatform = null;
        }
    }

    // Disable collision when player is on platform; this allows the player to fall to a platform below
    private IEnumerator DisableCollision(){
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(wait);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}