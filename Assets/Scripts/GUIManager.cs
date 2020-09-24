using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Text nameTxt;
    public Text health;
    //public Text level;
    public Slider hpSlider;
    public Slider shieldSlider;
    public GameObject effectGO;
    public List<GameObject> effectPrefabs;
    public int effectImagePos;
    public int nbEffect;

    public void SetHUD(Mutant mutant)
    {
        nameTxt.text = mutant.name;
        while (nameTxt.text.EndsWith("(Clone)"))
            nameTxt.text = nameTxt.text.Substring(0, nameTxt.text.Length-7);
        health.text = mutant.currentHealth + "/" + mutant.maxHealth;
        //level.text = "Lvl " + mutant.level;
        hpSlider.maxValue = mutant.maxHealth;
        if (mutant.shield > 0) {
            shieldSlider.gameObject.SetActive(true);
            shieldSlider.maxValue = mutant.damage;
            shieldSlider.value = mutant.shield;
        } else {
            shieldSlider.gameObject.SetActive(false);
        }
        if (hpSlider.value < mutant.currentHealth) {
            StartCoroutine(IncreaseHPSlide(mutant.currentHealth - (int)hpSlider.value));
            return ;
        }
        if (hpSlider.value > mutant.currentHealth) {
            StartCoroutine(DecreaseHPSlide((int)hpSlider.value - mutant.currentHealth));
        }
    }

    IEnumerator DecreaseHPSlide(int health)
    {
        int remainingValue = health;

        while (remainingValue > 0) {
            remainingValue -= health/10;
            hpSlider.value -= health/10;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator IncreaseHPSlide(int health)
    {
        int remainingValue = health;

        while (remainingValue != 0) {
            remainingValue -= health/10;
            hpSlider.value += health/10;
            yield return new WaitForSeconds(0.05f);
        }

    }

    public void AddEffectUI(MutantAttack.EffectType effect)
    {
        /* Pour l'instant on ajoute les effets les uns après les autres, mais faut mettre les effets similaires en dessous des effets identiques */
        int pos = 12 + nbEffect*16;
        GameObject instatiatedEffect;

        switch (effect) {
            case (MutantAttack.EffectType.BLEED):
                instatiatedEffect = Instantiate(effectPrefabs[0]);
                instatiatedEffect.transform.SetParent(effectGO.transform);
                instatiatedEffect.transform.position = new Vector3(effectGO.transform.position.x+pos, effectGO.transform.position.y-10, 0);
                break;
            case (MutantAttack.EffectType.BOOST):
                instatiatedEffect = Instantiate(effectPrefabs[1]);
                instatiatedEffect.transform.SetParent(effectGO.transform);
                instatiatedEffect.transform.position = new Vector3(effectGO.transform.position.x+pos, effectGO.transform.position.y-10, 0);
                break;
            case (MutantAttack.EffectType.WEAKEN):
                instatiatedEffect = Instantiate(effectPrefabs[2]);
                instatiatedEffect.transform.SetParent(effectGO.transform);
                instatiatedEffect.transform.position = new Vector3(effectGO.transform.position.x+pos, effectGO.transform.position.y-10, 0);
                break;
        }
        nbEffect++;
    }

    public void UpdateEffects()
    {
        int pos = 12;

        for (int i = 0; i != effectGO.transform.childCount; i++) {
            effectGO.transform.GetChild(i).transform.position = new Vector3(effectGO.transform.position.x+pos, effectGO.transform.position.y-10, effectGO.transform.position.z);
            Debug.Log("effect pos " + pos);
            pos += 16;
        }
    }
}
