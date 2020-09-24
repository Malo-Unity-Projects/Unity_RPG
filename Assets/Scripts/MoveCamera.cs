using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canMove) {
            Vector3 pos = new Vector3(transform.position.x - (Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime), transform.position.y - (Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime), transform.position.z);
            transform.position = pos;
        }
    }
}
