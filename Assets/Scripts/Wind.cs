using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    private float randomValue;

    void Start()
    {
        randomValue = Random.Range(0, 128.0f);
    }

	void Update () {
        float noise = Mathf.PerlinNoise(Time.time, randomValue) * 2 - 1;
        transform.localRotation = Quaternion.Euler(0, 0, noise*5);
	}

}
