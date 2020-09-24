using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMutantInfos : MonoBehaviour
{
    public Text nameText;
    public Text healthText;
    public Text levelText;
    public Text descriptionText;
    public Text speedText;
    public Text damageText;
    public Image mutantImage;
    public Image LevelCenterImage;
    public GameObject attackInfosTemplate;
    public GameObject skillInfosTemplate;

    public void InitMutantInfos(Mutant mutantRef)
    {
        nameText.text = mutantRef.name;
        while (nameText.text.EndsWith("(Clone)")) {
            nameText.text = nameText.text.Substring(0, nameText.text.Length-7);
        }
        healthText.text = mutantRef.currentHealth + " hp";
        levelText.text = "Level " + mutantRef.level;
        descriptionText.text  = mutantRef.description;
        damageText.text = mutantRef.damage + " damages";
        speedText.text = mutantRef.speed + " speed";
        mutantImage.sprite = mutantRef.sprite;
        LevelCenterImage.gameObject.SetActive(false);
        foreach (MutantAttack attack in mutantRef.attacks) {
            GameObject newAttackInfos = Instantiate(attackInfosTemplate);
            newAttackInfos.SetActive(true);
            newAttackInfos.transform.SetParent(attackInfosTemplate.transform.parent);
            newAttackInfos.GetComponent<MutantAttackInfos>().SetAttackInfos(attack, mutantRef.level);
        }
        foreach (Mutant.Effect effect in mutantRef.effects) {
            GameObject newEffectInfos = Instantiate(skillInfosTemplate);
            newEffectInfos.SetActive(true);
            newEffectInfos.transform.SetParent(skillInfosTemplate.transform.parent);
            switch (effect.effectType) {
                case (MutantAttack.EffectType.BLEED):
                    break;
            }
            newEffectInfos.transform.GetChild(1).GetComponent<Text>().text = effect.percentage + "%";
            if (effect.isUnlocked && !effect.isUpgraded) {
                newEffectInfos.transform.GetChild(2).GetComponent<Text>().text = "Upgraded at level " + effect.upgradeLevel;
            } else if (!effect.isUnlocked) {
                newEffectInfos.transform.GetChild(2).GetComponent<Text>().text = "Unlocked at level " + effect.unlockLevel;
                newEffectInfos.GetComponent<Image>().color = Color.grey;
            } else {
                newEffectInfos.gameObject.SetActive(false);
            }
        }
    }
}
