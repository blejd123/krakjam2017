using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneComeback : MonoBehaviour
{
	public float Delay;
	public Text Text;
	public List<RectTransform> Images;

	void Start ()
	{
		Text.gameObject.SetActive(false);
		foreach (RectTransform rectTransform in Images)
		{
			rectTransform.DOShakePosition(100.0f, 5.0f, 2);
			rectTransform.DOShakeRotation(100.0f, 5.0f, 2);
		}
		StartCoroutine(Animation());
	}

	IEnumerator Animation()
	{
		yield return new WaitForSeconds(Delay);
		Text.gameObject.SetActive(true);
		Text.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		Text.DOFade(1.0f, 0.25f);
	}
}
