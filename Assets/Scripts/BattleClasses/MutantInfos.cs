using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantInfos : MonoBehaviour
{    
    public BattleState state;
    public Mutant mutant;
    public int pos;
    public Text nameText;
    public Text healthText;
    public Text levelText;
    public Image mutantSprite;

    public MutantInfos(BattleState initState, Mutant initMutant, int initPos)
    {
        state = initState;
        mutant = initMutant;
        pos = initPos;
    }

    public void SetIcon(Mutant mutantRef)
    {
        nameText.text = mutantRef.name;
        levelText.text = "Level " + mutantRef.level;
        healthText.text = mutantRef.currentHealth + " hp";
        mutantSprite.sprite = mutantRef.sprite;
        mutant = mutantRef;
    }
}
