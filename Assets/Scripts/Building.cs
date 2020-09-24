using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isSelected = false;
    public enum BuildingType { GENERATOR, HOUSE, LEVELCENTER, INCUBATOR };
    public BuildingType buildingType;
    private Vector3 mOffset;

    void OnMouseDown()
    {
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 5;

        return (Camera.main.ScreenToWorldPoint(mousePoint));
    }


    void OnMouseDrag()
    {
        Debug.Log("dragging");
        if (isSelected) {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }
}
