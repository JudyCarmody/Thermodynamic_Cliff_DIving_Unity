//using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    public List<GameObject> inventoryItems = new List<GameObject>();
    public bool isOpen;
    [Header("UI Items Section")]
    public GameObject ui_Window;
    public Image[] items_images;
    [Header("UI Item Description")]
    public Image description_Image;
    public Text description_Name;
    public Text description_Text;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            ToggleInventory();
        }
    }

    public void PickUpItem(GameObject item){
        inventoryItems.Add(item);
        Update_UI();
    }

    void Update_UI(){
        for(int i=0; i < inventoryItems.Count; i++){
            items_images[i].sprite=inventoryItems[i].GetComponent<SpriteRenderer>().sprite;
            items_images[i].gameObject.SetActive(true);
        }
    }

    void HideAll(){ foreach(var i in items_images){ i.gameObject.SetActive(false); } }

    void ToggleInventory(){
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);
    }
}