using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    public float Speed;

    Rigidbody2D rigidBody2D;

    void Awake()
    {
        Instance = this;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = Vector2.zero;

        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);

        if (up)
            direction += new Vector2(0, 1);
        if (down)
            direction += new Vector2(0, -1);
        if (left)
            direction += new Vector2(-1, 0);
        if (right)
            direction += new Vector2(1, 0);

        direction.Normalize();

        rigidBody2D.velocity = direction * Speed;
    }

}
