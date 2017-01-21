using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour
{

    [SerializeField]
    private Transform _root;

    public float X = 0;
    public float Z = 0;

    // Update is called once per frame
    void Update()
    {
        var camera = GetComponent<Camera>();
        float height = 9.0f;
        float width = camera.aspect * height;
        camera.projectionMatrix = _root.localToWorldMatrix * Matrix4x4.Ortho(-width, width, -height, height, -100, 100);
    }
}