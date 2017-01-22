using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dancing : MonoBehaviour
{
	public RectTransform Head;
	public RectTransform LeftArm;
	public RectTransform RightArm;
	public RectTransform Body;
	public RectTransform LeftLeg;
	public RectTransform RightLeg;

	void Start ()
	{
		float str = 7.0f;
		int vibro = 1;
		Head.DOShakeRotation(100.0f, str, vibro);
		LeftArm.DOShakeRotation(100.0f, str, vibro);
		RightArm.DOShakeRotation(100.0f, str, vibro);
		Body.DOShakeRotation(100.0f, str, vibro);
		LeftLeg.DOShakeRotation(100.0f, str, vibro);
		RightLeg.DOShakeRotation(100.0f, str, vibro);
	}
}
