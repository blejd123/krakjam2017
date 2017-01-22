using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveRaycaster _waveRaycaster;
	[SerializeField] private Wave _wavePrefab;
	[SerializeField] private float _range = 5.0f;
	[SerializeField] private float _speed = 5.0f;

    private float _lastSpawn = -10000.0f;

    private const float COOLDOWN = 0.25f;
    private const float TIME_TO_SPAWN = 1.0f;

	public void Update()
	{
        if (GameState.Instance.state != GameState.State.Playing)
            return;

        if (Input.GetMouseButtonDown(0))
		{
            Crosshair.Instance.OnWaveSpawn();

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist = -ray.origin.z / ray.direction.z;
            var pos = ray.origin + ray.direction * dist;
			pos.z = 0;
			var rays = _waveRaycaster.Raycast(pos, _range);
			var go = Instantiate(_wavePrefab.gameObject);
            go.transform.position = pos;
			var wave = go.GetComponent<Wave>();
            float canStartTime = Mathf.Max(Time.time, _lastSpawn + COOLDOWN);
            _lastSpawn = canStartTime;
            wave.TimeToStart = (canStartTime - Time.time) + TIME_TO_SPAWN;
			wave.Origin = pos;
			wave.Range = _range;
			wave.Speed = _speed;
			wave.Rays = rays;
		}
	}
}
