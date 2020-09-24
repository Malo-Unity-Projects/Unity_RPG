using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateMutantsDisplay : MonoBehaviour
{
    public Image MutantsDisplay;
    public Image LevelCenterImage;
    public Button ShopButton;
    public Button FightButton;
    private bool isOpened = false;
    public SelectionController selectionController;
    public DisplayMutantInfos displayMutantInfos;
    
    public GameObject skillsGameObject;
    public GameObject attacksGameObject;

    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            if (isOpened) {
                if (displayMutantInfos.gameObject.activeInHierarchy) {
                    displayMutantInfos.gameObject.SetActive(false);
                } else {
                    MutantsDisplay.gameObject.SetActive(false);
                    LevelCenterImage.gameObject.SetActive(true);
                    ShopButton.gameObject.SetActive(true);
                    FightButton.gameObject.SetActive(true);
                    isOpened = false;
                    selectionController.ResetButtons();
                }
            }
        }
    }

    public void OnClick()
    {
        if (isOpened) {
            MutantsDisplay.gameObject.SetActive(false);
            displayMutantInfos.gameObject.SetActive(false);
            LevelCenterImage.gameObject.SetActive(true);
            ShopButton.gameObject.SetActive(true);
            FightButton.gameObject.SetActive(true);
            isOpened = false;
            selectionController.ResetButtons();
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
        } else {
            MutantsDisplay.gameObject.SetActive(true);
            LevelCenterImage.gameObject.SetActive(false);
            ShopButton.gameObject.SetActive(false);
            FightButton.gameObject.SetActive(false);
            isOpened = true;
            selectionController.StartCreation();
        }
    }
}
