using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveRaycaster _waveRaycaster;
	[SerializeField] private Wave _wavePrefab;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			float range = 10.0f;
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0.0f;
			var rays = _waveRaycaster.Raycast(pos, range);
			var go = Instantiate(_wavePrefab.gameObject);
			go.transform.position = pos;
			go.transform.localScale = Vector3.one * (2.0f * range);
			var wave = go.GetComponent<Wave>();
			wave.Origin = pos;
			wave.Range = range;
			wave.Rays = rays;
		}
	}
}
