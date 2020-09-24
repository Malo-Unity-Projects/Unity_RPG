using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMutant : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private Mutant mutant;
    private MutantButton mutantButton;
    public Button fightButton;
    public Text nameText;
    public Text healthText;
    public Text levelText;
    public Image mutantImage;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        for (int i = 0; i != playerInventory.selectedMutants.Count; i++) {
            if (playerInventory.selectedMutants[i] == mutant) {
                playerInventory.selectedMutants.Remove(mutant);
                mutantButton.ResetButton();
                fightButton.gameObject.SetActive(false);
                Destroy(gameObject);
                break ;
            }
        }
    }

    public void SetSelected(Mutant newMutant, MutantButton newMutantButton)
    {
        mutant = newMutant;
        mutantButton = newMutantButton;
        nameText.text = mutant.name;
        while (nameText.text.EndsWith("(Clone)"))
            nameText.text = nameText.text.Substring(0, nameText.text.Length-7);
        healthText.text = mutant.currentHealth + " hp";
        levelText.text = "Level " + mutant.level;
        mutantImage.sprite = newMutant.sprite;
    }
}
