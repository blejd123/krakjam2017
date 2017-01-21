using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
	[SerializeField] private string _onCompleteScene;
	[SerializeField] private bool _playOnStart;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private List<CutsceneElement> _elements = new List<CutsceneElement>();

	public void Start()
	{
		if (_playOnStart)
		{
			Play();
		}
	}

	public void Play()
	{
		foreach (CutsceneElement cutsceneElement in _elements)
		{
			cutsceneElement.Root.SetActive(false);
		}
		StartCoroutine(PlayCoroutine());
	}

	private IEnumerator PlayCoroutine()
	{
		foreach (CutsceneElement cutsceneElement in _elements)
		{
			cutsceneElement.Root.SetActive(true);
			StartCoroutine(cutsceneElement.PlayAudio(_audioSource));
			yield return cutsceneElement.Play();
			cutsceneElement.Root.SetActive(false);
		}
		AppFlow.Instance.LoadScene(_onCompleteScene);
	}
}
