using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppFlow : MonoBehaviour
{
	const float FADE_DURATION = 0.5f;

	public static AppFlow Instance;

	[SerializeField] private Image _fadeOverlay;
	[SerializeField] private string _introScene;
	[SerializeField] private string _mainMenuScene;
	[SerializeField] private string _gameplayScene;
	[SerializeField] private string _gameOverScene;


	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		GoToIntro();
	}

	public void GoToIntro()
	{
		LoadScene(_introScene);
	}

	public void GoToMainMenu()
	{
		LoadScene(_mainMenuScene);
	}
	
	public void GoToGameplay()
	{
		LoadScene(_gameplayScene);
	}

	public void GoToGameOver()
	{
		LoadScene(_gameOverScene);
	}
	
	public void LoadScene(string scene)
	{
		FadeIn(() =>
		{
			SceneManager.LoadScene(scene);
			FadeOut();
		});
	}

	private void FadeOut()
	{
		_fadeOverlay.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		_fadeOverlay.DOKill();
		_fadeOverlay.DOFade(0.0f, FADE_DURATION);
	}

	private void FadeIn(Action onComplete)
	{
		_fadeOverlay.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		_fadeOverlay.DOKill();
		_fadeOverlay.DOFade(1.0f, FADE_DURATION).OnComplete(() =>
		{
			onComplete();
		});
	}
}
