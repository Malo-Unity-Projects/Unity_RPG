using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public MoveCamera moveCamera;
    public Building selectedBuilding;

    void Update()
    {

        if (Input.GetMouseButtonUp(0)) {
            Vector3 originalMousePos = Input.mousePosition;
            /* Si jamais je rajoute une option pour zoom, faudra remplacer le -5 par la position de la cam */

            originalMousePos.z = 5;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(originalMousePos);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider) {
                Debug.Log(hit.collider.name);
                switch (hit.transform.tag) {
                    case ("Building"):
                        moveCamera.canMove = false;
                        selectedBuilding = hit.transform.GetComponent<Building>();
                        selectedBuilding.isSelected = true;
                        selectedBuilding.transform.Find("SelectionImage").GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                        break;
                    case ("Ground"):
                        moveCamera.canMove = true;
                        if (selectedBuilding) {
                            selectedBuilding.isSelected = false;
                            selectedBuilding.transform.Find("SelectionImage").GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                        }
                        selectedBuilding = null;
                        break;
                }
            } else {
            }
        }    
    }
}
