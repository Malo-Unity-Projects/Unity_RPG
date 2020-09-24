using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    private List<Mutant> mutants;
    private PlayerInventory playerInventory;

    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup group;
    public int constraintNb;

    public void StartCreation()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        mutants = new List<Mutant>();

        foreach(Mutant mutant in playerInventory.ownedMutants) {
            mutants.Add(mutant);
        }
        GenInventory();
    }

    void GenInventory()
    {
        group.constraintCount = constraintNb;

        foreach(Mutant mutant in mutants) {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);

            if (newButton.GetComponent<MutantButton>() != null) {
                newButton.GetComponent<MutantButton>().SetIcon(mutant.sprite, mutant);
            } else if (newButton.GetComponent<MutantInfos>() != null) {
                newButton.GetComponent<MutantInfos>().SetIcon(mutant);
            }
            newButton.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    public void ResetButtons()
    {
        foreach(Transform child in group.transform) {
            if (child.name == "MutantInfo(Clone)") {
                Destroy(child.gameObject);
            }
        }
    }
}