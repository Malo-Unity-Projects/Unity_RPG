using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyObject : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public DisplayStats stats;
    //public Mutant mutantToBuy;

    public enum ObjectType { MUTANT, BUILDING };
    public ObjectType objectType;
    public GameObject objectToBuy;

    public Text buttonText;

    public InputField nameInput;
    public Image WriteMutantName;
    
    public int cost;
    private string mutantName = "";

    private bool hasClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        buttonText.text = "Buy for " + cost + " credits";
    }

    void Update()
    {
        if (mutantName.Length != 0) {
            BuyNewMutant();
        }
    }

    void BuyNewMutant()
    {
        string ownedName;

        for (int i = 0; i != playerInventory.ownedMutants.Count; i++) {
            Debug.Log(playerInventory.ownedMutants[i].name);
            ownedName = playerInventory.ownedMutants[i].name;
            while (ownedName.EndsWith("(Clone)")) {
                ownedName = ownedName.Substring(0, ownedName.Length-7);
            }
            if (ownedName == mutantName) {
                mutantName = "";
                return ;
            }
        }
        Mutant mutantToBuy = objectToBuy.GetComponent<Mutant>();
        mutantToBuy.name = mutantName;
        playerInventory.ownedMutants.Add(Instantiate(mutantToBuy));
        playerInventory.ownedMutants[playerInventory.ownedMutants.Count-1].gameObject.SetActive(false);
        playerInventory.ownedMutants[playerInventory.ownedMutants.Count-1].name = mutantName;
        DontDestroyOnLoad(playerInventory.ownedMutants[playerInventory.ownedMutants.Count-1].gameObject);
        playerInventory.money -= cost;
        stats.UpdateStats();
        mutantName = "";
        WriteMutantName.gameObject.SetActive(false);
    }

    void BuyNewBuilding()
    {
        playerInventory.ownedBuildings.Add(Instantiate(objectToBuy));
        playerInventory.ownedBuildings[playerInventory.ownedBuildings.Count-1].gameObject.SetActive(false);
        DontDestroyOnLoad(playerInventory.ownedBuildings[playerInventory.ownedBuildings.Count-1].gameObject);
        playerInventory.money -= cost;
        stats.UpdateStats();
    }

    public void OnClick()
    {
        if (playerInventory.money >= cost) {
            switch (objectType) {
                case (ObjectType.MUTANT):
                    if (!WriteMutantName.gameObject.activeSelf) {
                        hasClicked = true;
                        Debug.Log("cost: " + cost);
                        WriteMutantName.gameObject.SetActive(true);
                        WriteMutantName.transform.GetChild(0).transform.Find("Text").GetComponent<Text>().text = objectToBuy.name;
                    }
                    break;
                case (ObjectType.BUILDING):
                    BuyNewBuilding();
                    break;
            }
        } else if (WriteMutantName.gameObject.activeSelf) {
            Debug.Log("stop");
            hasClicked = false;
            WriteMutantName.gameObject.SetActive(false);
        }
    }

    public void GetString(string str)
    {
        objectToBuy.name = str;
        nameInput.text = "";
        Debug.Log("str: " + str);
        mutantName = str;
    }
}
