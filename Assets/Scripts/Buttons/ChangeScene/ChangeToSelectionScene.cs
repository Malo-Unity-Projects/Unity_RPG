using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToSelectionScene : MonoBehaviour
{
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        foreach (Mutant mutant in playerInventory.ownedMutants) {
            mutant.isSelected = false;
        }
        playerInventory.selectedMutants.Clear();
    }

    public void OnClick()
    {
        SceneManager.LoadScene("SelectionBattle", LoadSceneMode.Single);
    }
}
