using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public static FollowingCamera Instance;
    public float Speed;
	public Transform MinX;
	public Transform MaxX;
	public Transform MinY;
	public Transform MaxY;

	void Awake()
    {
        Instance = this;
    }

	void FixedUpdate ()
    {
        var playerPos = Player.Instance.transform.position;
        playerPos.z = transform.position.z;
		var newPos = transform.position * (1 - Speed) + playerPos * Speed;
		float halfHeight = Camera.main.orthographicSize;
		float halfWidth = halfHeight * Camera.main.aspect;
		transform.position = Clamp(newPos, halfWidth, halfHeight);
    }

	public Vector3 Clamp(Vector3 vec, float halfWidth, float halfHeight)
	{
		if (vec.x - halfWidth < MinX.position.x)
		{
			vec.x = MinX.position.x + halfWidth;
		}
		if (vec.x + halfWidth > MaxX.position.x)
		{
			vec.x = MaxX.position.x - halfWidth;
		}
		if (vec.y - halfHeight < MinY.position.y)
		{
			vec.y = MinY.position.y + halfHeight;
		}
		if (vec.y + halfHeight > MaxY.position.y)
		{
			vec.y = MaxY.position.y - halfHeight;
		}
		return vec;
	}
}
