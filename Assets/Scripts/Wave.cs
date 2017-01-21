using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wave : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;

	public Vector3 Origin;
	public float Range;
	public float Speed;
	public float[] Rays;

	public void Start()
	{
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
		StartCoroutine(AnimateRange());
		//_spriteRenderer.material.SetFloat("_Range", Range);
	}

	private Color32 RangeToColor32(float range)
	{
		return new Color32((byte) range, (byte) ((range - (int) range)*255), 0, 0);
	}

	private IEnumerator AnimateRange()
	{
		_spriteRenderer.material.SetFloat("_MaxRange", Range);
		float range = 0.0f;
		while (range <= Range)
		{
			_spriteRenderer.material.SetFloat("_CurrentRange", range);
			range += Speed*Time.deltaTime;
			yield return null;
		}
	}
}
