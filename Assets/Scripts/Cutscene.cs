using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
	[SerializeField] private string _onCompleteScene;
	[SerializeField] private bool _playOnStart;
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
		StartCoroutine(PlayCoroutine());
	}

	private IEnumerator PlayCoroutine()
	{
		foreach (CutsceneElement cutsceneElement in _elements)
		{
			yield return cutsceneElement.Play();
			AppFlow.Instance.LoadScene(_onCompleteScene);
		}
	}
}
