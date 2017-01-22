using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RunningNigga : MonoBehaviour {

    public SpriteRenderer head;
    public SpriteRenderer torso;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;

    private Vector3 headStartPos;
    private Vector3 torsoStartPos;
    private Vector3 leftLegStartPos;
    private Vector3 rightLegStartPos;

    void Start()
    {
        headStartPos = head.transform.localPosition;
        torsoStartPos = torso.transform.localPosition;
        leftLegStartPos = leftLeg.transform.localPosition;
        rightLegStartPos = rightLeg.transform.localPosition;
        transform.DOMoveY(-45, 2.0f).SetDelay(2).OnComplete(() =>
        {
            if(AppFlow.Instance != null)
            {
                AppFlow.Instance.GoToIntro();
            }
        });
    }

    void Update()
    {

        const float scale = 17.5f;

        var headPosDiff = new Vector3(0, Mathf.Sin(Time.time * 12) * 0.03f, 0) * scale;

        head.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 6) * 10);
        head.transform.localPosition = headStartPos + headPosDiff;

        float armAngle = Mathf.Sin(Time.time * 12) * 10;

        leftArm.transform.localRotation = Quaternion.Euler(0, 0, armAngle);

        rightArm.transform.localRotation = Quaternion.Euler(0, 0, -armAngle);


        var torsoPosDiff = new Vector3(0, Mathf.Sin(Time.time * 12) * 0.02f, 0) * scale;
        torso.transform.localPosition = torsoStartPos + torsoPosDiff;

       const float legCycleTime = 0.06f;
       leftLeg.transform.localPosition = leftLegStartPos + new Vector3(0, Mathf.PingPong(Time.time / 2, legCycleTime), 0) * scale;
        rightLeg.transform.localPosition = rightLegStartPos + new Vector3(0, Mathf.PingPong(Time.time / 2 + legCycleTime, legCycleTime), 0) * scale;
    }

}
