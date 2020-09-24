using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateMutantInfosDisplay : MonoBehaviour
{
    public Image MutantInfosDisplay;
    public Image MutantInfoImage;
    public GameObject skillsGameObject;
    public GameObject attacksGameObject;

    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            MutantInfosDisplay.gameObject.SetActive(false);
            foreach (Transform child in attacksGameObject.transform) {
                if (child.name.EndsWith("(Clone)")) {
                    Destroy(child.gameObject);
                }
            }
            foreach (Transform child in skillsGameObject.transform) {
                if (child.name.EndsWith("(Clone)")) {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void EnableMutantInfosDisplay()
    {
        MutantInfosDisplay.gameObject.SetActive(true);
        MutantInfosDisplay.GetComponent<DisplayMutantInfos>().InitMutantInfos(MutantInfoImage.GetComponent<MutantInfos>().mutant);
    }

    public void DisableMutantInfosDisplay()
    {
        MutantInfosDisplay.gameObject.SetActive(false);
        foreach (Transform child in attacksGameObject.transform) {
            if (child.name.EndsWith("(Clone)")) {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform child in skillsGameObject.transform) {
            if (child.name.EndsWith("(Clone)")) {
                Destroy(child.gameObject);
            }
        }
    }
}
