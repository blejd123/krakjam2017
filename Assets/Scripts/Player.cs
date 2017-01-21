using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public SpriteRenderer torso;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public SpriteRenderer head;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;

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

        headStartPos = head.transform.localPosition;
        torsoStartPos = torso.transform.localPosition;
        leftLegStartPos = leftLeg.transform.localPosition;
        rightLegStartPos = rightLeg.transform.localPosition;
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

        Animate(direction != Vector2.zero);

	    transform.position = FollowingCamera.Instance.Clamp(transform.position, 1.0f, 1.0f);
    }

    void Animate(bool moving)
    {
        head.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 6) * 10);
        head.transform.localPosition = headStartPos + new Vector3(0, Mathf.Sin(Time.time * 12) * 0.03f, 0);

        float armAngle = Mathf.Sin(Time.time * 12) * 10;
        leftArm.transform.localRotation = Quaternion.Euler(0, 0, armAngle);
        rightArm.transform.localRotation = Quaternion.Euler(0, 0, -armAngle);

        torso.transform.localPosition = torsoStartPos + new Vector3(0, Mathf.Sin(Time.time * 12) * 0.02f, 0);

        if(moving)
        {
            const float legCycleTime = 0.06f;
            leftLeg.transform.localPosition = leftLegStartPos + new Vector3(0, Mathf.PingPong(Time.time / 2, legCycleTime), 0);
            rightLeg.transform.localPosition = rightLegStartPos + new Vector3(0, Mathf.PingPong(Time.time / 2 + legCycleTime, legCycleTime), 0);
        } else
        {
            leftLeg.transform.localPosition = leftLegStartPos;
            rightLeg.transform.localPosition = rightLegStartPos;
        }

    }

}
