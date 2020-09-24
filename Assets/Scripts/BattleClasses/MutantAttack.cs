using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAttack : MonoBehaviour
{
    public enum EffectType { NONE, BLEED, HEAL, SHIELD, BOOST, WEAKEN, RETALIATE };
    public enum AttackColor { BLUE, RED, GREEN };

    public string attackName;
    public int unlockLevel;
    public int mutationUnlockLevel;
    //public bool isUnlocked;
    public int upgradeLevel;
    //public bool isUpgraded;
    public AttackColor color;
    public int damage;
    public float animationTime;

    public MutantAttack(AttackColor newColor/*, EffectType newSelfEffect, int newSelfEffectValue*/)
    {
        color = newColor;
        //selfEffect = newSelfEffect;
        //selfEffectValue = newSelfEffectValue;
    }

    /*
    public MutantAttack(AttackColor newColor, EffectType newSelfEffect, int newSelfEffectValue, EffectType newEnemyEffect, int newEnemyEffectValue)
    {
        color = newColor;
        selfEffect = newSelfEffect;
        selfEffectValue = newSelfEffectValue;
        enemyEffect = newEnemyEffect;
        enemyEffectValue = newEnemyEffectValue;
    }
    */
}