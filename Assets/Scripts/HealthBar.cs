using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HealthBar : MonoBehaviour{
    public Image healthBarFill;
    public float health = 100;
    public int healthLoss = 5; // Amount of health lost when Jump is pressed.

    public void LoseHealth(int h){
        health -= h;
        healthBarFill.fillAmount = health / 100; // Slider in Unity goes from 0 to 1
        if(health <= 0){ FindObjectOfType<Movement>().Die(); }
    }

    /*
    CONSUMING TELEPORT POWER-UP
    if player uses teleport and health is full,
        then reduce health by 25 points.
        distance teleported: full distance
    else if health is less than 100, but more than 25,
        then reduce health by half
        distance teleported: half distance
    else, health is less than 25
        then teleport is not consumed.

    CONSUMING ENERGY REFILL
    refills 10% of health if 75% or more
    refills 15% if between 60% and 75%
    refills 25% if between 45% and 60%
    refills 50% is between 20% and 45%
    full refill less than 20%
    */

    private void Update(){ if(Input.GetButtonUp("Jump")){ LoseHealth(healthLoss); } }
}