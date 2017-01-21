using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    private Quaternion destRotation = Quaternion.Euler(0, 0, 0);
    private float rotationZ = 0;
    private Vector3 lastMousePos;
    public float speed;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diff = transform.position - lastMousePos;

        if(diff.magnitude < 0.01f)
        {
            return;
        }

        diff.Normalize();
        lastMousePos = transform.position;

        var rot = Quaternion.identity;
        rot.SetFromToRotation(Vector3.up, diff);
        destRotation = rot;
        
    }

    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, destRotation, speed);
    }

}
