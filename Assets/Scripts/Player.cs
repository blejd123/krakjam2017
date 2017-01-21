using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum Direction
    {
        Up, Down, Left, Right
    }

    public static Player Instance;

    public SpriteRenderer[] torsos;
    public SpriteRenderer[] leftArms;
    public SpriteRenderer[] rightArms;
    public SpriteRenderer[] heads;
    public SpriteRenderer[] leftLegs;
    public SpriteRenderer[] rightLegs;

    public GameObject Down;
    public GameObject Up;

    public float Speed;

    Rigidbody2D rigidBody2D;
    private Vector3 headStartPos;
    private Vector3 torsoStartPos;
    private Vector3 leftLegStartPos;
    private Vector3 rightLegStartPos;

    public Vector2 Position
	{
		get
		{
			return transform.position;
		} 
	}

	public void OnWaveCollision()
	{
		Debug.Log("OnWaveCollision");
	}

    void Awake()
    {
        Instance = this;
        rigidBody2D = GetComponent<Rigidbody2D>();

        headStartPos = heads[0].transform.localPosition;
        torsoStartPos = torsos[0].transform.localPosition;
        leftLegStartPos = leftLegs[0].transform.localPosition;
        rightLegStartPos = rightLegs[0].transform.localPosition;
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

        EnableAnimation(direction);
        Animate(direction != Vector2.zero);

        if(FollowingCamera.Instance != null)
        {
            transform.position = FollowingCamera.Instance.Clamp(transform.position, 1.0f, 1.0f);
        }
    }

    void Animate(bool moving)
    {
        foreach(var head in heads)
        {
            head.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 6) * 10);
            head.transform.localPosition = headStartPos + new Vector3(0, Mathf.Sin(Time.time * 12) * 0.03f, 0);
        }

        float armAngle = Mathf.Sin(Time.time * 12) * 10;
        foreach (var leftArm in leftArms)
            leftArm.transform.localRotation = Quaternion.Euler(0, 0, armAngle);

        foreach(var rightArm in rightArms)
            rightArm.transform.localRotation = Quaternion.Euler(0, 0, -armAngle);

        foreach (var torso in torsos)
            torso.transform.localPosition = torsoStartPos + new Vector3(0, Mathf.Sin(Time.time * 12) * 0.02f, 0);

        if(moving)
        {
            const float legCycleTime = 0.06f;
            foreach(var leftLeg in leftLegs)
                leftLeg.transform.localPosition = leftLegStartPos + new Vector3(0, Mathf.PingPong(Time.time / 2, legCycleTime), 0);
            foreach(var rightLeg in rightLegs)
                rightLeg.transform.localPosition = rightLegStartPos + new Vector3(0, Mathf.PingPong(Time.time / 2 + legCycleTime, legCycleTime), 0);
        } else
        {
            foreach (var leftLeg in leftLegs)
                leftLeg.transform.localPosition = leftLegStartPos;
            foreach (var rightLeg in rightLegs)
                rightLeg.transform.localPosition = rightLegStartPos;
        }
    }

    void EnableAnimation(Vector2 direction)
    {
        Direction animDir = Direction.Down;

        if(direction.x > 0)
        {
            animDir = Direction.Left;
        } else if(direction.x < 0)
        {
            animDir = Direction.Right;
        } else if (direction.y > 0)
        {
            animDir = Direction.Up;
        }

        Up.SetActive(animDir == Direction.Up);
        Down.SetActive(animDir == Direction.Down);
    }

}
