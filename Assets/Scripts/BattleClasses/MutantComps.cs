using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantComps : MonoBehaviour
{
    public Mutant mutant;
    public GameObject spawnPoint;
    public GUIManager GUIElement;
    public SelectMutantForAttack selectable;
    public int position;

    public MutantComps()
    {}

    public MutantComps(Mutant initMutant, GameObject initSpawnPoint, GUIManager initGUIElement)
    {          
        spawnPoint = initSpawnPoint;
        GUIElement = initGUIElement;

        mutant = Instantiate(initMutant);
        mutant.transform.position = spawnPoint.transform.position;
        mutant.gameObject.SetActive(true);
        GUIElement.SetHUD(mutant);
        GUIElement.gameObject.SetActive(true);
    }

    public void InitPlayerMutant(Mutant initMutant, GameObject initSpawnPoint, GUIManager initGUIElement)
    {
        spawnPoint = initSpawnPoint;
        GUIElement = initGUIElement;

        mutant = Instantiate(initMutant);
        mutant.transform.position = spawnPoint.transform.position;
        mutant.gameObject.SetActive(true);
        GUIElement.SetHUD(mutant);
        GUIElement.gameObject.SetActive(true);
    }

    public MutantComps(Mutant newMutant, GameObject newSpawnPoint, GUIManager newGUIElement, SelectMutantForAttack newSelectable, int newPos)
    {
        spawnPoint = newSpawnPoint;
        GUIElement = newGUIElement;
        selectable = newSelectable;
        position = newPos;

        mutant = Instantiate(newMutant);
        mutant.transform.position = spawnPoint.transform.position;
        mutant.transform.rotation = new Quaternion(0, -180, 0, 0);
        mutant.gameObject.SetActive(true);
        GUIElement.SetHUD(mutant);
        GUIElement.gameObject.SetActive(true);
    }

    public void InitEnemyMutant(Mutant newMutant, GameObject newSpawnPoint, GUIManager newGUIElement, SelectMutantForAttack newSelectable, int newPos)
    {
        spawnPoint = newSpawnPoint;
        GUIElement = newGUIElement;
        selectable = newSelectable;
        position = newPos;

        mutant = Instantiate(newMutant);
        mutant.transform.position = spawnPoint.transform.position;
        mutant.transform.rotation = new Quaternion(0, -180, 0, 0);
        mutant.gameObject.SetActive(true);
        GUIElement.SetHUD(mutant);
        GUIElement.gameObject.SetActive(true);
    }
}
