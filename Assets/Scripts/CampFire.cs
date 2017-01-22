using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour {

    public GameObject particles;

	void Start () {
        particles.SetActive(Random.RandomRange(0, 4) == 0);
	}

}
