using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restartbutton : MonoBehaviour
{
	public void Restart()
	{
		AppFlow.Instance.GoToIntro();
	}
}
