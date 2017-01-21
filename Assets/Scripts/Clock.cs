using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Clock : MonoBehaviour {

    const float TIME_TO_PULSE = 5.0f;
    const float ROUND_TIME = 30.0f;
    private float _timeToGameOver = ROUND_TIME;
    public Text text;
    public Image background;
    public Image foreground;
    private bool gameOver = false;

    void Update () {
        _timeToGameOver -= Time.deltaTime;
        float amount = Mathf.Max(_timeToGameOver, 0);
        foreground.fillAmount = amount / ROUND_TIME;
        background.color = Color.Lerp(Color.red, Color.green, foreground.fillAmount);
        text.text = Mathf.RoundToInt(amount).ToString();

        if(_timeToGameOver < TIME_TO_PULSE && _timeToGameOver > 0)
        {
            float animTime = TIME_TO_PULSE - _timeToGameOver;
            float scale = 1.0f+Mathf.Abs(Mathf.Sin(animTime * Mathf.PI * 2))/6.0f;
            transform.localScale = new Vector3(scale, scale, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }

        if(!gameOver && _timeToGameOver <= 0)
        {
            gameOver = true;
            // TODO: walking player won
            transform.DOShakePosition(3.0f, 5.0f, 20);
        }
    }
}
