using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;

	public Vector3 Origin;
	public float Range;
	public float[] Rays;

	public void Start()
	{
		Texture2D tex = new Texture2D(Constants.RAY_COUNT, 1, TextureFormat.ARGB32, false);
		Color32[] pixels = new Color32[Constants.RAY_COUNT];
		for (int i = 0; i < Constants.RAY_COUNT; i++)
		{
			float r = Rays[i];
			pixels[i] = r < 0.0f ? new Color32(255, 0, 0, 0) : new Color32((byte)(r / 255), (byte)((r - (int)r) * 255), 0, 0);
		}
		tex.SetPixels32(pixels);
		tex.Apply();
		_spriteRenderer.material.SetVector("_Origin", Origin);
		_spriteRenderer.material.SetFloat("_Range", Range);
		_spriteRenderer.material.SetTexture("_RaysTex", tex);
	}
}
