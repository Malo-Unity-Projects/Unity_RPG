using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxMutantLevel : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private DisplayStats displayStats;
    public MutantLevelCenter mutantLevelCenter;

    public void OnClick()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        if (playerInventory.money >= mutantLevelCenter.cost) {
            playerInventory.money -= mutantLevelCenter.cost;
            mutantLevelCenter.level++;
            mutantLevelCenter.cost += mutantLevelCenter.cost/10;
            playerInventory.maxMutantLevel++;
            foreach (Mutant mutant in playerInventory.ownedMutants) {
                if (mutant.currentXp == mutant.maxXp) {
                    mutant.LevelUp();
                }
            }
            mutantLevelCenter.UpdateDisplay();
            GameObject.Find("Stats").GetComponent<DisplayStats>().UpdateStats();
        }
    }
}
