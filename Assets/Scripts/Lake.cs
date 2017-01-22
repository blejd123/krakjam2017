using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lake : MonoBehaviour {

    public GameObject particles;

	void Start () {
        particles.SetActive(Random.RandomRange(0, 3) == 0);
	}

}
