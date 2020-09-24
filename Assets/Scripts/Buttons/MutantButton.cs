using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantButton : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    public Mutant mutant;
    private PlayerInventory playerInventory;
    public AddMutantToList adder;
    public Button fightButton;
    public Text nameText;
    public Text healhText;
    public Text levelText;

    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        GetComponent<Image>().color = Color.grey;
    }

    public void SetIcon(Sprite sprite, Mutant mutantRef)
    {
        icon.sprite = sprite;
        mutant = mutantRef;
        mutant.isSelected = false;
        levelText.text = "Level " + mutant.level.ToString();
        healhText.text = mutant.currentHealth + " hp";
        nameText.text = mutant.name;
        while (nameText.text.EndsWith("(Clone)"))
            nameText.text = nameText.text.Substring(0, nameText.text.Length-7);
    }

    public void ResetButton()
    {
        GetComponent<Image>().color = Color.grey;
        mutant.isSelected = false;
    }

    public void OnClick()
    {
        /*
        if (mutant.isSelected) {
            playerInventory.selectedMutants.Remove(mutant);
            mutant.isSelected = false;
            return ;
        }
        */
        if (!playerInventory)
            Debug.Log("No player Inv");
        if (!mutant)
            Debug.Log("No mutant");
        if (playerInventory.selectedMutants.Count == 3 || mutant.isSelected)
            return ;
        mutant.isSelected = true;
        playerInventory.selectedMutants.Add(mutant);
        if (playerInventory.selectedMutants.Count == 3) {
            fightButton.gameObject.SetActive(true);
        } else if (playerInventory.selectedMutants.Count != 3 && fightButton.gameObject.activeSelf) {
            fightButton.gameObject.SetActive(false);
        }
        GetComponent<Image>().color = Color.red;
        adder.AddElement(mutant, this);
        if (mutant.isSelected) {
            /* Faire un Reset et supprimer de la list de mutants sélectionnés du joueur et supprimer le bouton de la deuxième liste */            
        }
    }
}
