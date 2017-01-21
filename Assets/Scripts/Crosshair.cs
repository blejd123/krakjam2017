using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crosshair : MonoBehaviour {

    public static Crosshair Instance;

    private Quaternion destRotation = Quaternion.Euler(0, 0, 0);
    private float rotationZ = 0;
    private Vector3 lastMousePos;
    public float speed;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
    }

    public void OnWaveSpawn()
    {
        transform.localScale = Vector3.one;
        transform.DOShakeScale(0.2f, 0.4f);
    }

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diff = Input.mousePosition - lastMousePos;

        if(diff.magnitude < 0.01f)
        {
            return;
        }

        diff.Normalize();
        lastMousePos = Input.mousePosition;

        var rot = Quaternion.identity;
        rot.SetFromToRotation(Vector3.up, diff);
        destRotation = rot;
    }

    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, destRotation, speed);
    }

}
