using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuSelection : MonoBehaviour
{
    public GameObject menu;
    public List<GameObject> otherMenus;

    public void OnClick()
    {
        if (!menu.activeInHierarchy) {
            menu.gameObject.SetActive(true);
            foreach (GameObject GO in otherMenus) {
                GO.gameObject.SetActive(false);
            }
        }
    }
}
