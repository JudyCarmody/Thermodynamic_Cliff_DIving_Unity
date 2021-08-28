using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LawsDefinitions : MonoBehaviour
{
    [Header("General Fields")]
    public bool isOpen;
    public GameObject laws_Window;
    [Header("Laws Window Description")]
    public Text laws_Title;
    public Text laws_Explanation;

    private void Update(){ if(Input.GetKeyDown(KeyCode.L)){ ToggleReadingWindow(); } }

    void Update_UI(){ HideDescription(); }

    void ToggleReadingWindow(){
        isOpen = !isOpen;
        laws_Window.SetActive(isOpen);
       // Update_UI();
    }

    #region Show and Hide Laws Window
    public void ShowReadingWindow(){
        laws_Title.gameObject.SetActive(true);
        laws_Explanation.gameObject.SetActive(true);
    }
    
    public void HideDescription(){
        laws_Title.gameObject.SetActive(false);
        laws_Explanation.gameObject.SetActive(false);
    }
    #endregion
}