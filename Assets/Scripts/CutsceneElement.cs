using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class CutsceneElement
{
	[SerializeField] private float _delay;
	[SerializeField] private float _duration;
	[SerializeField] private Image _image;
	[SerializeField] private RectTransform _endTransform;
	[SerializeField] private UnityEvent _onComplete;
	[SerializeField] private float _fadeDuration;

	public IEnumerator Play()
	{
		yield return new WaitForSeconds(_delay);
		_image.DOKill();
		if (_fadeDuration > 0.0f)
		{
			_image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			_image.DOFade(1.0f, _fadeDuration);
			_image.DOFade(0.0f, _fadeDuration).SetDelay(_duration - _fadeDuration);
		}
		RectTransform tr = _image.rectTransform;
		tr.DOKill();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(tr.DOMove(_endTransform.position, _duration));
		sequence.Join(tr.DOScale(_endTransform.lossyScale, _duration));
		yield return sequence.Join(tr.DORotate(_endTransform.rotation.eulerAngles, _duration)).WaitForCompletion();
	}
}
