using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseDisplay : MonoBehaviour
{
    [Header("General Fields")]
    public bool isPaused;
    public GameObject pause_Window;
    [Header("Pause Window Description")]
    public Text pause_Title;

    private void Update(){ if(Input.GetKeyDown(KeyCode.P)){ TogglePauseWindow(); } }

    void Update_UI(){ HidePauseWindow(); }

    void TogglePauseWindow(){
        isPaused = !isPaused;
        pause_Window.SetActive(isPaused);
       // Update_UI();
    }

    #region Show and Hide Laws Window
    public void ShowPauseWindow(){ pause_Title.gameObject.SetActive(true); }
    public void HidePauseWindow(){ pause_Title.gameObject.SetActive(false); }
    #endregion
}