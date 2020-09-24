using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public GameObject objectsListTemplate;
    public GameObject objectTemplate;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void SetInventoryDisplay()
    {
        if (!playerInventory) {
            playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        }
        GameObject newList = Instantiate(objectsListTemplate);
        newList.transform.SetParent(objectsListTemplate.transform.parent);
        newList.gameObject.SetActive(true);
        InitList(newList, playerInventory.ownedBuildings, ObjectButton.ObjectType.BUILDING);
    }

    public void InitList(GameObject list, List<GameObject> objects, ObjectButton.ObjectType objectType)
    {
        foreach (GameObject playerObject in objects) {
            Debug.Log("name: " + playerObject.gameObject.name + ", child name: " + playerObject.transform.GetChild(0).gameObject.name);
            GameObject newImage = Instantiate(objectTemplate);
            newImage.gameObject.SetActive(true);
            newImage.transform.SetParent(list.transform.GetChild(0).transform);
            
            newImage.GetComponent<Image>().sprite = playerObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

            switch (objectType) {
                case (ObjectButton.ObjectType.BUILDING):
                    Debug.Log("pop");
                    newImage.GetComponent<ObjectButton>().objectType = ObjectButton.ObjectType.BUILDING;
                    break;
                case (ObjectButton.ObjectType.LARVA):
                    newImage.GetComponent<ObjectButton>().objectType = ObjectButton.ObjectType.LARVA;
                    break;
            }
        }
    }

    public void RemoveInventoryDisplay()
    {

    }
}
