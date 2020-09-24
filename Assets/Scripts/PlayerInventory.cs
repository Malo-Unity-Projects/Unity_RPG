using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> ownedBuildings;
    public List<Mutant> ownedMutants;
    public List<Mutant> selectedMutants;
    private SelectionController selectionController;
    private BattleSystem battleSystem;
    public MutantController mutantController;
    private DisplayStats displayStats;

    private Text levelText;
    private Text moneyText;

    public int level;
    public int currentXp;
    public int maxMutantLevel = 10;
    public int maxXp = 1000;
    public int money;

    private bool hasDisplay = false;

    void Start()
    {
        if (GlobalController.Instance.state == GlobalController.GameState.FIRST_LAUNCH) {
            mutantController = GameObject.Find("MutantController").GetComponent<MutantController>();
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(mutantController);
        } else {
            Destroy(gameObject);
        }
        if (GlobalController.Instance.state == GlobalController.GameState.SELECTING_MUTANTS) {
            selectionController = GameObject.Find("InventoryMenu").GetComponent<SelectionController>();
            selectionController.StartCreation();
        }
        if (GlobalController.Instance.state == GlobalController.GameState.FIGHT) {
            battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
            battleSystem.SetupBattle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalController.Instance.state == GlobalController.GameState.RESTART) {
            foreach (Mutant mutant in ownedMutants) {
                mutant.isSelected = false;
            }
            foreach (Mutant selectedMutant in selectedMutants) {
                foreach (Mutant mutant in ownedMutants) {
                    if (mutant.name == selectedMutant.name) {
                        mutant.currentXp += GlobalController.Instance.xp;
                        while (mutant.currentXp > mutant.maxXp) {
                            if (mutant.currentXp >= mutant.maxXp && mutant.level < maxMutantLevel) {
                                mutant.LevelUp();
                            } else if (mutant.currentXp > mutant.maxXp && mutant.level >= maxMutantLevel) {
                                mutant.currentXp = mutant.maxXp;
                            }
                        }
                    }
                }
            }
            money += GlobalController.Instance.money;
            currentXp += GlobalController.Instance.xp;
            GlobalController.Instance.money = 0;
            GlobalController.Instance.xp = 0;
            for (int i = GlobalController.Instance.enemyMutants.Count; i != 0; i--) {
                Mutant m = GlobalController.Instance.enemyMutants[0];
                GlobalController.Instance.enemyMutants.Remove(GlobalController.Instance.enemyMutants[0]);
                Destroy(m.gameObject);
            }
            selectedMutants.Clear();

            GlobalController.Instance.state = GlobalController.GameState.MAIN_MENU;
            GlobalController.Instance.SetCompletedBattle();
            hasDisplay = true;
        }
        if (!hasDisplay && (GlobalController.Instance.state == GlobalController.GameState.MAIN_MENU || GlobalController.Instance.state == GlobalController.GameState.SHOP)) {
            displayStats = GameObject.Find("Stats").GetComponent<DisplayStats>();
            displayStats.UpdateStats();
            hasDisplay = true;   
        }
    }

    public void SetOwnedMutants()
    {
        ownedMutants = new List<Mutant>();
        ownedMutants.Add(Instantiate(mutantController.mutants[0]));
        ownedMutants.Add(Instantiate(mutantController.mutants[1]));
        ownedMutants.Add(Instantiate(mutantController.mutants[2]));
        ownedMutants.Add(Instantiate(mutantController.mutants[6]));
        foreach(Mutant mutant in ownedMutants) {
            mutant.gameObject.SetActive(false);
            DontDestroyOnLoad(mutant.gameObject);
        }
        ownedBuildings = new List<GameObject>();
    }

    public void LevelUp()
    {
        level++;
        currentXp -= maxXp;
        maxXp += (maxXp/10);
    }
}
