using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantAttackInfos : MonoBehaviour
{
    public Text attackName;
    public Text attackDamage;
    public Text attackColor;
    public Text attackUnlockLevel;

    public void SetAttackInfos(MutantAttack mutantAttack, int currentLevel)
    {
        attackName.text = mutantAttack.attackName;
        attackDamage.text = mutantAttack.damage + " damages";
        switch (mutantAttack.color) {
            case (MutantAttack.AttackColor.RED):
                attackColor.text = "RED";
                break;
            case (MutantAttack.AttackColor.GREEN):
                attackColor.text = "GREEN";
                break;
            case (MutantAttack.AttackColor.BLUE):
                attackColor.text = "BLUE";
                break;
        }
        //if (!mutantAttack.isUnlocked) {
        if (mutantAttack.unlockLevel < currentLevel) {
            gameObject.GetComponent<Image>().color = Color.gray;
            attackUnlockLevel.gameObject.SetActive(true);
            attackUnlockLevel.text = "Unlocked at level " + mutantAttack.unlockLevel;
        } else if (currentLevel >= mutantAttack.upgradeLevel) {
            attackUnlockLevel.gameObject.SetActive(true);
            attackUnlockLevel.text = "Upgraded at level " + mutantAttack.upgradeLevel;
        }
    }
}
