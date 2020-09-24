using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
    public List<Mutant> mutantsPrefabs;
    public List<Mutant> mutants;
    private PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Mutant mutant in mutantsPrefabs) {
            mutants.Add(Instantiate(mutant));
        }
        foreach(Mutant mutant in mutants) {
            mutant.gameObject.SetActive(false);
            //DontDestroyOnLoad(mutant.gameObject);
        }
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        if (GlobalController.Instance.state == GlobalController.GameState.FIRST_LAUNCH) {
            //playerInventory.mutantController = GameObject.Find("MutantController").GetComponent<MutantController>();
            playerInventory.SetOwnedMutants();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
