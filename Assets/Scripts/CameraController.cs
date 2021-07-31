using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour{

    // Store a reference to the player game object
    public GameObject player;

    // Store the offset distance between the player and camera
    private Vector3 offset;            

    // Use this for initialization
    // Calculate and store the offset value by getting the distance between the
    // player's position and camera's position.
    void Start () { offset = transform.position - player.transform.position; }

    // LateUpdate is called after Update each frame
    // Set the position of the camera's transform to be the same as the
    // player's, but offset by the calculated offset distance.
    void LateUpdate () { transform.position = player.transform.position + offset; }
}