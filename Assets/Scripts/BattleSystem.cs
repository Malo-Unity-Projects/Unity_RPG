using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public enum BattleType { NORMAL, LARGE, BOSS}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public Dictionary<int, MutantInfos> turns;

    public List<MutantComps> playerComps;
    public List<MutantComps> enemyComps;

    private PlayerInventory playerInventory;
    public GameObject[] spawnPoints;
    public GameObject[] enemySpawnPoints;

    public List<SelectMutantForAttack> selectables;

    int actions = 0;

    private float speedCounter;
    private float maxSpeed = 0;
    private float minSpeed = 0;

    public List<GUIManager> playerGUI;
    public List<GUIManager> enemyGUI;
    public List<AttackButton> attackButtons;
    public int attackNb = 0;

    int xp;
    public RawImage endImage;
    public Text endText;

    private bool nextTurn = false;
    UnityEvent turnEvent;

    void Start()
    {
        if (turnEvent == null)
            turnEvent = new UnityEvent();
        turnEvent.AddListener(ManageTurns);
        nextTurn = true;
    }

    void Update()
    {
        if (nextTurn && turnEvent != null) {
            nextTurn = false;
            turnEvent.Invoke();
        }
    }

    public void ManageTurns()
    {
        if (CheckPlayerHP() == 0) {
            GameOver();
            return ;
        }
        if (CheckEnemyHP() == 0) {
            Win();
            return ;
        }
        while (turns[actions].mutant.currentHealth == 0) {
            Debug.Log("mutant is dead, skipping turn");
            turns.Remove(actions);
            actions++;
            if (turns.Count == 5) {
                InitTurns(actions);
            }
        }
        if (turns[actions].state == BattleState.PLAYERTURN) {
            StartCoroutine(PlayerTurn(turns[actions]));
        } else if (turns[actions].state == BattleState.ENEMYTURN) {
            StartCoroutine(EnemyTurn(turns[actions]));
        }
    }
    public void SetupBattle()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        state = BattleState.START;
        foreach (SelectMutantForAttack select in selectables) {
            select.gameObject.SetActive(false);
        }
        xp = GlobalController.Instance.xp;

        playerComps = new List<MutantComps>();
        enemyComps = new List<MutantComps>();

        if (GlobalController.Instance.battleType == BattleType.NORMAL) {

            /*
            GameObject comp = new GameObject();
            comp.AddComponent<MutantComps>();
            comp.GetComponent<MutantComps>().mutant = playerInventory.selectedMutants[0];
            comp.GetComponent<MutantComps>().spawnPoint = spawnPoints[0];
            comp.GetComponent<MutantComps>().GUIElement = playerGUI[0];
            */

            for (int i = 0; i != 3; i++) {
                GameObject comp = new GameObject();
                comp.AddComponent<MutantComps>();
                comp.GetComponent<MutantComps>().InitPlayerMutant(playerInventory.selectedMutants[i], spawnPoints[i], playerGUI[i]);

                //MutantComps comp = new MutantComps(playerInventory.selectedMutants[i], spawnPoints[i], playerGUI[i]);
                playerComps.Add(comp.GetComponent<MutantComps>());

                //comp = new MutantComps(GlobalController.Instance.enemyMutants[i], enemySpawnPoints[i], enemyGUI[i], selectables[i], 0);
                comp = new GameObject();
                comp.AddComponent<MutantComps>();
                comp.GetComponent<MutantComps>().InitEnemyMutant(GlobalController.Instance.enemyMutants[i], enemySpawnPoints[i], enemyGUI[i], selectables[i], 0);
                enemyComps.Add(comp.GetComponent<MutantComps>());
            }

            /*
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[0], spawnPoints[0], playerGUI[0]));
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[1], spawnPoints[1], playerGUI[1]));
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[2], spawnPoints[2], playerGUI[2]));

            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[0], enemySpawnPoints[0], enemyGUI[0], selectables[0], 0));
            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[1], enemySpawnPoints[1], enemyGUI[1], selectables[1], 1));
            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[2], enemySpawnPoints[2], enemyGUI[2], selectables[2], 2));
            */

        } else if (GlobalController.Instance.battleType == BattleType.LARGE) {

            /*
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[0], spawnPoints[0], playerGUI[0]));
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[1], spawnPoints[1], playerGUI[1]));
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[2], spawnPoints[2], playerGUI[2]));

            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[0], enemySpawnPoints[0], enemyGUI[0], selectables[0], 0));
            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[1], enemySpawnPoints[1], enemyGUI[1], selectables[1], 1));
            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[2], enemySpawnPoints[2], enemyGUI[2], selectables[2], 2));
            */

        } else if (GlobalController.Instance.battleType == BattleType.BOSS) {
            
            /*
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[0], spawnPoints[0], playerGUI[0]));
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[1], spawnPoints[1], playerGUI[1]));
            playerComps.Add(new MutantComps(playerInventory.selectedMutants[2], spawnPoints[2], playerGUI[2]));
            */

            for (int i = 0; i != 3; i++) {
                MutantComps comp = new MutantComps(playerInventory.selectedMutants[i], spawnPoints[i], playerGUI[i]);
                playerComps.Add(comp);
            }
            enemyComps.Add(new MutantComps(GlobalController.Instance.enemyMutants[0], enemySpawnPoints[1], enemyGUI[1], selectables[1], 1));
        }

        InitSpeeds();
        turns = new Dictionary<int, MutantInfos>();
        InitTurns(actions);
    }

    public int CheckPlayerHP()
    {
        int playerHealth = 0;

        foreach (MutantComps comps in playerComps) {
            playerHealth += comps.mutant.currentHealth;
        }
        return (playerHealth);
    }

    public int CheckEnemyHP()
    {
        int enemyHealth = 0;

        foreach (MutantComps comps in enemyComps) {
            enemyHealth += comps.mutant.currentHealth;
        }
        return (enemyHealth);
    }

    IEnumerator PlayerTurn(MutantInfos infos)
    {
        yield return new WaitForSeconds(.5f);
        infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("Move");
        yield return new WaitForSeconds(1);
        for (int i = 0; i != infos.mutant.attacks.Count; i++) {
            if (infos.mutant.level >= infos.mutant.attacks[i].unlockLevel)
            //if (infos.mutant.attacks[i].isUnlocked)
                attackButtons[i].gameObject.SetActive(true);
        }

        /* Avant de faire le TakeDamage, faut diminuer avec la faiblesse, et mettre les boosts */
        infos.mutant.currentDamage = infos.mutant.damage;
        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in infos.mutant.appliedEffects) {
            if (pair.Value.Key == MutantAttack.EffectType.WEAKEN) {
                infos.mutant.currentDamage -= pair.Value.Value;
            } else if (pair.Value.Key == MutantAttack.EffectType.BOOST) {
                infos.mutant.currentDamage += pair.Value.Value;
            }
        }
        /* Le current damage a été modifié en fonction des effets du mutant, on peut appliquer les dégats */

        /* Set les affichages ici parce que pour utiliser les valeurs mises à jour ca fonctionne pas */
        for (int i = 0; i != infos.mutant.attacks.Count; i+=2) {
            if (i >= infos.mutant.attacks.Count)
                break;
            attackButtons[i].SetButton(infos.mutant.attacks[i], infos.mutant.currentDamage);
            if (i != infos.mutant.attacks.Count-1)
                attackButtons[i+1].SetButton(infos.mutant.attacks[i+1], infos.mutant.currentDamage);
        }
        while (!isOneButtonClicked()) {
            yield return null;
        }
        foreach (MutantComps comps in enemyComps) {
            if (comps.mutant.currentHealth > 0) {
                comps.selectable.gameObject.SetActive(true);
            }
        }
        foreach (MutantComps comps in enemyComps) {
            if (comps.mutant.currentHealth > 0) {
                comps.selectable.gameObject.SetActive(true);
            }
        }
        while (!IsOneEnemySelected()) {
            yield return null;
        }

        foreach (MutantComps comps in enemyComps) {
            if (comps.selectable.isSelected) {
                if (comps.position == infos.pos-1) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackUpward");
                } else if (comps.position == infos.pos-2) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackUpper");
                } else if (comps.position == infos.pos) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackForward");
                } else if (comps.position == infos.pos+2) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackLower");
                } else {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackDownward");
                }
                yield return new WaitForSeconds(.7f);

                switch (attackNb) {
                    case (0):
                        infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("Attack1_1");
                        break;
                    case (1):
                        infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("Attack2_1");
                        break;
                    case (2):
                        break;
                    case (3):
                        break;
                }

                yield return new WaitForSeconds(infos.mutant.attacks[attackNb].animationTime);
                //yield return new WaitForSeconds(3f);

                if (comps.position == infos.pos-1) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackUpwardBack");
                } else if (comps.position == infos.pos-2) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackUpperBack");
                } else if (comps.position == infos.pos) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackForwardBack");
                } else if (comps.position == infos.pos+2) {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackLowerBack");
                } else {
                    infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("AttackDownwardBack");
                }

                yield return new WaitForSeconds(1f);

                ManageEffects(infos.mutant, comps.mutant, playerGUI[infos.pos], comps.GUIElement);
                RemoveConsumableEffects(infos.mutant, playerGUI[infos.pos]);
                playerGUI[infos.pos].UpdateEffects();

                if (comps.mutant.currentHealth == 0)
                    comps.GUIElement.gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(1.5f);
        turns.Remove(actions);
        actions++;
        if (turns.Count == 5) {
            InitTurns(actions);
        }
        for (int i = 0; i != infos.mutant.attacks.Count; i++) {
            attackButtons[i].wait = true;
            attackButtons[i].gameObject.SetActive(false);
        }

        foreach(MutantComps comps in enemyComps) {
            comps.selectable.isSelected = false;
            comps.selectable.gameObject.SetActive(false);
        }
        infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("MoveBack");
        yield return new WaitForSeconds(.5f);
        nextTurn = true;
    }

    IEnumerator EnemyTurn(MutantInfos infos)
    {
        KeyValuePair<int, Mutant> smallestMutantHealth = new KeyValuePair<int, Mutant>(0, playerComps[0].mutant);

        attackNb = 0;
        infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("Move");
        yield return new WaitForSeconds(1f);

        for (int i = 1; i != playerComps.Count; i++) {
            if (smallestMutantHealth.Value.currentHealth == 0) {
                smallestMutantHealth = new KeyValuePair<int, Mutant>(i, playerComps[i].mutant);
                attackNb = i;
            }
            if (playerComps[i].mutant.currentHealth < smallestMutantHealth.Value.currentHealth && playerComps[i].mutant.currentHealth > 0) {
                smallestMutantHealth = new KeyValuePair<int, Mutant>(i, playerComps[i].mutant);
                attackNb = i;
            }
        }

        infos.mutant.currentDamage = infos.mutant.damage;
        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in infos.mutant.appliedEffects) {
            if (pair.Value.Key == MutantAttack.EffectType.WEAKEN) {
                infos.mutant.currentDamage -= pair.Value.Value;
            } else if (pair.Value.Key == MutantAttack.EffectType.BOOST) {
                infos.mutant.currentDamage += pair.Value.Value;
            }
        }
        if (infos.mutant.currentDamage < 0)
            infos.mutant.currentDamage = 0;

        /* Jouer l'animation */
        yield return new WaitForSeconds(1f);
        ManageEffects(infos.mutant, smallestMutantHealth.Value, enemyGUI[infos.pos], playerGUI[smallestMutantHealth.Key]);
        RemoveConsumableEffects(infos.mutant, enemyGUI[infos.pos]);
        enemyGUI[infos.pos].UpdateEffects();

        turns.Remove(actions);
        actions++;
        if (turns.Count == 5) {
            InitTurns(actions);
        }
        infos.mutant.transform.GetChild(0).GetComponent<Animator>().Play("MoveBack");
        yield return new WaitForSeconds(1f);
        nextTurn = true;
    }

    bool IsOneEnemySelected()
    {
        foreach (MutantComps comps in enemyComps) {
            if (comps.selectable.isSelected)
                return (true);
        }
        return (false);
    }

    bool isOneButtonClicked()
    {
        for (int i = 0; i != attackButtons.Count; i++) {
            if (!attackButtons[i].wait) {
                attackNb = i;
                return (true);
            }
        }
        return (false);
    }

    void DecreaseChildrenPos(List<int> childrenPos)
    {
        for (int i = 0; i != childrenPos.Count; i++) {
            childrenPos[i]--;
        }
    }

    void RemoveUIEffect(GameObject effectsGO, List<int> childrenPos)
    {
        int max = effectsGO.transform.childCount;

        for (int i = 0; i != max; i++) {
            for (int pos = 0; pos != childrenPos.Count; pos++) {
                if (i == childrenPos[pos]) {
                    Transform child = effectsGO.transform.GetChild(i);
                    child.SetParent(null);
                    Destroy(child.gameObject);
                    childrenPos.Remove(childrenPos[pos]);
                    DecreaseChildrenPos(childrenPos);
                    return ;
                }
            }
        }
    }

    void RemoveConsumableEffects(Mutant mutant, GUIManager mutantGUI)
    {
        List<int> consumableIds = new List<int>();
        List<int> childrenPos = new List<int>();
        GameObject effectsGO = mutantGUI.transform.GetChild(5).gameObject;

        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in mutant.appliedEffects) {
            if (pair.Value.Key == MutantAttack.EffectType.BOOST || pair.Value.Key == MutantAttack.EffectType.WEAKEN) {
                consumableIds.Add(pair.Key);
            }
        }
        foreach (int key in consumableIds) {
            mutant.RemoveEffect(key);
        }
        for (int i = 0; i != effectsGO.transform.childCount; i++) {
            if (effectsGO.transform.GetChild(i).name == "BoostPrefab(Clone)" || effectsGO.transform.GetChild(i).name == "WeakenPrefab(Clone)") {
                childrenPos.Add(i);
            }
        }
        if (childrenPos.Count == 0)
            return ;
        /* un while ca boucle inf (jsp pourquoi) */
        while (childrenPos.Count != 0)
            RemoveUIEffect(effectsGO, childrenPos);
    }

    void ManageEffects(Mutant playerMutant, Mutant enemyMutant, GUIManager playerGUI, GUIManager enemyGUI)
    {
        int damageToDo = playerMutant.attacks[0].damage;
        int damagesDone;

        /* Avant de faire le TakeDamage, faut diminuer avec la faiblesse, et mettre les boosts */
        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in playerMutant.appliedEffects) {
            if (pair.Value.Key == MutantAttack.EffectType.WEAKEN) {
                damageToDo -= pair.Value.Value;
            } else if (pair.Value.Key == MutantAttack.EffectType.BOOST) {
                damageToDo += pair.Value.Value;
            }
        }
        /* Le current damage a été modifié en fonction des effets du mutant, on peut appliquer les dégats */
        damagesDone = enemyMutant.TakeDamage(damageToDo);
        enemyGUI.SetHUD(enemyMutant);

        /* Appliquer effets du mutant */
        foreach (Mutant.Effect effect in playerMutant.effects) {
            float percentage;
            
            if (effect.isUpgraded) {
                percentage = effect.upgradedPercentage;
            } else {
                percentage = effect.percentage;
            }
            percentage /= 100;
            percentage *= damagesDone;

            if (!effect.isUnlocked)
                continue ;
            //Effets appliqués à l'ennemi
            if (effect.effectType == MutantAttack.EffectType.WEAKEN || effect.effectType == MutantAttack.EffectType.BLEED) {
                if (effect.effectType == MutantAttack.EffectType.WEAKEN) {
                    enemyGUI.AddEffectUI(effect.effectType);
                } else {
                    if (!enemyMutant.CheckIfEffetApplied(effect.effectType)) {
                        enemyGUI.AddEffectUI(effect.effectType);
                    }
                }
                enemyMutant.AddEffect(effect.effectType, (int)percentage);
            } else {
                //Effets appliqués au mutant
                switch (effect.effectType) {
                    case (MutantAttack.EffectType.BOOST):
                        if (!playerMutant.CheckIfEffetApplied(effect.effectType)) {
                            float boostValue = effect.percentage;
                            boostValue /= 100;
                            boostValue *= playerMutant.damage;
                            playerMutant.AddEffect(effect.effectType, (int)boostValue);
                            playerGUI.AddEffectUI(effect.effectType);
                        }
                        break;
                    case (MutantAttack.EffectType.HEAL):
                        playerMutant.currentHealth += (int)percentage;
                        if (playerMutant.currentHealth > playerMutant.maxHealth)
                            playerMutant.currentHealth = playerMutant.maxHealth;
                        break;
                    case (MutantAttack.EffectType.SHIELD):
                        if (playerMutant.shield < percentage) {
                            playerMutant.shield = (int)percentage;
                            playerGUI.SetHUD(playerMutant);
                        }
                        break;
                }
            }
        }

        /* Appliquer effets de l'ennemi */
        foreach (Mutant.Effect effect in enemyMutant.effects) {
            if (!effect.isUnlocked)
                continue ;
            float percentage;
            
            if (effect.isUpgraded) {
                percentage = effect.upgradedPercentage;
            } else {
                percentage = effect.percentage;
            }
            percentage /= 100;
            percentage *= enemyMutant.damage;
            
            if (effect.effectType == MutantAttack.EffectType.RETALIATE) {
                playerMutant.TakeDamage((int)percentage);
                playerGUI.SetHUD(playerMutant);
            } else if (effect.effectType == MutantAttack.EffectType.BOOST) {
                if (!enemyMutant.CheckIfEffetApplied(effect.effectType)) {

                    enemyMutant.AddEffect(effect.effectType, (int)percentage);
                    enemyGUI.AddEffectUI(effect.effectType);
                }
            }
        }
        foreach (KeyValuePair<int, KeyValuePair<MutantAttack.EffectType, int>> pair in playerMutant.appliedEffects) {
            if (pair.Value.Key == MutantAttack.EffectType.BLEED) {
                playerMutant.TakeDamage(pair.Value.Value);
                playerGUI.SetHUD(playerMutant);
            }
        }
    }

    void InitSpeeds()
    {
        foreach (MutantComps comps in playerComps) {
            comps.mutant.currentSpeed = comps.mutant.speed;
            if (comps.mutant.speed > maxSpeed && comps.mutant.currentHealth > 0) {
                maxSpeed = comps.mutant.speed;
            }
        }
        foreach (MutantComps comps in enemyComps) {
            comps.mutant.currentSpeed = comps.mutant.speed;
            if (comps.mutant.speed > maxSpeed && comps.mutant.currentHealth > 0) {
                maxSpeed = comps.mutant.speed;
            }
        }
        minSpeed = maxSpeed;
        foreach (MutantComps comps in playerComps) {
            if (comps.mutant.speed < minSpeed && comps.mutant.currentHealth > 0) {
                minSpeed = comps.mutant.speed;
            }
        }
        foreach (MutantComps comps in enemyComps) {
            if (comps.mutant.speed < minSpeed && comps.mutant.currentHealth > 0) {
                minSpeed = comps.mutant.speed;
            }
        }
        speedCounter = maxSpeed;
    }

    void InitTurns(int actions)
    {
        int i = actions;
        int max = actions + 25;

        if (turns.Count != 0) {
            i += 5;
            max += 5;
        }
        while (i < max) {
            for (int pos = 0; pos != playerComps.Count; pos++) {
                if (playerComps[pos].mutant.currentSpeed == speedCounter) {
                    turns.Add(i, new MutantInfos(BattleState.PLAYERTURN, playerComps[pos].mutant, pos));
                    playerComps[pos].mutant.currentSpeed -= minSpeed;
                    i++;
                }
            }
            for (int pos = 0; pos != enemyComps.Count; pos++) {
                if (enemyComps[pos].mutant.currentSpeed == speedCounter) {
                    turns.Add(i, new MutantInfos(BattleState.ENEMYTURN, enemyComps[pos].mutant, pos));
                    enemyComps[pos].mutant.currentSpeed -= minSpeed;
                    i++;
                }
            }
            speedCounter -= 1;
            if (speedCounter <= minSpeed) {
                foreach(MutantComps comps in playerComps) {
                    comps.mutant.currentSpeed += maxSpeed;
                }
                foreach(MutantComps comps in enemyComps) {
                    comps.mutant.currentSpeed += maxSpeed;
                }
                speedCounter += maxSpeed;
            }
        }
    }

    void Win()
    {
        endImage.gameObject.SetActive(true);
        foreach (AttackButton attackButton in attackButtons) {
            attackButton.gameObject.SetActive(false);
        }
        foreach (MutantComps comps in playerComps) {
            comps.GUIElement.gameObject.SetActive(false);
        }
        foreach (MutantComps comps in enemyComps) {
            comps.GUIElement.gameObject.SetActive(false);
        }
        GlobalController.Instance.hasWon = true;
        endText.text = "Victory!";
    }

    void GameOver()
    {
        endImage.gameObject.SetActive(true);
        foreach (AttackButton attackButton in attackButtons) {
            attackButton.gameObject.SetActive(false);
        }
        foreach (MutantComps comps in playerComps) {
            comps.GUIElement.gameObject.SetActive(false);
        }
        foreach (MutantComps comps in enemyComps) {
            comps.GUIElement.gameObject.SetActive(false);
        }
        GlobalController.Instance.hasWon = false;
        endText.text = "Defeat!";
    }
}
