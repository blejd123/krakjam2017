﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wave : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _marker;

    public AudioSource placeAudioSource;
    public AudioSource explodeAudioSource;

	public Vector2 Origin;
	public float Range;
	public float Speed;
	public float[] Rays;
    public float TimeToStart;

    private float _currentRange = 0.0f;
    private float maxTimeToStart;
    private bool _detonated = false;

	public void Start()
	{
        placeAudioSource.pitch = 1.0f + Random.Range(-0.2f, 0.2f);
        explodeAudioSource.pitch = 1.0f + Random.Range(-0.2f, 0.2f);

        placeAudioSource.Play();

        maxTimeToStart = TimeToStart;

        _spriteRenderer.enabled = false;

        transform.localScale = Vector3.one * (2.0f * _spriteRenderer.sprite.pixelsPerUnit / _spriteRenderer.sprite.texture.width * Range);
		Texture2D tex = new Texture2D(Constants.RAY_COUNT, 1, TextureFormat.ARGB32, false);
		Color32[] pixels = new Color32[Constants.RAY_COUNT];
		for (int i = 0; i < Constants.RAY_COUNT; i++)
		{
			float r = Rays[i];
			pixels[i] = r < 0.0f ? RangeToColor32(Range) : RangeToColor32(r);
		}
		tex.SetPixels32(pixels);
		tex.Apply();
		_spriteRenderer.material.SetTexture("_RaysTex", tex);
		_spriteRenderer.material.SetFloat("_MaxRange", Range);

        _marker.transform.localScale = Vector3.zero;
    }

	public void Update()
	{
        TimeToStart -= Time.deltaTime;
        float markerParentScale = transform.lossyScale.x;

        if (TimeToStart > 0)
        {
            float speed = 4*Mathf.PI / maxTimeToStart;

            float markerScale = 0.8f + Mathf.Sin(TimeToStart * speed + Mathf.PI) * 0.2f;
            _marker.transform.localScale = new Vector3(markerScale/ markerParentScale, markerScale/ markerParentScale, 1);
            return;
        } else if(!_detonated)
        {
            explodeAudioSource.Play();
            _detonated = true;
            const float easeDuration = 0.4f;
            _marker.transform.DOScale(1.0f / markerParentScale * 1.3f, easeDuration).SetEase(Ease.OutSine);
            _marker.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), easeDuration).SetEase(Ease.OutSine);

            float fadeOutDuration = 0.3f;
            float totalAnimTime = Range / Speed;
            _spriteRenderer.DOColor(new Color(1, 1, 1, 0), fadeOutDuration).SetDelay(totalAnimTime - fadeOutDuration).SetEase(Ease.OutSine);
        }

        _spriteRenderer.enabled = true;
        _spriteRenderer.material.SetFloat("_CurrentRange", _currentRange);
		_currentRange += Speed * Time.deltaTime;

		RaycastHit2D hit = Physics2D.Raycast(Origin, Player.Instance.Position - Origin, _currentRange * 0.7f);

        if (hit.collider != null && hit.collider.attachedRigidbody != null && hit.collider.attachedRigidbody.gameObject == Player.Instance.gameObject)
		{
			Player.Instance.OnWaveCollision();
		}

		if (_currentRange >= Range)
		{
			Destroy(gameObject);
		}
	}

	private Color32 RangeToColor32(float range)
	{
		return new Color32((byte) range, (byte) ((range - (int) range)*255), 0, 0);
	}
}
