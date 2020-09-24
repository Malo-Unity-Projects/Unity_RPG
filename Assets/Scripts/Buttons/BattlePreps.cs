using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlePreps : MonoBehaviour
{
    public List<Mutant> enemyMutants;
    public GameObject infosTemplate;
    public int xp;
    public int money;
    public int level;
    public BattleType battleType;
    public int battleNb;
    public int arenaNb;
    public bool isUnlocked = false;
    public bool isCompleted = false;

    void Start()
    {
        /*
        float posX = infosTemplate.transform.position.x;
        float posY = infosTemplate.transform.position.y;
        float posZ = infosTemplate.transform.position.z;
        */

        foreach (Mutant mutant in enemyMutants) {
            GameObject newInfos = Instantiate(infosTemplate);
            //GO.transform.position = new Vector3(posX, posY, posZ);
            newInfos.SetActive(true);
            newInfos.GetComponent<MutantInfos>().SetIcon(mutant);
            newInfos.transform.SetParent(infosTemplate.transform.parent, false);
            /*
            if (posX >= infosTemplate.transform.position.x+90) {
                posX = infosTemplate.transform.position.x;
                posY += 50;
            }
            */
        }
    }

    public void SaveValues()
    {
        GlobalController.Instance.xp = xp;
        GlobalController.Instance.money = money;
        GlobalController.Instance.battleType = battleType;
        foreach (Mutant mutant in enemyMutants) {
            GlobalController.Instance.enemyMutants.Add(Instantiate(mutant));
        }
        foreach(Mutant mutant in GlobalController.Instance.enemyMutants) {
            for (int i = 0; i != level; i++)
                mutant.LevelUp();
            mutant.currentXp = 0;
            mutant.gameObject.SetActive(false);
            DontDestroyOnLoad(mutant.gameObject);
        }
    }

    public void OnClick()
    {
        SaveValues();
        GlobalController.Instance.state = GlobalController.GameState.SELECTING_MUTANTS;
        GlobalController.Instance.battleType = battleType;
        GlobalController.Instance.battleNb = battleNb;
        GlobalController.Instance.arenaNb = arenaNb;
        SceneManager.LoadScene("SelectionScene", LoadSceneMode.Single);
    }
}
