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
	[SerializeField] private string _mainMenuScene;
	[SerializeField] private string _introScene;
	[SerializeField] private string _gameplayScene;
	[SerializeField] private string _gameOverScene;


	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		GoToMainMenu();
	}

	public void GoToMainMenu()
	{
		FadeIn();
		SceneManager.LoadScene(_mainMenuScene);
		FadeOut();
	}

	public void GoToIntro()
	{
		FadeIn();
		SceneManager.LoadScene(_introScene);
		FadeOut();
	}

	public void GoToGameplay()
	{
		FadeIn();
		SceneManager.LoadScene(_gameplayScene);
		FadeOut();
	}

	public void GoToGameOver()
	{
		FadeIn();
		SceneManager.LoadScene(_gameOverScene);
		FadeOut();
	}

	private void FadeOut()
	{
		_fadeOverlay.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		_fadeOverlay.DOKill();
		_fadeOverlay.DOFade(0.0f, FADE_DURATION);
	}

	private void FadeIn()
	{
		_fadeOverlay.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		_fadeOverlay.DOKill();
		_fadeOverlay.DOFade(1.0f, FADE_DURATION);
	}
}
