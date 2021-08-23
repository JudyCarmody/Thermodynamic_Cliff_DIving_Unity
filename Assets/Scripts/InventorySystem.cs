using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour{
    [System.Serializable]
    public class InventoryItem{
        public GameObject itemObj;
        public int stack = 1;
        
        public InventoryItem(GameObject o, int s = 1){
            itemObj = o;
            stack = s;
        }
    }
    
    const int MAXSTACK = 5;
    [Header("General Fields")]
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
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
        if(item.GetComponent<Item>().stackable){  // item IS stackable
            InventoryItem existingItem = inventoryItems.Find(x => x.itemObj.name == item.name); // does this item exist in inventory?
            if(existingItem != null){ // item DOES exist in inventory,
                if(existingItem.stack == MAXSTACK){ return; }
                else{ existingItem.stack++; } // else, if stack not maxed, add one to stack.
            }
            else{   // item DOES NOT exist in inventory, add to inventory
                InventoryItem i = new InventoryItem(item);
                inventoryItems.Add(i);
            }
        }
        else{ // item is NOT stackable, add to inventory
            InventoryItem i = new InventoryItem(item);
            inventoryItems.Add(i);
        }
        /*if(item.GetComponent<Item>().stackable){
            InventoryItem existingItem = inventoryItems.Find(x => x.itemObj.name == item.name);
            if(existingItem != null){ existingItem.stack++; }
            else{
                InventoryItem i = new InventoryItem(item);
                inventoryItems.Add(i);
            }
        }
        else{
            InventoryItem i = new InventoryItem(item);
            inventoryItems.Add(i);            
        }*/
        Update_UI();
    }

    public bool CanPickUp(){
        if(inventoryItems.Count >= items_Images.Length){ return false; }
        else{ return true; }
    }

    void Update_UI(){
        HideAll();
        for(int i = 0; i < inventoryItems.Count; i++){
            items_Images[i].sprite = inventoryItems[i].itemObj.GetComponent<SpriteRenderer>().sprite;
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
        if(inventoryItems[id].stack ==1 ){ description_Name.text = inventoryItems[id].itemObj.name; }
        else{ description_Name.text = inventoryItems[id].itemObj.name + " x" + inventoryItems[id].stack; }
        description_Text.text = inventoryItems[id].itemObj.GetComponent<Item>().descriptionText;

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
        if(inventoryItems[id].itemObj.GetComponent<Item>().itemType == Item.ItemType.Consumable){
            Debug.Log($"CONSUMED {inventoryItems[id].itemObj.name}");
            inventoryItems[id].itemObj.GetComponent<Item>().consumeEvent.Invoke();
            inventoryItems[id].stack--;
            if(inventoryItems[id].stack == 0){
                Destroy(inventoryItems[id].itemObj, 0.1f);
                inventoryItems.RemoveAt(id);
            }
            Update_UI();
        }
    }
}