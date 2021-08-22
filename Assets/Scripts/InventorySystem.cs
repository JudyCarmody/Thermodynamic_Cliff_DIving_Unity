using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour{
    [Header("General Fields")]
    public List<GameObject> inventoryItems = new List<GameObject>();
    public bool isOpen;
    [Header("UI Items Section")]
    public GameObject ui_Window;
    public Image[] items_Images;
    [Header("UI Item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Name;
    public Text description_Text;

    private void Update(){ if(Input.GetKeyDown(KeyCode.I)){ ToggleInventory(); } }

    public void PickUpItem(GameObject item){
        inventoryItems.Add(item);
        Update_UI();
    }

    void Update_UI(){
        HideAll();
        for(int i = 0; i < inventoryItems.Count; i++){
            items_Images[i].sprite = inventoryItems[i].GetComponent<SpriteRenderer>().sprite;
            items_Images[i].gameObject.SetActive(true);
        }
    }

    void HideAll(){
        foreach(var i in items_Images){ i.gameObject.SetActive(false); }
        HideDescription();
    }

    void ToggleInventory(){
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);
        Update_UI();
    }

    #region Show and Hide item descriptions in inventory
    public void ShowDescription(int id){
        description_Image.sprite = items_Images[id].sprite;
        description_Name.text = inventoryItems[id].name;
        description_Text.text = inventoryItems[id].GetComponent<Item>().descriptionText;

        description_Image.gameObject.SetActive(true);
        description_Name.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true);
    }
    
    public void HideDescription(){
        description_Image.gameObject.SetActive(false);
        description_Name.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
    }
    #endregion

    public void Consume(int id){
        if(inventoryItems[id].GetComponent<Item>().itemType == Item.ItemType.Consumable){
            Debug.Log($"CONSUMED {inventoryItems[id].name}");
            inventoryItems[id].GetComponent<Item>().consumeEvent.Invoke();
            Destroy(inventoryItems[id], 0.2f);
            inventoryItems.RemoveAt(id);
            Update_UI();
        }
    }
}