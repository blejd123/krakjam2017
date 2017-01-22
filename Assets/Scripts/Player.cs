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

    public SpriteRenderer sideTorso;
    public SpriteRenderer sideFrontArm;
    public SpriteRenderer sideBackArm;
    public SpriteRenderer sideHead;
    public SpriteRenderer sideFrontLeg;
    public SpriteRenderer sideBackLeg;

    public GameObject Down;
    public GameObject Up;
    public GameObject Right;

    public float Speed;

    Rigidbody2D rigidBody2D;
    private Vector3 headStartPos;
    private Vector3 torsoStartPos;
    private Vector3 leftLegStartPos;
    private Vector3 rightLegStartPos;

    private Vector3 sideTorsoStartPos;
    private Vector3 sideHeadStartPos;

    public Vector2 Position
	{
		get
		{
			return transform.position;
		} 
	}

	public void OnWaveCollision()
	{
        if (GameState.Instance.state != GameState.State.Playing)
            return;

        GameState.Instance.state = GameState.State.GameOver;
        Invoke("ShowShamanWin", 3.0f);
    }

    void ShowShamanWin()
    {
        if (AppFlow.Instance != null)
        {
            AppFlow.Instance.GoToGameOverShamanWin();
        }
    }

    void Awake()
    {
        Instance = this;
        rigidBody2D = GetComponent<Rigidbody2D>();

        headStartPos = heads[0].transform.localPosition;
        torsoStartPos = torsos[0].transform.localPosition;
        leftLegStartPos = leftLegs[0].transform.localPosition;
        rightLegStartPos = rightLegs[0].transform.localPosition;

        sideTorsoStartPos = sideTorso.transform.localPosition;
        sideHeadStartPos = sideHead.transform.localPosition;
    }

    void Update()
    {
        if (GameState.Instance.state != GameState.State.Playing)
            return;

        Vector2 direction = Vector2.zero;

        bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

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

        var headPosDiff = new Vector3(0, Mathf.Sin(Time.time * 12) * 0.03f, 0);

        foreach (var head in heads)
        {
            head.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 6) * 10);
            head.transform.localPosition = headStartPos + headPosDiff;
        }

        sideHead.transform.localPosition = sideHeadStartPos + headPosDiff;

        float armAngle = Mathf.Sin(Time.time * 12) * 10;
        foreach (var leftArm in leftArms)
            leftArm.transform.localRotation = Quaternion.Euler(0, 0, armAngle);

        foreach(var rightArm in rightArms)
            rightArm.transform.localRotation = Quaternion.Euler(0, 0, -armAngle);

        float sideArmAngle = armAngle * 4;

        sideFrontArm.transform.localRotation = Quaternion.Euler(0, 0, sideArmAngle);
        sideBackArm.transform.localRotation = Quaternion.Euler(0, 0, -sideArmAngle);

        var torsoPosDiff = new Vector3(0, Mathf.Sin(Time.time * 12) * 0.02f, 0);

        foreach (var torso in torsos)
            torso.transform.localPosition = torsoStartPos + torsoPosDiff;

        sideTorso.transform.localPosition = sideTorsoStartPos + torsoPosDiff;

        float sideLegAngle = Mathf.Sin(Time.time * 12) * 35;
        sideFrontLeg.transform.localRotation = Quaternion.Euler(0, 0, -sideLegAngle);
        sideBackLeg.transform.localRotation = Quaternion.Euler(0, 0, sideLegAngle);

        if (moving)
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

        if(direction.x < 0)
        {
            animDir = Direction.Left;
        } else if(direction.x > 0)
        {
            animDir = Direction.Right;
        } else if (direction.y > 0)
        {
            animDir = Direction.Up;
        }

        Up.SetActive(animDir == Direction.Up);
        Down.SetActive(animDir == Direction.Down);
        Right.SetActive(animDir == Direction.Right || animDir == Direction.Left);

        if(animDir == Direction.Left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }
    }

}
