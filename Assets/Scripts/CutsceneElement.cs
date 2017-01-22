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
	public  GameObject Root;

	[SerializeField] private float _delay;
	[SerializeField] private float _duration;
	[SerializeField] private Image _image;
	[SerializeField] private RectTransform _endTransform;
	[SerializeField] private UnityEvent _onComplete;
	[SerializeField] private float _fadeDuration;
	[SerializeField] private AudioClip _sound;
	[SerializeField] private float _soundDelay;

	public IEnumerator Play()
	{
		_image.gameObject.SetActive(false);
		yield return new WaitForSeconds(_delay);
		_image.gameObject.SetActive(true);
		_image.DOKill();
		if (_fadeDuration > 0.0f)
		{
			_image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			_image.DOFade(1.0f, _fadeDuration);			
		}
		
		if (_endTransform != null)
		{
			if (_fadeDuration > 0.0f)
			{
				_image.DOFade(0.0f, _fadeDuration).SetDelay(_duration - _fadeDuration);
			}
			
			RectTransform tr = _image.rectTransform;
			tr.DOKill();
			Sequence sequence = DOTween.Sequence();
			sequence.Append(tr.DOMove(_endTransform.position, _duration));
			sequence.Join(tr.DOScale(_endTransform.lossyScale, _duration));
			yield return sequence.Join(tr.DORotate(_endTransform.rotation.eulerAngles, _duration)).WaitForCompletion();
		}
		else
		{
			if (_fadeDuration > 0.0f)
			{
				yield return _image.DOFade(0.0f, _fadeDuration).SetDelay(_duration - _fadeDuration).WaitForCompletion();
			}
		}
	}

	public IEnumerator PlayAudio(AudioSource audioSource)
	{
		if (_sound != null)
		{
			yield return new WaitForSeconds(_soundDelay);
			audioSource.loop = false;
			audioSource.clip = _sound;
			audioSource.Play();
		}
	}
}
