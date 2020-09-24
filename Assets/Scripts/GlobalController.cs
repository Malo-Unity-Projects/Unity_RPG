using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance;

    public List<Mutant> enemyMutants;
    public int xp;
    public int money;
    public enum GameState { FIRST_LAUNCH, RESTART, MAIN_MENU, SHOP, SELECTING_LEVEL, SELECTING_MUTANTS, FIGHT };
    public GameState state = GameState.FIRST_LAUNCH;
    public bool hasLoaded = true;
    public bool hasWon;
    public List<ArenaManager> arenas;
    public BattleType battleType;
    public int battleNb;
    public int arenaNb;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void SetCompletedBattle()
    {
        if (!hasWon)
            return ;
        arenas[arenaNb].battles[battleNb].isCompleted = true;
        if (battleNb < arenas[arenaNb].battles.Count-1)
            arenas[arenaNb].battles[battleNb+1].isUnlocked = true;
        if (battleNb == arenas[arenaNb].battles.Count-1 && arenaNb < arenas.Count-1) {
            arenas[arenaNb+1].isUnlocked = true;
            arenas[arenaNb+1].battles[0].isUnlocked = true;
        }
    }
}