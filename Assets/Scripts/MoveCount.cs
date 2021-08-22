using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

// If player uses all moves in a level, game is over.
// Might add an item the increases amount of turns in a level.
// Assigned to Player in Unity, but disabled it.
public class MoveCount : MonoBehaviour{
    public int turnsTaken = 0;
    public int turnsStart = 25;

    public void CountMove(){
        turnsTaken++;
        if(turnsTaken == turnsStart){
            Debug.Log("Game Over");
            return;
        }
    }

    public void Update(){ if(Input.GetButtonDown("Jump")){ CountMove(); } }
}
