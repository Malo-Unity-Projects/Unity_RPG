using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenasManager : MonoBehaviour
{
    public Button previousArena;
    public Button nextArena;
    public List<ArenaManager> arenas;
    int arenaIndex = 0;
    int battleIndex = 0;
    public Text arenaName;

    void Start()
    {
        /*  Désactiver tous les boutons sauf le premier (enfin juste pour la première arène,
            faut pas pouvoir accéder au premier combat de chaque arène) */
        /* Peut être désactiver tous les boutons, puis réactiver le tout premier */
        /* Sachant que cette méthode doit être rappelée à chaque fois qu'on revient dans la scène */
        /* Donc faudra récupérer toutes les infos du globalcontroller à chaque fois */
        arenaName.text = arenas[0].arenaName;
        for (int i = 0; i != arenas.Count; i++) {
            if (i != 0) {
                arenas[i].gameObject.SetActive(false);
            }
        }
        if (GlobalController.Instance.arenas.Count == 0) {
            foreach (ArenaManager arena in arenas) {
                foreach (BattlePreps battlePreps in arena.battles) {
                    battlePreps.GetComponent<Button>().interactable = false;
                }
                arena.gameObject.SetActive(false);
            }
            arenas[0].gameObject.SetActive(true);
            arenas[0].battles[0].GetComponent<Button>().interactable = true;
            arenas[0].isUnlocked = true;
            arenas[0].battles[0].isUnlocked = true;
            foreach (ArenaManager arena in arenas) {
                GlobalController.Instance.arenas.Add(Instantiate(arena));
            }
            foreach (ArenaManager arena in GlobalController.Instance.arenas) {
                DontDestroyOnLoad(arena.gameObject);
                arena.gameObject.SetActive(false);
            }
        } else {
            for (int arena = 0; arena != GlobalController.Instance.arenas.Count; arena++) {
                arenas[arena].isUnlocked = GlobalController.Instance.arenas[arena].isUnlocked;
                arenas[arena].isCompleted = GlobalController.Instance.arenas[arena].isCompleted;
            
                for (int i = 0; i != GlobalController.Instance.arenas[arena].battles.Count; i++) {
                    arenas[arena].battles[i].isUnlocked = GlobalController.Instance.arenas[arena].battles[i].isUnlocked;
                    arenas[arena].battles[i].isCompleted = GlobalController.Instance.arenas[arena].battles[i].isCompleted;
                    arenas[arena].battles[i].GetComponent<Button>().interactable = arenas[arena].battles[i].isUnlocked;
                }
            }
            //arenas = GlobalController.Instance.arenas;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseArenaIndex()
    {
        if (arenaIndex == arenas.Count-1)
            return ;
        arenas[arenaIndex].gameObject.SetActive(false);
        arenaIndex++;
        arenaName.text = arenas[arenaIndex].arenaName;
        arenas[arenaIndex].gameObject.SetActive(true);
        GlobalController.Instance.arenaNb = arenaIndex;
    }

    public void DecreaseArenaIndex()
    {
        if (arenaIndex == 0)
            return ;
        arenas[arenaIndex].gameObject.SetActive(false);
        arenaIndex--;
        arenaName.text = arenas[arenaIndex].arenaName;
        arenas[arenaIndex].gameObject.SetActive(true);
        GlobalController.Instance.arenaNb = arenaIndex; 
    }

    public void CompleteArena()
    {
        arenas[arenaIndex].battles[battleIndex].isCompleted = true;
        if (battleIndex == arenas[arenaIndex].battles.Count-1) {
            arenas[arenaIndex+1].isUnlocked = true;
            arenas[arenaIndex+1].battles[0].GetComponent<Button>().interactable = true;
        }
    }
}