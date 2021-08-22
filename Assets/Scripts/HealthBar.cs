using System.Security.Cryptography;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HealthBar : MonoBehaviour{
    public Image healthBarFill;
    public float health = 100;

    public void LoseHealth(int h){
        health -= h;
        healthBarFill.fillAmount = health / 100; // Slider in Unity goes from 0 to 1
        if(health <= 0){
            Debug.Log("Game Over: No Health");
            return;
        }
    }

    private void Update(){ if(Input.GetKeyDown(KeyCode.Return)){ LoseHealth(25); } }
}