using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantLevelCenter : MonoBehaviour
{
    public Image main;
    public Text costText;
    public Text levelText;
    public int cost = 2000;
    public int level = 10;

    void Start()
    {
        UpdateDisplay();
    }

    void Update()
    {
        Vector3 levelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        main.transform.position = levelPos;
    }

    public void UpdateDisplay()
    {
        costText.text = "Cost: " + cost;
        levelText.text = "Level " + level;
    }
}