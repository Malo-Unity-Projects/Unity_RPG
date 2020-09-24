using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    public Text levelText;
    public Text moneyText;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        UpdateStats();
    }

    public void UpdateStats()
    {
        levelText.text = "Level " + playerInventory.level;
        moneyText.text = playerInventory.money + " credits";
    }
}
