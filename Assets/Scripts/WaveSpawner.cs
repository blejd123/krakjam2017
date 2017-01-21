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
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist = -ray.origin.z / ray.direction.z;
            var pos = ray.origin + ray.direction * dist;
			var rays = _waveRaycaster.Raycast(pos, _range);
			var go = Instantiate(_wavePrefab.gameObject);
            pos.z = 0;
			go.transform.position = pos;
			var wave = go.GetComponent<Wave>();
			wave.Origin = pos;
			wave.Range = _range;
			wave.Speed = _speed;
			wave.Rays = rays;
		}
	}
}
