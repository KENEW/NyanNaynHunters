using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManage : MonoBehaviour
{
	[SerializeField] int maxScene = 1;

	public int curCount = 0;

	public void NextButton()
	{
		//curCount = curCount < (maxScene - 1) ? curCount++ : curCount;
		if(curCount < maxScene)
		{
			curCount++;
		}
	}

	public void PrevButton()
	{
		//curCount = curCount > 0 ? curCount-- : curCount;
		if (curCount > 0)
		{
			curCount--;
		}
	}
}
