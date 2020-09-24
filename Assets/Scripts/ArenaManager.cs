using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour
{
    public List<BattlePreps> battles;
    public bool isCompleted = false;
    public bool isUnlocked = false;
    public string arenaName;    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockArena()
    {
        isUnlocked = true;
        battles[0].GetComponent<Button>().interactable = true;
    }
}
