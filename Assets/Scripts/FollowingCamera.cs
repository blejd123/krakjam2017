using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public static FollowingCamera Instance;
    public float Speed;

    void Awake()
    {
        Instance = this;
    }

	void FixedUpdate ()
    {
        var playerPos = Player.Instance.transform.position;
        playerPos.z = transform.position.z;
        transform.position = transform.position * (1 - Speed) + playerPos * Speed;
	}

}
