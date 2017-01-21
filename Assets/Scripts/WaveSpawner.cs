using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveRaycaster _waveRaycaster;
	[SerializeField] private Wave _wavePrefab;
	[SerializeField] private float _range = 5.0f;
	[SerializeField] private float _speed = 5.0f;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0.0f;
			var rays = _waveRaycaster.Raycast(pos, _range);
			var go = Instantiate(_wavePrefab.gameObject);
			go.transform.position = pos;
			var wave = go.GetComponent<Wave>();
			wave.Origin = pos;
			wave.Range = _range;
			wave.Speed = _speed;
			wave.Rays = rays;
		}
	}
}
