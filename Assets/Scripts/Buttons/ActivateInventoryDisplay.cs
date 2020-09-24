using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateInventoryDisplay : MonoBehaviour
{
    public GameObject inventoryDisplay;
    public bool isOpened = false;
    public GameObject statsGO;
    public Button ShopButton;
    public Button FightButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            if (isOpened) {
                inventoryDisplay.gameObject.SetActive(false);
                statsGO.gameObject.SetActive(true);
                ShopButton.gameObject.SetActive(true);
                FightButton.gameObject.SetActive(true);
                isOpened = false;
                inventoryDisplay.transform.GetChild(0).GetComponent<InventoryDisplay>().RemoveInventoryDisplay();
            }
        }
    }

    public void OnClick()
    {
        if (isOpened) {
            inventoryDisplay.gameObject.SetActive(false);
            statsGO.gameObject.SetActive(true);
            ShopButton.gameObject.SetActive(true);
            FightButton.gameObject.SetActive(true);
            isOpened = false;
            inventoryDisplay.transform.GetChild(0).GetComponent<InventoryDisplay>().RemoveInventoryDisplay();
        } else {
            inventoryDisplay.gameObject.SetActive(true);
            statsGO.gameObject.SetActive(false);
            ShopButton.gameObject.SetActive(false);
            FightButton.gameObject.SetActive(false);
            isOpened = true;
            inventoryDisplay.transform.GetChild(0).GetComponent<InventoryDisplay>().SetInventoryDisplay();
        }
    }
}
