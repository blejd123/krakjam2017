using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public enum State
    {
        Playing,
        GameOver
    }

    public static GameState Instance;

    public State state = State.Playing;

    void Awake()
    {
        Instance = this;
    }

}
