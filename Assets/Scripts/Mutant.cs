using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mutant : MonoBehaviour
{
    public string mutantName;
    public string description;
    public int mutationLevel;
    public int level = 1;
    public int maxXp = 100;
    public int currentXp = 0;
    public float speed;
    public float currentSpeed;
    public int maxHealth;
    public int currentHealth;
    public int shield = 0;
    public int damage;
    public int currentDamage;
    public int levelUpDamagePercentage;
    public int levelUpUpgradedDamagePercentage;
    public int damageUpgradeLevel;

    public List<MutantAttack> attacks;
    public Dictionary<int, KeyValuePair<MutantAttack.EffectType, int>> appliedEffects;
    public int nbEffects;
    public MutantAttack.AttackColor color1;
    public MutantAttack.AttackColor color2;
    
    [System.Serializable]
    public struct Effect
    {
        public MutantAttack.EffectType effectType;
        public int percentage;
        public int upgradedPercentage;
        public bool isUnlocked;
        public int unlockLevel;
        public bool isUpgraded;
        public int upgradeLevel;
    }
    public List<Effect> effects;

    public bool isSelected;

    public Sprite sprite;

    void Start()
    {
        foreach (MutantAttack attack in attacks) {
            attack.damage = damage;
        }
        currentHealth = maxHealth;
        currentSpeed = speed;
        appliedEffects = new Dictionary<int, KeyValuePair<MutantAttack.EffectType, int>>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalController.Instance.state == GlobalController.GameState.RESTART) {
            Destroy(gameObject);
        }
    }

    public int TakeDamage(int damage)
    {
        int damageToTake = damage;

        if (shield - damageToTake >= 0) {
            shield -= damageToTake;
            return (0);
        }
        damageToTake -= shield;
        shield = 0;
        currentHealth -= damageToTake;
        if (currentHealth < 0) {
            currentHealth = 0;
        }
        if (currentHealth == 0) {
            gameObject.SetActive(false);
        }
        return (damageToTake);
    }

    public void LevelUp()
    {
        level++;
        maxHealth += maxHealth/20;
        currentHealth = maxHealth;
        //damage += damage/20;
        if (level == damageUpgradeLevel) {
            damage += damage/levelUpUpgradedDamagePercentage;
        } else {
            damage += damage/levelUpDamagePercentage;
        }
        currentXp -= maxXp;
        maxXp += (maxXp/10);

        foreach (MutantAttack attack in attacks) {
            /*
            if (level == attack.unlockLevel && mutationLevel >= attack.mutationUnlockLevel) {
                attack.isUnlocked = true;
            } else if (level == attack.upgradeLevel) {
                attack.isUpgraded = true;
            }
            */
            if (level >= attack.upgradeLevel) {
                attack.damage += attack.damage/levelUpUpgradedDamagePercentage;
            } else {
                attack.damage += attack.damage/levelUpDamagePercentage;
            }
        }
        Effect effect;
        for (int i = 0; i != effects.Count; i++) {
            if (effects[i].upgradeLevel == level) {
                effect = effects[i];
                effect.isUpgraded = true;
                effects[i] = effect;
            } else if (effects[i].unlockLevel == level) {
                effect = effects[i];
                effect.isUnlocked = true;
                effects[i] = effect;
            }
        }
    }

    public void AddEffect(MutantAttack.EffectType effect, int effectValue)
    {
        /* Gestion si l'effet est boost */
        if (effect == MutantAttack.EffectType.BOOST) {
            int nbBoostApplied = 0;

            foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in appliedEffects) {
                if (pair.Value.Key == MutantAttack.EffectType.BOOST)
                    nbBoostApplied++;
            }
            if (nbBoostApplied >= 2) {
                return ;
            } else {
                appliedEffects[nbEffects] = new KeyValuePair<MutantAttack.EffectType, int>(effect, effectValue);
                nbEffects++;
                return ;
            }
        }
        /* Fin de la gestion de l'effet boost */

        /* Pour l'effet Weaken, faut pas remplacer l'effet s'il est déja la, faut ajouter la valeur à l'effet déja présent */
        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in appliedEffects) {
            if (pair.Value.Key == effect) {
                if (effect == MutantAttack.EffectType.WEAKEN) {
                    int newEffectValue = effectValue + pair.Value.Value;
                    RemoveEffect(pair.Key);
                    AddEffect(effect, newEffectValue);
                    return ;
                } else if (effect != MutantAttack.EffectType.BOOST && pair.Value.Value < effectValue) {
                    /* Retirer la pair de la liste et la remplacer avec la nouvelle valeur */
                    RemoveEffect(pair.Key);
                    AddEffect(effect, effectValue);
                    return ;
                } else if (pair.Value.Key == effect && effect != MutantAttack.EffectType.BOOST && effect != MutantAttack.EffectType.WEAKEN && pair.Value.Value >= effectValue) {
                    return ;
                }
            }
        }
        appliedEffects[nbEffects] = new KeyValuePair<MutantAttack.EffectType, int>(effect, effectValue);
        nbEffects++;
    }

    public bool CheckIfEffetApplied(MutantAttack.EffectType effect)
    {
        int nbBoosts = 0;

        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in appliedEffects) {
            if (pair.Value.Key == MutantAttack.EffectType.BOOST)
                nbBoosts++;
            if (pair.Value.Key == effect && effect == MutantAttack.EffectType.BOOST && nbBoosts > 1) {
                return (true);
            }
            else if (pair.Value.Key == effect && effect != MutantAttack.EffectType.BOOST) {
                return (true);
            }
        }
        return (false);
    }

    public void RemoveEffect(int key)
    {
        appliedEffects.Remove(key);
        //nbEffects--;
    }
}