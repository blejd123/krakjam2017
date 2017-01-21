using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WaveRaycaster : MonoBehaviour
{
	[SerializeField] private float _debugDrawDuration = 2.0f;

	public float[] Raycast(Vector2 origin, float maxDistance)
	{
		Debug.Log(origin);
		float[] hits = new float[Constants.RAY_COUNT];
		for (int i = 0; i < Constants.RAY_COUNT; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(origin, Quaternion.Euler(0.0f, 0.0f, i)*Vector2.up, maxDistance);
			if(hit.collider != null)
			{
				hits[i] = hit.distance;
			}
			else
			{
				hits[i] = -1.0f;
			}
		}

		for (int i = 0; i < Constants.RAY_COUNT; i++)
		{
			Vector3 dir = Quaternion.Euler(0.0f, 0.0f, i)*Vector2.up;
			if (hits[i] >= 0.0f)
			{
				Debug.DrawLine(origin, new Vector3(origin.x, origin.y, 0.0f) + dir * hits[i], Color.red, _debugDrawDuration);
			}
			else
			{
				Debug.DrawRay(origin, dir * maxDistance, Color.green, _debugDrawDuration);
			}
		}
		return hits;
	}
}
