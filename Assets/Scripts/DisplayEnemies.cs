using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEnemies : MonoBehaviour
{
    private GlobalController globalController;
    public GameObject enemyInfosTemplate;

    // Start is called before the first frame update
    void Start()
    {
        globalController = GameObject.Find("GlobalController").GetComponent<GlobalController>();   

        foreach (Mutant mutant in globalController.enemyMutants) {
            GameObject newInfos = Instantiate(enemyInfosTemplate);
            newInfos.SetActive(true);
            newInfos.GetComponent<MutantInfos>().SetIcon(mutant);

            newInfos.transform.SetParent(enemyInfosTemplate.transform.parent, false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
