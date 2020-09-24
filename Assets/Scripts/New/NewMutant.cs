using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMutant : MonoBehaviour
{
    public int level;
    int currentXp;
    int maxXp;
    int currentViolence;
    int maxViolence;
    /* efficacité dans une arène */
    int brutality;
    /* boost global dans l'arène, qui augmente à chaque fois que le mutant y va */
    int followers;

    int currentDNA;
    int maxDNA;
    /* efficacité dans la chambre génétique */
    int geneticEnhancement;
    /* boost global dans la chambre génétique, qui augmente à chaque fois que le mutant y va */
    int geneticAfinity;

    // Start is called before the first frame update
    void Start()
    {
        currentDNA = maxDNA;
        currentViolence = maxViolence;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
